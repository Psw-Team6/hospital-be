using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.RabbitMQService.VehicleCoordinate
{
    public interface IVehicleCoordinateSender
    {
        void SendCoordinates(double latitude, double longitude, int bloodUnits, int status);
    }
}
