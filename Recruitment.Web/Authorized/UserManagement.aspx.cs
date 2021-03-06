using System;
using System.Web.UI.WebControls;
using CAESDO.Recruitment.BLL;
using CAESDO.Recruitment.Core.Domain;

namespace CAESDO.Recruitment.Web
{
    public partial class UserManagement : ApplicationPage
    {
        protected void Pre_Init(object sender, EventArgs e)
        {
            //Now we are using the Manage Users page to manage users
            Response.Redirect("ManageUsers");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSelectUser_Click(object sender, EventArgs e)
        {
            IButtonControl btn = (IButtonControl)sender; //get the calling button

            string selectedLoginID = btn.CommandArgument;

            //Now get the selected user's corresponding object
            User selectedUser = UserBLL.GetByLogin(selectedLoginID);

            //Fill in all User Info fields
            lblUserInfoName.Text = string.Format("{0} {1}", selectedUser.FirstName, selectedUser.LastName);

            lblUserInfoLoginID.Text = selectedLoginID;
            lblUserInfoEmployeeID.Text = selectedUser.EmployeeID;

            gViewUserUnits.DataSource = selectedUser.Units;
            gViewUserUnits.DataBind();

            gViewUserRoles.DataSource = CatbertManager.GetRolesByUser(selectedLoginID);
            gViewUserRoles.DataBind();

            //Update the panel with the newest information and show the modal popup
            updateUserInfo.Update();
            mpopupUserInfo.Show();
        }

        protected void GViewUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Grab the loginID for the selected user from the DataKeys
            GridView gview = (GridView)sender;

            string selectedLoginID = gview.SelectedDataKey["Login"].ToString();

            //Now get the selected user's corresponding object
            User selectedUser = UserBLL.GetByLogin(selectedLoginID);

            //Fill in all User Info fields
            lblUserInfoName.Text = string.Format("{0} {1}", selectedUser.FirstName, selectedUser.LastName);

            lblUserInfoLoginID.Text = selectedLoginID;
            lblUserInfoEmployeeID.Text = selectedUser.EmployeeID;

            gViewUserUnits.DataSource = selectedUser.Units;
            gViewUserUnits.DataBind();

            gViewUserRoles.DataSource = CatbertManager.GetRolesByUser(selectedLoginID);
            gViewUserRoles.DataBind();

            //Update the panel with the newest information and show the modal popup
            updateUserInfo.Update();
            mpopupUserInfo.Show();
        }

        protected void gViewUserUnits_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridView gv = (GridView)sender;

            //lblUserInfoEmployeeID.Text = gv.DataKeys[e.RowIndex].Value;
            //Remove the user from the desired unit
            bool success = CatbertManager.RemoveUserFromUnit(lblUserInfoLoginID.Text, (int)gv.DataKeys[e.RowIndex].Value);
            
            //update the grid
            User selectedUser = UserBLL.GetByLogin(lblUserInfoLoginID.Text);

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
            User selectedUser = UserBLL.GetByLogin(lblUserInfoLoginID.Text);

            gViewUserUnits.DataSource = selectedUser.Units;
            gViewUserUnits.DataBind();

            updateUserInfo.Update();
            mpopupUserInfo.Show();
        }

        protected void btnAddUserSearch_Click(object sender, EventArgs e)
        {
            //Show the gridview results
            gViewAddUserSearch.Visible = true;
            mpopupAddUser.Show();
        }
        
        protected void gViewAddUserSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView gview = (GridView)sender;
            
            //insert the new user
            string loginID = gview.SelectedDataKey["Login"] as string;
            
            int userID = -1;
            //Check to see if the user is already in CATBERT
            if (CatbertManager.VerifyUser(loginID) == false)
                userID = CatbertManager.InsertNewUser(loginID);
            else
                userID = 0;

            txtAddUserLoginID.Text = userID.ToString();
            //Add the user to the given role and unit
            DropDownList unit = gview.SelectedRow.FindControl("dlistAddUserUnits") as DropDownList;
            DropDownList role = gview.SelectedRow.FindControl("dlistAddUserRoles") as DropDownList;

            if (userID == -1 || unit == null || role == null) //make sure we found the dlists and the user was created.
            {
                lblAddUserStatus.ForeColor = System.Drawing.Color.Red;
                lblAddUserStatus.Text = "User " + loginID + " not added: Check your role and unit selection and try again";
            }
            else
            {
                //get the unit and role ID's, and add the user to those roles
                CatbertManager.AddUserToRole(loginID, int.Parse(role.SelectedValue));
                CatbertManager.AddUserToUnit(loginID, int.Parse(unit.SelectedValue));

                lblAddUserStatus.ForeColor = System.Drawing.Color.Green;
                lblAddUserStatus.Text = "User " + loginID + " successfully added";
            }

            gViewAddUserSearch.SelectedIndex = -1;
            gViewAddUserSearch.Visible = false; //hide the search grid

            lviewUsers.DataBind(); //rebind the user grid and update
            updateUserGrid.Update();

            updateAddUser.Update(); // update the add user panel

            mpopupAddUser.Show(); //keep up the popup
        }

        protected void btnUserInfoAddRole_Click(object sender, EventArgs e)
        {
            //Add the user to the desired unit
            bool success = CatbertManager.AddUserToRole(lblUserInfoLoginID.Text, int.Parse(dlistRoles.SelectedValue));
            
            //update the grid
            gViewUserRoles.DataSource = CatbertManager.GetRolesByUser(lblUserInfoLoginID.Text);
            gViewUserRoles.DataBind();

            updateUserInfo.Update();
            mpopupUserInfo.Show();
        }

        protected void gViewUserRoles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridView gv = (GridView)sender;

            //Remove the user from the desired unit
            bool success = CatbertManager.RemoveUserFromRole((int)gv.DataKeys[e.RowIndex].Value, lblUserInfoLoginID.Text);
            
            //update the grid
            gViewUserRoles.DataSource = CatbertManager.GetRolesByUser(lblUserInfoLoginID.Text);
            gViewUserRoles.DataBind();

            updateUserInfo.Update();
            mpopupUserInfo.Show();
        }
}
}