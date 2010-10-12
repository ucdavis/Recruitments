<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApplicationsList.aspx.cs" Inherits="CAESDO.Recruitment.Web.Authorized_ApplicationsList" Title="Applications List" Theme="MainTheme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <Ajax:ScriptManagerProxy ID="scriptProxy" runat="server">
        <Services>
            <Ajax:ServiceReference Path="RecruitmentService.asmx" />
        </Services>
    </Ajax:ScriptManagerProxy>
      
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

            $("input.qs_input").keydown(function() {
                //When searching, automatically clear out the submitted application filter
                $("#chkShowUnsubmitted").removeAttr("checked");
            });

            $("#chkShowUnsubmitted").click(function() {
                var allApplications = $("#tblApplications tbody tr");

                if ($(this).is(":checked")) {
                    //Hide the unsubmitted rows
                    var justSome = allApplications.filter(":has(td.submittedHeader:contains('False'))").hide(0); //allApplications.filter("td.submittedHeader").hide(0);
                    //debugger;
                }
                else {
                    //Show all rows
                    allApplications.show(0);
                }

                //$("input.qs_input").keydown();
                $("#tblApplications").trigger("applyWidgets"); //Apply the zebra stripes
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
            <span style="float:right;"><input id="chkShowUnsubmitted" type="checkbox" /><label for="chkShowUnsubmitted">Show Submitted Only</label></span>
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
                        <th>
                            Submit Date
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
                <td class="submittedHeader">
                    <%# Eval("Submitted") %>
                </td>
                <td>
                    <%# (bool)Eval("Submitted") ? ((DateTime)Eval("SubmitDate")).ToShortDateString() : "N/A" %>
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
        <asp:LinkButton ID="btnUpdateList" runat="server" OnClick="btnUpdateList_Click" CssClass="no_border right_space">
            <img src="../Images/reload.png" alt="Update Applications List" />Update Applications List</asp:LinkButton>
                    <script type="text/javascript">

                        function GetReferenceSample() {
                            var templateText = $("#referenceEmail").html();

                            RecruitmentService.GetTemplatePreview(templateText, DisplayReferenceSample);
                        }

                        function DisplayReferenceSample(result) {
                            var newWin = window.open('', 'Preview', '');

                            if (newWin != null) {
                                var doc = newWin.document;
                                doc.write(result);
                                doc.close();
                            }
                        }
        
        </script>
        <a href="javascript:GetReferenceSample();" class="no_border right_space">
            <img src="../Images/mail_find.png" alt="Preview Reference Email" />
            Preview Reference Email</a>
        <asp:LinkButton ID="btnEmailReferences" runat="server" OnClick="btnEmailReferences_Click" CssClass="no_border">
            <img src="../Images/mail_message.png" alt="Email References"/> Email References</asp:LinkButton>


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
        
       
         
        <div id="referenceEmail" style="display:none;">
            <asp:Literal ID="litReferenceTemplate" runat="server"></asp:Literal>
            <br />
        </div>
        
    </asp:Panel>
</asp:Content>
