using BookWorld.BLL.Abstract;
using BookWorld.DAL;
using BookWorld.Entity.DTO;
using BookWorld.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BookWorld.BLL.Conctrate
{
    public class UserRepository :  IUserRepository
    {
        protected readonly Context _context;
        public UserRepository(Context context)
        {
            _context = context;
        }

        public async Task<byte> Add(Users user)
        {
            if (_context.Users.Any(x=>x.Username == user.Username))
            {
                return 2;
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<Users> Login(LoginDTO login)
        {
            Users user = null;
            user = await _context.Users.SingleOrDefaultAsync(x => x.Username == login.username && x.Password == login.pass);
            if (user == null)
            {
                return null;
            }

            return user;
        }
    }
}
