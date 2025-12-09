using Microsoft.EntityFrameworkCore;
using SGC.Data;
using SGC.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGC.Business.Services
{
    public class SolicitudService : ISolicitudService
    {
        private readonly SGCDbContext _context;

        public SolicitudService(SGCDbContext context)
        {
            _context = context;
        }

        public async Task<List<Solicitud>> ObtenerTodas(string rolUsuario, int usuarioId)
        {
            var query = _context.Solicitudes
                .Include(s => s.Cliente)
                .Include(s => s.Usuario)
                .AsQueryable();

            if (rolUsuario == "Analista")
            {
                query = query.Where(s => s.Estado == "Ingresado" || s.Estado == "Devolucion" || s.Estado == "Registrado");
            }
            else if (rolUsuario == "Gestor")
            {
                query = query.Where(s => s.Estado == "EnviadoAprobacion");
            }
            
            return await query.ToListAsync();
        }

        public async Task<Solicitud?> ObtenerPorId(int id)
        {
            return await _context.Solicitudes
                .Include(s => s.Cliente)
                .Include(s => s.Usuario)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<string> Crear(Solicitud solicitud)
        {
            if (solicitud.Monto > 10000000)
            {
                return "Monto máximo excedido (10.000.000)";
            }

            bool clienteExiste = await _context.Clientes.AnyAsync(c => c.Id == solicitud.ClienteId);
            if (!clienteExiste)
            {
                return $"El cliente con ID {solicitud.ClienteId} no existe en el sistema";
            }

            bool existePendiente = await _context.Solicitudes.AnyAsync(s => 
                s.ClienteId == solicitud.ClienteId && 
                (s.Estado == "Ingresado" || s.Estado == "Registrado" || s.Estado == "Devolucion"));

            if (existePendiente)
            {
    
                var existente = await _context.Solicitudes.FirstOrDefaultAsync(s => 
                    s.ClienteId == solicitud.ClienteId && 
                    (s.Estado == "Ingresado" || s.Estado == "Registrado" || s.Estado == "Devolucion"));
                
                string idExistente = existente?.Id.ToString() ?? "N/A";
                var cliente = await _context.Clientes.FindAsync(solicitud.ClienteId);
                string cedula = cliente?.Identificacion ?? "xxxxxxx";

                return $"El usuario con identificación {cedula} ya cuenta con la solicitud de crédito {idExistente}, por favor resolver la gestión antes de ingresar otra nueva";
            }

            solicitud.Estado = "Ingresado"; 

            _context.Solicitudes.Add(solicitud);
            await _context.SaveChangesAsync();


            var bitacora = new SolicitudBitacora
            {
                SolicitudId = solicitud.Id,
                Accion = "Crear",
                Comentario = $"Se crea la gestión para el cliente {solicitud.ClienteId}", // Example comment
                UsuarioId = solicitud.UsuarioId,
                Fecha = DateTime.Now
            };
            _context.SolicitudBitacoras.Add(bitacora);
            await _context.SaveChangesAsync();

            return "OK";
        }

        public async Task<string> ActualizarEstado(int id, string nuevoEstado, string comentario, int usuarioId, string accion)
        {
            var solicitud = await _context.Solicitudes.FindAsync(id);
            if (solicitud == null) return "Solicitud no encontrada";

            solicitud.Estado = nuevoEstado;
            
            var bitacora = new SolicitudBitacora
            {
                SolicitudId = solicitud.Id,
                Accion = accion,
                Comentario = comentario ?? "Sin comentarios",
                UsuarioId = usuarioId,
                Fecha = DateTime.Now
            };

            _context.SolicitudBitacoras.Add(bitacora);
            await _context.SaveChangesAsync();

            return "OK";
        }

        public async Task<List<SolicitudBitacora>> ObtenerBitacora(int solicitudId)
        {
            return await _context.SolicitudBitacoras
                .Where(b => b.SolicitudId == solicitudId)
                .Include(b => b.Usuario)
                .OrderByDescending(b => b.Fecha)
                .ToListAsync();
        }
    }
}
