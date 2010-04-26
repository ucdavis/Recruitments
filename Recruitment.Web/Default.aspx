﻿<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="CAESDO.Recruitment.Web._Default" Trace="false" MasterPageFile="~/MasterPage.master" Title="Recruitments" %>

<asp:Content ContentPlaceHolderID="contentPlaceHolder1" runat="server">
<div style="text-align:center;">
    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/def_login.jpg" PostBackUrl="~/login.aspx" Visible="false" />
    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/def_createacc.jpg" PostBackUrl="~/Login/CreateUser.aspx" Visible="false" />
    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/def_viewpositions.jpg" PostBackUrl="~/viewPositions.aspx" /><br />
    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Applicant/App.aspx?ApplicationID=11" Visible="false">App (Applicant Tester)</asp:HyperLink><br />
    <br />
    <asp:HyperLink ID="hlinkadmin" runat="server" NavigateUrl="~/Authorized/AdminIndex.aspx">Admin Functions</asp:HyperLink>
    <br /><br />
    <asp:HyperLink ID="hlinkApplicationsInProgress" runat="server" NavigateUrl="~/Applicant/ViewApplicationsInProgress.aspx" Visible="false" >View Applications In Progress</asp:HyperLink>
    <a href="Review/Index.aspx">Committee Review</a>
</div>
</asp:Content>