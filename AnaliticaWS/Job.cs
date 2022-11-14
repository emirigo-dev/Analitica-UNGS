using AnaliticaWS.Data;
using AnaliticaWS.Models;
using MySql.Data.MySqlClient.Memcached;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using static System.Net.WebRequestMethods;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Common.Logging;


namespace AnaliticaWS
{
    public class Job : IJob
    {
        public async void Execute(IJobExecutionContext context)
        {
            Log alog = new Log();
            alog.fecha = DateTime.Now;
            alog.proceso = "Mediciones hacia blockchain";
            string url = "https://api-blockchain-production.up.railway.app/api/analitica?apiKey=b1a6a576d91d5796e";
            var client = new HttpClient();
            List<promediosSensores> prom = Userdata.getMedicionesPorDia();
            string json = "{ \"promediosSensores\":" + JsonConvert.SerializeObject(prom) + "}";
            HttpContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(url, content);
            if (httpResponse.IsSuccessStatusCode) {
                alog.estado = "Se insertaron las mediciones en blockchain";
            }
            else
            {
                alog.estado = "API caida";
            }
            Userdata.insertLog(alog);

        }
    }

   


}