using Azure;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using PrimeiroProjeto.Data.DTO.V1;
using PrimeiroProjeto.Tests.IntegrationTests.Tools;
using System.Net.Http.Headers;
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
        private static TokenDTO? _token;
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
            _httpClient.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue
                ("Bearer", _token?.AccessToken);
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
            _httpClient.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue
                ("Bearer", _token?.AccessToken);
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
            _httpClient.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue
                ("Bearer", _token?.AccessToken);
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
            _httpClient.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue
                ("Bearer", _token?.AccessToken);
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
        [Fact(DisplayName = "05 - Find Paged Persons with HATEOAS")]
        [TestPriority(5)]
        public async Task FindAll_ShouldReturnLinksForEachPerson()
        {
            _httpClient.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue
                ("Bearer", _token?.AccessToken);

            // ---------------------------
            // Act
            // ---------------------------
            // Perform the HTTP GET request to retrieve all persons.
            var response = await _httpClient
                .GetAsync("api/person/v1/asc/10/1");// Ensures the response status code is 2xx.

            // Read the response content as a string.
            var content = await response.Content.ReadAsStringAsync();

            // ---------------------------
            // Assert
            // ---------------------------
            // Extract all "id" values from the response JSON using Regex.
            var idMatches = Regex.Matches(content, @"""list"":\s*\[\s*{[^}]*""id"":\s*(\d+)");
            idMatches.Count.Should().BeGreaterThan(0, "There should be at least one person");

            // Iterate through each person id found in the response.
            foreach (Match match in idMatches)
            {
                var id = match.Groups[1].Value;

                // Expected hypermedia relations (HATEOAS links).
                var expectedRels = new[] { "colection", "self", "create", "update", "patch", "delete" };

                foreach (var rel in expectedRels)
                {
                    // Build the expected regex pattern depending on the relation.
                    // For "self" and "delete", the link must contain the specific id.
                    // For others, the link points to the base endpoint.
                    var pattern = rel switch
                    {
                        "self" or "delete" or "patch" =>
                            $@"""rel"":\s*""{rel}"".*?""href"":\s*""https?://.+/api/person/v1/{id}""",
                        _ =>
                            $@"""rel"":\s*""{rel}"".*?""href"":\s*""https?://.+/api/person/v1"""
                    };

                    // Assert that the link with the correct "rel" and "href" exists.
                    Regex.IsMatch(content, pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline)
                         .Should().BeTrue($"Link '{rel}' should exist for person {id}");

                    // Assert that each link also contains a "type" attribute.
                    var typePattern = $@"""rel"":\s*""{rel}"".*?""type"":\s*""[^""]+""";
                    Regex.IsMatch(content, typePattern, RegexOptions.IgnoreCase | RegexOptions.Singleline)
                         .Should().BeTrue($"Link '{rel}' must have a type for person {id}");
                }
            }
        }
    }
}
