using RentCar.Application.Dtos.RentedCarDtos;
using RentCar.Application.Interfaces.Services;
using RentCar.Application.Interfaces.Repositories;
using RentCar.Application.Dtos.UserDtos;
using RentCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Services.RentedCarServices
{
    public class RentedCarServices : IRentedCarServices
    {
        private readonly IRentedCarRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly ICarRepository _carRepository;

        public RentedCarServices(IRentedCarRepository repository, IUserRepository userRepository, ICarRepository carRepository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _carRepository = carRepository ?? throw new ArgumentNullException(nameof(carRepository));
        }

        public async Task CreateRentedCar(CreateRentedCarDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            ValidateRentedCarDto(dto);

            // Kullanıcı ve araç varlığını kontrol et
            var user = await _userRepository.GetByIdUserAsync(dto.UserId);
            if (user == null)
                throw new KeyNotFoundException($"User with ID: {dto.UserId} not found");

            var car = await _carRepository.GetByIdCarAsync(dto.CarId);
            if (car == null)
                throw new KeyNotFoundException($"Car with ID: {dto.CarId} not found");

            var value = new RentedCar
            {
                UserId = dto.UserId,
                CarId = dto.CarId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                TotalPrice = dto.TotalPrice,
                DamagePrice = dto.DamagePrice,
                IsCompleted = dto.IsCompleted,
            };
            await _repository.CreateRentedCarAsync(value);
        }

        public async Task DeleteRentedCar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID value", nameof(id));

            var value = await _repository.GetByIdRentedCarAsync(id);
            if (value == null)
                throw new KeyNotFoundException($"Rented car with ID: {id} not found");

            await _repository.DeleteRentedCarAsync(value);
        }

        public async Task<List<ResultRentedCarDto>> GetAllRentedCars()
        {
            var value = await _repository.GetAllRentedCarsAsync();
            if (value == null || !value.Any())
                return new List<ResultRentedCarDto>();

            var result = new List<ResultRentedCarDto>();

            foreach (var rentedCar in value)
            {
                var user = await _userRepository.GetByIdUserAsync(rentedCar.UserId);
                if (user == null)
                    continue; // Kullanıcı bulunamazsa bu kaydı atla

                var car = await _carRepository.GetByIdCarAsync(rentedCar.CarId);
                if (car == null)
                    continue; // Araç bulunamazsa bu kaydı atla

                var newrentedcar = new ResultRentedCarDto
                {
                    Id = rentedCar.Id,
                    UserId = rentedCar.UserId,
                    CarId = rentedCar.CarId,
                    StartDate = rentedCar.StartDate,
                    EndDate = rentedCar.EndDate,
                    TotalPrice = rentedCar.TotalPrice,
                    DamagePrice = rentedCar.DamagePrice,
                    IsCompleted = rentedCar.IsCompleted,
                };

                newrentedcar.User = new OnlyInfoUserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Phone = user.Phone,
                    Role = user.Role
                };
                newrentedcar.Car = car;
                result.Add(newrentedcar);
            }

            return result;
        }

        public async Task<GetByIdRentedCarDto> GetByIdRentedCar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID value", nameof(id));

            var value = await _repository.GetByIdRentedCarAsync(id);
            if (value == null)
                throw new KeyNotFoundException($"Rented car with ID: {id} not found");

            var user = await _userRepository.GetByIdUserAsync(value.UserId);
            if (user == null)
                throw new KeyNotFoundException($"User with ID: {value.UserId} not found");

            var car = await _carRepository.GetByIdCarAsync(value.CarId);
            if (car == null)
                throw new KeyNotFoundException($"Car with ID: {value.CarId} not found");

            var result = new GetByIdRentedCarDto
            {
                Id = value.Id,
                UserId = value.UserId,
                CarId = value.CarId,
                StartDate = value.StartDate,
                EndDate = value.EndDate,
                TotalPrice = value.TotalPrice,
                DamagePrice = value.DamagePrice,
                IsCompleted = value.IsCompleted,
            };

            result.User = new OnlyInfoUserDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Phone = user.Phone,
                Role = user.Role
            };
            result.Car = car;
            return result;
        }

        public async Task UpdateRentedCar(UpdateRentedCarDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.Id <= 0)
                throw new ArgumentException("Invalid ID value", nameof(dto.Id));

            ValidateRentedCarDto(dto);

            var value = await _repository.GetByIdRentedCarAsync(dto.Id);
            if (value == null)
                throw new KeyNotFoundException($"Rented car with ID: {dto.Id} not found");

            // Check user and car existence
            var user = await _userRepository.GetByIdUserAsync(dto.UserId);
            if (user == null)
                throw new KeyNotFoundException($"User with ID: {dto.UserId} not found");

            var car = await _carRepository.GetByIdCarAsync(dto.CarId);
            if (car == null)
                throw new KeyNotFoundException($"Car with ID: {dto.CarId} not found");

            value.UserId = dto.UserId;
            value.CarId = dto.CarId;
            value.StartDate = dto.StartDate;
            value.EndDate = dto.EndDate;
            value.TotalPrice = dto.TotalPrice;
            value.DamagePrice = dto.DamagePrice;
            value.IsCompleted = dto.IsCompleted;

            await _repository.UpdateRentedCarAsync(value);
        }

        private void ValidateRentedCarDto(CreateRentedCarDto dto)
        {
            if (dto.UserId <= 0)
                throw new ArgumentException("Invalid user ID value", nameof(dto.UserId));

            if (dto.CarId <= 0)
                throw new ArgumentException("Invalid car ID value", nameof(dto.CarId));

            if (dto.StartDate == default)
                throw new ArgumentException("Start date cannot be empty", nameof(dto.StartDate));

            if (dto.EndDate == default)
                throw new ArgumentException("End date cannot be empty", nameof(dto.EndDate));

            if (dto.EndDate <= dto.StartDate)
                throw new ArgumentException("End date must be after start date");

            if (dto.TotalPrice < 0)
                throw new ArgumentException("Total price cannot be negative", nameof(dto.TotalPrice));

            if (dto.DamagePrice < 0)
                throw new ArgumentException("Damage price cannot be negative", nameof(dto.DamagePrice));
        }

        private void ValidateRentedCarDto(UpdateRentedCarDto dto)
        {
            if (dto.UserId <= 0)
                throw new ArgumentException("Invalid user ID value", nameof(dto.UserId));

            if (dto.CarId <= 0)
                throw new ArgumentException("Invalid car ID value", nameof(dto.CarId));

            if (dto.StartDate == default)
                throw new ArgumentException("Start date cannot be empty", nameof(dto.StartDate));

            if (dto.EndDate == default)
                throw new ArgumentException("End date cannot be empty", nameof(dto.EndDate));

            if (dto.EndDate <= dto.StartDate)
                throw new ArgumentException("End date must be after start date");

            if (dto.TotalPrice < 0)
                throw new ArgumentException("Total price cannot be negative", nameof(dto.TotalPrice));

            if (dto.DamagePrice < 0)
                throw new ArgumentException("Damage price cannot be negative", nameof(dto.DamagePrice));
        }
    }
}
