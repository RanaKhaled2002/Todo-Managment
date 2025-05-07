using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Todo_Managment_BLL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetById(Guid id);
        Task AddAsync(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
