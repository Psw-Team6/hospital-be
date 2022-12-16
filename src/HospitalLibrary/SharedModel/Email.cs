using System.Text.RegularExpressions;
using HospitalLibrary.Common;

namespace HospitalLibrary.SharedModel
{
    public class Email : ValueObject<Email>
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string PlainTextContent { get; set; }
        public string HtmlContent { get; set; }
        public Email(string toEmail, string subject, string plainTextContent, string htmlContent)
        {
            ToEmail = toEmail;
            Subject = subject;
            PlainTextContent = plainTextContent;
            HtmlContent = htmlContent;
        }
        
        public bool IsEmailAddressValid()
        {
            string patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+"

                                   + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"

                                   + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"

                                   + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"

                                   + @"[a-zA-Z]{2,}))$";

            Regex regexStrict = new Regex(patternStrict);

            return regexStrict.IsMatch(ToEmail);
        }
        
        protected override bool EqualsCore(Email other)
        {
            return ToEmail == other.ToEmail && PlainTextContent == other.PlainTextContent && HtmlContent == other.HtmlContent;
        }
        
        protected override int GetHashCodeCore()
        {
            var hashCode = -1186395504;
            hashCode = hashCode * -1521134295 + ToEmail.GetHashCode();
            hashCode = hashCode * -1521134295 + Subject.GetHashCode();
            hashCode = hashCode * -1521134295 + PlainTextContent.GetHashCode();
            hashCode = hashCode * -1521134295 + HtmlContent.GetHashCode();
            return hashCode;
        }
    }
}