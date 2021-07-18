using BookWorld.Entity.Entities;
using BookWorld.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace BookWorld.Web.Controllers
{
    public class BooksController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            HttpResponseMessage response = ApiRequest.UserBookList();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonData = response.Content.ReadAsStringAsync().Result;
                return View(JsonConvert.DeserializeObject<List<Books>>(jsonData));
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("_Unauthorized", "ErrorPages");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return RedirectToAction("_BadRequest", "ErrorPages");
            }
            return View();
        }

        [HttpPost]
        public JsonResult AddUserBook(Books book)
        {
            HttpResponseMessage response = ApiRequest.AddUserBook(book);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return Json(1);
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                return Json(2);
            else
                return Json(0);
        }
    }
}
