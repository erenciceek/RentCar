using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentCar.Application.Dtos.RentedCarDtos;
using RentCar.Domain.Entities;

namespace RentCar.Application.Dtos.UserDtos
{
    public class ResultUserDto
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; } = null!;
        
        [Required]
        public string Surname { get; set; } = null!;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        
        [Required]
        [Phone]
        public string Phone { get; set; } = null!;
        
        [Required]
        public string Role { get; set; } = null!;

        public List<OnlyInfoRentedCarDto> RentedCars { get; set; } = new();
    }
}
