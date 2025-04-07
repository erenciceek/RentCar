using RentCar.Application.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentCar.Application.Interfaces.Services
{
    public interface IUserServices
    {
        Task<List<ResultUserDto>> GetAllUsers();
        Task<GetByIdUserDto> GetByIdUser(int id);
        Task<OnlyInfoUserDto> GetByEmail(string email);
        Task CreateUser(CreateUserDto dto);
        Task UpdateUser(UpdateUserDto dto);
        Task DeleteUser(int id);
    }
} 