using System;
using System.Collections.Generic;

namespace SGC.Data.Entities
{
    public class Solicitud
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;
        public decimal Monto { get; set; }
        public string Comentarios { get; set; } = null!;
        public string Documentos { get; set; } = null!; 
        public string Estado { get; set; } = "Registrado";
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
