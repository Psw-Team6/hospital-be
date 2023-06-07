using System;
using System.Threading;
//using IntegrationLibrary.RabbitMQService.VehicleCoordinate;


namespace IntegrationLibrary.RabbitMQService.VehicleCoordinate
{
    public class VehicleMovementService:IVehicleMovementService
	{
        private readonly IVehicleCoordinateSender _coordinateSender;
        public VehicleMovementService(IVehicleCoordinateSender coordinateSender)
        {
            _coordinateSender = coordinateSender;
        }
        public void StartMoving(double startLatitude, double startLongitude, double endLatitude, double endLongitude, double steps)
        {
            double latitudeStep = (endLatitude - startLatitude) / steps;
            double longitudeStep = (endLongitude - startLongitude) / steps;

            double currentLatitude = startLatitude;
            double currentLongitude = startLongitude;

            for (int i = 0; i <= steps; i++)
            {
                _coordinateSender.SendCoordinates(currentLatitude, currentLongitude);

                currentLatitude += latitudeStep;
                currentLongitude += longitudeStep;

                Thread.Sleep(1000); // Pauza od 1 sekunde između koordinata
            }

            _coordinateSender.SendCoordinates(endLatitude, endLongitude);
        }
    }
}
