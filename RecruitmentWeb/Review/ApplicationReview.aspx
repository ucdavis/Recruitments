<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="CAESDO.Recruitment.Web.Review_ApplicationReview" Title="Application Review" Theme="MainTheme" Codebehind="ApplicationReview.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="ApplicationReview" Src="~/Shared/ApplicationReview.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <uc:ApplicationReview ID="appReview" runat="server" AdministrativeAccess="false" />
    
</asp:Content>

