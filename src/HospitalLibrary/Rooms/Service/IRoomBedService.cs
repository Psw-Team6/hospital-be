using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Rooms.Service
{
    public interface IRoomBedService
    {
        Task UpdateRoomAvailability(PatientAdmission admission);
        Task<IEnumerable<RoomBed>> GetAll();
    }
}