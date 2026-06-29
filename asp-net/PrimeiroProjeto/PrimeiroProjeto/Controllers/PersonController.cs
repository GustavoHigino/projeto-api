using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeiroProjeto.Model;
using PrimeiroProjeto.Services;

namespace PrimeiroProjeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private IPersonServices _personServices;
        public PersonController
            (IPersonServices personService)
        {
            _personServices = personService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personServices.FindAll());
        }
        [HttpGet("{id:long}")]
        public IActionResult Get(long id)
        {
            var person = _personServices
                .FindById(id);
            if (person== null)
            {
                return NotFound();
            }
            return Ok(person);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            var personCreate = _personServices
                .Create(person);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(personCreate);
        }
        [HttpPut]
        public IActionResult Put([FromBody] Person 
            person)
        {
            var personChange = _personServices
                .Update(person);
            if(personChange == null)
            {
                return NotFound();
            }
            return Ok(personChange);
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete( int id)
        {
            _personServices.Delete(id);
            return NoContent();
        }
    }
}
