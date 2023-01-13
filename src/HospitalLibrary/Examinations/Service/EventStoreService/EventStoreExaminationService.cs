using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Common.EventSourcing;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Examinations.EventStores;
using HospitalLibrary.Examinations.Model;

namespace HospitalLibrary.Examinations.Service.EventStoreService
{
    public class EventStoreExaminationService : IEventStoreExaminationService
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

        public async Task<EventStoreExamination> CreateEvents(DomainEvent<EventStoreExaminationType> @event, Examination examination)
        {
            var name = GetEventTypeName(@event);
            var newEventStore = new EventStoreExamination(examination, @event.CreatedAt,
                await GetVersion(examination.Id), await GetSequence(),@event.Event, name);
             return await _unitOfWork.EventStoreExaminationRepository.CreateAsync(newEventStore);
        }

        public static string GetEventTypeName(DomainEvent<EventStoreExaminationType> @event)
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

        public async Task<int> GetAverageStepCount()
        {
           var examinations = (List<Examination>) await _unitOfWork.ExaminationRepository.GetAllAsync();
           var events =  (List<EventStoreExamination>)await _unitOfWork.EventStoreExaminationRepository.GetAllAsync();
           var counter = examinations.Count;
           foreach (var examination in examinations)
           {
               var eventNumber =
                    await _unitOfWork.EventStoreExaminationRepository.GetEventCountByAggregate(examination.Id);
               if (eventNumber == 0)
                   --counter;
           }
           return events.Count / counter;
        }

        public async Task<double> GetAverageTime()
        {
            var duration = TimeSpan.Zero;
            var examinations = (List<Examination>) await _unitOfWork.ExaminationRepository.GetAllAsync();
            var counter = examinations.Count;
            foreach (var examination in examinations)
            {
               var timeSpan = await CountDurationExamination(examination);
               duration += timeSpan;
               if (timeSpan == TimeSpan.Zero)
                   --counter;
            }

            var durationDouble = duration.TotalSeconds;
            return durationDouble / counter;
        }

        private async Task<TimeSpan> CountDurationExamination(Examination examination)
        {
            var events = await _unitOfWork.EventStoreExaminationRepository.GetEventsByAggregate(examination.Id);
            if(events.Count == 0)
                return TimeSpan.Zero;
            var startTime = events.Min(@event => @event.CreatedAt);
            var finishTime = events.Max(@event => @event.CreatedAt);
            return finishTime - startTime;
        }

        public async Task<Dictionary<EventStoreExaminationType, int>> GetAverageCountForEveryStep()
        {
          return await CountViewedSteps();
        }

        private async Task<Dictionary<EventStoreExaminationType, int>> CountViewedSteps()
        {
            var dictionary = new Dictionary<EventStoreExaminationType, int>();
            await GetAverageViewForType(EventStoreExaminationType.SYMPTOMS_VIEWED, dictionary);
            await GetAverageViewForType(EventStoreExaminationType.ANAMNESIS_VIEWED, dictionary);
            await GetAverageViewForType(EventStoreExaminationType.PRESCRIPTION_VIEWED, dictionary);
            await GetAverageViewForType(EventStoreExaminationType.EXAMINATION_INFO_VIEWED, dictionary);
            await GetAverageViewForType(EventStoreExaminationType.EXAMINATION_FINISHED, dictionary);
            return dictionary;
        }

        private async Task GetAverageViewForType(EventStoreExaminationType type,
            Dictionary<EventStoreExaminationType, int> dictionary)
        {
            var stepViewedCount = await _unitOfWork.EventStoreExaminationRepository.GetAverageViewForType(type);
            var counter = await CheckIfEventsExistsForExamination();

            var averageStepView = stepViewedCount / counter;
            dictionary.Add(type,averageStepView);
        }

        private async Task<int> CheckIfEventsExistsForExamination()
        {
            var examinations = (List<Examination>)await _unitOfWork.ExaminationRepository.GetAllAsync();
            var counter = examinations.Count;
            foreach (var examination in examinations)
            {
                var eventNumber =
                    await _unitOfWork.EventStoreExaminationRepository.GetEventCountByAggregate(examination.Id);
                if (eventNumber == 0)
                    --counter;
            }
            return counter;
        }

        public async Task<Dictionary<EventStoreExaminationType, double>> GetAverageTimeForEveryStep()
        {
            return await CountTimeForSteps();
        }
        private async Task<Dictionary<EventStoreExaminationType, double>> CountTimeForSteps()
        {
            var dictionary = new Dictionary<EventStoreExaminationType, double>();
            await GetAverageTimeForType(EventStoreExaminationType.SYMPTOMS_VIEWED, dictionary);
            await GetAverageTimeForType(EventStoreExaminationType.ANAMNESIS_VIEWED, dictionary);
            await GetAverageTimeForType(EventStoreExaminationType.PRESCRIPTION_VIEWED, dictionary);
            await GetAverageTimeForType(EventStoreExaminationType.EXAMINATION_INFO_VIEWED, dictionary);
            await GetAverageTimeForType(EventStoreExaminationType.EXAMINATION_FINISHED, dictionary);
            return dictionary;
        }

        private async Task GetAverageTimeForType(EventStoreExaminationType type, Dictionary<EventStoreExaminationType, double> dictionary)
        {
            var duration = await CountAverageTime(type);
            var durationInt = duration.TotalSeconds;
            var counter = await CheckIfEventsExistsForExamination();
            dictionary.Add(type,durationInt/counter);
        }

        private async Task<TimeSpan> CountAverageTime(EventStoreExaminationType type)
        {
            var duration = TimeSpan.Zero;
            var events = (List<EventStoreExamination>)await _unitOfWork.EventStoreExaminationRepository.GetAllAsync();
            for (int i = 0; i < events.Count - 1; i++)
            {
                if (events[i].Data == type)
                    duration += events[i + 1].CreatedAt - events[i].CreatedAt;
            }
            return duration;
        }

        public async Task<Dictionary<string,int>> GetStepsForMedicalBranch()
        {
            return await CountExaminationStepsSpecialization();
        }

        private async Task<Dictionary<string, int>> CountExaminationStepsSpecialization()
        {
            var dictionary = new Dictionary<string, int>();
            var specializations = await _unitOfWork.SpecializationsRepository.GetAllAsync();
            foreach (var specialization in specializations)
            {
                await GetAverageStepsSpecialization(specialization, dictionary);
            }
            return dictionary;
        }

        private async Task GetAverageStepsSpecialization(Specialization specialization, Dictionary<string, int> dictionary)
        {
            var specializationExaminations =
                await _unitOfWork.ExaminationRepository.GetExaminationsBySpecializations(specialization.Id);
            var specializationEvents =
                await _unitOfWork.EventStoreExaminationRepository.GetEventsBySpecialization(specialization.Id);
            if (specializationEvents.Count == 0 || specializationEvents.Count == 0)
            {
                dictionary.Add(specialization.Name,0);
                return;
            }
            dictionary.Add(specialization.Name,specializationEvents.Count/specializationExaminations.Count);
        }

        public async Task<Dictionary<string,double>> GetAverageTimeForMedicalBranch()
        {
            return await CountAverageTimeMedicalBranch();
        }

        private async Task<Dictionary<string, double>> CountAverageTimeMedicalBranch()
        {
            var dictionary = new Dictionary<string, double>();
            var specializations = await _unitOfWork.SpecializationsRepository.GetAllAsync();
            foreach (var specialization in specializations)
            {
                await CountAverageTimeSpecialization(specialization, dictionary);
            }

            return dictionary;
        }

        private async Task CountAverageTimeSpecialization(Specialization specialization, Dictionary<string, double> dictionary)
        {
            var duration = TimeSpan.Zero;
            var specializationExaminations =
                await _unitOfWork.ExaminationRepository.GetExaminationsBySpecializations(specialization.Id);
            foreach (var examination in specializationExaminations)
            {
                 duration += await CountDurationExamination(examination);
            }

            if (duration == TimeSpan.Zero || specializationExaminations.Count == 0)
            {
                dictionary.Add(specialization.Name,0);
                return;
            }
            dictionary.Add(specialization.Name,duration.TotalSeconds/specializationExaminations.Count);
        }
    }
}