using System;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Common.EventSourcing;

namespace HospitalLibrary.Appointments.DomainEvents
{
    public class EventStoreSchedulingAppointment : EventStore<Appointment, EventStoreSchedulingAppointmentType>
    {

        public EventStoreSchedulingAppointment()
        {
            
        }

        public EventStoreSchedulingAppointment(Appointment appointment, DateTime createdAt, int version, int sequence, EventStoreSchedulingAppointmentType data, string name)
        {
            Aggregate = appointment;
            CreatedAt = createdAt;
            Version = version;
            Sequence = sequence;
            Data = data;
            Name = name;
        }
    }
}