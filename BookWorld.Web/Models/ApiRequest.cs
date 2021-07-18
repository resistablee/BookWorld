using BookWorld.Entity.Entities;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace BookWorld.Web.Models
{
    public class ApiRequest
    {
        private readonly static string apiURL = "https://localhost:44371/";

        public static HttpResponseMessage BookList()
        {
            HttpResponseMessage response = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(apiURL);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Models.Token.token);
                try
                {
                    response = client.GetAsync("api/Book/BookList").Result;
                }
                catch (Exception ex)
                {
                    var asd = response.Content.ReadAsStringAsync().Result;
                }
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized && Token.token != null)
            {
                string newToken = RefreshToken();
                Token.token = newToken;
                return BookList();
            }
            return response;
        }

        public static HttpResponseMessage UserBookList()
        {
            HttpResponseMessage response = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(apiURL);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Models.Token.token);
                try
                {
                    response = client.GetAsync("api/Book/Books").Result;
                }
                catch (Exception ex)
                {
                    var asd = response.Content.ReadAsStringAsync().Result;
                }
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized && Token.token != null)
            {
                string newToken = RefreshToken();
                Token.token = newToken;
                return UserBookList();
            }
            return response;
        }

        public static HttpResponseMessage AddUserBook(Books book)
        {
            HttpResponseMessage response = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(apiURL);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Models.Token.token);

                var json = JsonConvert.SerializeObject(book);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                try
                {
                    response = client.PostAsync("api/Book/Books", data).Result;
                }
                catch (Exception ex)
                {
                    var asd = response.Content.ReadAsStringAsync().Result;
                }
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized && Token.token != null)
            {
                string newToken = RefreshToken();
                Token.token = newToken;
                return AddUserBook(book);
            }
            return response;
        }

        public static string RefreshToken()
        {
            HttpResponseMessage response = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(apiURL);
                try
                {
                    response = client.GetAsync("api/Token/RefreshToken?token=" + Token.token).Result;
                }
                catch (Exception ex)
                {
                    var asd = response.Content.ReadAsStringAsync().Result;
                }
            }
            var jsonData = response.Content.ReadAsStringAsync().Result;
            return jsonData;
        }
    }
}
