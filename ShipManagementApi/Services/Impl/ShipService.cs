using Microsoft.EntityFrameworkCore;
using ShipManagementApi.Dtos;
using ShipManagementApi.DTOs;
using ShipManagementApi.Models;
using ShipManagementApi.Repository;
using ShipManagementApi.Services.Api;

namespace ShipManagementApi.Services.Impl
{
    public class ShipService : IShipService
    {
        private readonly ShipRepository shipRepository;

        public ShipService(ShipRepository shipRepository)
        {
            this.shipRepository = shipRepository;
        }

        public async Task<List<ShipDto>> GetAllShips()
        {
            List<Ship> ships = await shipRepository.FindAllShips();
            
            return ships.Select(ship => new ShipDto
                {
                    ImoNumber = ship.ImoNumber,
                    Name = ship.Name,
                    Length = ship.Length,
                    Width = ship.Width,
                    ShipType = (ship is PassengerShip) ? ShipType.PassengerShip : ShipType.TankerShip
                })
                .ToList();
        }

        public async Task<List<PassengerShipDto>> GetAllPassengerShips()
        {
            List<Ship> ships = await shipRepository.FindAllShips();

            return ships.OfType<PassengerShip>()
                .Select(ship => new PassengerShipDto
                {
                    ImoNumber = ship.ImoNumber,
                    Name = ship.Name,
                    Length = ship.Length,
                    Width = ship.Width,
                    Passengers = ship.Passengers
                        .Select(p => new PassengerShipDto.PassengerDto
                        {
                            Id = p.Id,
                            FirstName = p.FirstName,
                            LastName = p.LastName
                        }).ToList()
                })
                .ToList();
        }

        public async Task<List<TankerShipDto>> GetAllTankerShips()
        {
            List<Ship> ships = await shipRepository.FindAllShips();

            return ships.OfType<TankerShip>()
                .Select(ship => new TankerShipDto
                {
                    ImoNumber = ship.ImoNumber,
                    Name = ship.Name,
                    Length = ship.Length,
                    Width = ship.Width,
                    FuelTanks = ship.FuelTanks
                        .Select(ft => new TankerShipDto.FuelTankDto
                        {
                            Id = ft.Id,
                            FuelCapacity = ft.FuelCapacity,
                            CurrentFuelAmount = ft.CurrentFuelAmount,
                            CurrentFuelType = ft.CurrentFuelType,
                            
                        }).ToList()
                })
                .ToList();
        }

        public async Task<bool> AddPassengerShip(PassengerShipDto dto)
        {
            if(!IsShipDtoValid(dto.ImoNumber, dto.Length, dto.Width))
            {
                return false;
            }

            PassengerShip ship = new PassengerShip
            {
                ImoNumber = dto.ImoNumber,
                Name = dto.Name,
                Length = dto.Length,
                Width = dto.Width,
                Passengers = dto.Passengers?.Select(p => new Passenger
                {
                    Id=p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName
                }).ToList() ?? new List<Passenger>()
            };

            await shipRepository.Save(ship);
            return true;
        }

        public async Task<bool> AddTankerShip(TankerShipDto dto)
        {
            if (!IsShipDtoValid(dto.ImoNumber, dto.Length, dto.Width))
            {
                return false;
            }

            TankerShip ship = new TankerShip
            {
                ImoNumber = dto.ImoNumber,
                Name = dto.Name,
                Length = dto.Length,
                Width = dto.Width,
                FuelTanks = dto.FuelTanks?.Select(ft => new FuelTank
                {
                    Id = ft.Id,
                    FuelCapacity = ft.FuelCapacity,
                    CurrentFuelType = ft.CurrentFuelType,
                    CurrentFuelAmount = ft.CurrentFuelAmount
                }).ToList() ?? new List<FuelTank>()
            };

            await shipRepository.Save(ship);
            return true;
        }

        public async Task<bool> AddPassengerToShip(string imoNumber, PassengerShipDto.PassengerDto passengerDto)
        {
            Ship? ship = await shipRepository.FindShip(imoNumber);

            if(ship == null || !(ship is PassengerShip passengerShip)) { 
                return false;
            }

            Passenger passenger = new Passenger
            {
                Id = passengerDto.Id,
                FirstName = passengerDto.FirstName,
                LastName = passengerDto.LastName,
                PassengerShipImo = imoNumber
            };

            passengerShip.Passengers.Add(passenger);
            await shipRepository.Update(passengerShip);
            return true;
        }

        public async Task<bool> RemovePassengerFromShip(string imoNumber, int passengerId)
        {
            Ship? ship = await shipRepository.FindShip(imoNumber);

            if (ship == null || !(ship is PassengerShip passengerShip))
            {
                return false;
            }

            Passenger? passenger = passengerShip.Passengers.FirstOrDefault(p => p.Id == passengerId);

            if (passenger == null)
            {
                return false;
            }

            passengerShip.Passengers.Remove(passenger);
            await shipRepository.Update(passengerShip);
            return true;
        }

        public async Task<bool> FillFuelTank(string imoNumber, int fuelTankId, FuelType fuelType, double fuelAmount)
        {
            Ship? ship = await shipRepository.FindShip(imoNumber);

            if (ship == null || !(ship is TankerShip tankerShip))
            {
                return false;
            }

            FuelTank? tank = tankerShip.FuelTanks.FirstOrDefault(t => t.Id == fuelTankId);

            if (tank == null 
                || (tank.CurrentFuelType != fuelType && tank.CurrentFuelAmount > 0)
                || (tank.CurrentFuelAmount + fuelAmount > tank.FuelCapacity))
            {
                return false;
            }

            tank.CurrentFuelType = fuelType;
            tank.CurrentFuelAmount += fuelAmount;

            await shipRepository.Update(tankerShip);
            return true;
        }

        public async Task<bool> EmptyFuelTank(string imoNumber, int fuelTankId)
        {
            Ship? ship = await shipRepository.FindShip(imoNumber);

            if (ship == null || !(ship is TankerShip tankerShip))
            {
                return false;
            }

            FuelTank? tank = tankerShip.FuelTanks.FirstOrDefault(t => t.Id == fuelTankId);

            if (tank == null)
            {
                return false;
            }

            tank.CurrentFuelAmount = 0;
            await shipRepository.Update(tankerShip);
            return true;
        }

        private bool IsImoValid(string imoNumber)
        {
            if (imoNumber.Length != 7 || (shipRepository.FindAllShips().Result.Find(ship => ship.ImoNumber == imoNumber)) != null)
            {
                // imo too long/short or not unique
                return false;
            }

            int sum = 0;
            int length = imoNumber.Length;

            for (int i = 0; i < imoNumber.Length-1; i++)
            {
                if (!char.IsDigit(imoNumber[i]))
                {
                    return false;
                }

                int digit = (int)char.GetNumericValue(imoNumber[i]);
                sum += (length - i) * digit;
            }
            return (sum % 10) == (int)char.GetNumericValue(imoNumber[length - 1]);
        }

        private bool AreLengthWidthValid(double length, double width)
        {
            if(length > 0 && width > 0)
            {
                return true;
            }
            return false;
        }

        private bool IsShipDtoValid(string imoNumber, double length, double width)
        {
            if (!IsImoValid(imoNumber) || !AreLengthWidthValid(length, width))
            {
                return false;
            }
            return true;
        }
    }
}
