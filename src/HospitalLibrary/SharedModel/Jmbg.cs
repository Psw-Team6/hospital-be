using System;
using System.Text.RegularExpressions;
using HospitalLibrary.Common;
using HospitalLibrary.CustomException;
using Microsoft.EntityFrameworkCore.Storage;
using RabbitMQ.Client;

namespace HospitalLibrary.SharedModel
{
    public class Jmbg : ValueObject<Jmbg>
    {

        public string Text { get; }

        public Jmbg(string text)
        {
            Text = text;
        }

        public void ValidateJmbg()
        {
            if (!IsJmbgValid(Text))
            {
                throw new JmbgException("Invalid jmbg");
            }
        }
        
        private bool IsJmbgValid(string text)
        {
            var patternStrict = "[0-9]{13}$";
            
            Regex regexStrict = new Regex(patternStrict);
            
            return regexStrict.IsMatch(text);
        }

        protected override bool EqualsCore(Jmbg other)
        {
            return Text == other.Text;
        }

        protected override int GetHashCodeCore()
        {
            return Text.GetHashCode();;
        }
    }
}