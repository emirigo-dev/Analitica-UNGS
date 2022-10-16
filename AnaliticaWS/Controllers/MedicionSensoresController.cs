using AnaliticaWS.Data;
using AnaliticaWS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AnaliticaWS.Controllers
{
    public class MedicionSensoresController : ApiController
    {

        [Authorize]

        // POST api/<controller>
        public IHttpActionResult Post([FromBody] JSONMediciones jsonM)
        {
            MedicionMessage medicion = Userdata.insertarMedicionesDeSensores(jsonM.idSensor, jsonM.valorMedicion, jsonM.horario);
            if (medicion.HasError) {
                return Content(HttpStatusCode.NotFound, medicion);// status code 404
            }
            return Ok(medicion);
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

    public class JSONMediciones
    {
        public string idSensor { get; set; }
        public string valorMedicion { get; set; }
        public DateTime horario { get; set; }

    }
}
