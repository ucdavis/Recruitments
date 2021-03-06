<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PositionDetails.aspx.cs" Inherits="CAESDO.Recruitment.Web.PositionDetails" Title="Position Details" Theme="MainTheme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <ul style="list-style:none; margin:0 0 0 20px; padding: 0;"><li class="viewPositionsList"><asp:HyperLink ID="hlinkPositionList" runat="server" NavigateUrl="~/viewPositions.aspx">Go to Positions List</asp:HyperLink></li></ul>
    <asp:Panel ID="pnlDepartmentLogo" runat="server" Height="0px" Width="340px" CssClass="CABAstyle"> </asp:Panel><br />  
     <br /><br /><br />
     <span class="boxTitle"><img src="Images/profile_sm.gif" style="vertical-align:middle;" alt="" /> Position Details</span><br />
    <table class="box" style="width:550px; height: 350px;" cellpadding="5">
        <tr>
            <td align="right"><br />
                Position Title:</td>
            <td><br />
                <asp:Label ID="lblPositionTitle" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td align="right">
                Position Number:</td>
            <td>
                <asp:Label ID="lblPositionNumber" runat="server" Text=""></asp:Label></td>
        </tr>
<%--        <tr>
            <td align="right">
                Date Posted:</td>
            <td>
                <asp:Label ID="lblDatePosted" runat="server" Text=""></asp:Label></td>
        </tr>--%>
        <tr>
            <td align="right">
                Review Date:
            </td>
            <td>
                <asp:Label ID="lblDeadline" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right">
                Department(s):
            </td>
            <td>
                <asp:Label ID="lblDepartments" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right">
                References:</td>
            <td>
                <asp:Label ID="lblNumReferences" runat="server" Text=""></asp:Label>
                Required</td>
        </tr>
        <tr>
            <td align="right">
                Publications:</td>
            <td>
                <asp:Label ID="lblNumPublications" runat="server" Text=""></asp:Label>
                Required</td>
        </tr>
        <%--<tr>
            <td align="right">
                HR Rep:</td>
            <td>
                <asp:Label ID="lblHRRep" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td align="right">
                HR Phone:</td>
            <td>
                <asp:Label ID="lblHRPhone" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td align="right">
                HR Email:</td>
            <td>
                <asp:Label ID="lblHREmail" runat="server" Text=""></asp:Label></td>
        </tr>--%>
        <%--<tr>
            <td align="right">
                Description:</td>
            <td>
                <asp:TextBox ID="txtPositionDescription" runat="server" ReadOnly="True" Height="150px" Rows="4" TextMode="MultiLine"
                    Width="375px" BackColor="#EFEFEF" BorderStyle="None" Font-Names="Trebuchet MS,Arial,Georgia" Font-Size="1em" ></asp:TextBox></td>
        </tr>
        --%>
        <tr>
            <td align="right">Description:</td>
            <td>
                <asp:Literal ID="litPositionDescription" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="right">
                Position Description:</td>
            <td>
                <asp:ImageButton ID="ibtnDownloadPD" runat="server" ImageUrl="~/Images/icon.pdf.gif" />
                <asp:LinkButton ID="lbtnDownloadPD" runat="server" Text="Download" OnClick="lbtnDownloadPD_Click"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td   align="right">
            </td>
            <td align="right">
                <br />
                <asp:LoginView ID="lviewPositionApply" runat="server">
                    <AnonymousTemplate>
                        <span style="font-weight: bold; color: #ff0000">
                        You must log in to apply to this position. <br />
                        </span>
                        <br />
                        Existing Users: <a href="<%= FormsAuthentication.LoginUrl %>?ReturnURL=<%= HttpUtility.UrlEncode(Request.Url.PathAndQuery) %>" >Click here to log in</a><br /><br />
                        New Users: <a href="Login/CreateUser.aspx?ReturnURL=<%= HttpUtility.UrlEncode(Request.Url.PathAndQuery) %>">Click here to create a new account</a>
                    </AnonymousTemplate>
                    <LoggedInTemplate>
                        <asp:Button ID="btnPositionApply" runat="server" Text="Apply For This Position" OnClick="btnPositionApply_Click" />
                    </LoggedInTemplate>
                </asp:LoginView>
            </td>
        </tr>
    </table>
    <br />
    
</asp:Content>