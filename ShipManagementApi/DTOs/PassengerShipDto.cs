using ShipManagementApi.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ShipManagementApi.Dtos
{
    public class PassengerShipDto : ShipDto
    {
        public List<PassengerDto> Passengers { get; set; }

        public class PassengerDto
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}
