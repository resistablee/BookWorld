using BookWorld.Entity.Entities;
using BookWorld.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace BookWorld.Web.ViewComponents
{
    public class BooksViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            HttpResponseMessage response = ApiRequest.BookList();

            var jsonData = response.Content.ReadAsStringAsync().Result;
            List<Books> bookList = JsonConvert.DeserializeObject<List<Books>>(jsonData);
            ViewBag.BookList = new SelectList(bookList, "Id", "Name");
            //ViewBag.BookList = bookList;
            return View();
        }
    }
}
