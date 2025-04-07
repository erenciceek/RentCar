using RentCar.Application.Dtos.AuthDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Interfaces.Services
{
    public interface IAuthServices
    {
        public Task<string> LoginAsync(LoginDto loginDto);
        public Task<string> RegisterAsync(RegisterDto registerDto);
        public string GenerateToken(int userId, string role);
    }
}
