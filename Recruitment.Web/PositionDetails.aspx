<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PositionDetails.aspx.cs" Inherits="CAESDO.Recruitment.Web.PositionDetails" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HyperLink ID="hlinkPositionList" runat="server" NavigateUrl="~/viewPositions.aspx">Go To Position List</asp:HyperLink><br />
    <br />
    <table>
        <tr>
            <td>
                Position Title:</td>
            <td>
                <asp:Label ID="lblPositionTitle" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>
                Position Number:</td>
            <td>
                <asp:Label ID="lblPositionNumber" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>
                Position Description:</td>
            <td>
                &nbsp;<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/icon.pdf.gif" />Download</td>
        </tr>
        <tr>
            <td>
                Description:</td>
            <td>
                <asp:TextBox ID="txtPositionDescription" runat="server" ReadOnly="True" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                Date Posted:</td>
            <td>
                <asp:Label ID="lblDatePosted" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>
                Deadline:</td>
            <td>
                <asp:Label ID="lblDeadline" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>
                References:</td>
            <td>
                <asp:Label ID="lblNumReferences" runat="server" Text=""></asp:Label>
                Required</td>
        </tr>
        <tr>
            <td>
                Publications:</td>
            <td>
                <asp:Label ID="lblNumPublications" runat="server" Text=""></asp:Label>
                Required</td>
        </tr>
        <tr>
            <td>
                HR Rep:</td>
            <td>
                <asp:Label ID="lblHRRep" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>
                HR Phone:</td>
            <td>
                <asp:Label ID="lblHRPhone" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>
                HR Email:</td>
            <td>
                <asp:Label ID="lblHREmail" runat="server" Text=""></asp:Label></td>
        </tr>
    </table>
    <br />
    
</asp:Content>