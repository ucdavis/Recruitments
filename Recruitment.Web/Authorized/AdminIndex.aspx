<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminIndex.aspx.cs" Inherits="Authorized_AdminIndex" Title="Admin Home Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="text-align:center;">
    <asp:ImageButton ID="ibViewpositions" runat="server" ImageUrl="~/Images/def_viewpositions.jpg" PostBackUrl="viewPositionsAdmin.aspx" />
    <asp:ImageButton ID="ibPendingpos" runat="server" ImageUrl="~/Images/def_pendingpos.jpg" PostBackUrl="ViewPositionsPending.aspx" />
    <asp:ImageButton ID="ibCreatepos" runat="server" ImageUrl="~/Images/def_createpositions.jpg" PostBackUrl="PositionManagement.aspx" />
    <br />
    <asp:ImageButton ID="ibManageusers" runat="server" ImageUrl="~/Images/def_manageusers.jpg" PostBackUrl="UserManagement.aspx" />
    <asp:ImageButton ID="ibEmailtemplates" runat="server" ImageUrl="~/Images/def_emailtemplates.jpg" PostBackUrl="EmailTemplates.aspx" />
    <asp:ImageButton ID="ibUploadFiles" runat="server" ImageUrl="~/Images/def_upload.jpg"
        PostBackUrl="~/Authorized/UploadFiles.aspx" /><br />
    <a href="ApplicationReview.aspx?ApplicationID=11">Review Application 11</a><br /><br />
    <a href="ShortList.aspx">Short List</a><br /><br />
    <a href="committeeManagement.aspx?type=committee">Committee Management</a><br /><br />
    
</div>
           
</asp:Content>

