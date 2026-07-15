using PrimeiroProjeto.Model;

namespace PrimeiroProjeto.Repositories
{
    public interface IPersonRepository : 
        IRepository<Person>
    {
        Person Disable(long id); 
    }
}
