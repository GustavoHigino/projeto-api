using PrimeiroProjeto.Hypermedia.Utils;
using PrimeiroProjeto.Model;
using PrimeiroProjeto.Model.Context;
using PrimeiroProjeto.Repositories.QueryBuilders;

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

        public List<Person> FindByName(string firstName, string lastName)
        {
            var query = _context.Persons
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                query = query.Where(p =>
                p.FirstName.Contains(firstName));
                
            }
            if (!string.IsNullOrWhiteSpace(lastName))
            {
                query = query.Where(p =>
                p.LastName.Contains(lastName));
            }
            return query.ToList();
        }

        public PagedSearch<Person> FindWithPagedSearch
            (
            string name,
            string sortDirection,
            int pageSize,
            int page
            )
        {
            var queryBuilder = 
                new PersonQueryBuilder();
            var (query, countQuery, sort, size, offset) =
                queryBuilder.buildQueries(
                    name, sortDirection, pageSize, page);
            var persons = base
                .FindWithPagedSearch(query);
            var totalResults = base
                .GetCount(countQuery);

            return new PagedSearch<Person>
            {
                CurrentPage = page,
                List = persons,
                PageSize = size,
                SortDirections = sort,
                TotalResult = totalResults

            };
        }
    }
}
