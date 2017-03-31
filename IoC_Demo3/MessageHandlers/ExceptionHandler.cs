using Interfaces;
using IoC_Demo3.Models;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace IoC_Demo3.MessageHandlers
{
    public class ExceptionHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (response != null && response.Content != null && response.Content.GetType() == typeof(ObjectContent<HttpError>))
            {
                var result = await response.Content.ReadAsAsync<HttpError>();
                var logServices = ServiceLocator.Current.GetAllInstances<ILogService>()?.ToList();
                if (logServices != null)
                    logServices.Add(ServiceLocator.Current.GetInstance<ILogService>());
                foreach(var msg in result.Where(x=>x.Key == "ExceptionMessage"))
                {
                    foreach (var logService in logServices)
                    {
                        
                        logService.Log(request.RequestUri.AbsolutePath, JsonConvert.SerializeObject(msg.Value));
                    }
                }
                
                response = request.CreateResponse(System.Net.HttpStatusCode.OK, new Response(new ValidationError("Exception")));
            }


            return response;
        }
    }
}