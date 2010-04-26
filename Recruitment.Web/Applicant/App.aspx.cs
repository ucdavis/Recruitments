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

namespace CAESDO.Recruitment.Web
{
    public partial class App : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Step> AllSteps = new List<Step>();

            AllSteps.Add(new Step("Home", true, true));
            AllSteps.Add(new Step("Contact Information", false, true));
            AllSteps.Add(new Step("Education Information", true, true));
            AllSteps.Add(new Step("Home", true, true));
            AllSteps.Add(new Step("Contact Information", false, true));
            AllSteps.Add(new Step("Education Information", true, true));
            AllSteps.Add(new Step("Home", true, true));
            AllSteps.Add(new Step("Contact Information", false, true));
            AllSteps.Add(new Step("Education Information", true, true));

            rptSteps.DataSource = AllSteps;
            rptSteps.DataBind();
   
            
        }
    }

    public class Step
    {
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


        public Step()
        {

        }

        public Step(string stepName, bool completed, bool stepVisible )
        {
            this.StepName = stepName;

            if (completed)
                this.ImgURL = "~/Images/appmenuCheck.gif";
            else
                this.ImgURL = "~/Images/appmenuX.gif";

            this.StepVisible = stepVisible;
        }
    }
}

