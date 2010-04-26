﻿using System.Net.Mail;

namespace CAESDO.Recruitment.Core.Abstractions
{
    public class DevMessageGateway : IMessageGateway
    {
        /// <summary>
        /// Use email to send out the message
        /// </summary>
        public void SendMessage(string from, string to, string subject, string body)
        {
            const string devFrom = "tepomroy@ucdavis.edu";
            const string devTo = "srkirkland@ucdavis.edu";

            SmtpClient client = new SmtpClient();
            MailMessage message = new MailMessage(devFrom, devTo, subject, body);

            message.IsBodyHtml = true;

            client.Send(message);
        }
    }    
}
