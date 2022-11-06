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


namespace AnaliticaWS
{
    public class Job : IJob
    {
        public async void Execute(IJobExecutionContext context)
        {
            string url = "https://para-boletin-production.up.railway.app/api/analitica?apiKey=b1a6a576d91d5796e";
            var client = new HttpClient();
            List<promediosSensores> prom = Userdata.getMedicionesPorDia();
            string json = "{ promediosSensores:" + JsonConvert.SerializeObject(prom) + "}";
            HttpContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(url, content);
            System.Diagnostics.Debug.WriteLine("cONTENTa" + content);
            System.Diagnostics.Debug.WriteLine(json);
            System.Diagnostics.Debug.WriteLine("LLegue hasta aca");
            System.Diagnostics.Debug.WriteLine(httpResponse);

            if (httpResponse.IsSuccessStatusCode) {
                System.Diagnostics.Debug.WriteLine("OK");
            }

        }
    }
}