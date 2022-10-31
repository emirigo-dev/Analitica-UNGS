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

    public class DeviceController : ApiController
    {
        [Authorize]

        // POST api/<controller>
        public IHttpActionResult Post([FromBody] JSONDevices device)
        {
            SensorABM sensorResponse = new SensorABM();
            sensorResponse = Userdata.insertOrUpdateDevice(device.idDispositivo, device.tipoDispositivo, device.nombreDispositivo, device.idArea);
            if (sensorResponse.hasError)
            {
                return Content(HttpStatusCode.NotFound, sensorResponse);
            }
            return Ok(sensorResponse);

        }
        [Authorize]

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            Userdata.Errors response = Userdata.deleteDevice(id);

            if (response.hasError)
            {
                return Content(HttpStatusCode.NotFound, response);
            }
            return Ok(response);

        }

    }

    public class JSONDevices

    {

        public string idDispositivo { get; set; }
        public string tipoDispositivo { get; set; }

        public string nombreDispositivo { get; set; }
        public string idArea { get; set; }

    }
}
