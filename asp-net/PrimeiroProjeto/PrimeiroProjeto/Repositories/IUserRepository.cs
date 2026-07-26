using PrimeiroProjeto.Model;

namespace PrimeiroProjeto.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User? FindByUsername(string username);

    }
}
