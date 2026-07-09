using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeiroProjeto.Data.DTO.V1;
using PrimeiroProjeto.Model;
using PrimeiroProjeto.Services;

namespace PrimeiroProjeto.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]/v1")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonServices _personServices;
        private readonly ILogger<PersonController>
            _logger;
        public PersonController
            (IPersonServices personService,
            ILogger<PersonController> logger)
        {
            _logger= logger;
            _personServices = personService;
        }
        [HttpGet]
        [ProducesResponseType(200,
            Type= typeof(List<PersonDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get()
        {
            _logger.LogInformation("Fetching all " +
                "persons");
            return Ok(_personServices.FindAll());
        }
        [ProducesResponseType(200,
            Type =typeof(PersonDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [HttpGet("{id:long}")]
        public IActionResult Get(long id)
        {
            _logger.LogInformation
                ($"Fetching person" +
                $" with ID {id}");
            var person = _personServices
                .FindById(id);
            if (person== null)
            {
                _logger.LogWarning($"Person with " +
                    $"ID {id} not found");
                return NotFound();
            }
            return Ok(person);
        }
        [ProducesResponseType(200,
            Type = typeof(PersonDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [HttpPost]
        public IActionResult Post([FromBody] 
        PersonDTO person)
        {
            _logger.LogInformation($"Creating new" +
                $" person : {person.FirstName}");
            var personCreate = _personServices
                .Create(person);
            if (person == null)
            {
                _logger.LogError($"Failed to" +
                    $" create person with name " +
                    $"{person.FirstName}");
                return NotFound();
            }
            return Ok(personCreate);
        }
        [ProducesResponseType(200,
            Type = typeof(PersonDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [HttpPut]
        public IActionResult Put([FromBody] PersonDTO 
            person)
        {
            _logger.LogInformation($"Updating " +
                $"person with ID {person.Id}");
            var personChange = _personServices
                .Update(person);
            if(personChange == null)
            {
                _logger.LogError($"Failed to " +
                    $"Update person with ID {person.Id}");
                return NotFound();
            }
            _logger.LogDebug($"Person updated " +
                $"successfully : " +
                $"{personChange.FirstName}");
            return Ok(personChange);
        }
        [ProducesResponseType(204,
            Type = typeof(PersonDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [HttpDelete("{id:int}")]
        public IActionResult Delete( int id)
        {
            _logger.LogInformation($"Deleting " +
                $"person with ID {id}");
            _personServices.Delete(id);
            _logger.LogDebug($"Person with ID " +
                $"{id} deleted successfully");
            return NoContent();
        }
    }
}
