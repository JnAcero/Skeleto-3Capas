using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        IUsuario Usuarios { get; }
        IRol Roles { get; }
        IUsuarioRol UsuariosRoles { get; }
        Task<int> SaveAsync();
        
    }
}