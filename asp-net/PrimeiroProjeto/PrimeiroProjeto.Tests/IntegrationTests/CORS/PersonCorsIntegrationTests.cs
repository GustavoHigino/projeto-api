using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using PrimeiroProjeto.Data.DTO.V1;
using PrimeiroProjeto.Tests.IntegrationTests.Tools;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
namespace PrimeiroProjeto.Tests.IntegrationTests.CORS
{
    [TestCaseOrderer
        ("PrimeiroProjeto.Tests.IntegrationTests.Tools.PriorityOrderer",
        "PrimeiroProjeto.Tests")]
    public class PersonCorsIntegrationTests : 
        IClassFixture<SqlServerFixture>
    {
        private readonly HttpClient _httpClient;
        private static TokenDTO? _token;
        private static PersonDTO _person;
        public PersonCorsIntegrationTests(
            SqlServerFixture sqlFixture)
        {
           
            var factory =
                new CustomWebApplicationFactory<Program>(
                    sqlFixture
                    .ConnectionString);
            _httpClient = factory.CreateClient(
                new
                WebApplicationFactoryClientOptions
                {
                    BaseAddress = new Uri(
                        "http://localhost")
                });
            
           
        }
        [Fact(DisplayName = "00 - Sign In")]
        [TestPriority(0)]
        public async Task SignIn_ShouldReturnToken()
        {
            var credentials = new
                UserDTO
            {
                Username = "leandro",
                Password = "admin123",
            };
            var response = await _httpClient
                .PostAsJsonAsync("api/auth/signin",
                credentials);
            response.EnsureSuccessStatusCode();
            var token = await response
                .Content.ReadFromJsonAsync
                <TokenDTO>();
            token.Should().NotBeNull();
            token.AccessToken.Should().NotBeNullOrWhiteSpace();
            token.RefreshToken.Should().NotBeNullOrWhiteSpace();
            _token = token;

        }
        private void AddOriginHeader(string origin)
        {
            _httpClient.DefaultRequestHeaders
                .Remove("Origin");
            _httpClient.DefaultRequestHeaders.Add
                ("Origin", origin);
        }
        [Fact(DisplayName ="01 - Create Person with " +
            "Allowed Origin")]
        [TestPriority(1)]
        public async Task
            CreatePerson_WithAllowedOrigin_ShouldReturnCreated()
        {
            _httpClient.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue
                ("Bearer", _token?.AccessToken);
            AddOriginHeader("https://erudio.com.br");
            var
                request
                = new PersonDTO
                {
                    FirstName = "Richard",
                    LastName = "Stallman",
                    Address = "New York City - New York - USA",
                    Gender = "Male"
                };
            var response = await _httpClient
                .PostAsJsonAsync
                ("api/person/v1",request);
            response.EnsureSuccessStatusCode();

            var created = await response.Content
                .ReadFromJsonAsync<PersonDTO>();
            created.Should().NotBeNull();
            created.Id.Should().BeGreaterThan(0);

            _person = created;
        }
        [Fact(DisplayName ="02 - Create Person with " +
            "Disallowed Origin")]
        [TestPriority(2)]
        public async Task
            CreatePerson_WithDisallowedOrigin_ShouldReturnForbiden()
        {
            _httpClient.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue
                ("Bearer", _token?.AccessToken);
            AddOriginHeader("https://semeru.com.br");
            var
                request
                = new PersonDTO
                {
                    FirstName = "Richard",
                    LastName = "Stallman",
                    Address = "New York City - New York - USA",
                    Gender = "Male"
                };
            var response = await _httpClient
                .PostAsJsonAsync
                ("api/person/v1",request);
            response.StatusCode.Should().Be
                (HttpStatusCode.Forbidden);

            var content = await response.Content
                .ReadAsStringAsync();
            content.Should().Be
                ("Cors origin not allowed.");

        }
        [Fact(DisplayName = "03 - Get Person By ID with " +
            "Allowed Origin")]
        [TestPriority(3)]
        public async Task
            FindByIdPerson_WithAllowedOrigin_ShouldReturnOk()
        {
            _httpClient.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue
                ("Bearer", _token?.AccessToken);

            AddOriginHeader("https://erudio.com.br");
            var response = await
                _httpClient
          .GetAsync($"api/person/v1/{_person.Id}");
            response.EnsureSuccessStatusCode();
            var found = await response.Content
                .ReadFromJsonAsync<PersonDTO>();
            found.Should().NotBeNull();
            found.Id.Should().Be(_person.Id);
            found.FirstName.Should().
                Be("Richard");
            found.LastName.Should().
                Be("Stallman");
            found.Address.Should().
                Be("New York City - New York - USA");
                

        }
        [Fact(DisplayName = "04 - Get Person By ID with " +
            "Disallowed Origin")]
        [TestPriority(4)]
        public async Task
            FindByIdPerson_WithDisallowedOrigin_ShouldReturnForbiden()
        {
            _httpClient.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue
                ("Bearer", _token?.AccessToken);

            AddOriginHeader("https://semeru.com.br");
            var response = await
                _httpClient
                .GetAsync
                ($"api/person/v1/{_person.Id}");
            response.StatusCode.Should().Be
                (HttpStatusCode.Forbidden);
            var content = await response.Content
                .ReadAsStringAsync();
            content.Should().Be("Cors origin not allowed.");
        }
    
            
    }
}
