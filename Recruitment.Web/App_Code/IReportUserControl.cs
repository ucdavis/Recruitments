using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;

namespace CAESDO.Recruitment.Web
{
    /// <summary>
    /// Defines an interface for report based user controls
    /// </summary>
    public interface IReportUserControl
    {
        void LoadReport(StringDictionary parameters);
    }
}