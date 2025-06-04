using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PracticeDomain.EmailService
{
  
       
        public class EmailService : IEmailService
        {

            private readonly SmtpClient _smtpClient;
            private readonly string _senderEmail;
            private readonly string _senderName;

        public EmailService(IConfiguration configuration)
        {
            var emailSettings = configuration.GetSection("EmailSettings");

            string host = emailSettings["MailServer"] ?? throw new ArgumentNullException(nameof(host));
            int port = int.TryParse(emailSettings["MailPort"], out int parsedPort) ? parsedPort : throw new ArgumentNullException(nameof(port));
            _senderEmail = emailSettings["SenderEmail"] ?? throw new ArgumentNullException(nameof(_senderEmail));
            _senderName = emailSettings["SenderName"] ?? throw new ArgumentNullException(nameof(_senderName));

            _smtpClient = new SmtpClient(host, port)
            {
                UseDefaultCredentials = true
            };
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_senderEmail, _senderName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(toEmail);

                await _smtpClient.SendMailAsync(mailMessage);
            }
        }
    }

