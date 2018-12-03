using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ColaboradorHojaRutaViewModel
    {
        public int idColHojaRuta { get; set; }
        public int idHojaRuta { get; set; }
        public int run { get; set; }
        public string rut { get; set; }
        public string nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string cargo { get; set; }
    }
}