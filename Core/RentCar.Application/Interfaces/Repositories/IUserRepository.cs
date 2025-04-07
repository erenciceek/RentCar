using RentCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentCar.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetByIdUserAsync(int id);
        Task<User> GetByEmailAsync(string email);
        Task CreateUserAsync(User entity);
        Task UpdateUserAsync(User entity);
        Task DeleteUserAsync(User entity);
        Task<User> CheckUser(string email, string password);
    }
} 