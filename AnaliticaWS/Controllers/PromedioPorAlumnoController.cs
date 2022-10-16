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
    public class PromedioPorAlumnoController : ApiController
    {
        [Authorize]
        public PromedioPorAlumno Post([FromBody] JSONBoletinByAnio jsonB)
        {
            return Userdata.getAllPromedioAlumnoPorLegajo(jsonB.Legajo);
        }
    }

    public class JSONBoletinByAnio
    {
        public string Legajo { get; set; }

    }

}
