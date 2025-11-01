using Emocare.Domain.Interfaces.Helper.Common;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;


namespace Emocare.Shared.Helpers.Common
{
    public class EmailHelper : IEmailHelper
    {
        private readonly IConfiguration _configuration;
        public EmailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject,string plainText, string htmlMessage)
        {
            var apiKey = _configuration["SendGrid:ApiKey"];
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress(
           _configuration["SendGrid:FromEmail"],
           _configuration["SendGrid:FromName"]
            );

            var to = new EmailAddress(toEmail);
            var mail = MailHelper.CreateSingleEmail(from, to, subject, plainText, htmlMessage);
            await client.SendEmailAsync(mail);
        }

    }
}
