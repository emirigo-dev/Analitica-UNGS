using AnaliticaWS.Data;
using AnaliticaWS.Models;
using MySql.Data.MySqlClient.Memcached;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using static System.Net.WebRequestMethods;

namespace AnaliticaWS
{
    public class Job : IJob
    {
        public void Execute(IJobExecutionContext context)
        {

        }
    }
}