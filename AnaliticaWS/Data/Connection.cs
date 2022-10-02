using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnaliticaWS.Data
{
    public class Connection
    {
        static string server = "pp1.ath.cx";
        static string bd = "analitica";
        static string user = "Devs";
        static string password = "Kadmio27977";
        static string port = "3306";

        static string connectionFull = "server=" + server + ";"+ "port=" + port + ";" + "user id=" + user + ";" + "password=" + password + ";" + "database=" + bd + ";";
        public static string getConnection()
        {
            return connectionFull;
        }
    }
}