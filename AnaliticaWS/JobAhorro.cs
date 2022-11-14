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
using AnaliticaWS.Data;
using AnaliticaWS.Models;
using MySqlX.XDevAPI.Common;

namespace AnaliticaWS
{
    public class JobAhorro : IJob
    {
        public async void Execute(IJobExecutionContext context)
        {
            Log alog = new Log();
            alog.fecha = DateTime.Now;
            alog.proceso = "Consumos diarios";
            string url = "https://ahorro-energetico-consumo.herokuapp.com/api/consumo/dispositivos";
            var client = new HttpClient();
            var httpResponse = await client.GetAsync(url);
            if (httpResponse.IsSuccessStatusCode)
            {
                var result = await httpResponse.Content.ReadAsStringAsync();
                if (result.Length > 18) {
                    List<Consumo> results = JsonConvert.DeserializeObject<List<Consumo>>(result);
                    Userdata.insertarConsumos(results);
                    alog.estado = "Insertado";

                }
                else
                {
                    alog.estado = "No hay consumos este dia";

                }

            }
            else
            {
                alog.estado = "Error en conexión con la api";

            }

            Userdata.insertLog(alog);
        }
    }

   
}