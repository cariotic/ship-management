using System.ComponentModel.DataAnnotations;

namespace ShipManagementApi.Models
{
    public abstract class Ship
    {
        [Key]
        public string ImoNumber { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
    }
}
