using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private UsuarioRepository _usuarios;
        private RolRepository _roles;
        private readonly AplicationDbContext _context;
        public UnitOfWork( AplicationDbContext context)
        {
            _context = context;
        }

        public IUsuario Usuarios
        {
            get
            {
                _usuarios ??= new UsuarioRepository(_context);
                return _usuarios;
            }

        }

        public IRol Roles
        {
            get
            {
                _roles ??= new RolRepository(_context);
                return _roles;
            }
        }

        public IUsuarioRol UsuariosRoles => throw new NotImplementedException();

        public void Dispose()
        {
            _context.Dispose();
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}