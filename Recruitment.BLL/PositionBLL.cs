using System;
using System.Collections.Generic;
using System.Text;
using CAESDO.Recruitment.Core.Domain;
using System.Web.Configuration;

namespace CAESDO.Recruitment.BLL
{
    public class PositionBLL : GenericBLL<Position, int>
    {
        public static List<Position> GetByStatus(bool Closed, bool AdminAccepted, bool? AllowApplications)
        {
            return daoFactory.GetPositionDao().GetAllPositionsByStatus(Closed, AdminAccepted, AllowApplications);
        }

        public static List<Position> GetByStatusAndDepartment(bool Closed, bool AdminAccepted, bool? AllowApplications, string DepartmentFIS, string SchoolCode)
        {
            return daoFactory.GetPositionDao().GetAllPositionsByStatusAndDepartment(Closed, AdminAccepted, AllowApplications, DepartmentFIS, SchoolCode);
        }

        public static List<Position> GetAllPositionsByStatusForCommittee(bool Closed, bool AdminAccepted)
        {
            return daoFactory.GetPositionDao().GetAllPositionsByStatusForCommittee(Closed, AdminAccepted);
        }

        /// <summary>
        /// Sends a notification about the given position to the AppMailTo address
        /// </summary>
        /// <param name="position"></param>
        public static void SendNotificationEmail(Position position, string pendingPageURL)
        {
            StringBuilder notificationBody = new StringBuilder();

            notificationBody.Append("A new position has been created which needs to be accepted before it can receive applications.<br/><br/> ");
            notificationBody.Append("<a href='" + pendingPageURL + "'>Click here to view pending positions</a>");
            
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(WebConfigurationManager.AppSettings["emailFromEmail"], 
                                                                                    WebConfigurationManager.AppSettings["AppMailTo"],
                                                                                    "CAESDO Recruitments: Position '" + position.PositionTitle + "' Created", 
                                                                                    notificationBody.ToString());
            message.IsBodyHtml = true;
            client.Send(message);
        }

        public static bool VerifyPositionAccess(Position position)
        {
            return daoFactory.GetPositionDao().VerifyPositionAccess(position);
        }
    }
}
