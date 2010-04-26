<%@ Page AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="CAESDO.Recruitment.Web.ChangePassword" Language="C#" MasterPageFile="~/MasterPage.master" Title="Change Password" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ChangePassword ID="ChangePassword1" runat="server" BackColor="#F7F6F3"
        BorderColor="#E6E2D8" BorderPadding="4" BorderStyle="Solid" BorderWidth="1px"
        DisplayUserName="True" Font-Names="Verdana" UserNameLabelText="Email:" CancelDestinationPageUrl="~/viewPositions.aspx" ContinueDestinationPageUrl="~/viewPositions.aspx" CreateUserUrl="~/Login/CreateUser.aspx">
        <CancelButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid"
            BorderWidth="1px" Font-Names="Verdana" ForeColor="#284775" />
        <PasswordHintStyle Font-Italic="True" ForeColor="#888888" />
        <ContinueButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid"
            BorderWidth="1px" Font-Names="Verdana" ForeColor="#284775" />
        <ChangePasswordButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid"
            BorderWidth="1px" Font-Names="Verdana" ForeColor="#284775" />
        <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <TextBoxStyle />
        <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
    </asp:ChangePassword>

</asp:Content>