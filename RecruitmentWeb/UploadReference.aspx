<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="CAESDO.Recruitment.Web.UploadReference" Title="Upload Reference" Codebehind="UploadReference.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:Label ID="lblInfo" runat="server"></asp:Label><br /><br />
    Please upload your file as a PDF Document. Maximum file size allowed is 10 MB.<br /><br />
    <asp:FileUpload ID="fileUploadReference" runat="server" />
    <asp:RequiredFieldValidator id="reqValUploaDReference" ControlToValidate="fileUploadReference" ErrorMessage="* File Required" runat="server"/>
    
    <br /><br />
    
    <asp:Button ID="btnUploadReference" runat="server" Text="Upload" OnClick="btnUploadReference_Click" /><br />
    <asp:Label ID="lblUploadStatus" runat="server" EnableViewState="false" ForeColor="red"></asp:Label>
</asp:Content>

