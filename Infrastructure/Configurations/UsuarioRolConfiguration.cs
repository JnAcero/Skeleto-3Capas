using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class UsuarioRolConfiguration : IEntityTypeConfiguration<UsuarioRol>
    {
        public void Configure(EntityTypeBuilder<UsuarioRol> builder)
        {
            builder.HasKey(x =>new{x.UsuarioId,x.RolId});
        }
    }
}