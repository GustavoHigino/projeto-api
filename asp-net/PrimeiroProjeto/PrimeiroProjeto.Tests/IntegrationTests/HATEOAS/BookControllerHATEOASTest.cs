using Azure;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using PrimeiroProjeto.Data.DTO.V1;
using PrimeiroProjeto.Model.Context;
using PrimeiroProjeto.Tests.IntegrationTests.Tools;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.RegularExpressions;

namespace PrimeiroProjeto.Tests.IntegrationTests.HATEOAS.Book
{
    [TestCaseOrderer
        (TestConfigs.TestCaseOrderFullName,
        TestConfigs.TestCaseOrdererAssembly)]
    [Collection("SequentialTests")]
    public class PersonControllerHATEOASTest
        : IClassFixture<SqlServerFixture>

    {
        private readonly HttpClient _httpClient;
        private static BookDTO _book;
        public PersonControllerHATEOASTest(
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
        private void AssertLinkPattern(string Content
            ,string rel)
        {
            var pattern = $@"""rel"":\s*""{rel}"".*?""href"":\s*""https?://.+/api/book/v1.*?""";
            Regex.IsMatch(Content, pattern
                , RegexOptions.IgnoreCase |
                RegexOptions.Singleline).Should()
                .BeTrue($"Link with rel='{rel}' should exist and have valid href");
        }
        [Fact (DisplayName = "01 - Create Book")]
        [TestPriority(1)]
        public async Task
            CreateBook_ShouldContainHateoasLinks()
        {
            var request = new BookDTO
            {
                Title="Titulo",
                Author="Autor",
                Data=new DateTime(1999,01,01),
                Price=25
                

            };
            var response = await 
                _httpClient.PostAsJsonAsync
                ("/api/book/v1", request);
            response.EnsureSuccessStatusCode();
            var content = await
                response.Content.ReadAsStringAsync();
            _book = await response.Content
                .ReadFromJsonAsync<BookDTO>();
            AssertLinkPattern
                (content, "colection");
            AssertLinkPattern
                (content, "self");
            AssertLinkPattern
                (content, "create");
            AssertLinkPattern
                (content, "update");
            
            AssertLinkPattern
                (content, "delete");
        }
        [Fact(DisplayName = "02 - Update Book")]
        [TestPriority(2)]
        public async Task
            UpdateBook_ShouldCOntainHateoasLinks()
        {
            _book!.Title = "Titulo alterado";

            var response =
                await _httpClient.PutAsJsonAsync
                ("/api/book/v1", _book);

            response.EnsureSuccessStatusCode();

            var content = await
                response.Content
                .ReadAsStringAsync();

            _book = await 
                response.Content.ReadFromJsonAsync
                <BookDTO>();

            AssertLinkPattern
                (content, "colection");
            AssertLinkPattern
                (content, "self");
            AssertLinkPattern
                (content, "create");
            AssertLinkPattern
                (content, "update");
            
            AssertLinkPattern
                (content, "delete");

        }
        
        [Fact(DisplayName = "03 - Get Book By Id")]
        [TestPriority(4)]
        public async Task
            GetBookById_ShouldContainHateoasLinks()
        {
            var response = await
                _httpClient.GetAsync
                ($"/api/book/v1/{_book!.Id}");
            response.EnsureSuccessStatusCode();
            var content = await
                response.Content.ReadAsStringAsync();
            _book = await response.Content
                .ReadFromJsonAsync<BookDTO>();
            AssertLinkPattern
                (content, "colection");
            AssertLinkPattern
                (content, "self");
            AssertLinkPattern
                (content, "create");
            AssertLinkPattern
                (content, "update");
            
            AssertLinkPattern
                (content, "delete");
        }
    }
}
