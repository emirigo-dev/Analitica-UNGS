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

namespace AnaliticaWS.Controllers
{
    public class CargarCSVController : ApiController
    {
        [Authorize]
        [HttpPost]
        public IHttpActionResult UploadFile()
        {

            List<NotasDTO> notas = new List<NotasDTO>();
            Response res = new Response();


            try
            {
                var httpRequest = HttpContext.Current.Request;
                HttpFileCollection Files;
                Files = httpRequest.Files;

                var Archivo = Files.Get(Files.AllKeys[0]);


                using (StreamReader csvReader = new StreamReader(Archivo.InputStream))
                {
                    while (!csvReader.EndOfStream)
                    {
                        var line = csvReader.ReadLine();
                        var values = line.Split(';');
                        notas.Add(new NotasDTO()
                        {
                            legajoAlumno = values[0],
                            idCursada = values[1],
                            nota = values[2]
                        });
                    }
                }

            } catch (Exception e) {
                res.Message = "Error falta de CSV cargado";
                return Content(HttpStatusCode.NotFound, res);
            }
           
            Boolean hasError = Userdata.BulkToMySQL(notas);

            if (hasError) {
                res.Message = "Error en la inserción masiva, revise que todos los datos del CSV esten correctos";
                return Content(HttpStatusCode.NotFound, res);// status code 404
            
            }
            res.Message = "Datos insertados correctamente";
            return Ok(res);

        }

        public class Response
        {
            public string Message { get; set; }
        }
    }
}
