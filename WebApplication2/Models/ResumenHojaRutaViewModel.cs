using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ResumenHojaRutaViewModel
    {
        public IEnumerable<guias> guiasID { get; set; }
        public IEnumerable<colaboradorHojaRuta> colaboradorHojaRutaID { get; set; }
        //public IEnumerable<colaborador> colaboradorID { get; set; }
    }
}