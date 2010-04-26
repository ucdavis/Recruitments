<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmailTemplates.aspx.cs" Inherits="CAESDO.Recruitment.Web.Authorized_EmailTemplates" Title="Send Templated Emails" Theme="MainTheme" ValidateRequest="false" %>
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
                sortList: [[3, 1], [1, 0]],
                cssAsc: 'headerSortUp',
                cssDesc: 'headerSortDown',
                cssHeader: 'header',
                headers: { 0: { sorter: false} },
                widgets: ['zebra']
            });
        });
        
    </script>

    <asp:DropDownList ID="dlistApplicants" runat="server" AutoPostBack="True" DataSourceID="ObjectDataPositions" DataTextField="TitleAndApplicationCount" DataTextFormatString="{0}" DataValueField="ID" AppendDataBoundItems="True">
        <asp:ListItem Selected="True" Value="0">Select a Position</asp:ListItem>
    </asp:DropDownList>
    <asp:ObjectDataSource ID="ObjectDataPositions" runat="server"
        SelectMethod="GetByStatus" TypeName="CAESDO.Recruitment.BLL.PositionBLL" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="false" Name="Closed" Type="Boolean" />
            <asp:Parameter DefaultValue="true" Name="AdminAccepted" Type="Boolean" />
            <asp:Parameter Name="AllowApplications" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    <br /><br />
    
    <asp:ListView ID="lviewApplications" runat="server" 
        DataSourceID="ObjectDataApplications" DataKeyNames="id" 
        ondatabound="lviewApplications_DataBound">
        <LayoutTemplate>
        <table id="tblApplications" class="tablesorter tablesearch">
            <thead>
                <tr>
                    <th>
                        Email?
                    </th>
                    <th>
                        Name
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
                <tr id="itemPlaceholder" runat="server"></tr>
            </tbody>
        </table>
        </LayoutTemplate>
        <ItemTemplate>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkEmailApplicant" runat="server" />
                    </td>
                    <td>
                        <%# GetNullSafeFullName((string)Eval("AssociatedProfile.FullName")) %>
                    </td>
                    <td>
                        <%# Eval("Email") %>
                    </td>
                    <td>
                        <%# Eval("Submitted") %>
                    </td>
                </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            No Applications Found For This Position
        </EmptyDataTemplate>
    </asp:ListView>

    <asp:ObjectDataSource ID="ObjectDataApplications" runat="server" SelectMethod="GetByPositionID"
        TypeName="CAESDO.Recruitment.BLL.ApplicationBLL" OldValuesParameterFormatString="original_{0}">
         <SelectParameters>
            <asp:ControlParameter ControlID="dlistApplicants" Name="positionID" PropertyName="SelectedValue"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    <br />
    <asp:Panel ID="pnlApplicationsExist" runat="server" Visible="false">
    <asp:Button ID="btnSendTemplate" runat="server" Text="Send Template Emails" 
            CausesValidation="true" ValidationGroup="EmailTemplate" 
            onclick="btnSendTemplate_Click" /><br />
    <asp:Label ID="lblSentEmail" runat="server" ForeColor="green" EnableViewState="false"></asp:Label>
        <AjaxControlToolkit:AnimationExtender ID="animationApplicationStatus" runat="server" TargetControlID="lblSentEmail">
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
                                
    <br />
    <hr />
    <br />
    
    Template Type: 
    <asp:DropDownList ID="dlistEmailTemplates" runat="server" 
            DataSourceID="odsTemplateTypes" DataTextField="Type" DataValueField="Type" AppendDataBoundItems="true">
            <asp:ListItem Text="-- Select A Template --" Value=""></asp:ListItem>
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="reqValEmailTemplate" runat="server" ControlToValidate="dlistEmailTemplates" ValidationGroup="EmailTemplate"
     ErrorMessage="* Please select a template type" InitialValue=""></asp:RequiredFieldValidator>
    
    <asp:ObjectDataSource ID="odsTemplateTypes" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetEmailTemplates" 
        TypeName="CAESDO.Recruitment.BLL.TemplateTypeBLL"></asp:ObjectDataSource>
    
    <script src="../JS/tiny_mce/tiny_mce.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        var EmailTemplateEditor = null;

        tinyMCE.init({
            mode: "specific_textareas",
            editor_selector: "richTextEditor", //Just use textareas with the richTextEditor class applied
            theme: "advanced",
            skin: "o2k7",
            plugins: "paste",

            theme_advanced_buttons1: "bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,formatselect,fontsizeselect",
            theme_advanced_buttons2: "cut,copy,pastetext,pasteword,|,bullist,numlist,|,undo,redo,|,link,unlink,anchor,image,|,forecolor,backcolor",
            theme_advanced_buttons3: "",
            theme_advanced_toolbar_location: "top",

            setup: function(ed) {
            EmailTemplateEditor = ed;
            }
        });

        function InsertTemplateText(text) {
            EmailTemplateEditor.focus();
            EmailTemplateEditor.selection.setContent(text);
        }

        //Setup the events around template creation
        $(document).ready(function() {
            $("select[id$=dlistEmailTemplates]").change(TemplateSectionChanged);

            $('#ReferenceTemplateHelp').bt('When creating a form letter you can click the fields bellow and the information will auto populate the reference template', {
                trigger: 'click',
                positions: 'top'
            });
        });

        function TemplateSectionChanged() {
            EmailTemplateEditor.setProgressState(1); //Set the progress image
            
            //Now get the template text
            RecruitmentService.GetTemplateText($(this).val(), TemplateSectionChangedSuccess, TemplateSectionChangedFailure);
        }

        function TemplateSectionChangedSuccess(result) {
            EmailTemplateEditor.setProgressState(0);
            EmailTemplateEditor.setContent(result);
        }

        function TemplateSectionChangedFailure() {
            EmailTemplateEditor.setProgressState(0);
            EmailTemplateEditor.setContent("An Error Has Occurred While Retrieving The Template");
        }
        
    </script>

        <div>
            <strong>Template Fields:</strong><img alt="Reference Template Help" id="ReferenceTemplateHelp"
                src="../Images/question_blue.png" /><br class="bottom_space" />
            <a href="javascript:InsertTemplateText('<%= GetGlobalResourceObject("RecruitmentResources", "ApplicantNameValue") %>');">
                <%= GetGlobalResourceObject("RecruitmentResources", "ApplicantNameText")%><br />
            </a><a href="javascript:InsertTemplateText('<%= GetGlobalResourceObject("RecruitmentResources", "DeadlineValue") %>');">
                <%= GetGlobalResourceObject("RecruitmentResources", "DeadlineText")%><br />
            </a><a href="javascript:InsertTemplateText('<%= GetGlobalResourceObject("RecruitmentResources", "PositionTitleValue") %>');">
                <%= GetGlobalResourceObject("RecruitmentResources", "PositionTitleText")%><br />
            </a><a href="javascript:InsertTemplateText('<%= GetGlobalResourceObject("RecruitmentResources", "PrimaryDepartmentValue") %>');">
                <%= GetGlobalResourceObject("RecruitmentResources", "PrimaryDepartmentText")%><br />
            </a><a href="javascript:InsertTemplateText('<%= GetGlobalResourceObject("RecruitmentResources", "RecruitmentAdminNameValue") %>');">
                <%= GetGlobalResourceObject("RecruitmentResources", "RecruitmentAdminNameText")%><br />
            </a>
        </div>
        <div style="width: 818px; height: 389px; background: url(../Images/envelope.jpg) no-repeat;
            padding: 50px;">
            <div class="blueletter">
                <asp:TextBox ID="txtEmailTemplate" runat="server" CssClass="richTextEditor" TextMode="MultiLine"
                    Width="100%"></asp:TextBox>
                <br />
            </div>
        </div>
    <br />
    </asp:Panel>

</asp:Content>

