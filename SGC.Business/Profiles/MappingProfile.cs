using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using SGC.Data.Entities;
using SGC.Business.Dtos;

namespace SGC.Business.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            // cliente
            CreateMap<Cliente, ClienteDTO>();
            CreateMap<ClienteDTO, Cliente>();


            CreateMap<Usuario, UsuarioDTO>();
            CreateMap<UsuarioDTO, Usuario>();
        }
    }
}

