using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.DomainEvents;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Common;
using HospitalLibrary.Common.EventSourcing;

namespace HospitalLibrary.Appointments.Service.EventStoreService
{
    public class EventStoreSchedulingAppointmentService : IEventStoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public EventStoreSchedulingAppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateEventStore(Appointment appointment, List<DomainEvent<EventStoreSchedulingAppointmentType>> events)
        {
            foreach (var @event in events)
            {
                await CreateEvents(@event, appointment);
                await _unitOfWork.CompleteAsync();
            }
        }
        
        private async Task CreateEvents(DomainEvent<EventStoreSchedulingAppointmentType> @event, Appointment appointment)
        {
            var name = GetEventTypeName(@event);
            var newEventStore = new EventStoreSchedulingAppointment(appointment, @event.CreatedAt,
                await GetVersion(appointment.Id), await GetSequence(),@event.Event, name);
            await _unitOfWork.EventStoreSchedulingAppointmentRepository.CreateAsync(newEventStore);
        }
        
        private static string GetEventTypeName(DomainEvent<EventStoreSchedulingAppointmentType> @event)
        {
            if (@event.Event == EventStoreSchedulingAppointmentType.SPECIALIZATION_VIEWED)
                return  "specialization viewed event";
            if (@event.Event == EventStoreSchedulingAppointmentType.DOCTOR_NAME_VIEWED)
                return  "doctor viewed event";
            if (@event.Event == EventStoreSchedulingAppointmentType.DATE_RANGE_VIEWED)
                return  "date range viewed event";
            if (@event.Event == EventStoreSchedulingAppointmentType.FREE_TERMS_VIEWED)
                return "free terms viewed event";
            return  "scheduling appointment finished event";
        }
        
        public async Task<int> GetVersion(Guid aggregateId)
        {
            var version = await _unitOfWork.EventStoreSchedulingAppointmentRepository.GetVersionCount(aggregateId);
            if (version != 0) 
                return ++version;
            return 1;
        }

        public async Task<int> GetSequence()
        {
            var sequence = await _unitOfWork.EventStoreSchedulingAppointmentRepository.GetSequenceCount();
            if (sequence != 0) 
                return ++sequence;
            return 1;
        }

    }
}