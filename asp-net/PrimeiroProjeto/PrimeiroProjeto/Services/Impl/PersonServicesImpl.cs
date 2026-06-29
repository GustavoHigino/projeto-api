using PrimeiroProjeto.Model;

namespace PrimeiroProjeto.Services.Impl
{
    public class PersonServicesImpl : IPersonServices
    {
        public List<Person> FindAll()
        {
            List<Person> persons = new List<Person>();
            for (int i = 0; i < 8; i++)
            {
                persons.Add(MockPerson(i));
            }
            return persons;
        }
        public Person FindById(long id)
        {
            var person = MockPerson(id);
            return person;
        }
        public Person Create(Person person)
        {
            person.Id = new Random()
                .Next(1, 1000);
            return person;
        }
        public Person Update(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {
            
        }

        private Person MockPerson(long i=0)
        {
            return new Person
            {
                Id = new Random()
                .Next(1, 1000),
                FirstName = "John" + i,
                LastName = "Doe" + i,
                Address = "123 Main Street" + i,
                Gender = "Male"
            };
        }

    }
}
