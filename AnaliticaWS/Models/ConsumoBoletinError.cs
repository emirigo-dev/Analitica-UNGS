using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnaliticaWS.Models
{
    public class ConsumoBoletinError
    
    {
        public int statusCode;
        public Boolean hasError;
        public string message;
        public List<ConsumoBoletin> consumos;
    }
}