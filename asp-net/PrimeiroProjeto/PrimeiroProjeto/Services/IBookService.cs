using PrimeiroProjeto.Data.DTO;

namespace PrimeiroProjeto.Services
{
    public interface IBookService 
    {
        BookDTO Create(BookDTO book);
        BookDTO FindById(long id);
        IEnumerable<BookDTO> FindAll();
        BookDTO Update(BookDTO book);
        void Delete(long id);
    }
}
