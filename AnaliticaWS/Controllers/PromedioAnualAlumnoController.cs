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
    public class PromedioAnualAlumnoController : ApiController
    {
        [Authorize]
  
        // POST api/<controller>
        public List<PromedioAnualAlumno> Post([FromBody] JSONBoletin jsonB)
        {
            return Userdata.getAllPromedioAnual(jsonB.Anio);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }

    public class JSONBoletin
    {
        public string Anio { get; set; }
    }
}
