using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using AjaxControlToolkit;
using System.Collections.Generic;
using CAESDO.Recruitment.Core.Domain;
using CAESDO.Recruitment.Data;
using CAESDO.Recruitment.Core.DataInterfaces;
using System.Collections.Specialized;


/// <summary>
/// Summary description for RecruitmentService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
public class RecruitmentService : System.Web.Services.WebService
{
    private const string STR_Positions = "Positions";

    public IDaoFactory daoFactory
    {
        get { return new NHibernateDaoFactory(); }
    }

    public RecruitmentService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public CascadingDropDownNameValue[] GetPositions(string knownCategoryValues, string category)
    {
        List<Position> positions = daoFactory.GetPositionDao().GetAllPositionsByStatus(false);

        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();

        foreach (Position p in positions)
        {
            values.Add(new CascadingDropDownNameValue(p.TitleAndApplicationCount, p.ID.ToString()));
        }

        return values.ToArray();
    }

    [WebMethod]
    public CascadingDropDownNameValue[] GetApplications(string knownCategoryValues, string category)
    {
        StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();

        int PositionID;

        //Verify that we have a parent position and parse it to the positionID variable
        if (!kv.ContainsKey(STR_Positions) || !int.TryParse(kv[STR_Positions], out PositionID))
            return null;

        List<Application> applicants = daoFactory.GetApplicationDao().GetApplicationsByPosition(daoFactory.GetPositionDao().GetById(PositionID, false));

        foreach (Application app in applicants)
        {
            string name = string.IsNullOrEmpty(app.AssociatedProfile.FullName.Trim()) ? app.Email : app.AssociatedProfile.FullName;
            values.Add(new CascadingDropDownNameValue(name, app.ID.ToString()));
        }

        return values.ToArray();
    }

    [WebMethod]
    public CascadingDropDownNameValue[] GetFileTypes(string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();

        List<FileType> fileTypes = daoFactory.GetFileTypeDao().GetAll();

        foreach (FileType t in fileTypes)
        {
            values.Add(new CascadingDropDownNameValue(BreakCamelCase(t.FileTypeName), t.ID.ToString()));
        }

        return values.ToArray();
    }

    /// <summary>
    /// helper method to convert CamelCaseString to Camel Case String
    /// by inserting spaces
    /// </summary>
    /// <see cref="http://www.developer.com/net/asp/article.php/3609991"/>
    private string BreakCamelCase(string CamelString)
    {
        string output = string.Empty;
        bool SpaceAdded = true;

        for (int i = 0; i < CamelString.Length; i++)
        {
            if (CamelString.Substring(i, 1) ==
                CamelString.Substring(i, 1).ToLower())
            {
                output += CamelString.Substring(i, 1);
                SpaceAdded = false;
            }
            else
            {
                if (!SpaceAdded)
                {
                    output += " ";
                    output += CamelString.Substring(i, 1);
                    SpaceAdded = true;
                }
                else
                    output += CamelString.Substring(i, 1);
            }
        }
        return output;
    }
}

