using System;
using System.Text.RegularExpressions;
using HospitalLibrary.Common;
using RabbitMQ.Client;

namespace HospitalLibrary.SharedModel
{
    public class Jmbg : ValueObject<Jmbg>
    {

        public String Text { get; }
        
        public Jmbg(String text)
        {
            if (IsJmbgValid(text))
            {
                Text = text;
            }
            else
            {
                throw new NullReferenceException("Invalid jmbg");
            }
        }
        
        public bool IsJmbgValid(String text)
        {
            String patternStrict = "^[0-9]{13}$";

            Regex regexStrict = new Regex(patternStrict);

            return regexStrict.IsMatch(text);
        }

        protected override bool EqualsCore(Jmbg other)
        {
            return Text == other.Text;
        }

        protected override int GetHashCodeCore()
        {
            var hashCode = -1186395504;
            hashCode = hashCode * -1521134295 + Text.GetHashCode();
            return hashCode;
        }
    }
}