using Microsoft.AspNetCore.Http;

namespace SGC.Models
{
    public class SolicitudViewModel
    {
        public int ClienteId { get; set; }
        public decimal Monto { get; set; }
        public string Comentarios { get; set; }
        public IFormFile? Archivo { get; set; }
    }
}
