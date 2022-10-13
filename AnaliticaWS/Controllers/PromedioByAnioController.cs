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
    public class PromedioByAnioController : ApiController
    {
        [Authorize]
        public PromedioByAnio Post([FromBody] JSONBoletinByAnio jsonB)
        {
            return Userdata.getAllPromedioAlumnoByAnio(jsonB.Legajo, jsonB.Anio);
        }
    }

    public class JSONBoletinByAnio
    {
        public string Anio { get; set; }
        public string Legajo { get; set; }

    }

}
