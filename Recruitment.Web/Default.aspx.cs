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

        }
    }  
}