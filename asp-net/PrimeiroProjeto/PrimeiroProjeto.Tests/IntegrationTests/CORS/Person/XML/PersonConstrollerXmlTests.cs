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
namespace PrimeiroProjeto.Tests.IntegrationTests.CORS.Person.XML
{
    [TestCaseOrderer
        ("PrimeiroProjeto.Tests.IntegrationTests.Tools.PriorityOrderer",
        "PrimeiroProjeto.Tests")]
    public class PersonConstrollerXmlTests : 
        IClassFixture<SqlServerFixture>
    {
        private readonly HttpClient _httpClient;
        private static PersonDTO _person;
        public PersonConstrollerXmlTests(
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
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add
                (new MediaTypeWithQualityHeaderValue
                ("application/xml"));
            
           
        }
        
        [Fact(DisplayName ="01 - Create Person ")]
        [TestPriority(1)]
        public async Task
            CreatePerson_ShouldReturnCreatedPerson()
        {
            
            var
                request
                = new PersonDTO
                {
                    FirstName = "Linus",
                    LastName = "Torvalds",
                    Address = "Helsinki - Finland",
                    Gender = "Male",
                    Enabled = true
                };
            var response = await 
                _httpClient
                .PostAsync
                ("api/person/v1",
                XmlHelper.SerizlizeToXml
                ( request));
            response.EnsureSuccessStatusCode();

            var created = await XmlHelper
                .DeserializeFromXmlAsync<PersonDTO>
                (response);
                

            created.Should().NotBeNull();
            created.Id.Should().BeGreaterThan(0);
            created.FirstName.Should().Be("Linus");
            created.LastName.Should().Be("Torvalds");
            created.Address.Should().Be
                ("Helsinki - Finland");
            created.Enabled.Should().BeTrue();

            _person = created;
        }
        
        [Fact(DisplayName ="02 - Update Person ")]
        [TestPriority(2)]
        public async Task
            UpdatePerson_ShouldReturnUpdatedPerson()
        {

            _person.LastName = "Benedict Torvalds";
            
            var response = await 
                _httpClient
                .PutAsync
                ("api/person/v1",XmlHelper
                .SerizlizeToXml(_person));
            response.EnsureSuccessStatusCode();

            var updated = await XmlHelper
                .DeserializeFromXmlAsync<PersonDTO>
                (response);

            updated.Should().NotBeNull();
            updated.Id.Should().BeGreaterThan(0);
            updated.FirstName.Should().Be("Linus");
            updated.LastName.Should().Be
                ("Benedict Torvalds");
            updated.Address.Should().Be
                ("Helsinki - Finland");
            updated.Enabled.Should().BeTrue();

            _person = updated;
        }

        [Fact(DisplayName ="03 - Disable Person By ID")]
        [TestPriority(3)]
        public async Task
            DisablePersonById_ShouldReturnDisablePerson()
        {
            var response = await _httpClient
                .PatchAsync($"api/person/v1/" +
                $"{_person.Id}", null);
            response.EnsureSuccessStatusCode();
            var disabled = await XmlHelper
                .DeserializeFromXmlAsync<PersonDTO>
                (response);
            disabled.Should().NotBeNull();
            disabled.Id.Should().BeGreaterThan(0);
            disabled.FirstName.Should().Be
                ("Linus");
            disabled.LastName.Should().Be
                ("Benedict Torvalds");
            disabled.Address.Should().Be
                ("Helsinki - Finland");
            disabled.Enabled.Should().BeFalse();
        }

        
        [Fact(DisplayName = "04 - Get Person By ID ")]
        [TestPriority(4)]
        public async Task
            GetPersonById_ShouldReturnPerson()
        {
            
            var response = await
                _httpClient.GetAsync
          ($"api/person/v1/{_person.Id}");

            response.EnsureSuccessStatusCode();

            var found = await XmlHelper
                .DeserializeFromXmlAsync<PersonDTO>
                (response);

            found.Should().NotBeNull();
            found.Id.Should().Be(_person.Id);
            found.FirstName.Should().Be
                ("Linus");
            found.LastName.Should().Be
                ("Benedict Torvalds");
            found.Address.Should().Be
                ("Helsinki - Finland");
            found.Enabled.Should().BeFalse();

        }
        [Fact(DisplayName = "05 - Delete Person By ID")]
        [TestPriority(5)]
        public async Task
            DeletePersonById_SHouldReturnNoContent()
        {
            var response = await 
                _httpClient
                .DeleteAsync($"api/person/v1/" +
                $"{_person.Id}");
            response.StatusCode.Should().Be
                (HttpStatusCode.NoContent);
        }
        [Fact(DisplayName = "06 - Find All Person")]
        [TestPriority(6)]
        public async Task
            FindAllPerson_ShouldListOfPerson()
        {
            var response = await
                _httpClient
                .GetAsync("api/person/v1");

            response.EnsureSuccessStatusCode();

            var list = await XmlHelper
                .DeserializeFromXmlAsync 
                < List < PersonDTO >>
                (response);
            list.Should().NotBeNull();
            list.Count.Should().BeGreaterThan(0);
            var first = list.First
                (p=>p.FirstName=="Ayrton");
            first.LastName.Should().Be("Senna");
            first.Address.Should().Be
                ("São Paulo - Brasil");
            first.Enabled.Should().BeTrue();
            first.Gender.Should().Be("Male");
            var fourth = list.First
                (p => p.FirstName == "Nelson");
            fourth.LastName.Should().Be
                ("Mandela");
            fourth.Address.Should().Be
                ("Mvezo - Soth Africa");
            fourth.Enabled.Should().BeTrue();
            fourth.Gender.Should().Be("Male");
        }
    }
}
