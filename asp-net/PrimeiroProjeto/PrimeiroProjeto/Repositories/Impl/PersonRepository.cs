using PrimeiroProjeto.Model;
using PrimeiroProjeto.Model.Context;

namespace PrimeiroProjeto.Repositories.Impl
{
    public class PersonRepository :
        GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MSSQLContext context) 
            : base(context)
        {
        }

        public Person Disable(long id)
        {
            var person = _context.Persons
                .Find(id);
            if (person == null) return null;
            person.Enabled = false;
            _context.SaveChanges();
            return person;
        }
    }
}
