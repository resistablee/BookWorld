using BookWorld.Entity.DTO;
using BookWorld.Entity.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookWorld.Test
{
    public class TestBookController : IClassFixture<WebApplicationFactory<API.Startup>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<API.Startup> _factory;
        //public TestBookController()
        //{
        //    //TestServer Asp.net core ile hayatımıza giren bir sınıf. Bu sınıf tıpkı bir iis gibi apimizi host edermiş gibi
        //    //api projemizi çalıştırıp o api üzerinde test yapmamızı sağlıyor.
        //    var testServer = new TestServer(new WebHostBuilder()
        //                                        .UseStartup<API.Startup>()
        //                                        .UseEnvironment("Development"));
        //    _client = testServer.CreateClient();
        //}

        public TestBookController(WebApplicationFactory<API.Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async void Should_GET_Books_Return_Unauthorized_when_Token_is_Null()
        {
            var expectedStatusCode = HttpStatusCode.Unauthorized;

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", null);

            var response = await _client.GetAsync("api/Book/Books");
            var actualStatusCode = response.StatusCode;
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public async void Should_GET_Books_Return_Ok_when_Token_is_Not_Null()
        {
            var expectedStatusCode = HttpStatusCode.OK;

            //gelen token ile beraber token'ın ait olduğu kullanıcının kitap listesi talebi
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GetToken().Result);

            var response = await _client.GetAsync("api/Book/Books");
            var actualStatusCode = response.StatusCode;
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public async void Should_POST_Books_Return_Unauthorized_when_Books_Token_is_Null()
        {
            var expectedStatusCode = HttpStatusCode.Unauthorized;

            //kullanıcı bilgilerini göndererek token talebi
            var book = new Books
            {
                Name = "Kitap-50",
                Author = "Yazar-8"
            };

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var json = JsonConvert.SerializeObject(book);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/Book/Books", data);
            var actualStatusCode = response.StatusCode;
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public async void Should_POST_Books_Return_BadRequest_when_User_TriesTo_ReadThe_Same_Book_Twice()
        {
            var expectedStatusCode = HttpStatusCode.BadRequest;

            //kullanıcı bilgilerini göndererek token talebi
            var book = new Books
            {
                Id = 7
            };

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GetToken().Result);
            var json = JsonConvert.SerializeObject(book);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/Book/Books", data);

            if (response.IsSuccessStatusCode)
            {
                var response2 = await _client.PostAsync("api/Book/Books", data);
                var actualStatusCode = response2.StatusCode;
                Assert.Equal(expectedStatusCode, actualStatusCode);
            }
        }

        public async Task<string> GetToken()
        {
            //kullanıcı bilgilerini göndererek token talebi
            var login = new LoginDTO
            {
                username = "admin",
                pass = "1"
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var json = JsonConvert.SerializeObject(login);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var loginResponse = await _client.PostAsync("api/User/Login", data);

            string jsondata = await loginResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<string>(jsondata);
        }
    }
}