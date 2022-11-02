using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.SendMail.Services
{
    public class EmailService:IEmailService
    {

        private readonly EmailOptions _options;

        public EmailService(IOptions<EmailOptions> options) => _options = options.Value;

        public async Task SendEmail(Email email)
        {
            var client = new SendGridClient(_options.APIKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_options.FromEmail, _options.FromName),
                Subject = email.Subject,
                PlainTextContent = email.PlainTextContent,
                HtmlContent = email.HtmlContent
            };
            msg.AddTo(new EmailAddress(email.ToEmail));
            _ = await client.SendEmailAsync(msg);

        }



    }
}
