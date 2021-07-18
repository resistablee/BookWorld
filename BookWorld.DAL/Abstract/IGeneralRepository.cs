using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWorld.DAL.Abstract
{
    public interface IGeneralRepository<T>
    {
        Task<T> GetAsync(int id);
        Task<List<T>> GetAllAsync();
        Task Add(T entity);
    }
}
