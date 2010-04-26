<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="CAESDO.Recruitment.Web._Default" Trace="true" MasterPageFile="~/MasterPage.master" Title="Recruitments" %>

<asp:Content ContentPlaceHolderID="contentPlaceHolder1" runat="server">

    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/def_login.gif" />
    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/def_createacc.gif" />
    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/def_viewpositions.gif" PostBackUrl="~/viewPositions.aspx" />
    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/def_manageusers.gif" PostBackUrl="~/Authorized/UserManagement.aspx" /><br />
    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Applicant/App.aspx">App (Applicant Tester)</asp:HyperLink>
    <br /><br />

</asp:Content>