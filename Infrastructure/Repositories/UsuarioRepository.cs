using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UsuarioRepository : GenericRepository<Usuario> ,IUsuario
    {
        public UsuarioRepository(AplicationDbContext context) : base(context)
        {
        }

        public async Task<Usuario> FindByUserNameAndPassword(string username, string password)
        {
             var user = await _context.Usuarios
            .Where(u =>u.NombreUsuario.ToLower() == username.ToLower() && u.Password == password)
            .Include(u =>u.UsuariosRoles)
            .FirstOrDefaultAsync();
            return user;
        }

        public async Task<Usuario> FindUserByUserName(string userName)
        {
            var user = await _context.Usuarios
           .Where( u => u.NombreUsuario.ToLower() == userName.ToLower() )
           .Include(u =>u.UsuariosRoles)
                .ThenInclude(ur =>ur.Rol)
           .FirstOrDefaultAsync()
           ;
           return user;
        }

        public async Task<Usuario> GetByRefreshToken(string refreshToken)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x =>x.RefreshToken == refreshToken);
        }
    }
}