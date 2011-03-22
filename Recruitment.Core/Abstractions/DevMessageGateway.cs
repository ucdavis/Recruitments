using System;
using System.Net.Mail;

namespace CAESDO.Recruitment.Core.Abstractions
{
    public class DevMessageGateway : IMessageGateway
    {
        /// <summary>
        /// Use email to send out the message
        /// </summary>
        public void SendMessage(string from, string to, string subject, string body)
        {
            SendMessage(from, to, null, subject, body);
        }

        public void SendMessage(string from, string to, string bcc, string subject, string body)
        {
            const string devFrom = "tepomroy@ucdavis.edu";
            const string devTo = "srkirkland@ucdavis.edu";

            SmtpClient client = new SmtpClient();
            MailMessage message = new MailMessage(devFrom, devTo, subject, body);

            if (!string.IsNullOrEmpty(bcc))
            {
                message.Bcc.Add(bcc);
            }

            message.IsBodyHtml = true;

            client.Send(message);
        }
    }    
}
