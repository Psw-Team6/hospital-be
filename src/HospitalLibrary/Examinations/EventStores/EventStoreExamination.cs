using System;
using HospitalLibrary.Common.EventSourcing;
using HospitalLibrary.Examinations.Model;

namespace HospitalLibrary.Examinations.EventStores
{
    public class EventStoreExamination : EventStore<Examination,EventStoreExaminationType>
    {
        public EventStoreExamination()
        {
            
        }

        public EventStoreExamination(Examination examination,DateTime createdAt, int version, int sequence, EventStoreExaminationType data, string name)
        {
            Aggregate = examination;
            CreatedAt = createdAt;
            Version = version;
            Sequence = sequence;
            Data = data;
            Name = name;
        }
    }
}