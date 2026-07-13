using FluentAssertions;
using PrimeiroProjeto.Data.Converter.Impl;
using PrimeiroProjeto.Data.DTO.V2;
using PrimeiroProjeto.Model;

namespace PrimeiroProjeto.Tests.UnitTests
{
    public class PersonConverterV2Tests
    {
        private readonly PersonConverterV2 _converter;
        public PersonConverterV2Tests()
        {
            _converter = new PersonConverterV2();
        }
        //PersonDTO to Person Conversion tests
        [Fact]
        public void Parse_ShouldConvertPersonDTOToPerson()
        {
            /*  -Arrange: prepare the data objects, and 
                dependencies required for the test*/
            var dto = new PersonDTO()
            {
                Id = 1,
                FirstName = "Mahatma",
                LastName = "Gandhi",
                Address = "Porbandar - India",
                Gender = "Male",
                BirthDay = new DateTime
                (1869, 10, 2)
            };
            var expectedPerson = new Person
            {
                Id = 1,
                FirstName = "Mahatma",
                LastName = "Gandhi",
                Address = "Porbandar - India",
                Gender = "Male"

            };
            /*-Act:execute the method or functionality
            under test*/
            var person = _converter
                .Parse(dto);


            /*-Assert: verify that the result matches
            the expected outcome*/
            person.Should().NotBeNull();
            person.Id.Should().Be
                (expectedPerson.Id);
            person.FirstName.Should()
                .Be(expectedPerson.FirstName);
            person.LastName.Should()
                .Be(expectedPerson.LastName);
            person.Address.Should()
                .Be(expectedPerson.Address);
            person.Should().BeEquivalentTo
                (expectedPerson);

        }
        [Fact]
        public void Parse_NullPersonDTOShouldReturnNull()
        {
            PersonDTO dto = null;
            var person = _converter.Parse(dto);
            person.Should().BeNull();
        }
        //Person <- PersonDTO conversion tests
        [Fact]
        public void Parse_ShouldConvertPersonToPersonDTO()
        {
            /*  -Arrange: prepare the data objects, and 
                dependencies required for the test*/
            var entity = new Person()
            {
                Id = 1,
                FirstName = "Mahatma",
                LastName = "Gandhi",
                Address = "Porbandar - India",
                Gender = "Male",
                
            };
            var expectedPerson = new PersonDTO
            {
                Id = 1,
                FirstName = "Mahatma",
                LastName = "Gandhi",
                Address = "Porbandar - India",
                Gender = "Male",
                BirthDay = new DateTime
                (1869, 10, 2)

            };
            /*-Act:execute the method or functionality
            under test*/
            var person = _converter
                .Parse(entity);


            /*-Assert: verify that the result matches
            the expected outcome*/
            person.Should().NotBeNull();
            person.Id.Should().Be
                (expectedPerson.Id);
            person.FirstName.Should()
                .Be(expectedPerson.FirstName);
            person.LastName.Should()
                .Be(expectedPerson.LastName);
            person.Address.Should()
                .Be(expectedPerson.Address);
            person.Should().BeEquivalentTo
                (expectedPerson,
                options=>
                options.Excluding
                (person =>person.BirthDay));
            person.BirthDay.Should().NotBeNull();

        }
        [Fact]
        public void Parse_NullPersonShouldReturnNull()
        {
            Person dto = null;
            var person = _converter.Parse(dto);
            person.Should().BeNull();
        }

        [Fact]
        public void
        ParseList_ShouldConvertPersonDTOListToPersonList()
        {
            //arrange
            var dtoList = new List<PersonDTO>
            {
                new PersonDTO
                {
                    Id = 1,
                    FirstName="Mahatma",
                    LastName="Gandhi",
                    Address="Porbandar - India",
                    Gender="Male",
                    BirthDay=new DateTime(1869,
                    10,2)
                },
                new PersonDTO
                {
                    Id=2,
                    FirstName="Indira",
                    LastName="Gandhi",
                    Address="Allahabad - India",
                    Gender="Female",
                    BirthDay= new DateTime(1917,
                    11,19)
                    
                }
            };
            //act
            var personList = _converter
                .ParseList(dtoList);
            //Assert
            personList.Should().NotBeNull();
            personList.Should().HaveCount(2);
            personList[0].Should()
                .BeEquivalentTo(new Person
                {
                    Id = 1,
                    FirstName = "Mahatma",
                    LastName = "Gandhi",
                    Address = "Porbandar - India",
                    Gender = "Male"
                });
            personList[1].Should()
                .BeEquivalentTo(new Person
                {
                    Id = 2,
                    FirstName = "Indira",
                    LastName = "Gandhi",
                    Address = "Allahabad - India",
                    Gender = "Female"
                });
            personList[0].FirstName.Should()
                .Be("Mahatma");
            personList[1].FirstName.Should()
                .Be("Indira");
            personList[1].LastName.Should()
                .Be("Gandhi");
        }
        [Fact]
        public void 
            Parse_NullListPersonDTOShouldReturnNull()
        {
            List<PersonDTO> dto = null;
            var listPerson = _converter
                .ParseList(dto);
            listPerson.Should().BeNull();
        }
        [Fact]
        public void
        ParseList_ShouldConvertPersonListToPersonDTOList()
        {
            List<Person> listPerson= new List<Person>
            {
                new Person()
                {
                    Id=1,
                    Address="abc",
                    FirstName="maria",
                    LastName="Laiara",
                    Gender="Male"
                }
            };
            List<PersonDTO> listPersonDtoEsperado = new
                List<PersonDTO>
            {
                new PersonDTO()
                {
                    Id=1,
                    Address="abc",
                    FirstName="maria",
                    LastName="Laiara",

                    Gender="Male",
                    BirthDay=new DateTime
                    (2022,12,1)
                }
            };
            var personDtoList = _converter
                .ParseList(listPerson);

            personDtoList.Should().NotBeNull();
            personDtoList[0].Id.Should().Be
                (listPersonDtoEsperado[0].Id);
            personDtoList[0].FirstName.Should().Be
                (listPersonDtoEsperado
                [0].FirstName);
            personDtoList[0].LastName.Should().Be
                (listPersonDtoEsperado
                [0].LastName);
            personDtoList[0].Gender.Should().Be
                (listPersonDtoEsperado
                [0].Gender);
            personDtoList.Should().BeEquivalentTo
                (listPersonDtoEsperado,
                options => options
                .Excluding(person => person
                .BirthDay));
            personDtoList[0].BirthDay.Should()
                .NotBeNull();
            personDtoList.Should().HaveCount(1);
        }
        [Fact]
        public void
            Parse_NullListPersonShouldReturnNull()
        {
            List<Person> lista = null;
            var personDtoList = 
                _converter.ParseList(lista);
            personDtoList.Should().BeNull();
        }


    }
}
