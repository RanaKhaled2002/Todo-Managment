using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Todo_Managment_BLL.Interfaces;
using Todo_Managment_DAL.Data;

namespace Todo_Managment_BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _Context;


        public GenericRepository(AppDbContext context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _Context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _Context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _Context.AddAsync(entity);
        }

        public async Task Update(T entity)
        {
            _Context.Update(entity);
        }

        public async Task Delete(T entity)
        {
            _Context.Remove(entity);
        }
    }
}
