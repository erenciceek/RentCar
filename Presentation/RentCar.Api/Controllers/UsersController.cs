using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using RentCar.Application.Dtos.UserDtos;
using RentCar.Application.Interfaces.Services;
using RentCar.Application.Services.UserServices;
using RentCar.Domain.Constants;

namespace RentCar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UsersController(IUserServices userServices)
        {
            _userServices = userServices ?? throw new ArgumentNullException(nameof(userServices));
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Manager}")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userServices.GetAllUsers();
            return result == null ? NotFound("No users found") : Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Manager}")]
        public async Task<IActionResult> GetByIdUser(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID value");

            var result = await _userServices.GetByIdUser(id);
            return result == null ? NotFound($"User with ID: {id} not found") : Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            if (dto == null)
                return BadRequest("User information cannot be empty");

            await _userServices.CreateUser(dto);
            return Ok("User created successfully");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Manager}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto dto)
        {
            if (dto == null)
                return BadRequest("User information to update cannot be empty");

            if (id <= 0)
                return BadRequest("Invalid ID value");

            dto.Id = id; 
            await _userServices.UpdateUser(dto);
            return Ok("User updated successfully");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID value");

            await _userServices.DeleteUser(id);
            return Ok("User deleted successfully");
        }
    }
}
