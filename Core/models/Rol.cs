using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.models
{
    public class Rol:BaseEntity
    {
        public string Nombre { get; set; } = null!;
        public ICollection<UsuarioRol> UsuariosRoles { get; set; } = new List<UsuarioRol>();
        public ICollection<Usuario> Usuarios { get; set; } = new HashSet<Usuario>();
    }
}