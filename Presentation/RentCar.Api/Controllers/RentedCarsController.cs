﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Dtos.RentedCarDtos;
using RentCar.Application.Services.RentedCarServices;

namespace RentCar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentedCarsController : ControllerBase
    {
        private readonly IRentedCarServices _services;

        public RentedCarsController(IRentedCarServices services)
        {
            _services = services;
        }
        [HttpGet("getAllRentedCars")]
        public async Task<IActionResult> GetAllRentedCars()
        {
            var result = await _services.GetAllRentedCars();
            return Ok(result);
        }
        [HttpGet("getByIdRentedCar")]
        public async Task<IActionResult> GetByIdRentedCar(int id)
        {
            var result = await _services.GetByIdRentedCar(id);
            return Ok(result);
        }
        [Authorize(Roles = "admin,user")]
        [HttpPost("createRentedCar")]
        public async Task<IActionResult> CreateRentedCar(CreateRentedCarDto dto)
        {
            await _services.CreateRentedCar(dto);
            return Ok("Rented car is created.");
        }
        [HttpPut("updateRentedCar")]
        public async Task<IActionResult> UpdateRentedCar(UpdateRentedCarDto dto)
        {
            await _services.UpdateRentedCar(dto);
            return Ok("The rented car is updated.");
        }
        [HttpDelete("deleteRentedCar")]
        public async Task<IActionResult> DeleteRentedCar(int id)
        {
            await _services.DeleteRentedCar(id);
            return Ok("The rented car is deleted.");
        }
    }
}