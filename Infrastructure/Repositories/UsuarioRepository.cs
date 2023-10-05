using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.models;

namespace Infrastructure.Repositories
{
    public class UsuarioRepository : GenericRepository<Usuario> ,IUsuario
    {
        public UsuarioRepository(AplicationDbContext context) : base(context)
        {
        }

        public Task<Usuario> FindByUserNameAndPassword(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> FindUserByUserName(string userName)
        {
            throw new NotImplementedException();
        }
    }
}