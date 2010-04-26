using System;
using System.Collections.Generic;
using System.Text;
using CAESDO.Recruitment.Core.Domain;
using System.Web;

namespace CAESDO.Recruitment.BLL
{
    public class ApplicantBLL : GenericBLL<Applicant, int>
    {
        /// <summary>
        /// Return the current applicant
        /// </summary>
        /// <returns></returns>
        public static Applicant GetCurrent()
        {
            return daoFactory.GetApplicantDao().GetApplicantByEmail(HttpContext.Current.User.Identity.Name);
        }

        public static string GetNullSafeFullName(string FullName)
        {
            return string.IsNullOrEmpty(FullName.Trim()) ? "Name Not Yet Given" : FullName;
        }
    }
}
