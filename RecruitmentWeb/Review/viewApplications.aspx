<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="CAESDO.Recruitment.Web.Review_viewApplications" Title="View Applications" Theme="MainTheme" Codebehind="viewApplications.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="ViewApplications" Src="~/Shared/ViewApplications.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <uc:ViewApplications ID="viewApp" runat="server" />
    
</asp:Content>

