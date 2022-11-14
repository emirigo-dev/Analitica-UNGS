using AnaliticaWS.Data;
using AnaliticaWS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Services.Description;

namespace AnaliticaWS.Controllers
{
    public class PromedioMensualConsumoController : ApiController
    {

        [Authorize]
        [HttpPost]

        // POST api/<controller>
        public IHttpActionResult Post([FromBody] Response res)
        {   
            ConsumoBoletinError consumos = Userdata.getConsumosMensaules(res.Institucion, res.Mes, res.Anio);
            if (consumos.hasError)
            {
                return Content(HttpStatusCode.NotFound, consumos);
            }
            return Ok(consumos);
        }
       
        public class Response
        {
            public string Institucion { get; set; }
            public string Mes { get; set; }
            public string Anio { get; set; }
        }
    }
}
