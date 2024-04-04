using DataLayer.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using MimeKit;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Runtime;
using Org.BouncyCastle.Asn1.Pkcs;
using MailKit.Net.Smtp;

namespace DataLayer.Email
{
    public class EmailSenderServices : IEmailSender
    {
        private readonly MailSettings _mailSettings;

        public EmailSenderServices(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        //public Task SendEmailAsync(string email, string subject, string htmlMessage)
        //{
            
        //    SmtpClient client = new SmtpClient
        //    {
        //        Port = _mailSettings.port,
        //        Host = _mailSettings.server,
        //        EnableSsl = true,
                
        //        DeliveryMethod = SmtpDeliveryMethod.Network,
        //        UseDefaultCredentials = false,
        //        Credentials = new NetworkCredential(_mailSettings.username, _mailSettings.password)
        //    };
        //    string filePath = Directory.GetCurrentDirectory() + "\\Email\\Templates\\Verified.html";
        //    string emailTemplateText = File.ReadAllText(filePath);
        //    emailTemplateText = string.Format(emailTemplateText, email, DateTime.Today.Date.ToShortDateString());
        //    var mm = new MailMessage();
        //    mm.Subject = subject;
        //    mm.Body = htmlMessage; 
        //    mm.IsBodyHtml = true;
        //    mm.To.Add(new MailboxAddress("ConJob", email));
        //    mm.From = _mailSettings.senderemail;
            
        //    //return client.SendMailAsync("temp@wpoven.com", email, subject, htmlMessage);
        //    return client.SendMailAsync(mm);
        //}
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            try
            {
                using (MimeMessage emailMessage = new MimeMessage())
                {
                    MailboxAddress emailFrom = new MailboxAddress(_mailSettings.sendername, _mailSettings.senderemail);
                    emailMessage.From.Add(emailFrom);

                    MailboxAddress emailTo = new MailboxAddress(email, email);
                    emailMessage.To.Add(emailTo);

                    emailMessage.Subject = "Hello";

                    string filePath = Directory.GetCurrentDirectory() + "\\Email\\Templates\\Verified.html";
                    string emailTemplateText = File.ReadAllText(filePath);

                    emailTemplateText = string.Format(emailTemplateText, email, DateTime.Today.Date.ToShortDateString());

                    BodyBuilder emailBodyBuilder = new BodyBuilder();
                    emailBodyBuilder.HtmlBody = emailTemplateText;
                    emailBodyBuilder.TextBody = "Plain Text goes here to avoid marked as spam for some email servers.";

                    emailMessage.Body = emailBodyBuilder.ToMessageBody();

                    using (SmtpClient mailClient = new SmtpClient())
                    {
                        mailClient.Connect(_mailSettings.server, _mailSettings.port, MailKit.Security.SecureSocketOptions.StartTls);
                        mailClient.Authenticate(_mailSettings.username, _mailSettings.password);
                        await mailClient.SendAsync(emailMessage);
                        mailClient.Disconnect(true);
                    }
                }

            }
            catch (Exception ex)
            {
                // Exception Details

            }
        }
        
    }
}
