using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WebApi.DTOs;
using WebApi.Helpers;

namespace WebApi.Services
{
    public class UserService:IUserService
    {
         private readonly JWT _jwt;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<Usuario> _passwordHasher;
       

        public UserService(IUnitOfWork unitOfWork, IOptions<JWT> jwt,
            IPasswordHasher<Usuario> passwordHasher)
        {
            _jwt = jwt.Value;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public Task<DatosUsuarioDTO> GetTokenAsync(LoginDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<string> RegisterAsync(RegisterDTO model)
        {
            throw new NotImplementedException();
        }
    }
}