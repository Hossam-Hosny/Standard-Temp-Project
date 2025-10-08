using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.AppUser.Dtos;
using Project.Application.Auth.Interface;
using Project.Domain.Constants;

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


        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto model)
        {
            var result = await _authServices.GetTokenAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

       // [Authorize(UserRoles.Admin)]
        [HttpPost("add-role")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleDto model)
        {
            

            var result = await _authServices.AddRoleAsync(model);
            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);


        }







    }
}
