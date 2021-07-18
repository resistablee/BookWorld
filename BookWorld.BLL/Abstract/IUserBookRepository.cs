using BookWorld.DAL.Abstract;
using BookWorld.Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookWorld.BLL.Abstract
{
    public interface IUserBookRepository : IGeneralRepository<Entity.Entities.UserBooks>
    {
        public Task<List<Entity.Entities.Books>> UserBookList(int id);
        public Task<byte> AddUserBook(UserBooks userbook);
    }
}