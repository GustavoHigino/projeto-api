using Microsoft.AspNetCore.Mvc;
using PrimeiroProjeto.Data.DTO.V1;
using PrimeiroProjeto.Services;

namespace PrimeiroProjeto.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]/v1")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;
        private readonly ILogger<BookController> _logger;
        public BookController(IBookService service,
            ILogger<BookController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [ProducesResponseType(200,
            Type = typeof(BookDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogDebug("Todos os livros");
            return Ok(_service.FindAll());
        }
        [ProducesResponseType(200,
            Type = typeof(BookDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [HttpGet("{id}")]
        public IActionResult GetId(long id)
        {
            var book = _service.FindById(id);
            if (book == null)
            {
                _logger.LogInformation("Book not found");
                return NotFound();
            }
            _logger.LogDebug("Livro encontrado com sucesso");
            return Ok(book);
        }
        [ProducesResponseType(200,
            Type = typeof(BookDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [HttpPost]
        public IActionResult Post(BookDTO book)
        {
            if (book == null)
            {
                _logger.LogInformation("Falha ao criar o livro");
                return NotFound();
            }
            _logger.LogDebug("Sucesso ao criar o livro");
            var bookCreated = _service.Create(book);
            Response.Headers.Add
                ("X-API-Deprecated", "true");
            Response.Headers.Add
                ("X-API-Deprecation-Date",
                "2026-12-31");
            return Ok(bookCreated);
        }
        [ProducesResponseType(200,
            Type = typeof(BookDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [HttpPut]
        public IActionResult Put(BookDTO book)
        {
            var bookPut = _service.Update(book);
            if (bookPut == null)
            {
                _logger.LogInformation("Falha ao encontrar o ID");
                return NotFound();
            }
            _logger.LogDebug("Sucesso ao mudar informações do livro");
            return Ok(bookPut);
        }
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var book=_service.FindById(id);
            if (book == null)
            {
                _logger.LogInformation("Erro ao localizar o livro para deletar");
                return NotFound();
            }
            _logger.LogDebug("Livro Deletado");
            _service.Delete(book.Id);
            return NoContent();
        }

    }
}
