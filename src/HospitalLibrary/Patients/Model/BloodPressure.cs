using HospitalLibrary.Common;
using HospitalLibrary.Patients.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Patients.Model
{
    [Owned]
    public class BloodPressure:ValueObject<BloodPressure>
    {
        public int LowerPressure { get;private set;}
        public int UpperPressure { get;private set; }

        public BloodPressure(int lowerPressure, int upperPressure)
        {
            LowerPressure = lowerPressure;
            UpperPressure = upperPressure;
            Validate();
        }

        protected override bool EqualsCore(BloodPressure other)
        {
            return other.LowerPressure == LowerPressure
                   && other.UpperPressure == UpperPressure;
        }
        private void Validate()
        {
            if (LowerPressure < 0 || UpperPressure > 300)
            {
                throw new BloodPressureException("Invalid blood pressure values!");
            }

            if (LowerPressure > UpperPressure)
            {
                throw new BloodPressureException("Lower pressure is higher than upper!");
            }
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + LowerPressure.GetHashCode();
                hash = hash * 23 + UpperPressure.GetHashCode();
                return hash;
            }
        }
    }
}