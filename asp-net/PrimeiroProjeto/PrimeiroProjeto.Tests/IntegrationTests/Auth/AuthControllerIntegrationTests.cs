using DocumentFormat.OpenXml.Bibliography;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using PrimeiroProjeto.Data.DTO.V1;
using PrimeiroProjeto.Tests.IntegrationTests.Tools;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace PrimeiroProjeto.Tests.IntegrationTests.Auth
{
    [TestCaseOrderer
        (TestConfigs.TestCaseOrderFullName,
        TestConfigs.TestCaseOrdererAssembly)]
    public class AuthControllerIntegrationTests : 
        IClassFixture<SqlServerFixture>
    {
        private readonly HttpClient _httpClient;
        private static TokenDTO _token;
        private static AccountCredentialsDTO? 
            _createdUser;
        public AuthControllerIntegrationTests(
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
        [Fact(DisplayName ="01 - Create User")]
        [TestPriority(1)]
        public async Task 
            CreateUser_ShouldReturnCreateUser()
        {
            var request = new
                AccountCredentialsDTO
            {
                Username = "solomon",
                Fullname = "Solomon Hykes",
                Password = "hykes123"

            };
            var response = await
                _httpClient.PostAsJsonAsync(
                    "api/auth/create", request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content
                .ReadFromJsonAsync<AccountCredentialsDTO>();
            result.Should().NotBeNull();
            result.Username.Should().Be
                ("solomon");
            result.Fullname.Should().Be
                ("Solomon Hykes");
            _createdUser= result;
            
        }
        [Fact(DisplayName = "02 - Sign In")]
        [TestPriority(2)]
        public async Task SignIn_ShouldReturnToken()
        {
            var credentials = new
                UserDTO
            {
                Username = "solomon",
                Password = "hykes123",
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
        [Fact(DisplayName = "03 - RefreshToken")]
        [TestPriority(3)]
        public async Task
            RefreshToken_ShouldReturnNewToken()
        {
            var response = await 
                _httpClient.PostAsJsonAsync
                ("api/auth/refresh",_token);
            response.EnsureSuccessStatusCode();
            var newToken = await response
                .Content.ReadFromJsonAsync<TokenDTO>();
            newToken.Should().NotBeNull();
            newToken.AccessToken.Should()
                .NotBeNullOrWhiteSpace();
            newToken.RefreshToken.Should()
                .NotBeNullOrWhiteSpace();
            newToken.AccessToken.Should().NotBeSameAs
                (_token?.AccessToken);
            newToken.RefreshToken.Should().NotBeSameAs
                (_token?.AccessToken);
            _token = newToken;
        }
        [Fact(DisplayName = "04 - Revoke Token")]
        [TestPriority(4)]
        public async Task
            RevokeToken_ShouldReturnNoContent()
        {
            _httpClient.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue
                ("Bearer", _token?.AccessToken);

            var response = await _httpClient
                .PostAsync("api/auth/revoke", null);
            response.StatusCode.Should().Be(HttpStatusCode
                .NoContent);
        }
        [Fact(DisplayName = "05 - Sign in with " +
            "Invalid Credentials")]
        [TestPriority(5)]
        public async Task
            SignIn_WithInvalidCredentials_ShouldReturnUNauthorized()
        {
            var credentials = new UserDTO
            {
                Username = "solomon",
                Password = "wrongpassword"

            };
            var response = await _httpClient
                .PostAsJsonAsync("api/auth/signin",
                credentials);
            response.StatusCode.Should()
                .Be(HttpStatusCode.Unauthorized);

        }
        [Fact(DisplayName = "06 - Revoke Token without " +
            "Authorization Header")]
        [TestPriority(6)]
        public async Task
            RevokeToke_WithoutAuthorizationHeader()
        {
            _httpClient.DefaultRequestHeaders
                .Authorization = null;
            var response = await _httpClient
                .PostAsync("api/auth/revoke",
                null);

            response.StatusCode.Should()
                .Be(HttpStatusCode.Unauthorized);
        }

    }
}
