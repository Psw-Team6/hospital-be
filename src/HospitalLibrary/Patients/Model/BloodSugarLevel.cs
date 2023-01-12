using HospitalLibrary.Common;
using HospitalLibrary.Patients.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Patients.Model
{
    [Owned]
    public class BloodSugarLevel:ValueObject<BloodSugarLevel>
    {
        public int SugarLevel { get;private set;}

        public BloodSugarLevel(int sugarLevel)
        {
            SugarLevel = sugarLevel;
            Validate();
        }

        private void Validate()
        {
            if (SugarLevel is < 0 or > 500)
            {
                throw new BloodPressureException("Invalid blood sugar value!");
            }
        }

        protected override bool EqualsCore(BloodSugarLevel other)
        {
            return other.SugarLevel == SugarLevel;
        }

        protected override int GetHashCodeCore()
        {
            return SugarLevel.GetHashCode();
        }
    }
}