using System.Net.Mail;

namespace CAESDO.Recruitment.Core.Abstractions
{
    public interface IMessageGateway
    {
        void SendMessage(string from, string to, string subject, string body);
        void SendMessage(string from, string to, string bcc, string subject, string body);
    }

    public class MessageGateway : IMessageGateway
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
            SmtpClient client = new SmtpClient();
            MailMessage message = new MailMessage(from, to, subject, body);
            
            if (!string.IsNullOrEmpty(bcc))
            {
                message.Bcc.Add(bcc);
            }

            message.IsBodyHtml = true;

            client.Send(message);
        }
    }
}