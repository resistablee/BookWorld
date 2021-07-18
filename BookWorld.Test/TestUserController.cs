using BookWorld.Entity.DTO;
using BookWorld.Entity.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xunit;

namespace BookWorld.Test
{
    public class TestUserController : IClassFixture<WebApplicationFactory<API.Startup>>
    {
        private readonly HttpClient _client;

        private readonly WebApplicationFactory<API.Startup> _factory;
        /*public TestUserController()
        {
            //TestServer Asp.net core ile hayatımıza giren bir sınıf. Bu sınıf tıpkı bir iis gibi apimizi host edermiş gibi
            //api projemizi çalıştırıp o api üzerinde test yapmamızı sağlıyor.
            var testServer = new TestServer(new WebHostBuilder().
                ConfigureAppConfiguration((context, builder) => builder.SetBasePath(context.HostingEnvironment.ContentRootPath).AddJsonFile("appsettings.json"))
                                                .UseStartup<API.Startup>());
            _client = testServer.CreateClient();
        }*/

        public TestUserController(WebApplicationFactory<API.Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async void Should_Login_Method_Return_Ok_when_Login_params_Not_Empty()
        {
            var expectedStatusCode = HttpStatusCode.OK;

            var login = new LoginDTO
            {
                username = "admin",
                pass = "1"
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var json = JsonConvert.SerializeObject(login);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/User/Login", data);

            var actualStatusCode = response.StatusCode;
            var actualResult = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public async void Should_Login_Method_Return_Unauthorized_when_Username_and_Password_is_Wrong()
        {
            var expectedStatusCode = HttpStatusCode.Unauthorized;

            var login = new LoginDTO
            {
                username = "admin",
                pass = "2"
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var json = JsonConvert.SerializeObject(login);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/User/Login", data);

            var actualStatusCode = response.StatusCode;
            var actualStatusCodes = response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public async void Should_Register_Method_Return_InternalServerError_when_Users_Properties_is_Empty()
        {
            //Burada InternalServerError hatası vermesini bekliyoruz çünkü Users'in properiy'lerinin null olup
            //olmama durumunu client tarafında kontrol ediyoruz. Yani client tarafından zaten null data gelmiyor.

            var expectedStatusCode = HttpStatusCode.InternalServerError;

            var login = new Users
            {
                Name = null,
                Surname = null,
                Username = null,
                Password = null
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var json = JsonConvert.SerializeObject(login);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("api/User/Register", data);

            var actualStatusCode = response.StatusCode;
            var actualResult = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public async void Should_Register_Method_Return_Ok_when_Users_Properties_Is_Not_Empty()
        {
            var expectedStatusCode = HttpStatusCode.Created;

            var login = new Users
            {
                Name = "test",
                Surname = "surname",
                Username = "deneme2",
                Password = "1"
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var json = JsonConvert.SerializeObject(login);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("api/User/Register", data);

            var actualStatusCode = response.StatusCode;

            // Assert
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public async void Should_Register_Method_Return_InternalServerError_when_Password_Is_33_Character_And_More()
        {
            //Burada InternalServerError hatası vermesini bekliyoruz çünkü Password properties'inin 32 karakterden fazla olup
            //olmama durumunu client tarafında kontrol ediyoruz.

            var expectedStatusCode = HttpStatusCode.InternalServerError;

            var login = new Users
            {
                Name = "test",
                Surname = "surname",
                Username = "deneme",
                Password = "123456789123456789123456789123456"
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var json = JsonConvert.SerializeObject(login);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/User/Register", data);

            var actualStatusCode = response.StatusCode;

            Assert.Equal(expectedStatusCode, actualStatusCode);
        }
    }
}