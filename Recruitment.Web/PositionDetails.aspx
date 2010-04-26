<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PositionDetails.aspx.cs" Inherits="CAESDO.Recruitment.Web.PositionDetails" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HyperLink ID="hlinkPositionList" runat="server" NavigateUrl="~/viewPositions.aspx">Go To Position List</asp:HyperLink><br />
    <br />
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
        <tr>
            <td align="right">
                Date Posted:</td>
            <td>
                <asp:Label ID="lblDatePosted" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td align="right">
                Deadline:</td>
            <td>
                <asp:Label ID="lblDeadline" runat="server" Text=""></asp:Label></td>
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
        <tr>
            <td align="right">
                Description:</td>
            <td>
                <asp:TextBox ID="txtPositionDescription" runat="server" ReadOnly="True" Height="90px" Rows="4" TextMode="MultiLine"
                    Width="233px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right">
                Position Description:</td>
            <td>
                <asp:ImageButton ID="ibtnDownloadPD" runat="server" ImageUrl="~/Images/icon.pdf.gif" />
                <asp:LinkButton ID="lbtnDownloadPD" runat="server" Text="Download"></asp:LinkButton>
            </td>
        </tr>
        
    </table>
    <br />
    
</asp:Content>