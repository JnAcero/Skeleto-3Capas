using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.models
{
    public class Usuario:BaseEntity
    {
         public string NombreUsuario { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; }= null!;
        public DateTime FechaCreacion { get; set; }
         public List<UsuarioRol> UsuariosRoles { get; set; } = new List<UsuarioRol>();
    }
}