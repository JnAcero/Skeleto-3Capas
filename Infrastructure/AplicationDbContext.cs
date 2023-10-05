
using System.Reflection;
using Core.models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AplicationDbContext:DbContext
    {
        public AplicationDbContext(DbContextOptions options) :base(options){

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<UsuarioRol> UsuariosRoles => Set<UsuarioRol>();
        public DbSet<Rol> Roles => Set<Rol>();

    }
}