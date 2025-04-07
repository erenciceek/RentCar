using Microsoft.EntityFrameworkCore;
using RentCar.Application.Interfaces.Repositories;
using RentCar.Domain.Entities;
using RentCar.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentCar.Persistence.Repositories.CarRepositories
{
    public class CarRepository : ICarRepository
    {
        private readonly RentCarDbContext _context;

        public CarRepository(RentCarDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task CreateCarAsync(Car entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _context.Cars.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCarAsync(Car entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Cars.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Car> GetByIdCarAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID value", nameof(id));

            return await _context.Cars.FindAsync(id);
        }

        public async Task UpdateCarAsync(Car entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Cars.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
