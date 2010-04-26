<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminIndex.aspx.cs" Inherits="Authorized_AdminIndex" Title="Admin Home Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <a href="viewPositionsAdmin.aspx">View Positions</a><br /><br />
    
    <a href="ViewPositionsPending.aspx">View Pending Positions</a><br /><br />
    
    <a href="PositionManagement.aspx">Create Position</a><br /><br />
        
    <a href="UserManagement.aspx">User Management</a><br /><br />
    
    <a href="EmailTemplates.aspx">Email Templates</a><br /><br />
    
    <a href="ApplicationReview.aspx?ApplicationID=11">Review Application 11</a><br /><br />
        
</asp:Content>

