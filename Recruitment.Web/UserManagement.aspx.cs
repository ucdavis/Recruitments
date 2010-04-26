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
using CAESDO.Recruitment.Core.Domain;

namespace CAESDO.Recruitment.Web
{
    public partial class UserManagement : ApplicationPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Roles.GetRolesForUser("postit");
        }

        protected void GViewUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Grab the loginID for the selected user from the DataKeys
            GridView gview = (GridView)sender;

            string selectedLoginID = gview.SelectedDataKey["Login"].ToString();

            //Now get the selected user's corresponding object
            User selectedUser = daoFactory.GetUserDao().GetUserByLogin(selectedLoginID);

            //Fill in all User Info fields
            lblUserInfoName.Text = string.Format("{0} {1}", selectedUser.FirstName, selectedUser.LastName);

            lblUserInfoLoginID.Text = selectedLoginID;
            lblUserInfoEmployeeID.Text = selectedUser.EmployeeID;

            //foreach (string s in Roles.GetRolesForUser(selectedLoginID))
            //{
            //    lblUserInfoEmployeeID.Text += s;
            //}
            
            //gViewUserRoles.DataSource = Roles.GetRolesForUser(selectedLoginID);
            //gViewUserRoles.DataBind();

            gViewUserUnits.DataSource = selectedUser.Units;
            gViewUserUnits.DataBind();

            //Update the panel with the newest information and show the modal popup
            updateUserInfo.Update();
            mpopupUserInfo.Show();
        }

        protected void gViewUserUnits_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Remove the user from the desired role
            //CatbertManager.RemoveUserFromRole((int)e.Keys["ID"], lblUserInfoLoginID.Text);

            //update the grid
            User selectedUser = daoFactory.GetUserDao().GetUserByLogin(lblUserInfoLoginID.Text);

            gViewUserUnits.DataSource = selectedUser.Units;
            gViewUserUnits.DataBind();

            updateUserInfo.Update();
            mpopupUserInfo.Show();
        }

        protected void btnUserInfoAddUnit_Click(object sender, EventArgs e)
        {
            //Add the user to the desired unit
            bool success = CatbertManager.AddUserToUnit(lblUserInfoLoginID.Text, int.Parse(dlistUnits.SelectedValue));
            //update the grid
            User selectedUser = daoFactory.GetUserDao().GetUserByLogin(lblUserInfoLoginID.Text);

            gViewUserUnits.DataSource = selectedUser.Units;
            gViewUserUnits.DataBind();

            updateUserInfo.Update();
            mpopupUserInfo.Show();
        }
}
}