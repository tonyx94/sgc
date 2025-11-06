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
    public class LoginService
    {
        private readonly SGCDbContext _context;
        private readonly IMapper _mapper;

        public LoginService(SGCDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UsuarioDTO?> ValidarUsuarioAsync(string username, string password)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            if (usuario == null)
                return null;

            return _mapper.Map<UsuarioDTO>(usuario);
        }
    }
}

