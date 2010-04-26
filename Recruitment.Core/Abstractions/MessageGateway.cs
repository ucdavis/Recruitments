using System.Net.Mail;

namespace CAESDO.Recruitment.Core.Abstractions
{
    public interface IMessageGateway
    {
        void SendMessage(string from, string to, string subject, string body);
    }

    public class MessageGateway : IMessageGateway
    {
        /// <summary>
        /// Use email to send out the message
        /// </summary>
        public void SendMessage(string from, string to, string subject, string body)
        {
            SmtpClient client = new SmtpClient();
            MailMessage message = new MailMessage(from, to, subject, body);

            message.IsBodyHtml = true;

            client.Send(message);
        }
    }
}
