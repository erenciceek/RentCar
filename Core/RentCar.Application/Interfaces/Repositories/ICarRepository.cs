using RentCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentCar.Application.Interfaces.Repositories
{
    public interface ICarRepository
    {
        Task<List<Car>> GetAllCarsAsync();
        Task<Car> GetByIdCarAsync(int id);
        Task CreateCarAsync(Car entity);
        Task UpdateCarAsync(Car entity);
        Task DeleteCarAsync(Car entity);
    }
} 