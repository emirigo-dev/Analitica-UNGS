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
    public class PromedioInstitucionController : ApiController
    {
        // GET api/<controller>
        public List<PromedioInstitucion> Get()
        {
            return Userdata.getAllPromedioInstitucion();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public PromedioWithParameters Post([FromBody]Promedio prom)
        {
            return Userdata.getPromedioWithInsitucionMateriaNivel(prom.idInstitucion, prom.idMateria, prom.idNivel);
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
}