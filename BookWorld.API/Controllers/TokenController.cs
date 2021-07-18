using BookWorld.BLL.Abstract;
using BookWorld.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookWorld.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IToken _token;

        public TokenController(IToken token)
        {
            _token = token;
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult RefreshToken(string token)
        {
            Users user = _token.ReadPayload(token);
            string newToken = _token.Generate(user);
            return Ok(newToken);
        }
    }
}
