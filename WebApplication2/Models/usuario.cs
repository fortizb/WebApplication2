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

    public partial class usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public usuario()
        {
            this.hojaRuta = new HashSet<hojaRuta>();
        }
        public int usuarioID { get; set; }
        [Required(ErrorMessage = "El Nombre de Usuario es requerido")]
        public string usuarioNombre { get; set; }
        [Required(ErrorMessage = "La Clave es requerido")]
        public string usuarioClave { get; set; }
        public bool usuarioActivo { get; set; }
        [Required(ErrorMessage = "El Nombre es requerido")]
        public string usuarioNombreCol { get; set; }
        [Required(ErrorMessage = "El Apellido es requerido")]
        public string usuarioApellido { get; set; }
        [Required(ErrorMessage = "El Telefono es requerido")]
        public string usuarioTelefono { get; set; }
        [Required(ErrorMessage = "El Cargo es requerido")]
        public string usuarioCargo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hojaRuta> hojaRuta { get; set; }
    }
}
