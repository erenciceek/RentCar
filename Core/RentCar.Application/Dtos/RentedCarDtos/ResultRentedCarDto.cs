using RentCar.Application.Dtos.UserDtos;
using RentCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Dtos.RentedCarDtos
{
    public class ResultRentedCarDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        
        [Required]
        public OnlyInfoUserDto User { get; set; } = null!;
        
        public int CarId { get; set; }
        
        [Required]
        public Car Car { get; set; } = null!;
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DamagePrice { get; set; }
        public bool IsCompleted { get; set; }
    }
}
