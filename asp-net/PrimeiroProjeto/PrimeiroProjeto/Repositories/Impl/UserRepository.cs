using PrimeiroProjeto.Model;
using PrimeiroProjeto.Model.Context;

namespace PrimeiroProjeto.Repositories.Impl
{
    public class UserRepository : GenericRepository<User>,
        IUserRepository
    {
        public UserRepository(MSSQLContext context) : base(context)
        {}

        public User? FindByUsername(string username)
        {
            return _context.Users.SingleOrDefault
                (p => p.Username == username);
        }
    }
}
