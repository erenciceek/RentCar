using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Dtos.AuthDtos;
using RentCar.Application.Interfaces.Services;
using RentCar.Application.Services.AuthServices;

namespace RentCar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;

        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices ?? throw new ArgumentNullException(nameof(authServices));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (model == null)
                return BadRequest("Login credentials cannot be empty");

            var token = await _authServices.LoginAsync(model);
            if (token != null)
            {
                return Ok(new { jwtToken = token });
            }
            return Unauthorized("Invalid username or password");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (model == null)
                return BadRequest("Registration data cannot be empty");

            try
            {
                var result = await _authServices.RegisterAsync(model);
                if (result == null)
                {
                    return BadRequest("Registration failed. User might already exist.");
                }

                return Ok(new { jwtToken = result });
            }
            catch (Exception ex)
            {
                return BadRequest($"Registration failed: {ex.Message}");
            }
        }
    }
}
