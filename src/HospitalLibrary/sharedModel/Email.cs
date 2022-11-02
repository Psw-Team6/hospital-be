namespace HospitalLibrary.sharedModel
{
    public class Email
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

        
    }
}