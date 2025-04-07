using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Dtos.RentedCarDtos;
using RentCar.Application.Interfaces.Services;
using RentCar.Application.Services.RentedCarServices;
using RentCar.Domain.Constants;

namespace RentCar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RentedCarsController : ControllerBase
    {
        private readonly IRentedCarServices _services;

        public RentedCarsController(IRentedCarServices services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Manager}")]
        public async Task<IActionResult> GetAllRentedCars()
        {
            var result = await _services.GetAllRentedCars();
            return result == null ? NotFound("No rented cars found") : Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Manager},{RoleConstants.User}")]
        public async Task<IActionResult> GetByIdRentedCar(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID value");

            var result = await _services.GetByIdRentedCar(id);
            return result == null ? NotFound($"Rented car with ID: {id} not found") : Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Manager},{RoleConstants.User}")]
        public async Task<IActionResult> CreateRentedCar([FromBody] CreateRentedCarDto dto)
        {
            if (dto == null)
                return BadRequest("Rented car information cannot be empty");

            await _services.CreateRentedCar(dto);
            return Ok("Rented car created successfully");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Manager}")]
        public async Task<IActionResult> UpdateRentedCar(int id, [FromBody] UpdateRentedCarDto dto)
        {
            if (dto == null)
                return BadRequest("Rented car information to update cannot be empty");

            if (id <= 0)
                return BadRequest("Invalid ID value");

            dto.Id = id; // Route'dan gelen ID'yi DTO'ya atıyoruz
            await _services.UpdateRentedCar(dto);
            return Ok("Rented car updated successfully");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> DeleteRentedCar(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID value");

            await _services.DeleteRentedCar(id);
            return Ok("Rented car deleted successfully");
        }
    }
}