using BookWorld.BLL.Abstract;
using BookWorld.DAL;
using BookWorld.DAL.Concrate;
using BookWorld.Entity.Entities;

namespace BookWorld.BLL.Conctrate
{
    public class BookRepository : GeneralRepository<Books>, IBookRepository
    {
        private readonly Context _context;

        public BookRepository(Context context) : base(context)
        {
            _context = context;
        }
    }
}
