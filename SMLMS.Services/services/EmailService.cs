using Microsoft.Extensions.Options;
using SMLMS.Helper.AppSetting;
using SMLMS.Model.DTO;
using SMLMS.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SMLMS.Services.services
{
    public class EmailService : IEmailService
    {
        private static SmtpDetails _smtpDetails;

        public EmailService(IOptions<SmtpDetails> smtpDetails)
        {
            _smtpDetails = smtpDetails.Value;

        }
        public  bool Send(EmailDto model)
        {
            bool emailSend = true;
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(model.ToEmail);
                mail.From = new MailAddress(_smtpDetails.FromEmail);
                mail.Subject = model.Subject;
                mail.IsBodyHtml = true;
                mail.Body = model.Body;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = _smtpDetails.Host;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(_smtpDetails.Username, _smtpDetails.Password);
                smtp.EnableSsl = true;
               // smtp.Send(mail);
            }
            catch
            {
                emailSend = false;
            }
            return emailSend;
        }
    }
}
