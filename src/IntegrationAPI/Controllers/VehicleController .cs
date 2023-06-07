using IntegrationLibrary.RabbitMQService.VehicleCoordinate;
using AutoMapper;
using IntegrationAPI.Dtos.Request;
using IntegrationLibrary.BloodBank;
using IntegrationLibrary.BloodBank.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Security;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Collections.Generic;
using IntegrationLibrary.HTTP;
using IntegrationLibrary.BloodRequests.Model;
using IntegrationAPI.Dtos.Response;

namespace IntegrationAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleMovementService _vehicleMovementService;

        public VehicleController(IVehicleMovementService vehicleMovementService)
        {
            _vehicleMovementService = vehicleMovementService;
        }

        [HttpPost]
        public IActionResult StartVehicleMovement()
        {
            // Fiksirane koordinate tačke A i tačke B
            double startLatitude = 42.123456;
            double startLongitude = 18.654321;
            double endLatitude = 42.987654;
            double endLongitude = 19.876543;

            double steps = 10; // Broj koraka (koordinata) između A i B

            _vehicleMovementService.StartMoving(startLatitude, startLongitude, endLatitude, endLongitude, steps);

            return Ok("Kretanje vozila završeno.");
        }
    }
}