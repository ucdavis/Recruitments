using System.Net.Mail;
using CAESDO.Recruitment.Core.Domain;
using System.Security.Principal;
using System;

namespace CAESDO.Recruitment.BLL
{
    public class MessageBLL
    {
        public static IMessageGateWay messageGateway = new MessageGateWay();


    }

    public interface IMessageGateWay
    {
        bool SendMessage(string to, string from, string body, string subject, IPrincipal currentUser);
    }

    public class MessageGateWay : IMessageGateWay
    {
        /// <summary>
        /// Sends a message according to the given criteria
        /// </summary>
        /// <returns>true on success</returns>
        public bool SendMessage(string to, string from, string body, string subject, IPrincipal currentUser)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                MailMessage message = new MailMessage(from, to, subject, body);
                
                message.IsBodyHtml = true;
                
                client.Send(message);
            }
            catch
            {
                return false; //False when message sending doesn't succeed
            }

            //Message is sent, record the event
            using (var ts = new TransactionScope())
            {
                var messageTracking = new MessageTracking
                                          {
                                              To = to,
                                              From = from,
                                              Body = body,
                                              DateSent = DateTime.Now,
                                              SentBy = currentUser.Identity == null ? "N/A" : currentUser.Identity.Name
                                          };

                MessageTrackingBLL.EnsurePersistent(messageTracking);

                ts.CommitTransaction();
            }

            return true; //Success
        }
    }
}