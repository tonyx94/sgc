using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SGC.Business.Dtos;
using SGC.Data;
using SGC.Data.Entities;

namespace SGC.Business.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly SGCDbContext _context;
        private readonly IMapper _mapper;

        public UsuarioService(SGCDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UsuarioDTO>> GetUsuariosAsync()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return _mapper.Map<List<UsuarioDTO>>(usuarios);
        }

        public async Task<UsuarioDTO?> GetByIdAsync(int id)
        {
            var user = await _context.Usuarios.FindAsync(id);
            return _mapper.Map<UsuarioDTO>(user);
        }

        public async Task<bool> CrearUsuarioAsync(UsuarioDTO dto)
        {
            // Validación: evitar usernames duplicados
            bool existe = await _context.Usuarios
                .AnyAsync(x => x.Username == dto.Username);

            if (existe)
                return false;

            var entity = _mapper.Map<Usuario>(dto);
            _context.Usuarios.Add(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ActualizarUsuarioAsync(UsuarioDTO dto)
        {
            var entity = _mapper.Map<Usuario>(dto);
            _context.Usuarios.Update(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> BorrarUsuarioAsync(int id)
        {
            var user = await _context.Usuarios.FindAsync(id);

            if (user == null)
                return false;

            _context.Usuarios.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
