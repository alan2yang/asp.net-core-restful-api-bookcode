using Library.API.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.API.Testing
{
    public class AuthorController_IntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    //public class AuthorController_IntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly LoginUser _loginUser;

        public AuthorController_IntegrationTests(WebApplicationFactory<Startup> factory)
        // public AuthorController_IntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _loginUser = new LoginUser
            {
                UserName = "demouser",
                Password = "demopassword"
            };
        }

        [Fact]
        public async Task Test_CreateAuthor()
        {
            // Arrange
            var client = _factory.CreateClient();
            var authorDto = new AuthorDto
            {
                Name = "Test Author",
                Email = "author_testing@xxx.com",
                BirthPlace = "Beijing",
                Age = 50
            };

            var jsonContent = JsonConvert.SerializeObject(authorDto);
            var bearerResult = await client.TryGetBearerTokenAsync(_loginUser);
            if (!bearerResult.result)
            {
                throw new Exception("Authentication failed");
            }

            client.DefaultRequestHeaders.Add(HeaderNames.Authorization, $"Bearer {bearerResult.token}");

            // Act
            var response = await client.PostAsync("api/authors", new StringContent(content: jsonContent,
                encoding: Encoding.UTF8,
                mediaType: "application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Test_CreateAuthor_Unauthorized()
        {
            // Arrange
            var client = _factory.CreateClient();
            var authorDto = new AuthorDto
            {
                Name = "Test Author",
                Email = "author_testing@xxx.com",
                BirthPlace = "Beijing",
                Age = 50
            };
            var jsonContent = JsonConvert.SerializeObject(authorDto);

            // Act
            var response = await client.PostAsync("api/authors", new StringContent(content: jsonContent,
                encoding: Encoding.UTF8,
                mediaType: "application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Theory]
        [InlineData("1029db57-c15c-4c0c-80a0-c811b7995cb4")]
        [InlineData("74556abd-1a6c-4d20-a8a7-271dd4393b2e")]
        public async Task Test_GetAuthorById(string authorId)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"api/authors/{authorId}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Contains(authorId, await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task Test_GetAuthorByNotExistId()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"api/authors/{Guid.NewGuid().ToString()}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("12")]
        public async Task Test_GetAuthorByNotInvalidId(string authorId)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"api/authors/{authorId}");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}