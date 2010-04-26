<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CommitteeManagement.aspx.cs" Inherits="CAESDO.Recruitment.Web.Authorized_CommitteeManagement" Title="Committee Management" Theme="MainTheme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    Member Type: <asp:DropDownList ID="dlistType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dlistType_SelectedIndexChanged">
        <asp:ListItem Value="committee">Committee</asp:ListItem>
        <asp:ListItem Value="faculty">Faculty</asp:ListItem>
    </asp:DropDownList>
    <br /><br />
    <asp:DropDownList ID="dlistPositions" runat="server" AutoPostBack="True" DataSourceID="ObjectDataPositions" DataTextField="TitleAndApplicationCount" DataValueField="ID" AppendDataBoundItems="true" OnSelectedIndexChanged="dlistPositions_SelectedIndexChanged">
        <asp:ListItem Selected="True" Value="0">Select a Position</asp:ListItem>
    </asp:DropDownList>
    
    <asp:RequiredFieldValidator id="reqValPositions" ControlToValidate="dlistPositions" ErrorMessage="*" runat="server"/>
    
    <asp:ObjectDataSource ID="ObjectDataPositions" runat="server" SelectMethod="GetAllPositionsByStatus" TypeName="CAESDO.Recruitment.Data.NHibernateDaoFactory+PositionDao" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="false" Name="Closed" Type="Boolean" />
            <asp:Parameter DefaultValue="true" Name="AdminAccepted" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <br /><br />

    <asp:GridView ID="gviewMembers" runat="server" AllowPaging="false" AutoGenerateColumns="false" SkinID="gridViewUM" DataKeyNames="id">
        <Columns>
            <asp:BoundField DataField="LoginID" HeaderText="LoginID" SortExpression="LoginID" />            
            <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
            <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
        </Columns>    
    </asp:GridView>

    <%--<asp:GridView ID="gviewMembers" runat="server" AllowPaging="True" skinID="gridViewUM" DataKeyNames="id" GridLines="None" CellPadding="0" AutoGenerateColumns="False" EmptyDataText="No Members Found For This Committee" OnRowDeleting="gviewMembers_RowDeleting">
        <Columns>
            <asp:BoundField DataField="LoginID" HeaderText="Login" >
                <ItemStyle CssClass="paddingLeft" />
                <HeaderStyle HorizontalAlign="Left" CssClass="paddingLeft" />
            </asp:BoundField>
            <asp:CommandField HeaderText="Remove" ShowDeleteButton="True" />
        </Columns>    
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
    <br />
        
    <br /><br />
    
    <asp:Panel ID="pnlAddMember" runat="server" Visible="false">
    
        Kerberos Login ID: 
        <asp:RequiredFieldValidator ID="reqValLoginID" runat="server" ControlToValidate="txtLoginID" ErrorMessage="*"></asp:RequiredFieldValidator>
        <asp:TextBox ID="txtLoginID" runat="server"></asp:TextBox>
        <asp:Button ID="btnAddMember" runat="server" Text="Add Member" OnClick="btnAddMember_Click" />
        
    </asp:Panel>--%>
    
</asp:Content>