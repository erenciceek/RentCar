using RentCar.Application.Dtos.AuthDtos;
using RentCar.Application.Dtos.RentedCarDtos;
using RentCar.Application.Dtos.UserDtos;
using RentCar.Application.Interfaces.Services;
using RentCar.Application.Interfaces.Repositories;
using RentCar.Application.Validators;
using RentCar.Domain.Constants;
using RentCar.Domain.Entities;
using RentCar.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RentCar.Application.Services.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _repository;
        private readonly IRentedCarRepository _rentedCarRepository;
        private readonly ICarRepository _carRepository;
        
        public UserServices(IUserRepository repository, IRentedCarRepository rentedCarRepository, ICarRepository carRepository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _rentedCarRepository = rentedCarRepository ?? throw new ArgumentNullException(nameof(rentedCarRepository));
            _carRepository = carRepository ?? throw new ArgumentNullException(nameof(carRepository));
        }

        private void ValidateRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentException("Role cannot be empty", nameof(role));

            if (role != RoleConstants.Admin && role != RoleConstants.User && role != RoleConstants.Manager)
                throw new ArgumentException("Invalid role", nameof(role));
        }

        private void ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty", nameof(email));

            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(email))
                throw new ArgumentException("Invalid email format", nameof(email));
        }

        private void ValidatePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Phone cannot be empty", nameof(phone));

            var phoneRegex = new Regex(@"^\+?[1-9][0-9]{7,14}$");
            if (!phoneRegex.IsMatch(phone))
                throw new ArgumentException("Invalid phone format", nameof(phone));
        }

        public async Task CreateUser(CreateUserDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            ValidateRole(dto.Role);
            ValidateEmail(dto.Email);
            ValidatePhone(dto.Phone);
            
            var value = new User
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                Password = dto.Password,
                Phone = dto.Phone,
                Role = dto.Role
            };

            await _repository.CreateUserAsync(value);
        }

        public async Task DeleteUser(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID value", nameof(id));

            var value = await _repository.GetByIdUserAsync(id);
            if (value == null)
                throw new KeyNotFoundException($"User with ID: {id} not found");

            await _repository.DeleteUserAsync(value);
        }

        public async Task<List<ResultUserDto>> GetAllUsers()
        {
            var value = await _repository.GetAllUsersAsync();
            if (value == null || !value.Any())
                return new List<ResultUserDto>();

            return value.Select(x => new ResultUserDto
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
                Email = x.Email,
                Phone = x.Phone,
                Role = x.Role
            }).ToList();
        }

        public async Task<GetByIdUserDto> GetByIdUser(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID value", nameof(id));

            var value = await _repository.GetByIdUserAsync(id);
            if (value == null)
                throw new KeyNotFoundException($"User with ID: {id} not found");
            
            return new GetByIdUserDto
            {
                Id = value.Id,
                Name = value.Name,
                Surname = value.Surname,
                Email = value.Email,
                Phone = value.Phone,
                Role = value.Role
            };
        }

        public async Task UpdateUser(UpdateUserDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            ValidateRole(dto.Role);
            ValidateEmail(dto.Email);
            ValidatePhone(dto.Phone);

            var value = await _repository.GetByIdUserAsync(dto.Id);
            if (value == null)
                throw new KeyNotFoundException($"User with ID: {dto.Id} not found");

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                PasswordValidator.ValidatePassword(dto.Password);
                value.Password = PasswordValidator.HashPassword(dto.Password);
            }

            value.Name = dto.Name;
            value.Surname = dto.Surname;
            value.Email = dto.Email;
            value.Phone = dto.Phone;
            value.Role = dto.Role;

            await _repository.UpdateUserAsync(value);
        }

        public async Task<OnlyInfoUserDto> GetByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty", nameof(email));

            var value = await _repository.GetByEmailAsync(email);
            if (value == null)
                throw new KeyNotFoundException($"User with email: {email} not found");

            return new OnlyInfoUserDto
            {
                Id = value.Id,
                Name = value.Name,
                Surname = value.Surname,
                Email = value.Email,
                Phone = value.Phone,
                Role = value.Role
            };
        }
    }
}