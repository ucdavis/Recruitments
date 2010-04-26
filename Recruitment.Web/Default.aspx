<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="CAESDO.Recruitment.Web._Default" Trace="true" MasterPageFile="~/MasterPage.master" Title="Recruitments" %>

<asp:Content ContentPlaceHolderID="contentPlaceHolder1" runat="server">

    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/viewPositions.aspx">viewPositions</asp:HyperLink>
    <br /><br />
    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Authorized/UserManagement.aspx">UserManagement</asp:HyperLink>
    <br /><br />
    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Applicant/App.aspx">App (Applicant Tester)</asp:HyperLink>
    <br /><br />

</asp:Content>