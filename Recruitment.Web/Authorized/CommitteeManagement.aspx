<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CommitteeManagement.aspx.cs" Inherits="CAESDO.Recruitment.Web.Authorized_CommitteeManagement" Title="Committee Management" Theme="MainTheme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:DropDownList ID="dlistPositions" runat="server" AutoPostBack="True" DataSourceID="ObjectDataPositions" DataTextField="TitleAndApplicationCount" DataValueField="ID" AppendDataBoundItems="true" OnSelectedIndexChanged="dlistPositions_SelectedIndexChanged">
        <asp:ListItem Selected="True" Value="0">Select a Position</asp:ListItem>
    </asp:DropDownList>
    
    <asp:RequiredFieldValidator id="reqValPositions" ControlToValidate="dlistPositions" ErrorMessage="*" runat="server"/>
    
    <asp:ObjectDataSource ID="ObjectDataPositions" runat="server" SelectMethod="GetAll" TypeName="CAESDO.Recruitment.Data.NHibernateDaoFactory+PositionDao"></asp:ObjectDataSource>
    <br /><br />

    <asp:GridView ID="gviewMembers" runat="server" AllowPaging="True" skinID="gridViewUM" DataKeyNames="id" GridLines="None" CellPadding="0" AutoGenerateColumns="False" EmptyDataText="No Members Found For This Committee" OnRowDeleting="gviewMembers_RowDeleting">
        <Columns>
            <asp:BoundField DataField="LoginID" HeaderText="Login" >
                <ItemStyle CssClass="paddingLeft" />
                <HeaderStyle HorizontalAlign="Left" CssClass="paddingLeft" />
            </asp:BoundField>
            <asp:CommandField HeaderText="Remove" ShowDeleteButton="True" />
        </Columns>    
    </asp:GridView>
        
</asp:Content>