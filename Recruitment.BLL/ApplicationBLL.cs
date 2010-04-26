using System;
using System.Collections.Generic;
using System.Text;
using CAESDO.Recruitment.Core.Domain;

namespace CAESDO.Recruitment.BLL
{
    public class ApplicationBLL : GenericBLL<Application, int>
    {
        public static List<Application> GetByPositionID(int positionID)
        {
            if (positionID == 0)
                return null;

            Position position = PositionBLL.GetByID(positionID);

            return new List<Application>(position.AssociatedApplications);
        }

        public static List<Application> GetByPosition(Position position)
        {
            return daoFactory.GetApplicationDao().GetApplicationsByPosition(position);
        }

        public static List<Application> GetByApplicant(Profile applicantProfile, bool submitted)
        {
            return daoFactory.GetApplicationDao().GetApplicationsByApplicant(applicantProfile, submitted);
        }

        public static Applicant GetByEmail(string email)
        {
            return daoFactory.GetApplicantDao().GetApplicantByEmail(email);
        }
    }
}
