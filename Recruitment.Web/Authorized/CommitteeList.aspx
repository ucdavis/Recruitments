<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CommitteeList.aspx.cs" Inherits="CAESDO.Recruitment.Web.Authorized_CommitteeList" Title="Committee List" Theme="MainTheme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    List Type: <asp:DropDownList ID="dlistType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dlistType_SelectedIndexChanged">
        <asp:ListItem Selected="True">Committee</asp:ListItem>
        <asp:ListItem>Faculty</asp:ListItem>
    </asp:DropDownList>
    
    <br /><br />
    
    Department: <asp:DropDownList ID="dlistDepartment" runat="server" AppendDataBoundItems="true" AutoPostBack="true" DataTextField="ShortName" DataValueField="FISCode" OnSelectedIndexChanged="dlistDepartment_SelectedIndexChanged">
        <asp:ListItem Value="0">Select A Department</asp:ListItem>
    </asp:DropDownList><br /><br />
    
    <asp:GridView ID="gviewCommitteeList" SkinID="gridViewShortList" runat="server" DataKeyNames="id" EmptyDataText="No Members Found" AutoGenerateColumns="False" DataSourceID="ObjectDepartmentMembers" OnRowDeleting="gviewCommitteeList_RowDeleting" CellPadding="0" GridLines="None">
        <Columns>
            <asp:BoundField DataField="LoginID" HeaderText="LoginID" SortExpression="LoginID" >
                <ItemStyle CssClass="paddingLeft" />
                <HeaderStyle CssClass="paddingLeft" />
            </asp:BoundField>            
            <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
            <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
            <asp:CommandField DeleteText="Remove" ShowDeleteButton="True" />
        </Columns>    
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
    
    <asp:ObjectDataSource ID="ObjectDepartmentMembers" runat="server" OnSelecting="ObjectDepartmentMembers_Selecting"
        SelectMethod="GetMembersByDepartmentAndType" TypeName="CAESDO.Recruitment.Data.NHibernateDaoFactory+DepartmentMemberDao">
        <SelectParameters>
            <asp:ControlParameter ControlID="dlistDepartment" Name="DepartmentFIS" PropertyName="SelectedValue"
                Type="String" />
            <asp:Parameter Name="type" Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    
    <br /><br />
    
    <span class="boxTitle">Add Member</span>
    <div style="width: 500px;" class="box">
    <br />
    <asp:Panel ID="pnlAddMember" runat="server" Visible="false">

        Login (Kerberos): <asp:TextBox ID="txtLoginID" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator id="reqValLoginID" ControlToValidate="txtLoginID" ErrorMessage="*" runat="server"/><br /><br />
        First Name: <asp:TextBox ID="txtFName" runat="server"></asp:TextBox><br /><br />
        Last Name: <asp:TextBox ID="txtLName" runat="server"></asp:TextBox><br />
        <br />
        <asp:Button ID="btnAddMember" runat="server" Text="Add Member" OnClick="btnAddMember_Click" />   
    </asp:Panel>
    </div>    
</asp:Content>

