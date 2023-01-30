namespace HospitalLibrary.SharedModel
{
    public class EmailOptions
    {
        public const string SendGridEmail = "EmailOptions";
        public string APIKey = System.Environment.GetEnvironmentVariable("Psw_Email_ApiKey");
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        
    }
}