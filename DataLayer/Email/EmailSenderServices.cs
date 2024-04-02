using DataLayer.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Email
{
    public class EmailSenderServices : IEmailSender
    {
        public void sendConfirmEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            SmtpClient client = new SmtpClient
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("tuanvynguyen51@gmail.com", "15022000Vy")
            };

            return client.SendMailAsync("tuanvynguyen51@gmail.com", email, subject, htmlMessage);
        }
    }
}
