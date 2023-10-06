using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.models;

namespace Core.Interfaces
{
    public interface IUsuario : IGenericRepository<Usuario>
    {
        Task<Usuario> FindUserByUserName(string userName);
        Task<Usuario> FindByUserNameAndPassword(string username, string password);
        Task<Usuario> GetByRefreshToken(string refreshToken);
    }
}