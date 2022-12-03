using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using HospitalLibrary.ApplicationUsers;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.Appointments.Model;
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

        public bool IsDoctorOnHoliday(DateTime startDate, DateTime finishDate)
        {
            return Holidays.All(holiday => holiday.DateRange.From.Date >= startDate.Date 
                                           && holiday.DateRange.To.Date <= finishDate.Date);
        }

        public bool IsDoctorWorking(DateTime startDate, DateTime finishDate)
        {
            foreach (var appointment in Appointments)
            {
                if (appointment.Duration.From.Date == startDate.Date
                    && appointment.Duration.To.Date == finishDate.Date)
                {
                    if (appointment.Duration.From.TimeOfDay >= startDate.TimeOfDay &&
                        appointment.Duration.To.TimeOfDay <= finishDate.TimeOfDay)
                    {
                        return true;
                    }
                }
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

        private bool CheckDoctorWorkingDate()
        {
            return WorkingSchedule.IsExpired();
        }
        

    }
}