﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CoreLayer.Utilities.MailUtilities
{
    public class OutlookMailManager : IMailService
    {
        readonly IConfiguration _configuration;

        public OutlookMailManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool SendMail(string mail, string subject, string message)
        {
            try
            {
                SmtpClient smtp = new();
                var senderMail = _configuration["MailInfo:Mail"];
                var senderPassword = _configuration["MailInfo:Password"];
                smtp.Credentials = new System.Net.NetworkCredential(senderMail, senderPassword);
                smtp.Port = 587;
                smtp.Host = "smtp-mail.outlook.com";
                smtp.EnableSsl = true;
                MailMessage eMail = new();
                eMail.From = new MailAddress(_configuration["MailInfo:Mail"]);
                eMail.To.Add(mail);
                eMail.Subject = subject;
                eMail.IsBodyHtml = true;
                eMail.Body = message;
                smtp.SendAsync(eMail, (object)eMail);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SendMails(string[] mail, string subject, string message)
        {
            try
            {
                SmtpClient smtp = new();
                var senderMail = _configuration["MailInfo:Mail"];
                var senderPassword = _configuration["MailInfo:Password"];
                smtp.Credentials = new System.Net.NetworkCredential(senderMail, senderPassword);
                smtp.Port = 587;
                smtp.Host = "smtp-mail.outlook.com";
                smtp.EnableSsl = true;
                MailMessage eMail = new();
                eMail.From = new MailAddress(_configuration["MailInfo:Mail"]);
                for (int i = 0; i < mail.Length; i++)
                    eMail.To.Add(mail[i]);
                eMail.Subject = subject;
                eMail.IsBodyHtml = true;
                eMail.Body = message;
                smtp.SendAsync(eMail, (object)eMail);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
