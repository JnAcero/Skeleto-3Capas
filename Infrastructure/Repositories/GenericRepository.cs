using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> :IGenericRepository<T> where T: class
    {
          protected AplicationDbContext _context;
        public GenericRepository(AplicationDbContext context)
        {
          _context = context;
        }

        public void Add(T entity)
        {
            _context.Add(entity); 
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.AddRange(entities);
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
        {
             return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
           return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
           var totalRegistros = await _context.Set<T>().CountAsync();
           var registros = await _context.Set<T>()
           .Skip((pageIndex -1)*pageSize)
           .Take(pageSize)
           .ToListAsync();
           return (totalRegistros, registros);
        }

        public  virtual Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(T entity)
        {
            _context.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
           _context.RemoveRange(entities);
        }

        public void Update(T entity)
        {
           _context.Update(entity);
        }
        
    }
}