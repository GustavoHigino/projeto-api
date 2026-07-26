using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeiroProjeto.Data.DTO.V1;
using PrimeiroProjeto.Files.Exporters.Factory;
using PrimeiroProjeto.Hypermedia.Utils;
using PrimeiroProjeto.Model;
using PrimeiroProjeto.Services;

namespace PrimeiroProjeto.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]/v1")]
    [Authorize("Bearer")]
    //[EnableCors("LocalPolicy")]
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
        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType(200,
            Type= typeof(PagedSearchDTO<PersonDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(
            [FromQuery] string? name,
            string sortDirection,
            int pageSize,
            int page)
        {
            _logger.LogInformation($"" +
                $"Fetching person with page search :" +
                $"{name},{sortDirection},{pageSize},{page}");
            return Ok(_personServices
                .FindWithPagedSearch(name,sortDirection,
                pageSize,page));
        }
        [HttpGet("find-by-name")]
        [ProducesResponseType(200,
            Type= typeof(List<PersonDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetByName(
            [FromQuery]string? firstName,
            [FromQuery]string? lastName)
        {
            _logger.LogInformation($"Fetching " +
                $"persons by name {firstName} {lastName}");
            return Ok(_personServices.
                 FindByName(firstName,lastName));
        }
        [ProducesResponseType(200,
            Type =typeof(PersonDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [HttpGet("{id:long}")]
        //[EnableCors("LocalPolicy")]
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
        //[EnableCors("MultipleOriginPolicy")]
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
        [HttpPatch("{id}")]
        [ProducesResponseType(200,Type =
            typeof(PersonDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Disable(long id)
        {
            _logger.LogInformation("Disabling " +
                "person with id {id}", id);
            var disabledPerson =
                _personServices.Disable(id);
            if (disabledPerson == null)
            {
                _logger.LogError("Failed to disa" +
                    "ble person with ID {id}", id);
                return NotFound();
            }
            _logger.LogDebug($"Person with ID" +
                $" {id} disable successfully");
            return Ok(disabledPerson);
        }
        [HttpPost("massCreation")]
        
        [ProducesResponseType(200,
            Type =typeof(List<PersonDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> MassCreation
            ([FromForm] FileUploadDTO input)
        {
            if (input.File == null || input.File.Length == 0) 
            {
                _logger.LogWarning("No file uploaded" +
                    "for mass creation");
                return BadRequest("File is Required!");
            }
            _logger.LogInformation($"Starting mass" +
                $"creation from uploaded file {input.File.FileName}");
            var persons = await _personServices
                .MassCreationAsync(input.File);
            if (persons == null)
            {
                _logger.LogError($"Mass Creation" +
                    $"failed for file {input.File.FileName}");
                return NoContent();
            }
            _logger.LogInformation($"{persons.Count}" +
                $" persons created" +
                $"successfully");
            return Ok(persons);


        }
        [HttpGet
            ("exportPage/{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType(200,
            Type = typeof(byte[]))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(415)]
        [Produces(
            MediaTypes.ApplicationXlsx,
            MediaTypes.ApplicationCsv)]
        public IActionResult ExportPage(
            string sortDirection,
            int pageSize,
            int page,
            [FromQuery] string name="")
        {
            var acceptHeader = Request.Headers
                ["Accept"].ToString();
            if (string.IsNullOrWhiteSpace(acceptHeader))
            {
                return BadRequest("Accept header is required");
            }
            _logger.LogInformation($"exporting persons " +
                $"with search {name}, {sortDirection}," +
                $" {page}, {acceptHeader}");
            try
            {
                var fileResult = 
                _personServices.ExportPage(page, pageSize,
                sortDirection, acceptHeader, name);
                return fileResult;
            }
            catch (NotSupportedException ex)
            {

                _logger.LogWarning(ex, 
                    $"Unsupported export format " +
                    $"requested : {acceptHeader}");
                return StatusCode(StatusCodes
                    .Status415UnsupportedMediaType, 
                    ex.Message);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,
                    "Unexpected error while exporting" +
                    "data");
                return StatusCode(StatusCodes
                    .Status500InternalServerError,
                    "Internal Server Error");
            }
        }
    }
}
