using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using PrimeiroProjeto.Tests.IntegrationTests.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeiroProjeto.Tests.IntegrationTests
{
    public class ScalarIntegrationTests : 
        IClassFixture<SqlServerFixture>
    {
        private readonly HttpClient _httpClient;

        public ScalarIntegrationTests
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
                    BaseAddress = new Uri
                    ("http://localhost")
                });

        }
        [Fact]
        public async Task Scalar_ShouldReturnScalarUI()
        {
            var response = await
                _httpClient.GetAsync("/scalar/");

            response.EnsureSuccessStatusCode();
            var content = await
                response.Content.ReadAsStringAsync();
            content.Should().Contain
                ("<title>ASP.NET 2026 REST API's from 0 to Azure and GCP with .NET 10, Docker e Kubernetes</title>");
            content.Should().Contain("script src");
        }
    }
}
