using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs
{
    public class DatosUsuarioDTO
    {
        public string Mensaje { get; set; }
        public bool EstaAutenticado { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        //public List<string> Roles { get; set; }
        public string Token { get; set; }

    }
}