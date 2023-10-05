
using System.Reflection;
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

    }
}