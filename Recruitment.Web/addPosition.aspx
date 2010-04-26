<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="addPosition.aspx.cs" Inherits="Default2" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <span class="boxTitle"><img src="Images/profile_sm.gif" style="vertical-align:middle;" alt="" /> Create Position</span><br />
    <table class="box" style="width:550px; height: 350px;" cellpadding="5">
        <tr>
            <td style="width:225px;" align="right">
                <br />Deadline</td>
            <td >
                <br /><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td   align="right">
                Department</td>
            <td  >
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td   align="right">
                Position Title</td>
            <td  >
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td   align="right">
                Position Number</td>
            <td  >
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td   align="right">
                HR Phone Number</td>
            <td  >
                <asp:TextBox ID="TextBox5" runat="server" MaxLength="3" Width="30px"></asp:TextBox>
                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td   align="right">
                HR Representative</td>
            <td  >
                <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td   align="right">
                Number of Required Publications</td>
            <td  >
                <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td   align="right">
                Number of Required References</td>
            <td  >
                <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td   align="right" valign="top">
                Summary</td>
            <td  >
                <asp:TextBox ID="TextBox10" runat="server" Height="90px" Rows="4" TextMode="MultiLine"
                    Width="233px"></asp:TextBox></td>
        </tr>
        <tr>
            <td   align="right">
                Full job description (PDF)</td>
            <td  >
                <asp:Button ID="Button1" runat="server" Text="Upload" /></td>
        </tr>
        <tr>
            <td   align="right">
                Allow Applications</td>
            <td  >
                <asp:RadioButton ID="RadioButton1" runat="server" Text="Yes" />
                <asp:RadioButton ID="RadioButton2" runat="server" Text="No" /></td>
        </tr>
        <tr>
            <td   align="right">
            </td>
            <td align="right"  >
                <br />
                <asp:Button ID="Button2" runat="server" Text="Create!" /></td>
        </tr>
    </table>
 
</asp:Content>

