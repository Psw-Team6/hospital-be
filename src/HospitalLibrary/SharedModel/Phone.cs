using System;
using System.Text.RegularExpressions;
using HospitalLibrary.Common;

namespace HospitalLibrary.SharedModel
{
    public class Phone : ValueObject<Phone>
    {
        public string ToPhone { get; set; }
        
        public Phone(string toPhone)
        {
            if (IsPhoneValid(toPhone))
            {
                ToPhone = toPhone;
                
            }
            else
            {
                throw new NullReferenceException("Inavalid phone number");
            }
        }
        
        public bool IsPhoneValid(String text)
        {
            string patternStrict = @"\+?[0-9]{9,12}";

            Regex regexStrict = new Regex(patternStrict);

            return regexStrict.IsMatch(text);
        }
        
        protected override bool EqualsCore(Phone other)
        {
            return ToPhone == other.ToPhone;
        }

        protected override int GetHashCodeCore()
        {
            var hashCode = -1186395504;
            hashCode = hashCode * -1521134295 + ToPhone.GetHashCode();
            return hashCode;
        }
    }
}