using Microsoft.AspNetCore.Mvc;
using PrimeiroProjeto.Data.DTO.V2;
using PrimeiroProjeto.Services.Impl;

namespace PrimeiroProjeto.Controllers.v2
{
    [ApiController]
    [Route("api/[controller]/v2")]
    public class PersonController : ControllerBase
    {
        private PersonServicesImplV2 _personServices;
        private readonly ILogger<PersonController>
            _logger;
        public PersonController
            (PersonServicesImplV2 personService,
            ILogger<PersonController> logger)
        {
            _logger= logger;
            _personServices = personService;
        }
        
       
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
            //personCreate.LastName = null;
            //personCreate.Age = 0;
            personCreate.Age = 20;
            return Ok(personCreate);
        }
        
    }
}
