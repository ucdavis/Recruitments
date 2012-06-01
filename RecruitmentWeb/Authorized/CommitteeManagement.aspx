<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="CAESDO.Recruitment.Web.Authorized_CommitteeManagement" Title="Committee Management" Theme="MainTheme" Trace="false" Codebehind="CommitteeManagement.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <Ajax:ScriptManagerProxy ID="scriptProxy" runat="server">
        <Services>
            <Ajax:ServiceReference Path="RecruitmentService.asmx" />
        </Services>
    </Ajax:ScriptManagerProxy>

    <script type="text/javascript" language="javascript">
    
    function LookupKerberosUser()
    {
        var query = $get('<%= txtSearchQuery.ClientID %>').value;
        var progressImage = $get('imgMemberLoginProgress');
                
        //Only lookup the person info if the search query is not empty
        if ( query.length > 0 )
        {
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
        var firstName = $get('<%= txtFName.ClientID %>');
        var lastName = $get('<%= txtLName.ClientID %>');
        var department = $get('<%= txtDepartment.ClientID %>');
        var membertype = $get('<%= dlistMemberType.ClientID %>');
        var addMember = $get('<%= btnAddMember.ClientID %>');

        if (result != null) {
            login.value = result.LoginID;
            firstName.value = result.FirstName;
            lastName.value = result.LastName;
            department.value = result.OtherDepartmentName;

            $(department).removeAttr("disabled");
            $(membertype).removeAttr("disabled");
            $(addMember).removeAttr("disabled");
        } else {
            login.value = "";
            firstName.value = "";
            lastName.value = "";
            department.value = "";

            $(department).attr("disabled", "disabled");
            $(membertype).attr("disabled", "disabled");
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
        $("#tblMembers").tablesorter(
        {
            sortList: [[5, 0]],
            cssAsc: 'headerSortUp',
            cssDesc: 'headerSortDown',
            cssHeader: 'header',
            headers: { 0: { sorter: 'checkbox' }
                        , 1: { sorter: 'checkbox' }
                        , 2: { sorter: 'checkbox' } 
                },
            widgets: ['zebra']
        });

        $("#CommitteeHelp").bt('Committee Members Can Review All Applications Throughout The Entire Application Process (Including In Progress/Un-Finalized Applications)');

        $("#FacultyHelp").bt('Faculty Members Can Review All Applications After "Allow Faculty Review" Is Selected For A Position');

        $('#ReviewerHelp').bt('A Reviewer Can View The Same Applications As Faculty, Except They Can Not See Confidential Reference Files');
    });
    </script>

    <asp:DropDownList ID="dlistPositions" runat="server" AutoPostBack="True" DataSourceID="ObjectDataPositions" DataTextField="TitleAndApplicationCount" DataValueField="ID" AppendDataBoundItems="true" OnSelectedIndexChanged="dlistPositions_SelectedIndexChanged">
        <asp:ListItem Selected="True" Value="0">Select a Position</asp:ListItem>
    </asp:DropDownList>
    
    <asp:RequiredFieldValidator id="reqValPositions" ControlToValidate="dlistPositions" ErrorMessage="* You Must Select A Position" InitialValue="0" runat="server"/>
    
    <asp:ObjectDataSource ID="ObjectDataPositions" runat="server" SelectMethod="GetByStatus" TypeName="CAESDO.Recruitment.BLL.PositionBLL" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="false" Name="Closed" Type="Boolean" />
            <asp:Parameter DefaultValue="true" Name="AdminAccepted" Type="Boolean" />
            <asp:Parameter Name="AllowApplications" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <br /><br />
    <asp:ListView ID="lviewMembers" runat="server" DataKeyNames="id" OnItemDataBound="lviewMembers_ItemDataBound">
        <LayoutTemplate>
            <table id="tblMembers" class="tablesorter tablesearch">
                <thead>
                    <tr>
                        <th>
                            Committee
                            <img id="CommitteeHelp" src="../Images/question_blue.png" />
                        </th>
                        <th>
                            Faculty
                            <img id="FacultyHelp" src="../Images/question_blue.png" />
                        </th>
                        <th>
                            Reviewer
                            <img id="ReviewerHelp" src="../Images/question_blue.png" />
                        </th>
                        <th>
                            LoginID
                        </th>
                        <th>
                            First Name
                        </th>
                        <th>
                            Last Name
                        </th>
                        <th>
                            Department
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:CheckBox ID="chkAllowMember" runat="server" />
                </td>
                <td>
                    <asp:CheckBox ID="chkAllowFaculty" runat="server" />
                </td>
                <td>
                    <asp:CheckBox ID="chkAllowReview" runat="server" />
                </td>
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
                    <%# string.IsNullOrEmpty(Eval("OtherDepartmentName") as string) ? Eval("Unit.ShortName") : Eval("OtherDepartmentName")%>
                </td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            No Committee Members Found For This Position
        </EmptyDataTemplate>
    </asp:ListView>

<%--    <asp:GridView ID="gviewMembers" runat="server" AutoGenerateColumns="False" SkinID="gridViewUM" AllowSorting="true" DataKeyNames="id" OnRowDataBound="gviewMembers_RowDataBound" CellPadding="0" GridLines="None" OnSorting="gviewMembers_Sorting">
        <Columns>
            <asp:TemplateField HeaderText="Committee">
                <ItemTemplate>
                    <asp:CheckBox ID="chkAllowMember" runat="server" />
                </ItemTemplate>
                <ItemStyle CssClass="paddingLeft" />
                <HeaderStyle CssClass="paddingLeft" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Faculty">
                <ItemTemplate>
                    <asp:CheckBox ID="chkAllowFaculty" runat="server" />
                </ItemTemplate>
                <ItemStyle CssClass="paddingLeft" />
                <HeaderStyle CssClass="paddingLeft" />
            </asp:TemplateField>            
            <asp:TemplateField HeaderText="Reviewer">
                <ItemTemplate>
                    <asp:CheckBox ID="chkAllowReview" runat="server" />
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
    </asp:GridView>--%>

    <br />
    <asp:Panel ID="pnlAccess" runat="server" Visible="false">
        <asp:Panel ID="pnlUpdateAccess" runat="server">
        
        <asp:Button ID="btnUpdateAccess" runat="server" Text="Update Access" OnClick="btnUpdateAccess_Click" /><br />        
        <asp:Label ID="lblCommitteeUpdated" runat="server" ForeColor="green" EnableViewState="false"></asp:Label>
        <AjaxControlToolkit:AnimationExtender ID="animationApplicationStatus" runat="server" TargetControlID="lblCommitteeUpdated">
            <Animations>
                <OnLoad>
                    <Sequence>
                        <Color Duration="2"
                        StartValue="#ffff99"
                        EndValue="#FFFFFF"
                        Property="style"
                        PropertyKey="backgroundColor" />
                        <StyleAction Attribute="backgroundColor" value="" />
                    </Sequence>
                </OnLoad>
            </Animations>                            
        </AjaxControlToolkit:AnimationExtender>        
        <br /><br />
        
        </asp:Panel>
            
        <br />
        <span class="boxTitle">Add Additional Member</span>
        <div style="width: 500px;" class="box">
        <br />
        <asp:Panel ID="pnlAddMember" runat="server">

            Email or Kerberos Login: <asp:TextBox ID="txtSearchQuery" runat="server" MaxLength="50" ></asp:TextBox>
            <input type="button" id="btnLookupUser" onclick="LookupKerberosUser()" value="Lookup Person"/>
                <img id="imgMemberLoginProgress" alt="Progress" src="../Images/progress.gif" style="visibility:hidden" />
                <asp:RequiredFieldValidator id="reqValLoginID" ControlToValidate="txtSearchQuery" ErrorMessage="* Email or Login Required" runat="server" ValidationGroup="ExternalMember" Display="Dynamic" />
            <br /><br />
            LoginID: <asp:TextBox ID="txtLoginID" runat="server" MaxLength="50" Enabled="False"></asp:TextBox><br /><br />
            First Name: <asp:TextBox ID="txtFName" runat="server" MaxLength="50" Enabled="False"></asp:TextBox><br /><br />
            Last Name: <asp:TextBox ID="txtLName" runat="server" MaxLength="50" Enabled="False"></asp:TextBox><br /><br />
            Department: <asp:TextBox ID="txtDepartment" runat="server" MaxLength="50" Enabled="False"></asp:TextBox><br /><br />
            Member Type: <asp:DropDownList ID="dlistMemberType" Enabled="False" runat="server">
                            <asp:ListItem Text="Committee" Selected="true"></asp:ListItem>
                            <asp:ListItem Text="Faculty"></asp:ListItem>
                            <asp:ListItem Text="Review"></asp:ListItem>
                        </asp:DropDownList><br /><br />
            
            <asp:Button ID="btnAddMember" runat="server" Text="Add Member" OnClick="btnAddMember_Click" ValidationGroup="ExternalMember" Enabled="False" />   
        </asp:Panel>
        </div>    
    </asp:Panel>
</asp:Content>