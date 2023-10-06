using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Interfaces;
using Core.models;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    public class UsuarioController : BaseApiController
    {
        private readonly IUserService _userService;

        public UsuarioController(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService) : base(unitOfWork, mapper)
        {
            _userService = userService;
        }
        [HttpPost("Registro")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> SignInUser(RegisterDTO registerDto)
        {
            var respuesta = await _userService.RegisterAsync(registerDto);
            if (respuesta.success == false)
            {
                return BadRequest(respuesta);
            }
            return Ok(respuesta);
        }
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> LoginUser(LoginDTO loginDto)
        {
            var respuesta = await _userService.LoginAsync(loginDto);

            if (respuesta.success == false)
            {
                return BadRequest(respuesta);
            }
            else
            {
                var usuario = await _unitOfWork.Usuarios.FindUserByUserName(loginDto.UserName);
                var refreshToken = _userService.GenerateRefreshToken();

                SetRefreshToken(refreshToken, usuario);
                
                _unitOfWork.Usuarios.Update(usuario);
                await _unitOfWork.SaveAsync();

                return Ok(respuesta);
            }
        }
        private async void SetRefreshToken(RefreshToken newRefeshToken, Usuario usuario)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefeshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefeshToken.Token, cookieOptions);
            usuario.RefreshToken = newRefeshToken.Token;
            usuario.TokenCreated = newRefeshToken.Created;
            usuario.TokenExpires = newRefeshToken.Expires;

        }
        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _userService.RefreshToken(refreshToken);
            if (!response.success)
            {
                return Unauthorized(response.message);
            }
            var usuario = await _unitOfWork.Usuarios.GetByRefreshToken(refreshToken);
            var token = response.result;

            RefreshToken newRefreshToken = _userService.GenerateRefreshToken();
            SetRefreshToken(newRefreshToken, usuario);

            return Ok(token);
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetCities()
        {
            List<string> Cities = new() { "Italy", "France", "Spain" };
            return Ok(Cities);
        }
    }
}