using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SGC.Business.Dtos;

namespace SGC.Business.Services
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDTO>> GetUsuariosAsync();
        Task<UsuarioDTO?> GetByIdAsync(int id);
        Task<bool> CrearUsuarioAsync(UsuarioDTO dto);
        Task<bool> ActualizarUsuarioAsync(UsuarioDTO dto);
        Task<bool> BorrarUsuarioAsync(int id);
    }
}
