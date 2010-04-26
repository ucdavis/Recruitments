<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="CAESDO.login" MasterPageFile="~/masterPage.master" %>

<asp:Content ContentPlaceHolderID="contentPlaceHolder1" runat="server">

    &nbsp;You have requested a secure UC Davis Web page.&nbsp;
    <br />
    <br />
    <asp:Panel ID="pnlKerberosLogin" runat="server">
    
    <asp:LinkButton ID="lbtnKerberosLogin" runat="server" OnClick="lbtnKerberosLogin_Click">If you have a Kerberos ID, click here to login.</asp:LinkButton><br />
    <br />
    OR<br />
    <br />
    </asp:Panel>
    
    <asp:Login ID="loginMembership" runat="server" BackColor="#F7F6F3" BorderColor="#E6E2D8" BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="1em" ForeColor="#333333" Height="124px" Width="293px" 
        CreateUserText="New User?" CreateUserUrl="~/Login/CreateUser.aspx" 
        PasswordRecoveryText="Forgot Password?" PasswordRecoveryUrl="~/Login/ResetPassword.aspx" UserNameLabelText="User Name/Email:">
        <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.9em" ForeColor="White" />
        <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
        <TextBoxStyle Font-Size="0.8em" />
        <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
            Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
    </asp:Login>
    
</asp:Content>    