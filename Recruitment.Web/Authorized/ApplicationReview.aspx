<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApplicationReview.aspx.cs" Inherits="CAESDO.Recruitment.Web.Authorized_ApplicationReview" Title="Application Review" Theme="MainTheme" %>
<%@ Register TagPrefix="uc" TagName="ApplicationReview" Src="~/Shared/ApplicationReview.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <uc:ApplicationReview ID="appReview" runat="server" AdministrativeAccess="true" />
            
</asp:Content>

