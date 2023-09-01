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
        private readonly IVehicleCoordinateSender _vehicleCoordinateSender;

        public VehicleController(IVehicleMovementService vehicleMovementService, IVehicleCoordinateSender vehicleCoordinateSender)
        {
            _vehicleMovementService = vehicleMovementService;
            _vehicleCoordinateSender = vehicleCoordinateSender;
        }   

        [HttpPost]
        public IActionResult StartVehicleMovement()
        {
            List<Tuple<double, double>> coordinates = new List<Tuple<double, double>>
        {
            Tuple.Create(-22.9, -43.1),
            Tuple.Create(-34.6, -58.3),
            Tuple.Create(-33.8, 151.2),
            Tuple.Create(-33.9, 18.4),
            Tuple.Create(-36.8, 174.7)
        };

            Random random = new Random();
            int randomIndex = random.Next(0, coordinates.Count);
            Tuple<double, double> randomCoordinates = coordinates[randomIndex];
            // Fiksirane koordinate tačke A i tačke B
            double startLatitude = randomCoordinates.Item1;
            double startLongitude = randomCoordinates.Item2;
           

            //_vehicleMovementService.StartMoving(startLatitude, startLongitude, endLatitude, endLongitude, steps);
            _vehicleCoordinateSender.SendCoordinates(startLatitude, startLongitude, 10, 0);

            return Ok("Zahtjev poslat.");
        }
    }
}