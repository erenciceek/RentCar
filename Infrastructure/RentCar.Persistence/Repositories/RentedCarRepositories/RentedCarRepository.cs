using Microsoft.EntityFrameworkCore;
using RentCar.Application.Interfaces.Repositories;
using RentCar.Domain.Entities;
using RentCar.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Persistence.Repositories.RentedCarRepositories
{
    public class RentedCarRepository : IRentedCarRepository
    {
        private readonly RentCarDbContext _context;
        public RentedCarRepository(RentCarDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateRentedCarAsync(RentedCar entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _context.RentedCars.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRentedCarAsync(RentedCar entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.RentedCars.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<RentedCar>> GetAllRentedCarsAsync()
        {
            var result = await _context.RentedCars.ToListAsync();
            return result;
        }

        public async Task<RentedCar> GetByIdRentedCarAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID value", nameof(id));

            var result = await _context.RentedCars.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<List<RentedCar>> GetRentedCarsByUserId(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("Invalid user ID value", nameof(userId));

            var result = await _context.RentedCars.Where(x => x.UserId == userId).ToListAsync();
            return result;
        }

        public Task UpdateRentedCarAsync(RentedCar entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.RentedCars.Update(entity);
            return _context.SaveChangesAsync();
        }
    }
}
