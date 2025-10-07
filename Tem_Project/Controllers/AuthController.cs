using Microsoft.AspNetCore.Mvc;
using Project.Application.AppUser.Dtos;
using Project.Application.Auth.Interface;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthServices _authServices) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] CreateUserDto model)
        {
            var result = await _authServices.CreateUserAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }









    }
}
