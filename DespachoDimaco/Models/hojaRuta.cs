//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class hojaRuta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hojaRuta()
        {
            this.colaboradorHojaRuta = new HashSet<colaboradorHojaRuta>();
            this.costosHojaRuta = new HashSet<costosHojaRuta>();
            this.guias = new HashSet<guias>();
            this.hojaRutaDetalle = new HashSet<hojaRutaDetalle>();
        }
        public int idHojaRuta { get; set; }
        [Required(ErrorMessage = "La patente es requerida")]
        public string patente { get; set; }
      
        public System.DateTime fechaCreacion { get; set; }

        public Nullable<System.DateTime> fechaModificacion { get; set; }
  
        public bool estado { get; set; }
   
        public Nullable<int> usuarioID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<colaboradorHojaRuta> colaboradorHojaRuta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<costosHojaRuta> costosHojaRuta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<guias> guias { get; set; }
        public virtual vehiculo vehiculo { get; set; }
        public virtual vehiculo vehiculo1 { get; set; }
        public virtual usuario usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hojaRutaDetalle> hojaRutaDetalle { get; set; }
    }
}
