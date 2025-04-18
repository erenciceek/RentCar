﻿using RentCar.Application.Dtos.RentedCarDtos;
using RentCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Dtos.UserDtos
{
    public class GetByIdUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public List<OnlyInfoRentedCarDto> RentedCars { get; set; }
    }
}
