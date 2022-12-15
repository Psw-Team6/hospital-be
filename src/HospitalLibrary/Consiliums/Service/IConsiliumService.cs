using System;
using System.Threading.Tasks;
using HospitalLibrary.Consiliums.Model;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.SharedModel;

namespace HospitalLibrary.Consiliums.Service
{
    public interface IConsiliumService
    {
        Task<Consilium> GetById(Guid id);
        Task<Consilium> ScheduleConsilium(Consilium consilium);
        TimeRange FindTimeRangeForAllDoctors(Consilium meeting);
        TimeRange FindDateWhenDoctorsAreAvailable(Consilium consilium);
        Task<Room> FindAvailableMeetingRoom(TimeRange timeRange);
    }
}