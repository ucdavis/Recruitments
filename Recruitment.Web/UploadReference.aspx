<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UploadReference.aspx.cs" Inherits="CAESDO.Recruitment.Web.UploadReference" Title="Upload Reference" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:Label ID="lblInfo" runat="server"></asp:Label><br /><br />
    
    <asp:FileUpload ID="fileUploadReference" runat="server" />
    <asp:RequiredFieldValidator id="reqValUploaDReference" ControlToValidate="fileUploadReference" ErrorMessage="*" runat="server"/>
    
    <br /><br />
    
    <asp:Button ID="btnUploadReference" runat="server" Text="Upload" OnClick="btnUploadReference_Click" /><br />
    <asp:Label ID="lblUploadStatus" runat="server" EnableViewState="false" ForeColor="red"></asp:Label>
</asp:Content>

