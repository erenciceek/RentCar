using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RentCar.Application.Dtos.AuthDtos;
using RentCar.Application.Dtos.UserDtos;
using RentCar.Application.Interfaces.Repositories;
using RentCar.Application.Interfaces.Services;
using RentCar.Application.Validators;
using RentCar.Domain.Constants;
using RentCar.Domain.Entities;
using RentCar.Domain.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RentCar.Application.Services.AuthServices
{
    public class AuthServices : IAuthServices
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IUserServices _userServices;

        public AuthServices(IConfiguration configuration, IUserRepository userRepository, IUserServices userServices)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userServices = userServices ?? throw new ArgumentNullException(nameof(userServices));
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            if (loginDto == null)
                throw new ArgumentNullException(nameof(loginDto));

            if (string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
                return null;

            var user = await _userRepository.GetByEmailAsync(loginDto.Email);
            if (user == null)
                return null;

            if (!PasswordHashHelper.VerifyPassword(loginDto.Password, user.Password))
                return null;

            return GenerateToken(user.Id, user.Role);
        }

        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            if (registerDto == null)
                throw new ArgumentNullException(nameof(registerDto));

            try
            {
                var existingUser = await _userServices.GetByEmail(registerDto.Email);
                if (existingUser != null)
                    return null; 
            }
            catch (KeyNotFoundException)
            {
           
            }

            PasswordValidator.ValidatePassword(registerDto.Password);

            var createUserDto = new CreateUserDto
            {
                Name = registerDto.Name,
                Surname = registerDto.Surname,
                Email = registerDto.Email,
                Password = PasswordValidator.HashPassword(registerDto.Password),
                Phone = registerDto.Phone,
                Role = RoleConstants.User
            };

            await _userServices.CreateUser(createUserDto);

            var createdUser = await _userRepository.GetByEmailAsync(registerDto.Email);
            if (createdUser == null)
                throw new Exception("User creation failed");

            return GenerateToken(createdUser.Id, createdUser.Role);
        }

        public string GenerateToken(int userId, string role)
        {
            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentException("Role cannot be empty", nameof(role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
} 