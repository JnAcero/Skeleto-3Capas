using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.models;
using Core.Models;
using WebApi.DTOs;

namespace WebApi.Services
{
    public interface IUserService
    {
        Task<RespuestaDTO> RegisterAsync(RegisterDTO registerDTO);
        Task<RespuestaDTO> LoginAsync(LoginDTO loginDTO);
        void UpdateAndSaveUserAsync(Usuario usuario);
        string HashPaswordOfUser(Usuario usuario);
        RefreshToken GenerateRefreshToken();
        Task<RespuestaDTO> RefreshToken(string refreshToken);

    }
}