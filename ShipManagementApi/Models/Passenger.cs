using System.ComponentModel.DataAnnotations.Schema;

namespace ShipManagementApi.Models
{
    public class Passenger
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [ForeignKey("PassengerShip")]
        public string PassengerShipImo { get; set; }
    }
}
