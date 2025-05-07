using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo_Managment_BLL.Interfaces;
using Todo_Managment_DAL.Data;

namespace Todo_Managment_BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _Context;
        private Hashtable _repositories;

        public UnitOfWork(AppDbContext context)
        {
            _Context = context;
            _repositories = new Hashtable();
        }

        public async Task<int> CompleteAsync()
        {
            return await _Context.SaveChangesAsync();
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repoistory = new GenericRepository<T>(_Context);

                _repositories.Add(type, repoistory);
            }
            return _repositories[type] as IGenericRepository<T>;
        }
    }
}
