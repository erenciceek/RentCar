using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Dtos.CarDtos;
using RentCar.Application.Interfaces.Services;
using RentCar.Application.Services.CarServices;
using RentCar.Domain.Constants;

namespace RentCar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarsController : ControllerBase
    {
        private readonly ICarServices _services;
        public CarsController(ICarServices services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Manager},{RoleConstants.User},{RoleConstants.Guest}")]
        public async Task<IActionResult> GetAllCars()
        {
            var result = await _services.GetAllCars();
            return result == null ? NotFound("No cars found") : Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Manager},{RoleConstants.User},{RoleConstants.Guest}")]
        public async Task<IActionResult> GetByIdCar(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID value");

            var result = await _services.GetByIdCar(id);
            return result == null ? NotFound($"Car with ID: {id} not found") : Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Manager}")]
        public async Task<IActionResult> CreateCar([FromBody] CreateCarDto dto)
        {
            if (dto == null)
                return BadRequest("Car information cannot be empty");

            await _services.CreateCar(dto);
            return Ok("Car created successfully");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Manager}")]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] UpdateCarDto dto)
        {
            if (dto == null)
                return BadRequest("Car information to update cannot be empty");

            if (id <= 0)
                return BadRequest("Invalid ID value");

            dto.Id = id; // Route'dan gelen ID'yi DTO'ya atıyoruz
            await _services.UpdateCar(dto);
            return Ok("Car updated successfully");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> DeleteCar(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID value");

            await _services.DeleteCar(id);
            return Ok("Car deleted successfully");
        }
    }
}
