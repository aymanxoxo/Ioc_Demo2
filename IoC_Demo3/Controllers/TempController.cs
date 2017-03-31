using Interfaces;
using Microsoft.Practices.ServiceLocation;
using System.Web.Http;

namespace Ioc_Demo3.Controllers
{
    [Authorize(Roles = "User")]
    public class TempController : ApiController
    {
        private readonly ITempService _service;
        public TempController(ITempService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("api/Temp/Talk")]
        public string Talk()
        {
            string s = "";
            var services = ServiceLocator.Current.GetAllInstances<ITempService>();
            foreach(var service in services)
            {
                s += service.Say("Ayman") + " from service locator ";
            }
            s += _service.Say("Ayman") + " from constructor";
            return s;
        }
    }
}
