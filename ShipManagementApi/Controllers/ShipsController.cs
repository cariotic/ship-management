using Microsoft.AspNetCore.Mvc;
using ShipManagementApi.Dtos;
using ShipManagementApi.Models;
using ShipManagementApi.Services.Api;

namespace ShipManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShipsController : ControllerBase
    {
        private readonly IShipService shipService;

        public ShipsController(IShipService shipService)
        {
            this.shipService = shipService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShips()
        {
            return Ok(await shipService.GetAllShips());
        }

        [HttpPost("passenger")]
        public async Task<IActionResult> AddPassengerShip([FromBody] PassengerShipDto dto)
        {
            if(!await shipService.AddPassengerShip(dto))
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost("tanker")]
        public async Task<IActionResult> AddTankerShip([FromBody] TankerShipDto dto)
        {
            if(!await shipService.AddTankerShip(dto)){
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost("{imoNumber}/passengers")]
        public async Task<IActionResult> AddPassengerToShip(string imoNumber, [FromBody] PassengerShipDto.PassengerDto passengerDto)
        {
            bool result = await shipService.AddPassengerToShip(imoNumber, passengerDto);

            if (result == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{imoNumber}/passengers/{passengerId}")]
        public async Task<IActionResult> RemovePassengerFromShip(string imoNumber, int passengerId)
        {
            bool result = await shipService.RemovePassengerFromShip(imoNumber, passengerId);

            if (result == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("{imoNumber}/tanks/{fuelTankId}/fill")]
        public async Task<IActionResult> FillFuelTank(string imoNumber, int fuelTankId, FuelType fuelType, double fuelAmount)
        {
            bool result = await shipService.FillFuelTank(imoNumber, fuelTankId, fuelType, fuelAmount);

            if (result == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPatch("{imoNumber}/tanks/{fuelTankId}/empty")]
        public async Task<IActionResult> EmptyFuelTank(string imoNumber, int fuelTankId)
        {
            bool result = await shipService.EmptyFuelTank(imoNumber, fuelTankId);

            if (result == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
