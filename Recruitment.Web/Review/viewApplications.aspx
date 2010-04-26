<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="viewApplications.aspx.cs" Inherits="CAESDO.Recruitment.Web.Authorized_viewApplications" Title="View Applications" Theme="MainTheme" %>
<%@ Register TagPrefix="uc" TagName="ViewApplications" Src="~/Shared/ViewApplications.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <uc:ViewApplications ID="viewApp" runat="server" />
    
</asp:Content>

