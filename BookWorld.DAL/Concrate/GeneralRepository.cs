using BookWorld.DAL.Abstract;
using BookWorld.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookWorld.DAL.Concrate
{
    public class GeneralRepository<T> : IGeneralRepository<T> where T : BaseEntity, new()
    {
        protected readonly Context _contex;
        public GeneralRepository(Context contex)
        {
            _contex = contex;
        }

        public async Task Add(T entity)
        {
            _contex.Set<T>().Add(entity);
            await _contex.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _contex.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _contex.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
