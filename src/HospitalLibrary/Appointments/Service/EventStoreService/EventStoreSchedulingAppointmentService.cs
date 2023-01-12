using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public async Task<int> GetAverageStepCount()
        {
            var appointments = (List<Appointment>) await _unitOfWork.AppointmentRepository.GetAllAsync();
            var events =  (List<EventStoreSchedulingAppointment>)await _unitOfWork.EventStoreSchedulingAppointmentRepository.GetAllAsync();
            var counter = appointments.Count;
            foreach (var appointment in appointments)
            {
                var eventNumber =
                    await _unitOfWork.EventStoreSchedulingAppointmentRepository.GetEventCountByAggregate(appointment.Id);
                if (eventNumber == 0)
                    --counter;
            }
            return events.Count / counter;
        }
        
        public async Task<TimeSpan> GetAverageTime()
        {
            var duration = TimeSpan.Zero;
            var appointments = (List<Appointment>) await _unitOfWork.AppointmentRepository.GetAllAsync();
            var counter = appointments.Count;
            foreach (var appointment in appointments)
            {
                var timeSpan = await CountDurationSchedulingAppointment(appointment);
                duration += timeSpan;
                if (timeSpan == TimeSpan.Zero)
                    --counter;
            }
            return duration / counter;
        }
        
        private async Task<TimeSpan> CountDurationSchedulingAppointment(Appointment appointment)
        {
            var events = await _unitOfWork.EventStoreSchedulingAppointmentRepository.GetEventsByAggregate(appointment.Id);
            if(events.Count == 0)
                return TimeSpan.Zero;
            var startTime = events.Min(@event => @event.CreatedAt);
            var finishTime = events.Max(@event => @event.CreatedAt);
            return finishTime - startTime;
        }
        
        public async Task<Dictionary<EventStoreSchedulingAppointmentType, int>> GetAverageCountForEveryStep()
        {
            return await CountViewedSteps();
        }
        
        private async Task<Dictionary<EventStoreSchedulingAppointmentType, int>> CountViewedSteps()
        {
            var dictionary = new Dictionary<EventStoreSchedulingAppointmentType, int>();
            await GetAverageViewForType(EventStoreSchedulingAppointmentType.SPECIALIZATION_VIEWED, dictionary);
            await GetAverageViewForType(EventStoreSchedulingAppointmentType.DOCTOR_NAME_VIEWED, dictionary);
            await GetAverageViewForType(EventStoreSchedulingAppointmentType.DATE_RANGE_VIEWED, dictionary);
            await GetAverageViewForType(EventStoreSchedulingAppointmentType.FREE_TERMS_VIEWED, dictionary);
            await GetAverageViewForType(EventStoreSchedulingAppointmentType.SCHEDULING_APPOINTMENT_FINISHED, dictionary);
            return dictionary;
        }
        
        private async Task GetAverageViewForType(EventStoreSchedulingAppointmentType type,
            Dictionary<EventStoreSchedulingAppointmentType, int> dictionary)
        {
            var stepViewedCount = await _unitOfWork.EventStoreSchedulingAppointmentRepository.GetAverageViewForType(type);
            var counter = await CheckIfEventsExistsForSchedulingAppointment();

            var averageStepView = stepViewedCount / counter;
            dictionary.Add(type,averageStepView);
        }
        
        private async Task<int> CheckIfEventsExistsForSchedulingAppointment()
        {
            var appointments = (List<Appointment>)await _unitOfWork.AppointmentRepository.GetAllAsync();
            var counter = appointments.Count;
            foreach (var appointment in appointments)
            {
                var eventNumber =
                    await _unitOfWork.EventStoreSchedulingAppointmentRepository.GetEventCountByAggregate(appointment.Id);
                if (eventNumber == 0)
                    --counter;
            }
            return counter;
        }
        
        public async Task<Dictionary<EventStoreSchedulingAppointmentType, double>> GetAverageTimeForEveryStep()
        {
            return await CountTimeForSteps();
        }
        
        private async Task<Dictionary<EventStoreSchedulingAppointmentType, double>> CountTimeForSteps()
        {
            var dictionary = new Dictionary<EventStoreSchedulingAppointmentType, double>();
            await GetAverageTimeForType(EventStoreSchedulingAppointmentType.SPECIALIZATION_VIEWED, dictionary);
            await GetAverageTimeForType(EventStoreSchedulingAppointmentType.DOCTOR_NAME_VIEWED, dictionary);
            await GetAverageTimeForType(EventStoreSchedulingAppointmentType.DATE_RANGE_VIEWED, dictionary);
            await GetAverageTimeForType(EventStoreSchedulingAppointmentType.FREE_TERMS_VIEWED, dictionary);
            await GetAverageTimeForType(EventStoreSchedulingAppointmentType.SCHEDULING_APPOINTMENT_FINISHED, dictionary);
            return dictionary;
        }

        private async Task GetAverageTimeForType(EventStoreSchedulingAppointmentType type, Dictionary<EventStoreSchedulingAppointmentType, double> dictionary)
        {
            var duration = await CountAverageTime(type);
            var durationInt = duration.TotalSeconds;
            var counter = await CheckIfEventsExistsForSchedulingAppointment();
            dictionary.Add(type,durationInt/counter);
        }
        private async Task<TimeSpan> CountAverageTime(EventStoreSchedulingAppointmentType type)
        {
            var duration = TimeSpan.Zero;
            var events = (List<EventStoreSchedulingAppointment>)await _unitOfWork.EventStoreSchedulingAppointmentRepository.GetAllAsync();
            for (int i = 0; i < events.Count - 1; i++)
            {
                if (events[i].Data == type)
                    duration += events[i + 1].CreatedAt - events[i].CreatedAt;
            }
            return duration;
        }

    }
}