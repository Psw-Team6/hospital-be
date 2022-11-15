using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.sharedModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace HospitalLibrary.Appointments.Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions _options;

        public EmailService(IOptions<EmailOptions> options)
        {
            _options = options.Value;
        }

        public async Task SendEmail(Email email)
        {
            var client = new SendGridClient(_options.APIKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_options.FromEmail, _options.FromName),
                Subject =email.Subject,
                PlainTextContent = email.PlainTextContent,
                HtmlContent = email.HtmlContent  
            };
            msg.AddTo(new EmailAddress(email.ToEmail));
            _ = await client.SendEmailAsync(msg);
            
        }

        public async Task SendCancelAppointmentEmail(Appointment appointment)
        {
            string toMail = appointment.Patient.Email;
            string subject = "Appointment has been canclled";
            string plainTextContent = "Your appointment at "+ appointment.Duration.From + " has been canclled.";
            string htmlContent = "<p> Your appointment at "+ appointment.Duration.From + " has been cancelled</p>";
            Email email = new Email(toMail, subject, plainTextContent, htmlContent);
            await SendEmail(email);
        }
        
        public async Task SendRescheduleAppointmentEmail(Appointment appointment)
        {
            string toMail = appointment.Patient.Email;
            string subject = "Appointment has been rescheduled ";
            string plainTextContent = "Your appointment has been rescheduled to " + appointment.Duration.From;
            string htmlContent = "<p> Your appointment has been rescheduled to " +appointment.Duration.From +"</p>";
            Email email = new Email(toMail, subject, plainTextContent, htmlContent);
            await SendEmail(email);
        }

    }
}