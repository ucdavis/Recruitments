using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using CAESDO.Recruitment.Core.Domain;
using System.Web.Configuration;
using CAESDO.Recruitment.Core.Abstractions;

namespace CAESDO.Recruitment.BLL
{
    public class PositionBLL : GenericBLL<Position, int>
    {
        public static List<Position> GetByStatus(bool Closed, bool AdminAccepted, bool? AllowApplications)
        {
            var userContext = System.Web.HttpContext.Current.User;

            return GetByStatusAndDepartment(Closed, AdminAccepted, AllowApplications, null, null, userContext); //no filtering by department
        }

        /// <summary>
        /// This overload doesn't take a usercontext (meaning we don't want to filter by user)
        /// </summary>
        public static List<Position> GetByStatusAndDepartment(bool Closed, bool AdminAccepted, bool? AllowApplications, string DepartmentFIS, string SchoolCode)
        {
            return GetByStatusAndDepartment(Closed, AdminAccepted, AllowApplications, DepartmentFIS, SchoolCode, null);
        }

        /// <summary>
        /// Get by school and/or department, and optionally filter by the user given in the usercontext
        /// </summary>
        public static List<Position> GetByStatusAndDepartment(bool Closed, bool AdminAccepted, bool? AllowApplications, string DepartmentFIS, string SchoolCode, IPrincipal userContext)
        {
            return daoFactory.GetPositionDao().GetAllPositionsByStatusAndDepartment(Closed, AdminAccepted, AllowApplications, DepartmentFIS, SchoolCode, userContext);
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
