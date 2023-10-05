using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.models
{
    public class UsuarioRol
    {
         public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuario Usuario{ get; set; } =null!;
        public int RolId { get; set; }
        [ForeignKey("RolId")]
        public Rol Rol { get; set; } = null!;
    }
}