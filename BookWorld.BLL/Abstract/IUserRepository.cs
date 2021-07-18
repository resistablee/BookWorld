using BookWorld.Entity.DTO;
using BookWorld.Entity.Entities;
using System.Threading.Tasks;

namespace BookWorld.BLL.Abstract
{
    public interface IUserRepository
    {
        public Task<Users> Login(LoginDTO login);
        Task<byte> Add(Users user);
    }
}
