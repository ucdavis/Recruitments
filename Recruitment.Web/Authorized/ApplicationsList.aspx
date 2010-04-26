<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApplicationsList.aspx.cs" Inherits="CAESDO.Recruitment.Web.Authorized_ApplicationsList" Title="Applications List" Theme="MainTheme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
    <script type="text/javascript">

        $(document).ready(function() {
            //Sort table
            $("#tblApplications").tablesorter(
            {
                sortList: [[5, 1], [3, 0]],
                cssAsc: 'headerSortUp',
                cssDesc: 'headerSortDown',
                cssHeader: 'header',
                headers: { 0: { sorter: 'checkbox' }
                            , 1: { sorter: 'checkbox' }
                            , 2: { sorter: 'checkbox' }
                            , 3: { sorter: 'link' }
                            , 5: { sorter: 'checkbox' }
                },
                widgets: ['zebra']
            });
        });
        
    </script>
    
    <asp:DropDownList ID="dlistPositions" runat="server" AutoPostBack="True" DataSourceID="ObjectDataPositions" DataTextField="TitleAndApplicationCount" DataValueField="ID" OnSelectedIndexChanged="dlistPositions_SelectedIndexChanged" AppendDataBoundItems="true">
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
    <br /><br />
    
    <asp:ListView ID="lviewApplications" runat="server" DataSourceID="ObjectDataApplications" DataKeyNames="id">
        <LayoutTemplate>
            <table id="tblApplications" class="tablesorter tablesearch">
                <thead>
                    <tr>
                        <th>
                            Interview
                        </th>
                        <th>
                            Get References
                        </th>
                        <th>
                            No Consideration
                        </th>
                        <th>
                            Applicant Name
                        </th>
                        <th>
                            Email
                        </th>
                        <th>
                            Submitted
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
                    <asp:CheckBox ID="chkShortList" runat="server" Checked='<%# Eval("InterviewList") %>' />
                </td>
                <td>
                    <asp:CheckBox ID="chkReferences" runat="server" Checked='<%# Eval("GetReferences") %>' />
                </td>
                <td>
                    <asp:CheckBox ID="chkNoConsideration" runat="server" Checked='<%# Eval("NoConsideration") %>' />
                </td>
                <td>
                    <asp:LinkButton ID="lbtnViewApplication" runat="server" CommandArgument='<%# Eval("id") %>'
                        Text='<%# GetNullSafeFullName((string)Eval("AssociatedProfile.FullName")) %>'
                        OnClick="lbtnViewApplication_Click"></asp:LinkButton>
                </td>
                <td>
                    <%# Eval("Email") %>
                </td>
                <td>
                    <asp:CheckBox ID="checkbox" runat="server" Enabled="false" Checked='<%# (bool)Eval("Submitted")  %>' />
                </td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            No Applications Found For This Position
        </EmptyDataTemplate>
    </asp:ListView>
        
    <asp:ObjectDataSource ID="ObjectDataApplications" runat="server" SelectMethod="GetByPosition"
        TypeName="CAESDO.Recruitment.BLL.ApplicationBLL" OnSelecting="ObjectDataApplications_Selecting" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Name="position" Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:Panel ID="pnlPositionSelected" runat="server" Visible="false">
        <asp:Button ID="btnUpdateList" runat="server" Text="Update Applications List"
            OnClick="btnUpdateList_Click" />
        <asp:Button ID="btnEmailReferences" runat="server" Text="Email References"
            OnClick="btnEmailReferences_Click" />
        <br />
        <asp:Label ID="lblResult" runat="server" EnableViewState="false"></asp:Label>
        <AjaxControlToolkit:AnimationExtender ID="animationApplicationStatus" runat="server"
            TargetControlID="lblResult">
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
        <AjaxControlToolkit:ConfirmButtonExtender ID="confirmEmailReferences" runat="server"
            ConfirmText="You are about to email all references for the Short Listed applicants"
            TargetControlID="btnEmailReferences">
        </AjaxControlToolkit:ConfirmButtonExtender>
    </asp:Panel>
</asp:Content>
