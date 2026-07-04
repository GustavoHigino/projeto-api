using PrimeiroProjeto.Model;
using PrimeiroProjeto.Model.Base;

namespace PrimeiroProjeto.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> FindAll();
        T FindById(long id);
        T Create(T entity);
        
        T Update(T entity);
        void Delete(long id);
        bool Exists(long id);
    }
}
