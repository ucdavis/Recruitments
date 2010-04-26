<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserManagement.aspx.cs" Inherits="CAESDO.Recruitment.Web.UserManagement" MasterPageFile="~/MasterPage.master" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<Ajax:UpdatePanel ID="updateAddUser" runat="server" UpdateMode="Conditional">
<ContentTemplate>
 <asp:Button ID="btnAddUser" runat="server" Text="Add A User" />
 
 <asp:Panel ID="pnlAddUser" runat="server" CssClass="modalPopup" style="display:none;">
    Add A User Here
    <br />
     <br />
     Search For New User:<br />
     <br />
     EmployeeID:
     <asp:TextBox ID="txtAddUserEmployeeID" runat="server"></asp:TextBox><br />
     First Name:
     <asp:TextBox ID="txtAddUserFirstName" runat="server"></asp:TextBox><br />
     Last Name:
     <asp:TextBox ID="txtAddUserLastName" runat="server"></asp:TextBox><br />
     Login ID:
     <asp:TextBox ID="txtAddUserLoginID" runat="server"></asp:TextBox><br />
     <br />
     <asp:Button ID="btnAddUserSearch" runat="server" OnClick="btnAddUserSearch_Click"
         Text="Search" />
     <%--<asp:Button ID="btnAddUserOK" runat="server" Text="Add User" />--%> 
     <asp:Button ID="btnAddUserCancel" runat="server" Text="Cancel" />
     <br />
     <br />
     <asp:GridView ID="gViewAddUserSearch" runat="server" CellPadding="4" DataSourceID="ObjectDataUserSearch" EmptyDataText="No Matching Users Found" ForeColor="#333333" GridLines="None" Visible="False" AutoGenerateColumns="False">
         <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
         <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
         <Columns>
             <asp:CommandField ShowSelectButton="True" SelectText="Add" />
             <asp:BoundField DataField="Login" HeaderText="Login" />
             <asp:BoundField DataField="LastName" HeaderText="LastName" />
             <asp:BoundField DataField="FirstName" HeaderText="FirstName" />
             <asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID" />
             <asp:BoundField DataField="Email" HeaderText="Email" />
         </Columns>
         <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
         <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
         <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
         <EditRowStyle BackColor="#999999" />
         <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
     </asp:GridView>
     <asp:ObjectDataSource ID="ObjectDataUserSearch" runat="server" OldValuesParameterFormatString="original_{0}"
         SelectMethod="SearchNewUsersByLogin" TypeName="CAESDO.Recruitment.CatbertManager">
         <SelectParameters>
             <asp:ControlParameter ControlID="txtAddUserEmployeeID" Name="EmployeeID" PropertyName="Text"
                 Type="String" />
             <asp:ControlParameter ControlID="txtAddUserFirstName" Name="FirstName" PropertyName="Text"
                 Type="String" />
             <asp:ControlParameter ControlID="txtAddUserLastName" Name="LastName" PropertyName="Text"
                 Type="String" />
             <asp:ControlParameter ControlID="txtAddUserLoginID" Name="LoginID" PropertyName="Text"
                 Type="String" />
         </SelectParameters>
     </asp:ObjectDataSource>
     
     <br /><br />
 </asp:Panel>
 
 <AjaxControlToolkit:ModalPopupExtender ID="mpopupAddUser" runat="server" BackgroundCssClass="modalBackground" 
    PopupControlID="pnlAddUser" TargetControlID="btnAddUser" CancelControlID="btnAddUserCancel" >
 </AjaxControlToolkit:ModalPopupExtender>
</ContentTemplate>
</Ajax:UpdatePanel>

 
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
