using BookWorld.BLL.Abstract;
using BookWorld.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;

namespace BookWorld.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IUserBookRepository _userBookRepository;
        private readonly IToken _token;
        private readonly IBookRepository _book;

        public BookController(IUserBookRepository userBookRepository,
                                IToken token,
                                IBookRepository book)
        {
            _userBookRepository = userBookRepository;
            _token = token;
            _book = book;
        }

        [HttpGet]
        public IActionResult BookList()
        {
            List<Books> books = _book.GetAllAsync().Result;
            if (books == null)
            {
                return NoContent();
            }
            return Ok(books);
        }

        [HttpGet]
        public IActionResult Books()
        {
            //kullanıcının header içerisinde gönderdiği tokenı aldık.
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            Users user = _token.ReadPayload(token);

            if (user == null)
            {
                return Unauthorized();
            }
            List<Books> books = null;
            books = _userBookRepository.UserBookList(user.Id).Result;
            if (books == null)
            {
                return NoContent();
            }
            return Ok(books);
        }

        [HttpPost]
        public IActionResult Books(Books book)
        {
            if (book == null || book.Id < 1)
            {
                return BadRequest();
            }

            //kullanıcının header içerisinde gönderdiği tokenı aldık.
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            Users user = _token.ReadPayload(token);

            UserBooks userBooks = new UserBooks();
            userBooks.BookID = book.Id;
            userBooks.UserID = user.Id;
            byte val = _userBookRepository.AddUserBook(userBooks).Result;

            if (val == 1)
                return Ok();
            else if (val == 0) //Aynı kitabı birden fazla okursa
                return BadRequest();
            else if (val == 2) //Server hatası
                return StatusCode(500);
            else
                return NoContent();
        }
    }
}
