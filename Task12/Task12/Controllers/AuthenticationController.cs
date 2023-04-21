using Microsoft.AspNetCore.Mvc;
using Services.Services.Authentication;
using Task12.API.Authentication;
using Task12.Models;

namespace Task12.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost()]
        public async Task<IActionResult> RegistrateUser(UserRequest request)
        {
            var user = new UserAccount() { Id =Guid.NewGuid(), UserName = request.UserName, Password = request.Password, Role = request.Role };
            await _authenticationService.RegistrateUser(user);
            var response = new UserResponse(user.Id, user.UserName, user.Role);

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, response);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _authenticationService.GetUser(id);
            var response = new UserResponse(user.Id, user.UserName, user.Role);
            return Ok(response);
        }
        [HttpGet("{name}")]
        public async Task<IActionResult> GetUser(string name)
        {
            var user = await _authenticationService.GetUser(name);
            var response = new UserResponse(user.Id, user.UserName, user.Role);
            return Ok(response);
        }
        [HttpPost("/Validation")]
        public async Task<IActionResult> ValidateUser(ValidateRequest request)
        {
            UserAccount user = await _authenticationService.GetUser(request.UserName);
            if (user != null)
            {
                if (user.Password == request.Password)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, UserRequest request)
        {
            await _authenticationService.UpdateUser(id, new UserAccount() { Id= id, UserName= request.UserName, Password= request.Password, Role= request.Role});
            return Ok();
        }
    }
}
