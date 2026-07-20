using PrimeiroProjeto.Hypermedia.Utils;
using PrimeiroProjeto.Model;

namespace PrimeiroProjeto.Repositories
{
    public interface IPersonRepository : 
        IRepository<Person>
    {
        Person Disable(long id);
        List<Person> FindByName(string firstName,
            string lastName);
        PagedSearch<Person> FindWithPagedSearch
            (string name, string sortDirection,
            int pageSize, int page);
    }
}
