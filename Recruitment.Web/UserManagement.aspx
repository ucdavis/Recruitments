<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserManagement.aspx.cs" Inherits="UserManagement" MasterPageFile="~/MasterPage.master" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <asp:Button ID="btnAddUser" runat="server" Text="Add A User" />
 
 <asp:Panel ID="pnlAddUser" runat="server" CssClass="modalPopup" style="display:none;">
    Add A User Here
    <br /><br />
    <asp:Button ID="btnAddUserOK" runat="server" Text="Add User" />
    <asp:Button ID="btnAddUserCancel" runat="server" Text="Cancel" />
 </asp:Panel>
 <AjaxControlToolkit:ModalPopupExtender ID="mpopupAddUser" runat="server" BackgroundCssClass="modalBackground" 
    PopupControlID="pnlAddUser" TargetControlID="btnAddUser" CancelControlID="btnAddUserCancel" >
 </AjaxControlToolkit:ModalPopupExtender>
 
        <br />
        <br />
        User List:
        
        <asp:GridView ID="GViewUsers" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" DataSourceID="ObjectDataSource1" AutoGenerateColumns="False">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <EditRowStyle BackColor="#999999" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="Login" HeaderText="UserName" />
                <asp:BoundField DataField="Role" HeaderText="Role" />
                <asp:BoundField DataField="LastName" HeaderText="LastName" />
                <asp:BoundField DataField="FirstName" HeaderText="FirstName" />
                <asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetUsersInApplication" TypeName="CAESDO.Recruitment.CatbertManager"></asp:ObjectDataSource>

</asp:Content>
