<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserManagement.aspx.cs" Inherits="CAESDO.Recruitment.Web.UserManagement" MasterPageFile="~/MasterPage.master" %>

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
        
        <Ajax:UpdatePanel ID="updateUserGrid" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        
            <asp:GridView ID="GViewUsers" runat="server" DataKeyNames="Login" CellPadding="4" ForeColor="#333333" GridLines="None" DataSourceID="ObjectDataSource1" AutoGenerateColumns="False" OnSelectedIndexChanged="GViewUsers_SelectedIndexChanged">
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <EditRowStyle BackColor="#999999" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField DataField="Login" HeaderText="UserName" />
                    <asp:BoundField DataField="Role" HeaderText="Role" />
                    <asp:BoundField DataField="LastName" HeaderText="LastName" />
                    <asp:BoundField DataField="FirstName" HeaderText="FirstName" />
                    <asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID" />
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetUsersInApplication" TypeName="CAESDO.Recruitment.CatbertManager"></asp:ObjectDataSource>

        </ContentTemplate>
        </Ajax:UpdatePanel>

        <Ajax:UpdatePanel ID="updateUserInfo" runat="server" UpdateMode="conditional">
        <ContentTemplate>
            <asp:Button ID="btnHiddenSelectUser" runat="server" style="display:none; visibility:hidden;" />
            <asp:Panel ID="pnlUserInfo" runat="server" style="display:none" CssClass="modalPopup">
            
            User Information For <asp:Label ID="lblUserInfoName" runat="server" Text=""></asp:Label>
            <br /><br />
            LoginID: <asp:Label ID="lblUserInfoLoginID" runat="server" Text=""></asp:Label>
            <br />
            EmployeeID: <asp:Label ID="lblUserInfoEmployeeID" runat="server" Text=""></asp:Label>
            <br /><br />
                <asp:GridView ID="gViewUserUnits" runat="server" DataKeyNames="ID" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" OnRowDeleting="gViewUserUnits_RowDeleting">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="FullName" HeaderText="Name"  />
                        <asp:BoundField DataField="FISCode" HeaderText="FISCode" />
                        <asp:CommandField DeleteText="Remove" ShowDeleteButton="True" />
                    </Columns>  
                </asp:GridView>
                <br />
                <asp:LinkButton ID="btnUserInfoAddUnit" runat="server" OnClick="btnUserInfoAddUnit_Click">Add Unit: </asp:LinkButton>
                <asp:DropDownList ID="dlistUnits" runat="server" DataTextField="Unit" DataValueField="UnitID" DataSourceID="ObjectDataUnits"></asp:DropDownList><asp:ObjectDataSource ID="ObjectDataUnits" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetUnits" TypeName="CAESDO.Recruitment.CatbertManager"></asp:ObjectDataSource>
                
                <br /><br />
            <asp:Button ID="btnSaveUserInfo" runat="server" Text="Save" />
            <asp:Button ID="btnCancelUserInfo" runat="server" Text="Cancel" />
            
            </asp:Panel>
            <AjaxControlToolkit:ModalPopupExtender ID="mpopupUserInfo" runat="server" BackgroundCssClass="modalBackground" 
                    PopupControlID="pnlUserInfo" TargetControlID="btnHiddenSelectUser" CancelControlID="btnCancelUserInfo">
            </AjaxControlToolkit:ModalPopupExtender>
        </ContentTemplate>
        </Ajax:UpdatePanel>        
        
</asp:Content>
