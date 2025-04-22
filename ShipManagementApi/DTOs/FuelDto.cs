using ShipManagementApi.Models;
using System.Text.Json.Serialization;

namespace ShipManagementApi.DTOs
{
    public class FuelDto
    {
        [JsonConverter(typeof(JsonStringEnumConverter<FuelType>))]
        public FuelType fuelType { get; set; }
        public double fuelAmount { get; set; }
    }
}
