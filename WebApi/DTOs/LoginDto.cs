using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage ="El nombre de Usuario es requerdio")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="La contraseña es requerdia")]
        public string Password { get; set; }

    }
}