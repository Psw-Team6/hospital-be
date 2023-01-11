using System;

namespace HospitalLibrary.Common
{
    public interface IAggregateRoot
    {
        public Guid Id { get; set; }
    }
}