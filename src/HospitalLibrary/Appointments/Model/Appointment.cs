using System;
using System.Collections.Generic;
using HospitalLibrary.Appointments.DomainEvents;
using HospitalLibrary.Common;
using HospitalLibrary.Common.EventSourcing;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.SharedModel;

namespace HospitalLibrary.Appointments.Model
{
    public class Appointment: EventSourcedAggregate<EventStoreSchedulingAppointmentType>
    {
        public bool Emergent { get; set; }
        public DateRange Duration { get; set; }
        public Patient Patient { get; set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public AppointmentType AppointmentType { get; set; }
        public Doctor Doctor { get; set; }
        public AppointmentState AppointmentState { get; set; }
        public Appointment(Guid id) : base(id)
        {
        }
        public Appointment()
        {
            
        }

        public Appointment(List<Doctor> doctors, DateRange duration, IEnumerable<DateRange> freeTerms)
        {
            Duration = duration;
            
        }

        public bool CanBeExamined()
        {
            if (!Duration.IsValidRange()) return false;
            //if more than 2 day earlier
            if (AppointmentState != AppointmentState.Pending) return false;
            var durationRange = Duration.To - Duration.From;
            if (Duration.From.Date < DateTime.Now.Date.AddDays(-2) 
                && Duration.To.Date < DateTime.Now.Date.AddDays(-2).Add(durationRange))
            {
                return false;
            }
            //if more than 2 hours before
            if (Duration.To.Date > DateTime.Now.Date.AddHours(2) && 
                Duration.From.Date > DateTime.Now.Date.AddHours(2).Add(-durationRange))
            {
                return false;
            }
            return true;
        }
        
        public bool IsDoctorConflicts(Appointment appointment)
        {
            return CheckDate(appointment) && CheckTimeOfDay(appointment);
        }
        public bool IsPatientConflicts(Appointment appointment)
        {
            return CheckDate(appointment) && CheckTimeOfDay(appointment);
        }

        private bool CheckDate(Appointment appointment)
        {
            return appointment.Duration.From.Date == Duration.From.Date &&
                   appointment.Duration.To.Date == Duration.To.Date;
        }
        private bool CheckTimeOfDay(Appointment appointment)
        {
            return appointment.Duration.From.TimeOfDay < Duration.To.TimeOfDay 
                   && appointment.Duration.To.TimeOfDay > Duration.From.TimeOfDay;
        }
        
        public override void Apply(DomainEvent<EventStoreSchedulingAppointmentType> @event)
        {
            Changes.Add(@event);
        }
    }
    
}