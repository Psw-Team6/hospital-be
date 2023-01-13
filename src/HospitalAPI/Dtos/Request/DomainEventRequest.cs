using System;
using HospitalLibrary.Examinations.EventStores;

namespace HospitalAPI.Dtos.Request
{
    public class DomainEventRequest
    {
        public DateTime CreatedAt { get; set; }
        public  EventStoreExaminationType Event { get; set; }
    }
}