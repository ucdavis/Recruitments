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
using System.Collections.Generic;
using CAESDO.Recruitment.Core.Domain;

namespace CAESDO.Recruitment.Web
{
    public partial class App : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Step> AllSteps = new List<Step>();

            AllSteps.Add(new Step("Home", true, false, true));
            AllSteps.Add(new Step("Contact Information", false, true, true));
            AllSteps.Add(new Step("Education Information", true, false, true));
            AllSteps.Add(new Step("Home", true, false, true));
            AllSteps.Add(new Step("Contact Information", false, false, true));
            AllSteps.Add(new Step("Education Information", true, false, true));
            AllSteps.Add(new Step("Home", true, false, true));
            AllSteps.Add(new Step("Contact Information", false, false, true));
            AllSteps.Add(new Step("Education Information", true, false, true));

            rptSteps.DataSource = AllSteps;
            rptSteps.DataBind();
        }
    }

    /// <summary>
    /// Represents a Step object (including Home) to be used for tabbed browsing
    /// </summary>
    public class Step
    {
        private const string STR_AppCheckedImage = "~/Images/appmenuCheck.gif";
        private const string STR_AppUncheckedImage = "~/Images/appmenuX.gif";
        private const string STR_Selected = "selected";
        private const string STR_Unselected = "unselected";

        private bool _StepVisible;

        public bool StepVisible
        {
            get { return _StepVisible; }
            set { _StepVisible = value; }
        }

        private string _ImgURL;

        public string ImgURL
        {
            get { return _ImgURL; }
            set { _ImgURL = value; }
        }

        private string _StepName;

        public string StepName
        {
            get { return _StepName; }
            set { _StepName = value; }
        }

        private string _CSSClass;

        public string CSSClass
        {
            get { return _CSSClass; }
            set { _CSSClass = value; }
        }
        
        public Step()
        {

        }

        public Step(string stepName, bool completed, bool selected, bool stepVisible )
        {
            this.StepName = stepName;

            if (completed)
                this.ImgURL = STR_AppCheckedImage;
            else
                this.ImgURL = STR_AppUncheckedImage;

            if (selected)
                this.CSSClass = STR_Selected;
            else
                this.CSSClass = STR_Unselected;

            this.StepVisible = stepVisible;
        }
    }
}

