using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using HospitalLibrary.Doctors.Model;

namespace HospitalLibrary.Rooms.Model
{
    public class Room
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public Doctor Doctor { get; set; }
        public List<RoomBed> Beds { get; set; }
        public Guid FloorId { get; set; }
        public Floor Floor { get; set; }
        public Guid BuildingId { get; set; }
        
        public Guid GRoomId { get; set; }

        public List<RoomBed> GetAllAvailableBeds()
        {
            return Beds.Where(bed => bed.IsFree).ToList();
        }
        public int GetNumberOfAvailableBeds()
        {
            return Beds.Count(bed => bed.IsFree);
        }
        public bool IsRoomAvailable()
        {
            return Beds.Where(bed => bed.IsFree).ToList().Count > 0;
        }

    }
}
