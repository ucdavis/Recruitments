using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;
using System.Web.Configuration;
using System.Net;

public partial class Authorized_AdminIndex : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Roles.IsUserInRole("Admin"))
        {
            //If the user isn't an admin, hide admin only pages
            
            ibPendingpos.Visible = false; //View Pending Positions
            ibManageusers.Visible = false; //Manage Users
            ibClosedPos.Visible = false; //View closed positions
        }
    }
}