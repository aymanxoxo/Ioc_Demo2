using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.IO;
using System.Web;
using System.Security.Cryptography;
using System.Xml;

namespace IoC_Demo3.Controllers
{
    public class UtilitiesController : ApiController
    {
        [HttpGet]
        [Route("api/Utilities/GetCulture")]
        public string GetCulture()
        {
            return Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        }
        
    }
}

