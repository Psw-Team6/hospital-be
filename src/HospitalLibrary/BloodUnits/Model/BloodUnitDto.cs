namespace HospitalLibrary.BloodUnits.Model
{
    public class BloodUnitDto
    {
        public BloodUnitDto(BloodType type, int amount)
        {
            this.BloodType = type;
            this.Amount = amount;
        }

        public BloodType BloodType { get; set; }
        public int Amount { get; set; }
    }
}