<%@ Page Language="C#" AutoEventWireup="true" Inherits="CAESDO.login" MasterPageFile="~/masterPage.master" Codebehind="login.aspx.cs" %>

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
    <span class="loginboxTitle"><img src="Images/profile_sm.gif" style="vertical-align:middle;" alt="" /> Log In</span>
    <asp:Login ID="loginMembership" runat="server" BackColor="#EFEFEF" BorderColor="#7f7f7f" BorderPadding="4" BorderStyle="Dotted" BorderWidth="1px" Font-Names="Verdana" Font-Size="1em" ForeColor="#333333" Height="124px" Width="293px" 
        CreateUserText="New User?" CreateUserUrl="~/Login/CreateUser.aspx" 
        PasswordRecoveryText="Forgot Password?" PasswordRecoveryUrl="~/Login/ResetPassword.aspx" UserNameLabelText="User Name/Email:">
        <TitleTextStyle Font-Bold="True" Font-Size="0em" ForeColor="White" Height="20px" />
        <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
        <TextBoxStyle  />
        <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
            Font-Names="Verdana" ForeColor="#284775" />
    </asp:Login>
    
</asp:Content>    