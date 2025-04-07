using RentCar.Application.Dtos.RentedCarDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentCar.Application.Interfaces.Services
{
    public interface IRentedCarServices
    {
        Task<List<ResultRentedCarDto>> GetAllRentedCars();
        Task<GetByIdRentedCarDto> GetByIdRentedCar(int id);
        Task CreateRentedCar(CreateRentedCarDto dto);
        Task UpdateRentedCar(UpdateRentedCarDto dto);
        Task DeleteRentedCar(int id);
    }
} 