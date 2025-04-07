using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Dtos.CarDtos
{
    public class ResultCarDto
    {
        public int Id { get; set; }
        
        public string? ImageUrl { get; set; }
        
        [Required]
        public string Brand { get; set; } = null!;
        
        [Required]
        public string Model { get; set; } = null!;
        
        public int Year { get; set; }
        public int KM { get; set; }

        [Required]
        public string Type { get; set; } = null!; // automatic or manual
        
        [Required]
        public string Fuel { get; set; } = null!;
        
        public decimal DailyPrice { get; set; }
        public bool IsAvailable { get; set; }
    }
}
