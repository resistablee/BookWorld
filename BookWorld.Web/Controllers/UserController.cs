using BookWorld.Entity.DTO;
using BookWorld.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace BookWorld.Web.Controllers
{
    public class UserController : Controller
    {
        HttpClient client = new HttpClient()
        {
            BaseAddress = new System.Uri("https://localhost:44371/"),
        };
        HttpResponseMessage response = null;

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Login(LoginDTO login)
        {
            if (!ModelState.IsValid)
            {
                return Json(0);
            }
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var json = JsonConvert.SerializeObject(login);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                response = client.PostAsync("api/User/Login", data).Result;
            }
            catch (Exception ex)
            {
                var asd = response.Content.ReadAsStringAsync().Result;
                return Json(3);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var token = response.Content.ReadAsStringAsync().Result;
                Models.Token.token = JsonConvert.DeserializeObject<string>(token);
                return Json(1);
            }
            else if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return Json(2);
            }
            else
            {
                return Json(3);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Register(Users user)
        {
            if (user.Name == null || user.Surname == null || user.Username == null || user.Password == null || user.Password.Length < 6 || user.Password.Length > 32)
            {
                return Json(0);
            }

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                response = client.PostAsync("api/User/Register", data).Result;
            }
            catch (Exception ex)
            {
                var asd = response.Content.ReadAsStringAsync();
                return Json(3);
            }

            if (response.IsSuccessStatusCode)
            {
                return Json(1);
            }
            else if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return Json(2);
            }
            return Json(3);
        }
    }
}