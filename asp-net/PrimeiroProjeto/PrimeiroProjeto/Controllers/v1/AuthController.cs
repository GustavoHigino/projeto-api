using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimeiroProjeto.Data.DTO.V1;
using PrimeiroProjeto.Services;

namespace PrimeiroProjeto.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class AuthController:ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IUserAuthService _userAuthService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(
            ILoginService service,
            ILogger<AuthController> logger,
            IUserAuthService userAuthService)
        {
            _loginService = service;
            _logger = logger;
            _userAuthService = userAuthService;
        }
        [HttpPost("signin")]
        [AllowAnonymous]
        public IActionResult SignIn([FromBody] UserDTO user)
        {
            _logger.LogInformation(
                $"Attempting to sign in user: " +
                $"{user.Username}");
            if(user==null||
            string.IsNullOrWhiteSpace(user.Username)||
            string.IsNullOrWhiteSpace(user.Password))
            {
                _logger.LogWarning("Sign in failed");
                return BadRequest("Sign in failed");
            }
            var token = _loginService
                .ValidateCredentials(user);
            if(token == null)
            {
                return Unauthorized();
            }
            _logger.LogInformation($"user {user.Username} " +
                $"signed in successfully");
            return Ok(token);
        }
        [HttpPost("refresh")]
        [AllowAnonymous]
        public IActionResult Refresh
            ([FromBody] TokenDTO tokenDto)
        {
            if(tokenDto == null)
            {
                return BadRequest
                    ("Invalid Client request!");
            }
            var token = _loginService
                .ValidateCredentials(tokenDto);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
        [HttpPost("Revoke")]
        [Authorize]
        public IActionResult Revoke()
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest(
                    "Invalid Client Request!");
            }
            var result = _loginService
                .RevokeToken(username);
            if (!result)
            {
                return BadRequest(
                    "Invalid Client Request!");
            }
            return NoContent();
        }
        [HttpPost("create")]
        [AllowAnonymous]
        public IActionResult Create(
            [FromBody]AccountCredentialsDTO user)
        {
            if (user == null)
            {
                return BadRequest
                    ("Invalid client request!");

            }
            var result = _loginService
                .Create(user);
            return Ok(result);

        }
    }
}
