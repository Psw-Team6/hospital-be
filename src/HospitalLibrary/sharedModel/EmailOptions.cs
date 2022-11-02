namespace HospitalLibrary.sharedModel
{
    public class EmailOptions
    {
        public const string SendGridEmail = "EmailOptions";
        public string APIKey { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        
    }
}