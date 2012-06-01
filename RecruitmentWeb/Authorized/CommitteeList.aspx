<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="CAESDO.Recruitment.Web.Authorized_CommitteeList" Title="Committee List" Theme="MainTheme" Codebehind="CommitteeList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
    <Ajax:ScriptManagerProxy ID="scriptProxy" runat="server">
        <Services>
            <Ajax:ServiceReference Path="RecruitmentService.asmx" />
        </Services>
    </Ajax:ScriptManagerProxy>

    <script type="text/javascript" language="javascript">

    function LookupKerberosUser() {
        var query = $get('<%= txtSearchQuery.ClientID %>').value;
        var progressImage = $get('imgMemberLoginProgress');

        //Only lookup the person info if the search query is not empty
        if (query.length > 0) {
            RecruitmentService.LookupKerberosUser(query, SucceededCallback, FailedCallback);
            progressImage.style.visibility = 'visible';
        }
    }

    // This is the callback function invoked if the Web service
    // succeeded.
    // It accepts the result object as a parameter.
    function SucceededCallback(result, eventArgs) {
        var search = $get('<%= txtSearchQuery.ClientID %>');
        var login = $get('<%= txtLoginID.ClientID %>');
        var hLogin = $get('<%= hLoginID.ClientID %>');
        var firstName = $get('<%= txtFName.ClientID %>');
        var hFirstName = $get('<%= hFirstName.ClientID %>');
        var lastName = $get('<%= txtLName.ClientID %>');
        var hLastName = $get('<%= hLastName.ClientID %>');

        var addMember = $get('<%= btnAddMember.ClientID %>');

        if (result != null) {
            login.value = result.LoginID;
            hLogin.value = result.LoginID;
            firstName.value = result.FirstName;
            hFirstName.value = result.FirstName;
            lastName.value = result.LastName;
            hLastName.value = result.LastName;
            
            $(addMember).removeAttr("disabled");
        } else {
            login.value = "";
            hLogin.value = "";
            firstName.value = "";
            hFirstName.value = "";
            lastName.value = "";
            hLastName.value = "";
            $(addMember).attr("disabled", "disabled");

            var query = search.value;
            alert("No user found with the email or kerberos login: " + query);
        }

        //No matter what, hide the image progress icon
        var progressImage = $get('imgMemberLoginProgress');
        progressImage.style.visibility = 'hidden';
    }

    // This is the callback function invoked if the Web service
    // failed.
    // It accepts the error object as a parameter.
    function FailedCallback(error)
    {
    }

    $(document).ready(function() {
        //Sort table
        $("#tblCommitteeList").tablesorter({
            sortList: [[2, 0]],
            cssAsc: 'headerSortUp',
            cssDesc: 'headerSortDown',
            cssHeader: 'header',
            headers: { 3: { sorter: false } },
            widgets: ['zebra']
        });
    });
    
    </script>
   
    Department: <asp:DropDownList ID="dlistDepartment" runat="server" AppendDataBoundItems="true" AutoPostBack="true" 
            DataTextField="ShortName" DataValueField="id" OnSelectedIndexChanged="dlistDepartment_SelectedIndexChanged">
        <asp:ListItem Value="0">Select A Department</asp:ListItem>
    </asp:DropDownList><br /><br />
    
        <asp:ListView ID="lviewApplications" runat="server" DataSourceID="ObjectDepartmentMembers">
        <LayoutTemplate>
        <table id="tblCommitteeList" class="tablesorter tablesearch">
            <thead>
                <tr>
                    <th>
                        LoginID
                    </th>
                    <th>
                        FirstName
                    </th>
                    <th>
                        LastName
                    </th>
                    <th>
                        &nbsp;
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr id="itemPlaceholder" runat="server"></tr>
            </tbody>
        </table>
        </LayoutTemplate>
        <ItemTemplate>
                <tr>
                    <td>
                        <%# Eval("LoginID") %>
                    </td>
                    <td>
                        <%# Eval("FirstName") %>
                    </td>
                    <td>
                        <%# Eval("LastName") %>
                    </td>
                    <td>
                        <asp:LinkButton ID="btnRemove" runat="server" Text="Remove" CommandArgument='<%# Eval("id") %>' OnClick="btnRemove_Click" CausesValidation="false"></asp:LinkButton>
                    </td>
                </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            No Members Found
        </EmptyDataTemplate>
    </asp:ListView>
    
<%--    <asp:GridView ID="gviewCommitteeList" SkinID="gridViewShortList" runat="server" DataKeyNames="id" EmptyDataText="No Members Found" AutoGenerateColumns="False" DataSourceID="ObjectDepartmentMembers" OnRowDeleting="gviewCommitteeList_RowDeleting" CellPadding="0" GridLines="None">
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
    </asp:GridView>--%>
    
    <asp:ObjectDataSource ID="ObjectDepartmentMembers" runat="server" SelectMethod="GetMembersByDepartment" 
        TypeName="CAESDO.Recruitment.BLL.DepartmentMemberBLL" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:ControlParameter ControlID="dlistDepartment" Name="DepartmentFIS" PropertyName="SelectedValue"
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>    
    
    <br /><br />
    
    <asp:Panel ID="pnlAddMember" runat="server" Visible="false">
        <span class="boxTitle">Add Member</span>
        <div style="width: 500px;" class="box">
        <br />        
            Email or Kerberos Login: <asp:TextBox ID="txtSearchQuery" runat="server" MaxLength="50" ></asp:TextBox>
            <input type="button" id="btnLookupUser" onclick="LookupKerberosUser()" value="Lookup Person" />
                <img id="imgMemberLoginProgress" alt="Progress" src="../Images/progress.gif" style="visibility:hidden" />
                <asp:RequiredFieldValidator id="reqValLoginID" ControlToValidate="txtSearchQuery" ErrorMessage="* Email or Login Required" runat="server" ValidationGroup="ExternalMember" Display="Dynamic" />
            <br /><br />
            <asp:HiddenField runat="server" ID="hLoginID"/><asp:HiddenField runat="server" ID="hFirstName"/><asp:HiddenField runat="server" ID="hLastName"/>
            LoginID: <asp:TextBox ID="txtLoginID" runat="server" MaxLength="50" Enabled="False"></asp:TextBox><br /><br />
            First Name: <asp:TextBox ID="txtFName" runat="server" MaxLength="50" Enabled="False"></asp:TextBox><br /><br />
            Last Name: <asp:TextBox ID="txtLName" runat="server" MaxLength="50" Enabled="False"></asp:TextBox><br /><br />
            <br />
            <asp:Button ID="btnAddMember" runat="server" Text="Add Member" OnClick="btnAddMember_Click" Enabled="False" />   
        </div>    
    </asp:Panel>
</asp:Content>

