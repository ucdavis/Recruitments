using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using AjaxControlToolkit;
using CAESDO.Recruitment.Core.Domain;
using System.Collections.Generic;
using CAESDO.Recruitment.Core.DataInterfaces;
using CAESDO.Recruitment.Data;
using System.Collections.Specialized;
using System.Web.Script.Services;


/// <summary>
/// Summary description for RecruitmentCommitteeService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
public class RecruitmentCommitteeService : System.Web.Services.WebService
{
    private const string STR_Positions = "Positions";
    private const string STR_Applications = "Applications";

    public IDaoFactory daoFactory
    {
        get { return new NHibernateDaoFactory(); }
    }

    public RecruitmentCommitteeService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public CascadingDropDownNameValue[] GetPositionsForCommittee(string knownCategoryValues, string category)
    {
        List<Position> positions = daoFactory.GetPositionDao().GetAllPositionsByStatusForCommittee(false, true);

        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();

        foreach (Position p in positions)
        {
            values.Add(new CascadingDropDownNameValue(p.TitleAndApplicationCount, p.ID.ToString()));
        }

        return values.ToArray();
    }

    /// <summary>
    /// Return only positions where the current user is in the committee (not faculty)
    /// </summary>
    [WebMethod]
    public CascadingDropDownNameValue[] GetPositionsForCommitteeOnly(string knownCategoryValues, string category)
    {
        List<Position> positions = daoFactory.GetPositionDao().GetAllPositionsByStatusForCommittee(false, true);

        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();

        foreach (Position p in positions)
        {
            foreach (CommitteeMember committeeAccess in p.CommitteeMembers)
            {
                if ( committeeAccess.DepartmentMember.LoginID == User.Identity.Name)
                {
                    if (committeeAccess.MemberType.ID == (int)MemberTypes.CommitteeChair || committeeAccess.MemberType.ID == (int)MemberTypes.CommitteeMember)
                    {
                        //If the user is in the position committee as a committee member, add the position to the values array
                        values.Add(new CascadingDropDownNameValue(p.TitleAndApplicationCount, p.ID.ToString()));

                        break;
                    }
                }
            }
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

}

