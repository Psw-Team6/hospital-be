using System;
using HospitalLibrary.Common;
using HospitalLibrary.SharedModel;

namespace HospitalLibrary.Rooms.Model
{
    public class RoomMerging : IAggregateRoot <Guid>
    {
        public Guid Id { get; set; }
        public Guid Room1Id { get; set; }
        public Room Room1 { get; set; }

        public Guid Room2Id { get; set; }
        public Room Room2 { get; set; }

        public DateRange DateRangeOfMerging { get; set; }

        public TimeSpan Duration{ get; set; }
        
        
        public void calculateDateRan()
        {
            Duration = TimeSpan.FromDays(Double.MaxValue);
            TimeSpan span = new TimeSpan();
        }
        
    }
}