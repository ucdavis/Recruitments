<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="CAESDO.Recruitment.Web.Authorized_Emulation" Title="Emulation" Codebehind="Emulation.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    Login: 
    <asp:TextBox ID="txtLoginID" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator id="reqValLogin" ControlToValidate="txtLoginID" ErrorMessage="* LoginID Required" runat="server"/>
    
    <br /><br />
    <asp:Button ID="btnLoginID" runat="server" Text="Emulate!" OnClick="btnLoginID_Click" />

</asp:Content>

