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
            var httpRequest = HttpContext.Current.Request;
            var postedFile = httpRequest.Files["Filessss"];
            String chau = postedFile.ToString();
            //the parsing logic

            int loop1;
            HttpFileCollection Files;

            Files = httpRequest.Files;
            var holitas = Files.Get("Filessss");// Load File collection into HttpFileCollection variable.

            var holite = holitas.ToString();
            String[] arr1 = Files.AllKeys;  // This will get names of all files into a string array.

            using (StreamReader csvReader = new StreamReader(holitas.InputStream))
            {
                while (!csvReader.EndOfStream)
                {
                    var line = csvReader.ReadLine();
                    var values = line.Split(';');
                }
            }

            for (loop1 = 0; loop1 < arr1.Length; loop1++)
            {
                String hola = (arr1[loop1].ToString());
            }
            return Unauthorized();
        }
    }
}
