using AnaliticaWS.Data;
using AnaliticaWS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AnaliticaWS.Controllers
{
    public class PromedioMedicionesController : ApiController
    {
        [Authorize]
        [HttpPost]

        // POST api/<controller>
        public IHttpActionResult Post([FromBody] JSON res)
        {
            MedicionPortal medicion = Userdata.getConsumoPortal(res.idInstitucion, res.tipoMedicion, res.tiempo);
            if (medicion.hasError)
            {
                return Content(HttpStatusCode.NotFound, medicion);
            }
            return Ok(medicion);
        }
    }

    public class JSON{
        public string idInstitucion { get; set; }
        public string tipoMedicion { get; set; }
        public string tiempo { get; set; }
    }
}
