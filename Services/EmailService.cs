using BookStoreWeb.Models.Configs;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWeb.Services
{
    public class EmailService
    {
        private readonly MailSettings _Settings;
        public EmailService(IConfiguration configuration)
        {
            _Settings = configuration.GetSection("MailSettings").Get<MailSettings>();
        }

        public async Task SendMailAsync(string emailTo, string subject, string content)
        {
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(_Settings.UserName, _Settings.Sender);
            email.From.Add(new MailboxAddress(_Settings.UserName, _Settings.Sender));
            email.To.Add(MailboxAddress.Parse(emailTo));
            email.Subject = subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = content;
            email.Body = builder.ToMessageBody();

            var smtp = new SmtpClient();

            try
            {
                smtp.Connect(_Settings.SmtpServer, _Settings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_Settings.Sender, _Settings.Password);
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {

            }

            smtp.Disconnect(true);
        }
    }
}
