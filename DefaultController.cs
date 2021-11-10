using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace linebotluis.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpPost]
        public IHttpActionResult POST()
        {
            try
            {
                return Ok();
            }
            catch
            {
                return Ok();
            }
        } 
    }
}
