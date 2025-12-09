using System;

namespace SGC.Data.Entities
{
    public class SolicitudBitacora
    {
        public int Id { get; set; }
        public int SolicitudId { get; set; }
        public Solicitud Solicitud { get; set; } = null!;
        public string Accion { get; set; } = null!;
        public string Comentario { get; set; } = null!;
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
