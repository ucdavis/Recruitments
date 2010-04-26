using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CAESDO.Recruitment.Core.DataInterfaces;
using CAESDO.Recruitment.Data;
using CAESDO.Recruitment.Core.Domain;
using System.Collections.Generic;

namespace CAESDO.Recruitment.Web
{
    public partial class _Default : ApplicationPage
    {
        public Application app11
        {
            get
            {
                if (Session["APP"] == null)
                {
                    IApplicationDao appDao = daoFactory.GetApplicationDao();
                    Session["APP"] = appDao.GetById(11, false);
                }

                return (Application)Session["APP"];
            }

            set
            {
                Session["APP"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            //Response.Write(Roles.IsUserInRole("Admin"));
            
            //IPositionsDao posDao = daoFactory.GetPositionsDao();
            //IPositionsDao posDao = daoFactory.GetPositionsDao();

            //IPositionDao posDao = daoFactory.GetPositionDao();

            //Position pos = posDao.GetById(15, false);

            //Department dept = new Department();

            //dept.AssociatedPosition = pos;
            //dept.DepartmentFIS = "ADNO";

            //IDepartmentDao deptDao = daoFactory.GetDepartmentDao();

            //if ( deptDao.GetByExample(dept).Count == 0 )
            //    deptDao.SaveOrUpdate(dept);            
          
            //List<Position> posList = posDao.GetAll();

            //IApplicantDao applicantDao = daoFactory.GetApplicantDao();

            //Applicant app = applicantDao.GetById(4, false);

            //IProfileDao proDao = daoFactory.GetProfileDao();

            //List<Profile> profiles = proDao.GetAll();

            //ISurveyDao surveyDao = daoFactory.GetSurveyDao();

            //Survey survey = surveyDao.GetById(1, false);
            
            if (!Page.IsPostBack)
            {
                //app11 = null;

                using (new NHibernateTransaction())
                {
                    Response.Write(app11.ID.ToString() + "   " + app11.SubmitDate.ToShortDateString() + "<br/>");
                }
            }
            else
            {
                Response.Write(app11.SubmitDate);

                app11.SubmitDate = DateTime.Now;

                using (NHibernateTransaction tx = new NHibernateTransaction())
                {
                    IApplicationDao aDao = daoFactory.GetApplicationDao();

                    aDao.SaveOrUpdate(app11);

                    Response.Write(tx.HasOpenTransaction);
                }
            }

            //Response.Write(app.ID.ToString() + "  " + app.SubmitDate.ToShortDateString() + "<br/>");
            //Response.Write(app.References[0].City);
            //IApplicantDao appDao = daoFactory.GetApplicantDao();

            //Applicant applicant = appDao.GetById(1, false);
            //applicant.Profiles.Add();
            //survey.Ethnicity = ethList[2];
            
            //surveyDao.SaveOrUpdate(survey);


            //IFileDao fDao = daoFactory.GetFileDao();

            //List<File> files = fDao.GetAll();

            //IUserDao uDao = daoFactory.GetUserDao();

            //User user = uDao.GetById(1, false);
            
        }
    }  
}