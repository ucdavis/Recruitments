<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="CAESDO.Recruitment.Web.Review_Index" Title="Recruitments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:Panel ID="pnlCommitteeAccess" runat="server" Visible="false">

Committee Members:
    <br />
    <ul style="list-style-type:none;">
        <li><a href="ApplicationChoice.aspx">Individual Review</a></li>
        <li>Biographical Data Spreadsheet</li>
    </ul>

<br /><br />
</asp:Panel>
<asp:Panel ID="pnlFacultyAccess" runat="server" Visible="false">

Faculty Members:
    <br />
    <ul style="list-style-type:none;">
        <li><a href="ApplicationChoice.aspx">Review Candidates</a></li>
    </ul>

</asp:Panel>

</asp:Content>

