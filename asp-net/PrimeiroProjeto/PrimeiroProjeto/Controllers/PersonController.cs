using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
