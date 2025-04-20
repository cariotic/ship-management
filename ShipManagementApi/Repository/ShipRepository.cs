using ShipManagementApi.Dtos;
using ShipManagementApi.Models;
using System.Numerics;

namespace ShipManagementApi.Repository
{
    public class ShipRepository
    {
        private static List<Ship> shipRegistry = new();

        public Task<List<Ship>> FindAllShips()
        {
            return Task.FromResult(shipRegistry.ToList());
        }
        public Task Save(Ship ship)
        {
            shipRegistry.Add(ship);
            return Task.CompletedTask;
        }

        public Task Update(Ship ship)
        {
            shipRegistry.Remove(shipRegistry.Find(s => s.ImoNumber == ship.ImoNumber));
            shipRegistry.Add(ship);
            return Task.CompletedTask;
        }

        public Task Remove(string imoNumber)
        {
            shipRegistry.Remove(shipRegistry.Find(ship => ship.ImoNumber == imoNumber));
            return Task.CompletedTask;
        }

        public Task<List<PassengerShip>> FindAllPassengerShips()
        {
            return Task.FromResult(shipRegistry.OfType<PassengerShip>().ToList());
        }

        public Task<List<TankerShip>> FindAllTankerShips()
        {
            return Task.FromResult(shipRegistry.OfType<TankerShip>().ToList());
        }

        public Task<Ship> FindShip(string imoNumber)
        {
            return Task.FromResult(shipRegistry.Find(s => s.ImoNumber == imoNumber));
        }
    }
}
