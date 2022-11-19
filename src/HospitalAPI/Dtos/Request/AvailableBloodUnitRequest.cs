using HospitalLibrary.BloodUnits.Model;

namespace HospitalAPI.Dtos.Request
{
    public class AvailableBloodUnitRequest
    {
        public BloodType BloodType { get; set; }
        public int Amount { get; set; }
    }
}