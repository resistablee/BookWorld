using BookWorld.BLL.Abstract;
using BookWorld.Entity.DTO;
using BookWorld.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookWorld.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IToken _token;

        public UserController(IUserRepository userRepository,
                                IToken token)
        {
            _userRepository = userRepository;
            _token = token;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginDTO login)
        {
            Users user = _userRepository.Login(login).Result;
            if (user == null)
            {
                return Unauthorized();
            }
            string token = _token.Generate(user);

            return Ok(token);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(Entity.Entities.Users user)
        {
            byte val = 0;
            try
            {
                val = _userRepository.Add(user).Result;
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            if (val == 2)
            {
                return BadRequest();
            }
            return Created("/", "İşlem başarılı");
        }
    }
}