using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using PrimeiroProjeto.Tests.IntegrationTests.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeiroProjeto.Tests.IntegrationTests
{
    public class SwaggerIntegrationTests
        : IClassFixture<SqlServerFixture>
    {
        private readonly HttpClient _httpClient;

        public SwaggerIntegrationTests
            (SqlServerFixture sqlServerFixture)
        {
            var factory = new
                CustomWebApplicationFactory<Program>
                (sqlServerFixture
                .ConnectionString);
            _httpClient = factory.CreateClient(
                new 
                WebApplicationFactoryClientOptions
                {
                    BaseAddress=new Uri
                    ("http://localhost")
                });

        }
        [Fact]
        public async Task
            SwaggerJson_SHouldReturnSwaggerJson()
        {
            var response = await
                _httpClient.GetAsync
                ("/swagger/v1/swagger.json");

            response.EnsureSuccessStatusCode();
            var content = await response.Content
                .ReadAsStringAsync();
            content.Should().NotBeNull();
            content.Should().Contain(
                "/api/person/v1");
            
                
        }
        [Fact]
        public async Task SwaggerUI_ShouldReturnSwaggerUI
            ()
        {
            var response = await
                _httpClient.GetAsync
                ("/swagger-ui/index.html");

            response.EnsureSuccessStatusCode();
            var content = await
                response.Content.ReadAsStringAsync();
            content.Should().NotBeNull();
            content.Should().Contain
                ("<div id=\"swagger-ui\">");
        }
    }
}
