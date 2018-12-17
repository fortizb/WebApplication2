using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage = "El Nombre de Usuario es requerido")]
        public string usuarioNombre { get; set; }
        [Required(ErrorMessage = "La Clave es requerido")]
        public string usuarioClave { get; set; }
    }
}