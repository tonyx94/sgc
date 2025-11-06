using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC.Business.Dtos
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }
}

