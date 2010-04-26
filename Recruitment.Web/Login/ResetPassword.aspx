<%@ Page AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs" Inherits="CAESDO.Recruitment.Web.ResetPassword" Language="C#" MasterPageFile="~/MasterPage.master" Title="Reset Password" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:PasswordRecovery ID="PasswordRecovery1" runat="server" BackColor="#F7F6F3" BorderColor="#E6E2D8" BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="1em" GeneralFailureText="Your attempt to reset your password was not successful. Please try again." QuestionInstructionText="Answer the following question to reset your password." UserNameInstructionText="Enter your User Name to reset your password.">
<MailDefinition From="automatedemail@caes.ucdavis.edu" Subject="Password Reset">
</MailDefinition>
    <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
    <SuccessTextStyle Font-Bold="True" ForeColor="#5D7B9D" />
    <TextBoxStyle Font-Size="0.8em" />
    <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.9em" ForeColor="White" />
    <SubmitButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid"
        BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
</asp:PasswordRecovery>

</asp:Content>