using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DTOs;

namespace WebApi.Services
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterDTO model);
        Task<DatosUsuarioDTO> GetTokenAsync(LoginDTO model);

    }
}