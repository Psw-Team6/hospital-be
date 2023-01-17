using System;
using System.Collections;
using System.Collections.Generic;
using HospitalLibrary.Common;
using HospitalLibrary.SharedModel;

namespace HospitalLibrary.Rooms.Model
{
    public class Renovation : IAggregateRoot <Guid>
    {
        public Guid Id { get; set; }
        public Guid Room1Id { get; set; }
        public Room Room1 { get; set; }

        public Guid Room2Id { get; set; }
        public Room Room2 { get; set; }
        public TimeSpan Duration{ get; set; }
        
        public IEnumerable<RoomSpliting>RoomMerging { get; set; }

       




        public void calculateDateRange()
        {
            Duration = TimeSpan.FromDays(Double.MaxValue);
            TimeSpan span = new TimeSpan();
        }


        public void findRoom()
        {
            Id = Room1Id;
            Id = Room2Id;
        }
    }
}