using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using RentCar.Application.Dtos.UserDtos;
using RentCar.Application.Services.UserServices;

namespace RentCar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userServices.GetAllUsers();
            return Ok(result);
        }

        [HttpGet("getByIdUser")]
        public async Task<IActionResult> GetByIdUser(int id)
        {
            var result = await _userServices.GetByIdUser(id);
            return Ok(result);
        }


        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser(CreateUserDto dto)
        {
            await _userServices.CreateUser(dto);
            return Ok("User is created");
        }

        [HttpPut("updateUser")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto dto)
        {
            await _userServices.UpdateUser(dto);
            return Ok("User is updated");
        }

        [HttpDelete("deleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userServices.DeleteUser(id);
            return Ok("User is deleted");
        }










    }
}
