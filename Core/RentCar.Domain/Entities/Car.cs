﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int KM { get; set; }

        // automatic or manual
        public string Type { get; set; }
        public string Fuel { get; set; }
        public decimal DailyPrice { get; set; }
        public bool IsAvailable { get; set; }


    }
}
