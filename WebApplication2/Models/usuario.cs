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
    
    public partial class usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public usuario()
        {
            this.hojaRuta = new HashSet<hojaRuta>();
        }
    
        public int usuarioID { get; set; }
        public string usuarioNombre { get; set; }
        public string usuarioClave { get; set; }
        public bool usuarioActivo { get; set; }
        public string usuarioNombreCol { get; set; }
        public string usuarioApellido { get; set; }
        public string usuarioTelefono { get; set; }
        public string usuarioCargo { get; set; }
        public int rolID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hojaRuta> hojaRuta { get; set; }
        public virtual rol rol { get; set; }
    }
}