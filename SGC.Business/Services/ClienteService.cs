using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SGC.Business.Dtos;
using SGC.Data;
using SGC.Data.Entities;

namespace SGC.Business.Services
{
    public class ClienteService
    {
        private readonly SGCDbContext _context;
        private readonly IMapper _mapper;

        public ClienteService(SGCDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Obtener todos los clientes
        public async Task<List<ClienteDTO>> ObtenerTodosAsync()
        {
            var clientes = await _context.Clientes
                .OrderByDescending(c => c.FechaRegistro)
                .ToListAsync();

            return _mapper.Map<List<ClienteDTO>>(clientes);
        }

        // Obtener cliente por ID
        public async Task<ClienteDTO?> ObtenerPorIdAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            return cliente == null ? null : _mapper.Map<ClienteDTO>(cliente);
        }

        // Validar si existe identificación duplicada
        public async Task<bool> ExisteIdentificacionAsync(string identificacion, int? idExcluir = null)
        {
            if (idExcluir.HasValue)
            {
                return await _context.Clientes
                    .AnyAsync(c => c.Identificacion == identificacion && c.Id != idExcluir.Value);
            }
            return await _context.Clientes
                .AnyAsync(c => c.Identificacion == identificacion);
        }

        // Crear cliente
        public async Task<(bool Success, string Message)> CrearAsync(ClienteDTO clienteDto)
        {
            // Validar identificación duplicada
            if (await ExisteIdentificacionAsync(clienteDto.Identificacion))
            {
                return (false, $"Ya existe un cliente con la identificación {clienteDto.Identificacion}");
            }

            var cliente = _mapper.Map<Cliente>(clienteDto);
            cliente.FechaRegistro = DateTime.Now;

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return (true, "Cliente creado exitosamente");
        }

        // Actualizar cliente
        public async Task<(bool Success, string Message)> ActualizarAsync(ClienteDTO clienteDto)
        {
            var clienteExistente = await _context.Clientes.FindAsync(clienteDto.Id);
            if (clienteExistente == null)
            {
                return (false, "Cliente no encontrado");
            }

            // Validar identificación duplicada (excluyendo el cliente actual)
            if (await ExisteIdentificacionAsync(clienteDto.Identificacion, clienteDto.Id))
            {
                return (false, $"Ya existe otro cliente con la identificación {clienteDto.Identificacion}");
            }

            // Actualizar propiedades
            clienteExistente.Identificacion = clienteDto.Identificacion;
            clienteExistente.Nombre = clienteDto.Nombre;
            clienteExistente.Apellido = clienteDto.Apellido;
            clienteExistente.Email = clienteDto.Email;
            clienteExistente.Telefono = clienteDto.Telefono;
            clienteExistente.Direccion = clienteDto.Direccion;
            clienteExistente.Activo = clienteDto.Activo;

            _context.Clientes.Update(clienteExistente);
            await _context.SaveChangesAsync();

            return (true, "Cliente actualizado exitosamente");
        }

        // Eliminar cliente
        public async Task<(bool Success, string Message)> EliminarAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return (false, "Cliente no encontrado");
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return (true, "Cliente eliminado exitosamente");
        }

        // Obtener clientes activos
        public async Task<List<ClienteDTO>> ObtenerActivosAsync()
        {
            var clientes = await _context.Clientes
                .Where(c => c.Activo)
                .OrderBy(c => c.Nombre)
                .ToListAsync();

            return _mapper.Map<List<ClienteDTO>>(clientes);
        }
    }
}