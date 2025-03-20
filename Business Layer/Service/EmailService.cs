using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Business_Layer.Service
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string resetToken)
        {
            var smtpSettings = _configuration.GetSection("SMTP");

            var smtpClient = new SmtpClient(smtpSettings["Host"])
            {
                Port = int.Parse(smtpSettings["Port"]),
                Credentials = new NetworkCredential(smtpSettings["Username"], smtpSettings["Password"]),
                EnableSsl = true
            };

            string resetLink = $"https://yourfrontend.com/reset-password?token={resetToken}";

            string body = $"Your password reset token is: {resetToken}\n\nClick the link below to reset your password:\n{resetLink}";

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpSettings["SenderEmail"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };

            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
        public bool SendResetPasswordEmail(string toEmail, string resetToken)
        {
            try
            {
                var smtpSettings = _configuration.GetSection("SMTP");

                var smtpClient = new SmtpClient(smtpSettings["Host"])
                {
                    Port = int.Parse(smtpSettings["Port"]),
                    Credentials = new NetworkCredential(smtpSettings["Username"], smtpSettings["Password"]),
                    EnableSsl = true
                };

                string resetLink = $"https://yourfrontend.com/reset-password?token={resetToken}";

                string body = $"Your password reset token is: {resetToken}\n\nClick the link below to reset your password:\n{resetLink}";

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpSettings["SenderEmail"]),
                    Subject = "Password Reset Request",
                    Body = body,
                    IsBodyHtml = false
                };

                mailMessage.To.Add(toEmail);

                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email sending failed: {ex.Message}");
                return false;
            }
        }
    }
}
