using Microsoft.EntityFrameworkCore;
using RentCar.Domain.Entities;
using RentCar.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Persistence.Repositories.UserRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RentCarDbContext _context;

        public UserRepository(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task CreateUserAsync(User entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User entity)
        {
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var result = await _context.Users.ToListAsync();
            return result;
        }

        public async Task<User> GetByIdUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task UpdateUserAsync(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
