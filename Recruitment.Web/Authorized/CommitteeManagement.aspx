<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CommitteeManagement.aspx.cs" Inherits="CAESDO.Recruitment.Web.Authorized_CommitteeManagement" Title="Committee Management" Theme="MainTheme" Trace="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <Ajax:ScriptManagerProxy ID="scriptProxy" runat="server">
        <Services>
            <Ajax:ServiceReference Path="RecruitmentService.asmx" />
        </Services>
    </Ajax:ScriptManagerProxy>

    <script type="text/javascript" language="javascript">
    
    function LookupKerberosUser()
    {
        var loginID = $get('<%= txtLoginID.ClientID %>').value;
        var progressImage = $get('imgMemberLoginProgress');
                
        //Only lookup the kerberos info if the loginID is not empty
        if ( loginID.length > 0 )
        {
            RecruitmentService.LookupKerberosUser(loginID, SucceededCallback, FailedCallback);
            progressImage.style.visibility = 'visible';
        }
    }
    
    // This is the callback function invoked if the Web service
    // succeeded.
    // It accepts the result object as a parameter.
    function SucceededCallback(result, eventArgs)
    {
        var firstName = $get('<%= txtFName.ClientID %>');
        var lastName = $get('<%= txtLName.ClientID %>');
        var department = $get('<%= txtDepartment.ClientID %>');
        
        if ( result != null )
        {
            firstName.value = result.FirstName;
            lastName.value = result.LastName;
            department.value = result.Department;        
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
    
    </script>

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

    <asp:GridView ID="gviewMembers" runat="server" AutoGenerateColumns="False" SkinID="gridViewUM" AllowSorting="true" DataKeyNames="id" OnRowDataBound="gviewMembers_RowDataBound" CellPadding="0" GridLines="None" OnSorting="gviewMembers_Sorting">
        <Columns>
            <asp:TemplateField HeaderText="Allow">
                <ItemTemplate>
                    <asp:CheckBox ID="chkAllowMember" runat="server" />
                </ItemTemplate>
                <ItemStyle CssClass="paddingLeft" />
                <HeaderStyle CssClass="paddingLeft" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="LoginID" SortExpression="LoginID">
                <ItemTemplate>
                    <%# Eval("LoginID") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="FirstName" SortExpression="FirstName">
                <ItemTemplate>
                    <%# Eval("FirstName") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="LastName" SortExpression="LastName">
                <ItemTemplate>
                    <%# Eval("LastName") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Department" SortExpression="Department">
                <ItemTemplate>
                    <%# string.IsNullOrEmpty(Eval("OtherDepartmentName") as string) ? Eval("Unit.ShortName") : Eval("OtherDepartmentName")%>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>

    <br /><br />
    <asp:Panel ID="pnlAccess" runat="server" Visible="false">
        <asp:Button ID="btnUpdateAccess" runat="server" Text="Update Access" OnClick="btnUpdateAccess_Click" />
        <br /><br />     
                
        <br />
        <span class="boxTitle">Add External Member</span>
        <div style="width: 500px;" class="box">
        <br />
        <asp:Panel ID="pnlAddMember" runat="server">

            Login (Kerberos): <asp:TextBox ID="txtLoginID" runat="server" MaxLength="50" onblur="LookupKerberosUser()" ></asp:TextBox>
                <img id="imgMemberLoginProgress" alt="Progress" src="../Images/progress.gif" style="visibility:hidden" />
                <asp:RequiredFieldValidator id="reqValLoginID" ControlToValidate="txtLoginID" ErrorMessage="* LoginID Required" runat="server" ValidationGroup="ExternalMember" />
                                                
            <br /><br />
            First Name: <asp:TextBox ID="txtFName" runat="server" MaxLength="50"></asp:TextBox><br /><br />
            Last Name: <asp:TextBox ID="txtLName" runat="server" MaxLength="50"></asp:TextBox><br /><br />
            Department: <asp:TextBox ID="txtDepartment" runat="server" MaxLength="50"></asp:TextBox><br /><br />
            
            <asp:Button ID="btnAddMember" runat="server" Text="Add Member" OnClick="btnAddMember_Click" ValidationGroup="ExternalMember" />   
        </asp:Panel>
        </div>    
    </asp:Panel>
</asp:Content>