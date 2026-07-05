using Mapster;
using PrimeiroProjeto.Controllers;
using PrimeiroProjeto.Data.DTO;
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
        public IEnumerable<BookDTO> FindAll()
        {
            return _repository.FindAll()
                .Adapt<IEnumerable<BookDTO>>();
        }

        public BookDTO FindById(long id)
        {
            return _repository.FindById(id)
                .Adapt<BookDTO>();
            
        }
        public BookDTO Create(BookDTO bookDTO)
        {
            var book = bookDTO.Adapt<Book>();
            book= _repository
                .Create(book);
            return book.Adapt<BookDTO>();
        }
        
        public BookDTO Update(BookDTO book)
        {
            var entity = book.Adapt<Book>();
            entity=_repository.Update(entity);
            return entity.Adapt<BookDTO>();
        }
        public void Delete(long Id)
        {
            _repository.Delete(Id);
        }

        
    }
}
