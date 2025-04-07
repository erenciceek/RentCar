using RentCar.Application.Dtos.CarDtos;
using RentCar.Application.Interfaces.Services;
using RentCar.Application.Interfaces.Repositories;
using RentCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Services.CarServices
{
    public class CarServices : ICarServices
    {
        private readonly ICarRepository _repository;

        public CarServices(ICarRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task CreateCar(CreateCarDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            ValidateCarDto(dto);

            var value = new Car
            {
                ImageUrl = dto.ImageUrl,
                Brand = dto.Brand,
                Model = dto.Model,
                Year = dto.Year,
                KM = dto.KM,
                Type = dto.Type,
                Fuel = dto.Fuel,
                DailyPrice = dto.DailyPrice,
                IsAvailable = dto.IsAvailable,
            };
            await _repository.CreateCarAsync(value);
        }

        public async Task DeleteCar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID value", nameof(id));

            var value = await _repository.GetByIdCarAsync(id);
            if (value == null)
                throw new KeyNotFoundException($"Car with ID: {id} not found");

            await _repository.DeleteCarAsync(value);
        }

        public async Task<List<ResultCarDto>> GetAllCars()
        {
            var value = await _repository.GetAllCarsAsync();
            if (value == null || !value.Any())
                return new List<ResultCarDto>();

            var result = value.Select(x => new ResultCarDto
            {
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Brand = x.Brand,
                Model = x.Model,
                Year = x.Year,
                KM = x.KM,
                Type = x.Type,
                Fuel = x.Fuel,
                DailyPrice = x.DailyPrice,
                IsAvailable = x.IsAvailable,
            }).ToList();
            return result;
        }

        public async Task<GetByIdCarDto> GetByIdCar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID value", nameof(id));

            var value = await _repository.GetByIdCarAsync(id);
            if (value == null)
                throw new KeyNotFoundException($"Car with ID: {id} not found");

            var result = new GetByIdCarDto
            {
                Id = value.Id,
                ImageUrl = value.ImageUrl,
                Brand = value.Brand,
                Model = value.Model,
                Year = value.Year,
                KM = value.KM,
                Type = value.Type,
                Fuel = value.Fuel,
                DailyPrice = value.DailyPrice,
                IsAvailable = value.IsAvailable,
            };
            return result;
        }

        public async Task UpdateCar(UpdateCarDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.Id <= 0)
                throw new ArgumentException("Invalid ID value", nameof(dto.Id));

            ValidateCarDto(dto);

            var value = await _repository.GetByIdCarAsync(dto.Id);
            if (value == null)
                throw new KeyNotFoundException($"Car with ID: {dto.Id} not found");

            value.ImageUrl = dto.ImageUrl;
            value.Brand = dto.Brand;
            value.Model = dto.Model;
            value.Year = dto.Year;
            value.KM = dto.KM;
            value.Type = dto.Type;
            value.Fuel = dto.Fuel;
            value.DailyPrice = dto.DailyPrice;
            value.IsAvailable = dto.IsAvailable;

            await _repository.UpdateCarAsync(value);
        }

        private void ValidateCarDto(CreateCarDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Brand))
                throw new ArgumentException("Brand cannot be empty", nameof(dto.Brand));

            if (string.IsNullOrWhiteSpace(dto.Model))
                throw new ArgumentException("Model cannot be empty", nameof(dto.Model));

            if (dto.Year <= 0)
                throw new ArgumentException("Invalid year value", nameof(dto.Year));

            if (dto.KM < 0)
                throw new ArgumentException("KM value cannot be negative", nameof(dto.KM));

            if (dto.DailyPrice <= 0)
                throw new ArgumentException("Daily price must be greater than 0", nameof(dto.DailyPrice));
        }

        private void ValidateCarDto(UpdateCarDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Brand))
                throw new ArgumentException("Brand cannot be empty", nameof(dto.Brand));

            if (string.IsNullOrWhiteSpace(dto.Model))
                throw new ArgumentException("Model cannot be empty", nameof(dto.Model));

            if (dto.Year <= 0)
                throw new ArgumentException("Invalid year value", nameof(dto.Year));

            if (dto.KM < 0)
                throw new ArgumentException("KM value cannot be negative", nameof(dto.KM));

            if (dto.DailyPrice <= 0)
                throw new ArgumentException("Daily price must be greater than 0", nameof(dto.DailyPrice));
        }
    }
}
