using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.models;
using Core.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi.DTOs;
using WebApi.Helpers;

namespace WebApi.Services
{
    public class UserService : IUserService
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

        public async Task<RespuestaDTO> LoginAsync(LoginDTO loginDTO)
        {
            var usuario = await _unitOfWork.Usuarios.FindUserByUserName(loginDTO.UserName);
            bool UsuarioIsVerified = VerifyPassword(usuario, loginDTO.Password);
            if (UsuarioIsVerified && (usuario is not null))
            {
                JwtSecurityToken jwtSecurityToken = CreateJwtToken(usuario);
                var Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                //var refreshToken = GenerateRefreshToken();
                //SetRefreshToken(refreshToken);
                return new RespuestaDTO
                {
                    success = true,
                    message = "Ok,Verificado",
                    result = new
                    {
                        Id = usuario.Id,
                        nombreUsuario = usuario.NombreUsuario,
                        email = usuario.Email,
                        rol = string.Join(',', GetRolesUsuario(usuario)),
                        token = Token
                    }

                };
            }
            else
            {
                return new RespuestaDTO
                {
                    success = false,
                    message = "Credenciales incorrectas para el usuario",
                    result = ""
                };
            }
        }
        public RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(654)),
                Expires = DateTime.Now.AddDays(7)

            };
            return refreshToken;
        }

        private bool VerifyPassword(Usuario usuario, string passwordToCompare)
        {
            var passwordVerificationResult = PasswordVerificationResult.Failed;
            try
            {
                passwordVerificationResult = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, passwordToCompare);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return passwordVerificationResult == PasswordVerificationResult.Success;
        }

        public async Task<RespuestaDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            var usuarioAVerificar = await _unitOfWork.Usuarios.FindUserByUserName(registerDTO.UserName);
            if (usuarioAVerificar is null)
            {
                var usuario = new Usuario()
                {
                    NombreUsuario = registerDTO.UserName,
                    Email = registerDTO.Email,
                    FechaCreacion = DateTime.Now,
                };

                usuario.Password = _passwordHasher.HashPassword(usuario, registerDTO.Password);

                var rolUsuario = await _unitOfWork.Roles.GetByIdAsync(registerDTO.RolId);

                if (rolUsuario is null)
                {
                    return new RespuestaDTO
                    {
                        success = false,
                        message = $"El rol con id:{registerDTO.RolId} no existe ",
                        result = ""
                    };
                }
                var datos_Usuario_Rol = new List<UsuarioRol>(){
                        new()
                        {
                            Usuario = usuario,
                            Rol = rolUsuario
                        }
                    };
                // usuario.UsuariosRoles.AddRange(datos_Usuario_Rol);
                usuario.UsuariosRoles.AddRange(datos_Usuario_Rol);
                _unitOfWork.Usuarios.Add(usuario);
                await _unitOfWork.SaveAsync();
                return new RespuestaDTO
                {
                    success = true,
                    message = "Usuario creado exitosamente",
                    result = new
                    {
                        Id = usuario.Id,
                        nameUser = usuario.NombreUsuario,
                        email = usuario.Email
                    }
                };
            }
            else
            {
                return new RespuestaDTO
                {
                    success = false,
                    message = "Usuario ya existe",
                    result = ""
                };
            }
        }
        private JwtSecurityToken CreateJwtToken(Usuario usuario)
        {
            var claims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.Sub , usuario.NombreUsuario),
                    new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
                    new Claim("id", usuario.Id.ToString()),
                    new Claim("username", usuario.NombreUsuario),
                    new Claim("email", usuario.Email)
                };
            var usuariosRoles = usuario.UsuariosRoles;
            var roleClaims = new List<Claim>();
            foreach (var usuarioRol in usuariosRoles)
            {
                roleClaims.Add(new Claim("role", usuarioRol.Rol.Nombre));
            }
            claims.AddRange(roleClaims);

            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signInCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var JwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signInCredentials
            );
            return JwtSecurityToken;
        }

        public async Task<RespuestaDTO> RefreshToken(string refreshToken)
        {
            var usuario = await _unitOfWork.Usuarios.GetByRefreshToken(refreshToken);
            if (usuario is null)
            {
                return new RespuestaDTO
                {
                    success = false,
                    message = "Token is not assigned to any user",
                    result = ""
                };
            }
            else if (usuario.TokenExpires < DateTime.Now)
            {
                return new RespuestaDTO
                {
                    success = false,
                    message = "Token expired",
                    result = ""
                };
            }
            JwtSecurityToken jwtSecurityToken = CreateJwtToken(usuario);
            string Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return new RespuestaDTO
            {
                success = true,
                message = "Ok",
                result = Token

            };
        }
        public async void UpdateAndSaveUserAsync(Usuario usuario)
        {
            _unitOfWork.Usuarios.Update(usuario);
            await _unitOfWork.SaveAsync();

        }
        public string HashPaswordOfUser(Usuario usuario)
        {
            return usuario.Password = _passwordHasher.HashPassword(usuario, usuario.Password);
        }
        private List<string> GetRolesUsuario(Usuario usuario)
        {
            var rolesUsuario = new List<string>();
            foreach (var user in usuario.UsuariosRoles)
            {
                rolesUsuario.Add(user.Rol.Nombre);
            }
            return rolesUsuario;
        }



    }
}