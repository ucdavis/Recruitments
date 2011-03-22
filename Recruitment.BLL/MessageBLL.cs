using CAESDO.Recruitment.Core.Abstractions;
using CAESDO.Recruitment.Core.Domain;
using System.Security.Principal;
using System;

namespace CAESDO.Recruitment.BLL
{
    public class MessageBLL
    {
        public static IMessageGateway MessageGateway = new MessageGateway();
        public static IPrincipal UserContext = new UserContext();
        
        /// <summary>
        /// Sends a message according to the given criteria
        /// </summary>
        /// <returns>true on success</returns>
        public static bool SendMessage(string from, string to, string subject, string body)
        {
            return SendMessage(from, to, null, subject, body);
        }

        /// <summary>
        /// Sends a message according to the given criteria
        /// </summary>
        /// <returns>true on success</returns>
        public static bool SendMessage(string from, string to, string bcc, string subject, string body)
        {
            try
            {
                MessageGateway.SendMessage(from, to, bcc, subject, body);
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
                    SentBy = UserContext.Identity == null ? "N/A" : UserContext.Identity.Name
                };

                MessageTrackingBLL.EnsurePersistent(messageTracking);

                ts.CommitTransaction();
            }

            return true; //Success

        }
    }
}