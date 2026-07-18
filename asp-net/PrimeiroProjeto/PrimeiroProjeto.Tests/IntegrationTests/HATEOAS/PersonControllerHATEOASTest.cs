using Azure;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using PrimeiroProjeto.Data.DTO.V1;
using PrimeiroProjeto.Tests.IntegrationTests.Tools;
using System.Net.Http.Json;
using System.Text.RegularExpressions;

namespace PrimeiroProjeto.Tests.IntegrationTests.HATEOAS.Person
{
    [TestCaseOrderer
        (TestConfigs.TestCaseOrderFullName,
        TestConfigs.TestCaseOrdererAssembly)]
    [Collection("SequentialTests")]
    public class PersonControllerHATEOASTest
        : IClassFixture<SqlServerFixture>

    {
        private readonly HttpClient _httpClient;
        private static PersonDTO _person;
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
            var pattern = $@"""rel"":\s*""{rel}"".*?""href"":\s*""https?://.+/api/person/v1.*?""";
            Regex.IsMatch(Content, pattern
                , RegexOptions.IgnoreCase |
                RegexOptions.Singleline).Should()
                .BeTrue($"Link with rel='{rel}' should exist and have valid href");
        }
        [Fact (DisplayName = "01 - Create Person")]
        [TestPriority(1)]
        public async Task
            CreatePerson_ShouldContainHateoasLinks()
        {
            var request = new PersonDTO
            {
                FirstName = "David",
                LastName = "Heinemeier",
                Address = "Copenhagen - Denmark",
                Gender = "Male",
                Enabled = true,

            };
            var response = await 
                _httpClient.PostAsJsonAsync
                ("/api/person/v1", request);
            response.EnsureSuccessStatusCode();
            var content = await
                response.Content.ReadAsStringAsync();
            _person = await response.Content
                .ReadFromJsonAsync<PersonDTO>();
            AssertLinkPattern
                (content, "colection");
            AssertLinkPattern
                (content, "self");
            AssertLinkPattern
                (content, "create");
            AssertLinkPattern
                (content, "update");
            AssertLinkPattern
                (content, "patch");
            AssertLinkPattern
                (content, "delete");
        }
        [Fact(DisplayName = "02 - Update Person")]
        [TestPriority(2)]
        public async Task
            UpdatePerson_ShouldCOntainHateoasLinks()
        {
            _person!.LastName = "Heinemeier Hansson";

            var response =
                await _httpClient.PutAsJsonAsync
                ("/api/person/v1", _person);

            response.EnsureSuccessStatusCode();

            var content = await
                response.Content
                .ReadAsStringAsync();

            _person = await 
                response.Content.ReadFromJsonAsync
                <PersonDTO>();

            AssertLinkPattern
                (content, "colection");
            AssertLinkPattern
                (content, "self");
            AssertLinkPattern
                (content, "create");
            AssertLinkPattern
                (content, "update");
            AssertLinkPattern
                (content, "patch");
            AssertLinkPattern
                (content, "delete");

        }
        [Fact(DisplayName = "03 - Disable Person By Id")]
        [TestPriority(3)]
        public async Task
            DisablePersonById_ShouldContainHateosLinks()
        {
            var response = await 
                _httpClient.PatchAsync
                ($"/api/person/v1/{_person.Id}",
                null);
            response.EnsureSuccessStatusCode();
            var content = await
                response.Content.ReadAsStringAsync();
            _person = await response.Content
                .ReadFromJsonAsync<PersonDTO>();
            AssertLinkPattern
                (content, "colection");
            AssertLinkPattern
                (content, "self");
            AssertLinkPattern
                (content, "create");
            AssertLinkPattern
                (content, "update");
            AssertLinkPattern
                (content, "patch");
            AssertLinkPattern
                (content, "delete");

        }
        [Fact(DisplayName = "04 - Get Person By Id")]
        [TestPriority(4)]
        public async Task
            GetPersonById_ShouldContainHateoasLinks()
        {
            var response = await
                _httpClient.GetAsync
                ($"/api/person/v1/{_person!.Id}");
            response.EnsureSuccessStatusCode();
            var content = await
                response.Content.ReadAsStringAsync();
            _person = await response.Content
                .ReadFromJsonAsync<PersonDTO>();
            AssertLinkPattern
                (content, "colection");
            AssertLinkPattern
                (content, "self");
            AssertLinkPattern
                (content, "create");
            AssertLinkPattern
                (content, "update");
            AssertLinkPattern
                (content, "patch");
            AssertLinkPattern
                (content, "delete");
        }
    }
}
