using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Consiliums.Model;
using HospitalLibrary.Holidays.Model;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.SharedModel;

namespace HospitalLibrary.Doctors.Model
{
    [Table("Doctors")]
    public class Doctor:ApplicationUser
    {
        public Guid SpecializationId { get; set; }
        public Specialization Specialization{ get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }
        
        public IEnumerable<Holiday> Holidays { get; set; }
        public IEnumerable<Patient> Patients { get; set; }
        public Room Room { get; set; }
        public Guid RoomId { get; set; }
        public Guid WorkingScheduleId { get; set; }
        public WorkingSchedule WorkingSchedule { get; set; }
        public IEnumerable<Consilium> Consiliums { get; set; }

        public bool IsDoctorOnHoliday(DateTime startDate, DateTime finishDate)
        {
            if (Holidays.Count() > 0)
            {
                return Holidays.All(holiday => holiday.DateRange.From.Date >= startDate.Date 
                                               && holiday.DateRange.To.Date <= finishDate.Date);
            }
            return false;
        }

        public bool IsDoctorWorking(DateTime startDate, DateTime finishDate)
        {
            foreach (var appointment in Appointments)
            {
                if (appointment.Duration.From.Date == startDate.Date
                    && appointment.Duration.To.Date == finishDate.Date)
                {
                    if (CheckIfAppointmentOverlaps(startDate, finishDate, appointment)) return true;
                }
            }
            return false;
        }

        private static bool CheckIfAppointmentOverlaps(DateTime startDate, DateTime finishDate, Appointment appointment)
        {
            if ((appointment.Duration.From.TimeOfDay >= startDate.TimeOfDay &&
                 appointment.Duration.To.TimeOfDay <= finishDate.TimeOfDay) ||
                (appointment.Duration.From.TimeOfDay <= startDate.TimeOfDay &&
                 appointment.Duration.To.TimeOfDay >= startDate.TimeOfDay) ||
                (appointment.Duration.From.TimeOfDay <= finishDate.TimeOfDay &&
                 appointment.Duration.To.TimeOfDay >= finishDate.TimeOfDay))
            {
                return true;
            }

            return false;
        }

        public bool IsDoctorOnConsilium(DateTime startDate, DateTime finishDate)
       {
            foreach (var consilium in Consiliums)
            {
                if (consilium.TimeRange.From.Date == startDate.Date
                    && consilium.TimeRange.To.Date == finishDate.Date)
                {
                    if (CheckIfConsiliumOverlaps(startDate, finishDate, consilium)) return true;
                }
            }
            return false;
       }

       private static bool CheckIfConsiliumOverlaps(DateTime startDate, DateTime finishDate, Consilium consilium)
       {
           if ((consilium.TimeRange.From.TimeOfDay >= startDate.TimeOfDay &&
                consilium.TimeRange.To.TimeOfDay <= finishDate.TimeOfDay) ||
               (consilium.TimeRange.From.TimeOfDay <= startDate.TimeOfDay &&
                consilium.TimeRange.To.TimeOfDay >= startDate.TimeOfDay) ||
               (consilium.TimeRange.From.TimeOfDay <= finishDate.TimeOfDay &&
                consilium.TimeRange.To.TimeOfDay >= finishDate.TimeOfDay))
           {
               return true;
           }

           return false;
       }

       public bool IsDoctorAvailableByWorkingSchedule(DateRange dateRange)
        {
            if (WorkingSchedule.IsExpired())
            {
                return false;
            }
            return dateRange.From.TimeOfDay >= WorkingSchedule.DayOfWork.From.TimeOfDay
                   && dateRange.To.TimeOfDay <= WorkingSchedule.DayOfWork.To.TimeOfDay;
        }

        public bool IsAvailable(DateTime startTime, int duration)
        {
            if (IsDoctorWorking(startTime, startTime.AddMinutes(duration)))
                return false;
            if (IsDoctorOnConsilium(startTime, startTime.AddMinutes(duration)))
                return false;
            if (IsDoctorOnHoliday(startTime, startTime.AddMinutes(duration)))
                return false;
            return true;
        }
        private bool CheckDoctorWorkingDate()
        {
            return WorkingSchedule.IsExpired();
        }
        

    }
}