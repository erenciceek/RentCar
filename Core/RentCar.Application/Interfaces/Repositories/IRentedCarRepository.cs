using RentCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentCar.Application.Interfaces.Repositories
{
    public interface IRentedCarRepository
    {
        Task<List<RentedCar>> GetAllRentedCarsAsync();
        Task<RentedCar> GetByIdRentedCarAsync(int id);
        Task CreateRentedCarAsync(RentedCar entity);
        Task UpdateRentedCarAsync(RentedCar entity);
        Task DeleteRentedCarAsync(RentedCar entity);
        Task<List<RentedCar>> GetRentedCarsByUserId(int userId);
    }
} 