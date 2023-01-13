using System;
using HospitalLibrary.Common;
using HospitalLibrary.CustomException;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.SharedModel
{
    [Owned]
    public class Percentage:ValueObject<Percentage>
    {
        public int Value { get;  set; }

        public void Validate()
        {
            if (Value is < 0 or > 100)
            {
                throw new PercentageException("Value must be between 0 and 100");
            }
        }

        public Percentage(int value)
        {
            Value = value;
        }

        public Percentage()
        {
        }

        public override string ToString() => $"{Value}%";
        protected override bool EqualsCore(Percentage other)
        {
           return other.Value == Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}