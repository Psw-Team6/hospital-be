using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.SharedModel;

namespace HospitalLibrary.Consiliums.Model
{
    [Table("Consiliums")]
    public class Consilium
    {
        public Guid Id { get; private set; }
        public string Theme { get; private set; }
        public IEnumerable<Doctor> Doctors { get; private set; }
        public TimeRange TimeRange { get; private set; }
        public Room Room { get; private set; }
        /*public Guid RoomId { get; private set; }*/
        public Consilium(string theme, IEnumerable<Doctor> doctors, TimeRange timeRange, Room room)
        {
            Theme = theme;
            Doctors = doctors;
            TimeRange = timeRange;
            Room = room;
        }

        public Consilium(string theme, IEnumerable<Doctor> doctors, TimeRange timeRange)
        {
            Theme = theme;
            Doctors = doctors;
            TimeRange = timeRange;
        }

        public bool IsConsiliumInRoom(Guid roomId,TimeRange timeRange)
        {
            if (TimeRange.From.Date == timeRange.From.Date
                && TimeRange.To.Date == timeRange.To.Date)
            {
                if ((TimeRange.From.TimeOfDay >= timeRange.From.TimeOfDay &&
                     TimeRange.To.TimeOfDay <= timeRange.To.TimeOfDay) ||
                    (TimeRange.From.TimeOfDay <= timeRange.From.TimeOfDay &&
                     TimeRange.To.TimeOfDay >= timeRange.From.TimeOfDay) ||
                    (TimeRange.From.TimeOfDay <=  timeRange.To.TimeOfDay &&
                     TimeRange.To.TimeOfDay >= timeRange.To.TimeOfDay))
                    return Room.Id == roomId;
            }
            return false;
        }
        public Consilium()
        {
        }
    }
}