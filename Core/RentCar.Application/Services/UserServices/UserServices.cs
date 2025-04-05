﻿using RentCar.Application.Dtos.AuthDtos;
using RentCar.Application.Dtos.RentedCarDtos;
using RentCar.Application.Dtos.UserDtos;
using RentCar.Domain.Entities;
using RentCar.Persistence.Repositories.CarRepositories;
using RentCar.Persistence.Repositories.RentedCarRepositories;
using RentCar.Persistence.Repositories.UserRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Services.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IRentedCarRepository _rentedCarRepository;
        private readonly ICarRepository _carRepository;
        
        public UserServices(IUserRepository userRepository, IRentedCarRepository rentedCarRepository, ICarRepository carRepository)
        {
            _userRepository = userRepository;
            _rentedCarRepository = rentedCarRepository;
            _carRepository = carRepository;
        }


        public async Task CreateUser(CreateUserDto dto)
        {
            var value = new User
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                Phone = dto.Phone,
                Password = dto.Password,
                Role = dto.Role,
            };
            await _userRepository.CreateUserAsync(value);
        }

        public async Task DeleteUser(int id)
        {
            var value = await _userRepository.GetByIdUserAsync(id);
            await _userRepository.DeleteUserAsync(value);
        }

        public async Task<List<ResultUserDto>> GetAllUsers()
        {
            var value = await _userRepository.GetAllUsersAsync();
            var result = new List<ResultUserDto>();

            foreach (var user in value)
            {
                var newuser = new ResultUserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Phone = user.Phone,
                    Password = user.Password,
                    Role = user.Role,
                };

                var userrents = await _rentedCarRepository.GetRentedCarsByUserId(user.Id);
                var onlyinforent = new List<OnlyInfoRentedCarDto>();
                foreach (var userrent in userrents)
                {

                    var newonlyinforent = new OnlyInfoRentedCarDto
                    {
                        Id = userrent.Id,
                        CarId = userrent.CarId,
                        StartDate = userrent.StartDate,
                        EndDate = userrent.EndDate,
                        DamagePrice = userrent.DamagePrice,
                        TotalPrice = userrent.TotalPrice,
                        IsCompleted = userrent.IsCompleted,
                    };
                    newonlyinforent.Car = await _carRepository.GetByIdCarAsync(userrent.CarId);

                    onlyinforent.Add(newonlyinforent);
                }
                newuser.RentedCars = onlyinforent;
                result.Add(newuser);

            }
            return result;
        }
        

        public async Task<GetByIdUserDto> GetByIdUser(int id)
        {
            var value = await _userRepository.GetByIdUserAsync(id);
            
            var result = new GetByIdUserDto();
            result.Id = value.Id;
            result.Name = value.Name;
            result.Surname = value.Surname;
            result.Email = value.Email;
            result.Phone = value.Phone;
            result.Password = value.Password;
            result.Role = value.Role;


            var userrents = await _rentedCarRepository.GetRentedCarsByUserId(value.Id);
            var onlyinforent = new List<OnlyInfoRentedCarDto>();
            foreach (var userrent in userrents)
            {

                var newonlyinforent = new OnlyInfoRentedCarDto
                {
                    Id = userrent.Id,
                    CarId = userrent.CarId,
                    StartDate = userrent.StartDate,
                    EndDate = userrent.EndDate,
                    DamagePrice = userrent.DamagePrice,
                    TotalPrice = userrent.TotalPrice,
                    IsCompleted = userrent.IsCompleted,
                };
                newonlyinforent.Car = await _carRepository.GetByIdCarAsync(userrent.CarId);

                onlyinforent.Add(newonlyinforent);
            }
            result.RentedCars = onlyinforent;
           

            return result;
        }

        public async Task UpdateUser(UpdateUserDto dto)
        {
            var value = await _userRepository.GetByIdUserAsync(dto.Id);
            value.Name = dto.Name;
            value.Surname = dto.Surname;
            value.Email = dto.Email;
            value.Phone = dto.Phone;
            value.Password = dto.Password;
            value.Role = dto.Role;
            await _userRepository.UpdateUserAsync(value);
        }

        public async Task<OnlyInfoUserDto> CheckUser(LoginDto dto)
        {
            var user = await _userRepository.CheckUser(dto.Email, dto.Password);
            if (user != null)
            {
                var result = new OnlyInfoUserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Password = user.Password,
                    Phone = user.Phone,
                    Role = user.Role,
                };
                return result;
            }
            return null;
        }
    }
}