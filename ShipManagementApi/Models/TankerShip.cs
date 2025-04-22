namespace ShipManagementApi.Models
{
    public class TankerShip : Ship
    {
        public List<FuelTank> FuelTanks { get; set; } = new();
    }
}
