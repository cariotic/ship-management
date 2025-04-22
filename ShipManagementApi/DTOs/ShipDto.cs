using ShipManagementApi.Models;
using System.Text.Json.Serialization;

namespace ShipManagementApi.DTOs
{
    public class ShipDto
    {
        public string ImoNumber { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter<ShipType>))]
        public ShipType ShipType { get; set; }
    }
}
