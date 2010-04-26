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

namespace CAESDO.Recruitment.Web
{
    public partial class Authorized_ShortList : ApplicationPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // TODO: Create a shortlist table in Recruitments which maps one Position to many applications (use PosXDept as your guide)
            //      Add an IList of Applications to the positions class, and enable cascading.

            // TODO: Loop through all of the checked applicants in the grid and add them to the positions.shortlistApplications list 
            //          (don't forget to clear the list first if there was a previous short list created).
        }
    }
}