using PrimeiroProjeto.Model;
using PrimeiroProjeto.Repositories;

namespace PrimeiroProjeto.Services.Impl
{
    public class BookServiceImpl : IBookService
    {
        public BookServiceImpl(IRepository<Book> repository)
        {
            _repository = repository;
        }
        private readonly IRepository<Book> _repository;
        public IEnumerable<Book> FindAll()
        {
            return _repository.FindAll();
        }

        public Book FindById(long id)
        {
            return _repository.FindById(id);
            
        }
        public Book Create(Book book)
        {
            return _repository.Create(book);
        }

        public Book Update(Book book)
        {
            return _repository.Update(book);
        }
        public void Delete(long Id)
        {
            _repository.Delete(Id);
        }

        
    }
}
