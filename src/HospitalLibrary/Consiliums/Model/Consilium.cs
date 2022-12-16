using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using HospitalLibrary.CustomException;
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

        private void ValidateTheme()
        {
            if (string.IsNullOrEmpty(Theme))
                throw new ThemeNotExist("You must fill out theme!");
        }

        private void ValidateDoctors()
        {
            if (!Doctors.Any())
                throw new ConsiliumDoctorsNotExist("There are no doctors!");
        }   
        private void ValidateRange()
        {
            if(!TimeRange.IsValidFrom())
                throw new TimeRangeException("Start date is invalid!"); 
            if(!TimeRange.IsValidTo())
                throw new TimeRangeException("End date is invalid!");
            if (!TimeRange.IsValidRange())
                throw new TimeRangeException("Dates are invalid!");
            if(!TimeRange.IsValidDuration())
                throw new TimeRangeException("Duration is to short!");

        }

        public void ValidateConsilium()
        {
            ValidateTheme();
            ValidateRange();
            ValidateDoctors();
        }
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