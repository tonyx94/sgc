using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SGC.Business.Dtos
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(30, ErrorMessage = "El nombre de usuario no puede exceder los 30 caracteres.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(50, ErrorMessage = "La contraseña es demasiado larga.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar un rol.")]
        public string Rol { get; set; } = string.Empty;
    }
}

