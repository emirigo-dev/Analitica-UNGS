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
    [Authorize]

    public class SensorsController : ApiController
    {
        [Authorize]

        // POST api/<controller>
        public IHttpActionResult Post([FromBody] JSONSensors sens)
        {
            SensorABM sensorResponse = new SensorABM();
            sensorResponse = Userdata.insertOrUpdateSensors(sens.idSensor, sens.tipoMedicion, sens.idArea);
            if (sensorResponse.hasError)
            {
                return Content(HttpStatusCode.NotFound, sensorResponse);
            }
            return Ok(sensorResponse);

        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            Userdata.Errors response = Userdata.deleteSensor(id);

            if (response.hasError) {
                return Content(HttpStatusCode.NotFound, response);
            }
            return Ok(response);
 
        }

    }

    public class JSONSensors

    {

        public string idSensor { get; set; }
        public string tipoMedicion { get; set; }
        public string idArea { get; set; }

    }
}
