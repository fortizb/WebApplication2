using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class GuiasViewModel
    {
        public int numeroGuia { get; set; }
        public int idHojaRuta { get; set; }
        public string rut { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public Nullable<int> telefono { get; set; }
        public string ciudad { get; set; }
        public Nullable<System.DateTime> fechaIngreso { get; set; }
        public string observacion { get; set; }
        public string estado { get; set; }
    }
}