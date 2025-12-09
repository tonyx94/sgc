using SGC.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGC.Business.Services
{
    public interface ISolicitudService
    {
        Task<List<Solicitud>> ObtenerTodas(string rolUsuario, int usuarioId);
        Task<Solicitud?> ObtenerPorId(int id);
        Task<string> Crear(Solicitud solicitud);
        Task<string> ActualizarEstado(int id, string nuevoEstado, string comentario, int usuarioId, string accion);
        Task<List<SolicitudBitacora>> ObtenerBitacora(int solicitudId);
    }
}
