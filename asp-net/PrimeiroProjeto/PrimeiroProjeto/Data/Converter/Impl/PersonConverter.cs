using PrimeiroProjeto.Data.Converter.Contract;
using PrimeiroProjeto.Data.DTO;
using PrimeiroProjeto.Model;

namespace PrimeiroProjeto.Data.Converter.Impl
{
    public class PersonConverter : 
        IParser<Person, PersonDTO>, 
        IParser< PersonDTO, Person>
    {
        public Person Parse(PersonDTO origin)
        {
            if (origin == null)
            {
                return null;
            }
            return new Person()
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                Address = origin.Address,
                LastName = origin.LastName,
                Gender = origin.Gender,
            };
        }
        public List<Person> ParseList(List<PersonDTO> origin)
        {
            if (origin == null)
            {
                return null;

            }
            return origin.Select
                (item => Parse(item))
                .ToList();
        }
        public PersonDTO Parse(Person origin)
        {
            if (origin == null)
            {
                return null;
            }
            return new PersonDTO()
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                Address = origin.Address,
                LastName = origin.LastName,
                Gender = origin.Gender,
            };
        }


        public List<PersonDTO> ParseList
            (List<Person> origin)
        {
            if (origin == null)
            {
                return null;
            }
            return origin.Select
                (item => Parse(item))
                .ToList();
        }

    }
}
