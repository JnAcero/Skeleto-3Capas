using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.models;

namespace Infrastructure.Repositories
{
    public class RolRepository : GenericRepository<Rol>, IRol
    {
        public RolRepository(AplicationDbContext context) : base(context)
        {
        }
    }
}