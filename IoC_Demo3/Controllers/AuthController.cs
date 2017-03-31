using DataEntities;
using Interfaces;
using IoC_Demo3.Models;
using IoC_Demo3.Models.AccountModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace IoC_Demo3.Controllers
{
    public class AuthController : ApiController

    {
        private readonly IRoleService _roleService;
        private readonly ICryptoService _cryptoService;
        private readonly IAuthenticationService _authService;
        public AuthController(ICryptoService cryptoService, IAuthenticationService authService, IRoleService roleService)
        {
            _cryptoService = cryptoService;
            _authService = authService;
            _roleService = roleService;
        }


        // returns the public key to the client for encryption
        [HttpGet]
        [Route("api/Auth/GetKey")]
        public string GetKey()
        {
            var strKey = _cryptoService.GetPublickKey();
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(strKey));
        }

        [HttpPost]
        [Route("api/Auth/Login")]
        public HttpResponseMessage Login(AuthModel model)
        {
            // If model is null
            if (model == null || string.IsNullOrEmpty(model.Credentials))
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new Response(new ValidationError(DataEntities.Resources.ServerLabel.LoginModelNotProvided)));

            // Decrypt client's credentials
            var userPass = _cryptoService.Decrypt(model.Credentials);

            // If decryption failed
            if (string.IsNullOrEmpty(userPass))
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            // Split the concatenated username and password
            var userData = userPass.Split('\\');

            // Call the authentication service to Check if user is authenticated
            var user = _authService.IsAuthorized(userData[0], userData[1]);

            // If user is authenticated
            if(user != null)
            {
                // Save the user's token in the cache so that the user will be authenticated in the Authentication Handler
                AddTokentoCache(model.Credentials);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.OK, new Response(new ValidationError("No Content")));
        }

        [HttpPost]
        [Route("api/Auth/Register")]
        public HttpResponseMessage Register(AuthModel encryptedStrModel)
        {
            if (encryptedStrModel == null || string.IsNullOrEmpty(encryptedStrModel.Credentials))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new ArgumentNullException(nameof(encryptedStrModel)));

            // decrypt the coming data and deserialize it to a model
            var decryptedStrModel = _cryptoService.Decrypt(encryptedStrModel.Credentials);
            var model = JsonConvert.DeserializeObject<RegisterationModel>(decryptedStrModel);
            if (model == null)
                return Request.CreateResponse(HttpStatusCode.OK, new Response(new ValidationError("No Content")));

            // Call model validator using each property's data annotation
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, context, results);

            // if not valid, send validation errors to the client so that he can validate his info
            if (!isValid)
            {
                var response = new Response();
                var validationErrors = new List<ValidationError>();
                foreach (var c in results)
                {
                    validationErrors.Add(new ValidationError(c.ErrorMessage, c.MemberNames?.FirstOrDefault()));
                }
                response.ValidationErrors = validationErrors;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            // set the default role to the user and sends it to the registeration service
            var user = model.GetUser();
            user.Roles = new List<Role> { _roleService.GetByName("User", true) };
            try
            {
                _authService.Register(user);
            }
            catch(DataEntities.Exceptions.ValidationException ex)
            {
                // return validation exceptions to the client 
                return Request.CreateResponse(HttpStatusCode.OK, new Response(new ValidationError(ex.ValidationMessage, ex.Element)));
            }

            // add user's token to the cache so that he can be authenticated before each request
            AddTokentoCache(_cryptoService.Encrypt(user.Email + "\\" + user.Password));

            return Request.CreateResponse(HttpStatusCode.OK, new Response { IsSuccess = true });

        }

        [Authorize]
        [HttpGet]
        [Route("api/Auth/GetCurrentUser")]
        public HttpResponseMessage GetCurrentUser()
        {
            var cachedToken = HttpContext.Current.Cache["authToken"];
            if (cachedToken != null)
            {
                var decryptedData = _cryptoService.Decrypt(cachedToken.ToString());
                return Request.CreateResponse(HttpStatusCode.OK, new Response<string>(decryptedData.Split('\\').FirstOrDefault()));
            }
            return Request.CreateResponse(HttpStatusCode.OK, new Response(new ValidationError("No Content")));
        }

        [Authorize]
        [HttpGet]
        [Route("api/Auth/Signout")]
        public HttpResponseMessage Signout()
        {
            HttpContext.Current.Cache.Remove("authToken");
            return Request.CreateResponse(HttpStatusCode.OK, new Response { IsSuccess = true });
        }

        internal void AddTokentoCache(string token)
        {
            HttpContext.Current.Cache.Insert("authToken", token, null, DateTime.MaxValue, TimeSpan.FromMinutes(20), System.Web.Caching.CacheItemPriority.High, null);
        }


    }
}
