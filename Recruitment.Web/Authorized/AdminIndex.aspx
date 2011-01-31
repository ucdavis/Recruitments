<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminIndex.aspx.cs" Inherits="Authorized_AdminIndex" Title="Admin Home Page" Trace="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="text-align:center;">
    <asp:ImageButton ID="ibViewpositions" runat="server" ImageUrl="~/Images/def_viewpositions.jpg" PostBackUrl="viewPositionsAdmin.aspx" AlternateText="View Positions" />
    <asp:ImageButton ID="ibPendingpos" runat="server" ImageUrl="~/Images/def_pendingpos_admin.jpg" PostBackUrl="ViewPositionsPending.aspx" AlternateText="Pending Positions" />
    <asp:ImageButton ID="ibClosedPos" runat="server" ImageUrl="~/Images/def_closedposition_admin.jpg" PostBackUrl="~/Authorized/viewPositionsClosed.aspx" AlternateText="Closed Positions" />
    <asp:ImageButton ID="ibCreatepos" runat="server" ImageUrl="~/Images/def_createpositions.jpg" PostBackUrl="PositionManagement.aspx" AlternateText="Create Positions" />
    <asp:ImageButton ID="ibManageusers" runat="server" ImageUrl="~/Images/def_manageusers_admin.jpg" PostBackUrl="ManageUsers.aspx" AlternateText="Manage Users" />
    <asp:ImageButton ID="ibEmailtemplates" runat="server" ImageUrl="~/Images/def_emailtemplates.jpg" PostBackUrl="EmailTemplates.aspx" AlternateText="Reminder Emails" />
    <asp:ImageButton ID="ibUnsolicited" runat="server" ImageUrl="~/Images/def_unsolicited.jpg" PostBackUrl="~/Authorized/UnsolicitedReferences.aspx" AlternateText="Unsolicited References" />    
    <asp:ImageButton ID="ibApplicationsList" runat="server" ImageUrl="~/Images/def_applist.jpg" PostBackUrl="~/Authorized/ApplicationsList.aspx" AlternateText="Application List" />
    <asp:ImageButton ID="ibCommitteeList" runat="server" ImageUrl="~/Images/def_comlist.jpg" PostBackUrl="~/Authorized/committeeList.aspx" AlternateText="Committee List" />
    <asp:ImageButton ID="ibCommitteeManagement" runat="server" ImageUrl="~/Images/def_commanage.jpg" PostBackUrl="~/Authorized/committeeManagement.aspx?type=committee" AlternateText="Committee Management" />
    <asp:ImageButton ID="ibReports" runat="server" ImageUrl="~/Images/def_reports.jpg" PostBackUrl="~/Authorized/reports.aspx" AlternateText="Reports" />
    <asp:ImageButton ID="ibUploadFiles" runat="server" ImageUrl="~/Images/def_upload.jpg" PostBackUrl="~/Authorized/UploadFiles.aspx" AlternateText="Upload Files" />
    <br />
    <br /> 
    <h3><a href="../Help/V2Updates.aspx" style="display:inline; padding:0.75em; border:1px solid #C6C3C6; background-color:#F7F7F7; text-decoration:none;">Check out the new features in Recruitments version 2</a></h3>
</div>
           
</asp:Content>

