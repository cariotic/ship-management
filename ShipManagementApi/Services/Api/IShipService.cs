using ShipManagementApi.Dtos;
using ShipManagementApi.DTOs;
using ShipManagementApi.Models;

namespace ShipManagementApi.Services.Api
{
    public interface IShipService
    {
        public Task<bool> AddPassengerShip(PassengerShipDto dto);
        public Task<bool> AddTankerShip(TankerShipDto dto);
        public Task<List<ShipDto>> GetAllShips();
        public Task<List<PassengerShipDto>> GetAllPassengerShips();
        public Task<List<TankerShipDto>> GetAllTankerShips();
        public Task<bool> AddPassengerToShip(string imoNumber, PassengerShipDto.PassengerDto passengerDto);
        public Task<bool> RemovePassengerFromShip(string imoNumber, int passengerId);
        public Task<bool> FillFuelTank(string imoNumber, int fuelTankId, FuelDto fuelDto);
        public Task<bool> EmptyFuelTank(string imoNumber, int fuelTankId);
    }
}
