<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminIndex.aspx.cs" Inherits="Authorized_AdminIndex" Title="Admin Home Page" Trace="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="text-align:center;">
    <asp:ImageButton ID="ibViewpositions" runat="server" ImageUrl="~/Images/def_viewpositions.jpg" PostBackUrl="viewPositionsAdmin.aspx" />
    <asp:ImageButton ID="ibPendingpos" runat="server" ImageUrl="~/Images/def_pendingpos.jpg" PostBackUrl="ViewPositionsPending.aspx" />
    <asp:ImageButton ID="ibClosedPos" runat="server" ImageUrl="~/Images/def_closedposition.jpg" PostBackUrl="~/Authorized/viewPositionsClosed.aspx" />
    <asp:ImageButton ID="ibCreatepos" runat="server" ImageUrl="~/Images/def_createpositions.jpg" PostBackUrl="PositionManagement.aspx" />
    <asp:ImageButton ID="ibManageusers" runat="server" ImageUrl="~/Images/def_manageusers.jpg" PostBackUrl="UserManagement.aspx" />
    <asp:ImageButton ID="ibEmailtemplates" runat="server" ImageUrl="~/Images/def_emailtemplates.jpg" PostBackUrl="EmailTemplates.aspx" />
    <asp:ImageButton ID="ibApplicationsList" runat="server" ImageUrl="~/Images/def_applist.jpg" PostBackUrl="~/Authorized/ApplicationsList.aspx" />
    <asp:ImageButton ID="ibCommitteeList" runat="server" ImageUrl="~/Images/def_comlist.jpg" PostBackUrl="~/Authorized/committeeList.aspx" />
    <asp:ImageButton ID="ibCommitteeManagement" runat="server" ImageUrl="~/Images/def_commanage.jpg" PostBackUrl="~/Authorized/committeeManagement.aspx?type=committee" />
    <asp:ImageButton ID="ibReports" runat="server" ImageUrl="~/Images/def_reports.jpg" PostBackUrl="~/Authorized/reports.aspx" />
    <asp:ImageButton ID="ibUploadFiles" runat="server" ImageUrl="~/Images/def_upload.jpg" PostBackUrl="~/Authorized/UploadFiles.aspx" /><br />
    <br />
    <a href="unsolicitedReferences.aspx">Unsolicited References</a>
   
</div>
           
</asp:Content>

