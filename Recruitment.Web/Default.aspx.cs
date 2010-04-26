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

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write(Roles.IsUserInRole("Admin"));
        IDaoFactory daoFactory = new NHibernateDaoFactory();

        IPositionsDao posDao = daoFactory.GetPositionsDao();

        List<Positions> posList = posDao.GetAll();

    }
}
