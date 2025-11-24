using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC.Business.Dtos
{
    public class ClienteDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La identificación es obligatoria")]
        [StringLength(20, ErrorMessage = "La identificación no puede exceder 20 caracteres")]
        public string Identificacion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(100, ErrorMessage = "El apellido no puede exceder 100 caracteres")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [Phone(ErrorMessage = "Formato de teléfono inválido")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(250, ErrorMessage = "La dirección no puede exceder 250 caracteres")]
        public string Direccion { get; set; } = string.Empty;

        public DateTime FechaRegistro { get; set; }

        public bool Activo { get; set; } = true;

        public string NombreCompleto => $"{Nombre} {Apellido}";
    }
}