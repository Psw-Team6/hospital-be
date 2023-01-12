using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Common.EventSourcing;
using HospitalLibrary.Examinations.EventStores;
using HospitalLibrary.Examinations.Model;

namespace HospitalLibrary.Examinations.Service.EventStoreService
{
    public class EventStoreExaminationService : IEventStoreService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventStoreExaminationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateEventStore(Examination examination,List<DomainEvent<EventStoreExaminationType>> events)
        {
            foreach (var @event in events)
            {
                await CreateEvents(@event,examination);
                await _unitOfWork.CompleteAsync();
            }
        }

        private async Task CreateEvents(DomainEvent<EventStoreExaminationType> @event, Examination examination)
        {
            var name = GetEventTypeName(@event);
            var newEventStore = new EventStoreExamination(examination, @event.CreatedAt,
                await GetVersion(examination.Id), await GetSequence(),@event.Event, name);
            await _unitOfWork.EventStoreExaminationRepository.CreateAsync(newEventStore);
        }

        private static string GetEventTypeName(DomainEvent<EventStoreExaminationType> @event)
        {
            if (@event.Event == EventStoreExaminationType.SYMPTOMS_VIEWED)
                return  "symptoms viewed event";
            if (@event.Event == EventStoreExaminationType.ANAMNESIS_VIEWED)
                return  "anamnesis viewed event";
            if (@event.Event == EventStoreExaminationType.PRESCRIPTION_VIEWED)
                return  "prescription viewed event";
            if (@event.Event == EventStoreExaminationType.EXAMINATION_INFO_VIEWED)
                return "examination info viewed event";
            return  "examination finished event";
        }

        public async Task<int> GetVersion(Guid aggregateId)
        {
            var version = await _unitOfWork.EventStoreExaminationRepository.GetVersionCount(aggregateId);
            if (version != 0) 
                return ++version;
            return 1;
        }

        public async Task<int> GetSequence()
        {
            var sequence = await _unitOfWork.EventStoreExaminationRepository.GetSequenceCount();
            if (sequence != 0) 
                return ++sequence;
            return 1;
        }
    }
}