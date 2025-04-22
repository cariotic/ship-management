using ShipManagementApi.DTOs;
using ShipManagementApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShipManagementApi.Dtos
{
    public class TankerShipDto : ShipDto
    {
        public List<FuelTankDto> FuelTanks { get; set; } = new();

        public class FuelTankDto
        {
            public int Id { get; set; }
            public double FuelCapacity { get; set; }
            public double CurrentFuelAmount { get; set; }
            [JsonConverter(typeof(JsonStringEnumConverter<FuelType>))]
            public FuelType? CurrentFuelType { get; set; }
        }
    }
}
