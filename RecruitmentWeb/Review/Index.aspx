<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="CAESDO.Recruitment.Web.Review_Index" Title="Recruitments" Codebehind="Index.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:Panel ID="pnlCommitteeAccess" runat="server" Visible="false">

Committee Members:
    <br />
    <ul style="list-style-type:none;">
        <li><a href="ApplicationChoice.aspx">Individual Review</a></li>
        <li><a href="BiographicalSelection.aspx">Biographical Data Spreadsheet</a></li>
    </ul>

<br /><br />
</asp:Panel>
<asp:Panel ID="pnlFacultyAccess" runat="server" Visible="false">

Faculty Members And Reviewers:
    <br />
    <ul style="list-style-type:none;">
        <li><a href="ApplicationChoice.aspx">Review Candidates</a></li>
    </ul>

</asp:Panel>

</asp:Content>

