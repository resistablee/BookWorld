using BookWorld.BLL.Abstract;
using BookWorld.DAL;
using BookWorld.DAL.Concrate;
using BookWorld.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookWorld.BLL.Conctrate
{
    public class UserBookRepository : GeneralRepository<Entity.Entities.UserBooks>, IUserBookRepository
    {
        protected readonly Context _context;
        public UserBookRepository(Context contex) : base(contex)
        {
            _context = contex;
        }
        public async Task<List<Books>> UserBookList(int id)
        {
            List<Books> books = new List<Books>();
            List<UserBooks> userbooks = await _context.UserBooks.Where(x => x.UserID == id).ToListAsync();
            foreach (var item in userbooks)
            {
                books.Add(await _context.Books.FirstOrDefaultAsync(x => x.Id == item.BookID));
            }
            return books;
        }

        public async Task<byte> AddUserBook(UserBooks userbook)
        {
            //kullanıcı id ve kitap id veritabanında var olan bir kayıt var mı kontrol et yoksa ekle
            if (await _context.UserBooks.AnyAsync(x=>x.BookID == userbook.BookID && x.UserID == userbook.UserID))
            {
                return 0;
            }
            await _context.UserBooks.AddAsync(userbook);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                return 2;
            }
            return 1;
        }
    }
}
