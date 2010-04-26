<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CommitteeManagement.aspx.cs" Inherits="CAESDO.Recruitment.Web.Authorized_CommitteeManagement" Title="Committee Management" Theme="MainTheme" Trace="true" %>
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

    <asp:GridView ID="gviewMembers" runat="server" AllowPaging="false" AutoGenerateColumns="false" SkinID="gridViewUM" DataKeyNames="id" OnRowDataBound="gviewMembers_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="Allow">
                <ItemTemplate>
                    <asp:CheckBox ID="chkAllowMember" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="LoginID" HeaderText="LoginID" SortExpression="LoginID" />            
            <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
            <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
        </Columns>
    </asp:GridView>

    <br /><br />
    <asp:Panel ID="pnlAccess" runat="server" Visible="false">
        <asp:Button ID="btnUpdateAccess" runat="server" Text="Update Access" />
    </asp:Panel>
</asp:Content>