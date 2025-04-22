using System.ComponentModel.DataAnnotations.Schema;

namespace ShipManagementApi.Models
{
    public class FuelTank
    {
        public int Id { get; set; }
        public double FuelCapacity { get; set; }
        public double CurrentFuelAmount { get; set; }
        public FuelType? CurrentFuelType { get; set; }

        [ForeignKey("TankerShip")]
        public string TankerShipImo { get; set; }
    }
}
