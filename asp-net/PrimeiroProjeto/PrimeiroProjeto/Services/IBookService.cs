using PrimeiroProjeto.Model;
using PrimeiroProjeto.Repositories;

namespace PrimeiroProjeto.Services
{
    public interface IBookService 
    {
        Book Create(Book book);
        Book FindById(long id);
        IEnumerable<Book> FindAll();
        Book Update(Book book);
        void Delete(long id);
    }
}
