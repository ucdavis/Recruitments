﻿<%@ Page Language="C#" AutoEventWireup="true" Inherits="CAESDO.Recruitment.Web._Default" Trace="false" MasterPageFile="~/MasterPage.master" Title="Recruitments" Codebehind="Default.aspx.cs" %>

<asp:Content ContentPlaceHolderID="contentPlaceHolder1" runat="server">
<div style="text-align:center;">
    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/def_login.jpg" PostBackUrl="~/login.aspx" Visible="false" />
    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/def_createacc.jpg" PostBackUrl="~/Login/CreateUser.aspx" Visible="false" />
    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/def_viewpositions.jpg" PostBackUrl="~/viewPositions.aspx" AlternateText="View Positions" /><br />
    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Applicant/App.aspx?ApplicationID=11" Visible="false">App (Applicant Tester)</asp:HyperLink><br />
    
    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/def_smadmin.jpg" PostBackUrl="~/Authorized/AdminIndex.aspx" AlternateText="Admin Functions" />
    <br />
    <asp:HyperLink ID="hlinkApplicationsInProgress" runat="server" NavigateUrl="~/Applicant/ViewApplicationsInProgress.aspx" Visible="false" >View Applications In Progress</asp:HyperLink>
    <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/def_smcomm.jpg" PostBackUrl="Review/Index.aspx" AlternateText="Committee Review" />
</div>
</asp:Content>