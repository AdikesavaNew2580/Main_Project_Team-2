using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Employee_Travel_Booking_System_WebApplication.Services
{
    public class EmailService
    {
        private readonly string _smtpServer;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;
        private readonly string _fromEmail;

        public EmailService()
        {
            _smtpServer = "smtp.infinite.com";
            _port = 587;
            _username = "adikesava.thummala@infinite.com";
            _password = "password";
            _fromEmail = "noreply@infinite.com";
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var smtpClient = new SmtpClient(_smtpServer)
            {
                Port = _port,
                Credentials = new NetworkCredential(_username, _password),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_fromEmail),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }

    
}