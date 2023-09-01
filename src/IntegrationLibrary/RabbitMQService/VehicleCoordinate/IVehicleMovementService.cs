using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.RabbitMQService.VehicleCoordinate
{
    public interface IVehicleMovementService
    {
        public void StartMoving(double startLatitude, double startLongitude, double endLatitude, double endLongitude, double steps);
    }
}
