<%@ Page AutoEventWireup="true" CodeFile="PositionManagement.aspx.cs" Inherits="CAESDO.Recruitment.Web.PositionManagement" Theme="MainTheme" ValidateRequest="false" Language="C#" MasterPageFile="~/MasterPage.master" Title="Position Management"  Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
     <Ajax:ScriptManagerProxy ID="scriptProxy" runat="server">
        <Services>
            <Ajax:ServiceReference Path="RecruitmentService.asmx" />
        </Services>
    </Ajax:ScriptManagerProxy>
    
    <script src="../JS/tiny_mce/tiny_mce.js" type="text/javascript"></script>
    <script type="text/javascript">
        var refTemplateEditor = null;

        tinyMCE.init({
            mode: "specific_textareas",
            editor_selector: "richTextEditor", //Just use textareas with the richTextEditor class applied
            theme: "advanced",
            skin: "o2k7",
            plugins: "paste",

            theme_advanced_buttons1: "preview,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,formatselect,fontsizeselect",
            theme_advanced_buttons2: "cut,copy,pastetext,pasteword,|,bullist,numlist,|,undo,redo,|,link,unlink,anchor,image,|,forecolor,backcolor",
            theme_advanced_buttons3: "",
            theme_advanced_toolbar_location: "top",

            setup: function(ed) {
                refTemplateEditor = ed;

                ed.addButton('preview', {
                    title: 'Preview', image: '../Images/delete.gif',
                    onclick: function() {

                        var content = ed.getContent(); //Get the current content inside the editor

                        RecruitmentService.GetTemplatePreview(content, callback);

                        function callback(result) {
                            newWin = window.open('', 'Preview', '');

                            if (newWin != null) {
                                var doc = newWin.document;
                                doc.write(result);
                                doc.close();
                            }
                        }
                    }
                });
            }
        });

        function InsertTemplateText(text) {
            refTemplateEditor.focus();
            refTemplateEditor.selection.setContent(text);
        }

        //Help balloons
        $(document).ready(function() {
            $("input[id$=txtPositionTitle]").bt('Examples: <br/>*Asst. Prof of Climate Control, LAWR<br/>*Professor of Brewing', {
                trigger: ['focus', 'blur'],
                positions: ['right']
            });

            $("input[id$=txtHRPhone]").bt('Phone Number Format Examples: <br/>xxx-yyy-zzzz<br/>(xxx) yyy-zzzz', {
                trigger: ['focus', 'blur'],
                positions: ['right']
            });
            $('#ReferenceTemplateHelp').bt('When creating a form letter you can click the fields bellow and the information will auto populate the reference template', {
                trigger: 'click',
                positions: 'top'
            });
        });
        
    </script>
    <span class="boxTitle"><asp:Image ID="imgProfile" runat="server" EnableViewState="False" ImageUrl="~/Images/profile_sm.gif" style="vertical-align:middle;" meta:resourcekey="imgProfileResource1" /><asp:Literal ID="litPositionState" runat="server" Text="Create Position" EnableViewState="False" meta:resourcekey="litPositionStateResource1"></asp:Literal></span><br />
    <table class="box" style="width:690px;" cellpadding="5">
        <tr>
            <td colspan="2"><br /></td>
        </tr>
        <tr>
            <td   align="right" style="width: 240">
                Position Title:</td>
            <td  >
                <asp:TextBox ID="txtPositionTitle" runat="server" MaxLength="100" meta:resourcekey="txtPositionTitleResource1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqValPositionTitle" runat="server" ControlToValidate="txtPositionTitle" ErrorMessage="* Position Title Required" meta:resourcekey="reqValPositionTitleResource1"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td   align="right" style="width: 240">
                Position Number:</td>
            <td  >
                <asp:TextBox ID="txtPositionNumber" runat="server" MaxLength="20" meta:resourcekey="txtPositionNumberResource1"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 240" valign="top">
                Review Date:</td>
            <td >
                <asp:TextBox ID="txtDeadline" runat="server" meta:resourcekey="txtDeadlineResource1"></asp:TextBox> <asp:Image ID="imgDeadlineCalendar" runat="server" ImageUrl="~/Images/icon.calendar.png" AlternateText="Click to show calendar" meta:resourcekey="imgDeadlineCalendarResource1" />
                <AjaxControlToolkit:CalendarExtender ID="calDeadline" runat="server" TargetControlID="txtDeadline" PopupButtonID="imgDeadlineCalendar" Enabled="True"></AjaxControlToolkit:CalendarExtender>
                
                <asp:RequiredFieldValidator ID="reqValDeadline" runat="server" ControlToValidate="txtDeadline" ErrorMessage="* Review Date Required" Display="Dynamic" meta:resourcekey="reqValDeadlineResource1"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="comValDeadline" Type="Date" runat="server" Operator="DataTypeCheck" ControlToValidate="txtDeadline" ErrorMessage="* Review Date Format Not Valid" Display="Dynamic" meta:resourcekey="comValDeadlineResource1"></asp:CompareValidator>
                
            </td>
        </tr>
        <tr>
            <td   align="right" style="width: 240" valign="top">
                Department:</td>
            <td  >
                <Ajax:UpdatePanel ID="updateDepartments" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DropDownList ID="dlistDepartment" runat="server" DataSourceID="ObjectDataUnits" DataTextField="ShortName" DataValueField="id" meta:resourcekey="dlistDepartmentResource1"></asp:DropDownList>
                    <asp:LinkButton ID="lbtnAddDepartment" runat="server" Text="Add Department" CausesValidation="False" OnClick="lbtnAddDepartment_Click" meta:resourcekey="lbtnAddDepartmentResource1"></asp:LinkButton>
                    <br />
                    <br />
                    
                    <asp:GridView ID="gviewDepartments" runat="server" DataKeyNames="DepartmentFIS" AutoGenerateColumns="False" OnRowDeleting="gviewDepartments_RowDeleting" Width="346px" SkinID="gridViewPosManage" meta:resourcekey="gviewDepartmentsResource1">
                    <Columns>
                        <asp:TemplateField HeaderText="Home Dept" meta:resourcekey="TemplateFieldResource1">
                            <ItemTemplate>
                                <asp:CheckBox ID="cboxPrimary" runat="server" Checked='<%# Eval("PrimaryDept") %>' AutoPostBack="True" OnCheckedChanged="cboxPrimary_CheckedChanged" meta:resourcekey="cboxPrimaryResource1" />
                            </ItemTemplate>
                            <ItemStyle CssClass="paddingLeft" />
                            <HeaderStyle CssClass="paddingLeft" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department Name" meta:resourcekey="TemplateFieldResource2">
                            <ItemTemplate>
                                <asp:Literal ID="litDepartmentName" runat="server" Text='<%# Eval("Unit.ShortName") %>'></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField DeleteText="Remove" ShowDeleteButton="True" meta:resourcekey="CommandFieldResource1" />
                    </Columns>                    
                        <HeaderStyle HorizontalAlign="Left" CssClass="paddingRight" />
                        <RowStyle CssClass="paddingRight" />
                    </asp:GridView>
                    <asp:Label ID="lblPrimaryDeptErrorMessage" runat="server" EnableViewState="False" ForeColor="Red" meta:resourcekey="lblPrimaryDeptErrorMessageResource1"></asp:Label>
                </ContentTemplate>
                </Ajax:UpdatePanel>
                
                <asp:ObjectDataSource ID="ObjectDataUnits" runat="server" SelectMethod="GetAll"
                    TypeName="CAESDO.Recruitment.BLL.UnitBLL">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="ShortName" Name="propertyName" Type="String" />
                        <asp:Parameter DefaultValue="true" Name="ascending" Type="Boolean" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td   align="right" style="width: 240">
                Recruitment Representative:</td>
            <td  >
                <asp:TextBox ID="txtHRRep" runat="server" MaxLength="100" meta:resourcekey="txtHRRepResource1"></asp:TextBox>
                <asp:RequiredFieldValidator id="reqValHRRep" ControlToValidate="txtHRRep" ErrorMessage="* Recruitment Reprenentative Required" runat="server" meta:resourcekey="reqValHRRepResource1"/>
            </td>
        </tr>
        <tr>
            <td   align="right" style="width: 240">
                Recruitment Rep Phone Number:</td>
            <td  >
                <asp:TextBox ID="txtHRPhone" runat="server" MaxLength="13" meta:resourcekey="txtHRPhoneResource1"></asp:TextBox>                
                <asp:RegularExpressionValidator ID="regValHRPhone" runat="server" ControlToValidate="txtHRPhone" ErrorMessage="* Phone Number Required" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" meta:resourcekey="regValHRPhoneResource1"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 240">
                Recruitment Rep Email:
            </td>
            <td>
                <asp:TextBox ID="txtHREmail" runat="server" MaxLength="100" meta:resourcekey="txtHREmailResource1"></asp:TextBox>
                
                <asp:RequiredFieldValidator id="reqValHREmail" ControlToValidate="txtHREmail" ErrorMessage="* Email Required" runat="server" Display="Dynamic" meta:resourcekey="reqValHREmailResource1"/>                
                <asp:RegularExpressionValidator ID="regValHREmail" runat="server" ErrorMessage="* Email Format Not Valid" ControlToValidate="txtHREmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" meta:resourcekey="regValHREmailResource1"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td align="right" style="width:240">
                Search Plan (PDF):
            </td>
            <td  >
                <asp:LinkButton ID="lbtnDownloadSearchPlan" runat="server" Text="Download Existing Search Plan" Visible="False" OnClick="lbtnDownloadSearchPlan_Click"></asp:LinkButton>
                <asp:ImageButton ID="ibtnReplaceSearchPlan" runat="server" ImageUrl="~/Images/delete.gif" AlternateText="[Replace]" Visible="False" />
                <asp:Literal ID="litDownloadSearchPlan" runat="server" Visible="False" Text="<br /><br />"></asp:Literal>
                <asp:FileUpload ID="fileSearchPlan" runat="server" />
                <asp:RequiredFieldValidator ID="reqValSearchPlan" runat="server" Display="Dynamic" ControlToValidate="fileSearchPlan" ErrorMessage="* Search Plan Required" ></asp:RequiredFieldValidator>
                <asp:Label ID="lblInvalidSearchPlanFileType" runat="server" ForeColor="Red" EnableViewState="False"></asp:Label>
                                                                     
                <asp:Panel ID="pnlSearchPlan" runat="server" CssClass="modalPopup" style="display:none;">
                    Replace Existing Search Plan: <br /><br />
                    <asp:FileUpload ID="fileSearchPlanReplace" runat="server" />
                    <asp:RequiredFieldValidator ID="reqValSearchPlanReplace" runat="server" ControlToValidate="fileSearchPlanReplace" ErrorMessage="* Search Plan Required" ValidationGroup="ReplaceSearchPlan"></asp:RequiredFieldValidator>
                    <br /><br />
                    <asp:Button ID="btnSearchPlanReplace" runat="server" Text="Upload" ValidationGroup="ReplaceSearchPlan" OnClick="btnSearchPlanReplace_Click" />
                    <asp:Button ID="btnSearchPlanReplaceCancel" runat="server" CausesValidation="False" Text="Cancel" />
                </asp:Panel>
                                
                <AjaxControlToolkit:ModalPopupExtender ID="mpopupSearchPlanReplacement" runat="server" TargetControlID="ibtnReplaceSearchPlan"
                                                     PopupControlID="pnlSearchPlan" BackgroundCssClass="modalBackground" CancelControlID="btnSearchPlanReplaceCancel" DynamicServicePath="" Enabled="True" />
            </td>
        </tr>
        <tr>
            <td align="right" valign="top" style="width: 240">
                <span id="spanSummaryInfo">Summary<%--<img src="../Images/modify.gif" alt="SummaryInfo" /></span>--%>:</td>
            <td  >
                <asp:TextBox ID="txtShortDescription" runat="server" Height="255px" Rows="4" TextMode="MultiLine"
                    Width="532px" meta:resourcekey="txtShortDescriptionResource1" ></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" valign="top" style="width:240">
                Reference Template v2:
            </td>
            <td>
                <div id="create_pos_word_bank">
                    <strong>Template Fields:</strong><img id="ReferenceTemplateHelp" src="../Images/question_blue.png" /><br
                        class="bottom_space" />
                    <a href="javascript:InsertTemplateText('<%= GetLocalResourceObject("ReferenceTitle.Value") %>');">
                        <%= GetLocalResourceObject("ReferenceTitle.Text")%><br />
                    </a><a href="javascript:InsertTemplateText('<%= GetLocalResourceObject("ReferenceLastName.Value") %>');">
                        <%= GetLocalResourceObject("ReferenceLastName.Text") %><br />
                    </a><a href="javascript:InsertTemplateText('<%= GetLocalResourceObject("ApplicantName.Value") %>');">
                        <%= GetLocalResourceObject("ApplicantName.Text") %><br />
                    </a><a href="javascript:InsertTemplateText('<%= GetLocalResourceObject("PositionTitle.Value") %>');">
                        <%= GetLocalResourceObject("PositionTitle.Text") %><br />
                    </a><a href="javascript:InsertTemplateText('<%= GetLocalResourceObject("RecruitmentAdminName.Value") %>');">
                        <%= GetLocalResourceObject("RecruitmentAdminName.Text") %><br />
                    </a><a href="javascript:InsertTemplateText('<%= GetLocalResourceObject("PrimaryDepartment.Value") %>');">
                        <%= GetLocalResourceObject("PrimaryDepartment.Text") %><br />
                    </a><a href="javascript:InsertTemplateText('<hr/><%= GetLocalResourceObject("ConfidentialityStatement.Value") %>');">
                        <%= GetLocalResourceObject("ConfidentialityStatement.Text") %><br />
                    </a><a href="javascript:InsertTemplateText('<%= GetLocalResourceObject("UploadLink.Value") %>');">
                        <%= GetLocalResourceObject("UploadLink.Text")%></a>
                </div>
                <asp:TextBox ID="txtReferenceTemplate" TextMode="MultiLine" CssClass="richTextEditor"
                    runat="server" Width="530px" Height="300px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqValReferenceTemplate2" ControlToValidate="txtReferenceTemplate"
                    ErrorMessage="* Reference Template Required" runat="server" meta:resourcekey="reqValReferenceTemplateResource1">
                </asp:RequiredFieldValidator>
                <br />
                <asp:Literal ID="litReferenceTemplateHelp" runat="server" meta:resourcekey="litReferenceTemplateHelp"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 240">
                Full job description (PDF):</td>
            <td  >
                <asp:LinkButton ID="lbtnDownloadPositionDescription" runat="server" Text="Download Existing File" Visible="False" OnClick="lbtnDownloadPositionDescription_Click" meta:resourcekey="lbtnDownloadPositionDescriptionResource1"></asp:LinkButton>
                <asp:ImageButton ID="ibtnReplacePositionDescription" runat="server" ImageUrl="~/Images/delete.gif" AlternateText="[Replace]" Visible="False" meta:resourcekey="ibtnReplacePositionDescriptionResource1" />
                <asp:Literal ID="litDownloadPositionDescription" runat="server" Visible="False" meta:resourcekey="litDownloadPositionDescriptionResource1" Text="<br /><br />"></asp:Literal>
                <asp:FileUpload ID="filePositionDescription" runat="server" meta:resourcekey="filePositionDescriptionResource1" />                
                <asp:RequiredFieldValidator ID="reqValPositionDescription" runat="server" Display="Dynamic" ControlToValidate="filePositionDescription" ErrorMessage="* Job Description Required" meta:resourcekey="reqValPositionDescriptionResource1"></asp:RequiredFieldValidator>
                <asp:Label ID="lblInvalidFileType" runat="server" ForeColor="Red" EnableViewState="False" meta:resourcekey="lblInvalidFileTypeResource1"></asp:Label>
                                                                     
                <asp:Panel ID="pnlPositionDescription" runat="server" CssClass="modalPopup" style="display:none;" meta:resourcekey="pnlPositionDescriptionResource1">
                    Replace Existing Position Description: <br /><br />
                    <asp:FileUpload ID="filePositionDescriptionReplace" runat="server" meta:resourcekey="filePositionDescriptionReplaceResource1" />
                    <asp:RequiredFieldValidator ID="reqValPositionDescriptionReplace" runat="server" ControlToValidate="filePositionDescriptionReplace" ErrorMessage="* Position Description Required" ValidationGroup="Replace" meta:resourcekey="reqValPositionDescriptionReplaceResource1"></asp:RequiredFieldValidator>
                    <br /><br />
                    <asp:Button ID="btnPositionDescriptionReplace" runat="server" Text="Upload" ValidationGroup="Replace" OnClick="btnPositionDescriptionReplace_Click" meta:resourcekey="btnPositionDescriptionReplaceResource1" />
                    <asp:Button ID="btnPositionDescriptionReplaceCancel" runat="server" CausesValidation="False" Text="Cancel" meta:resourcekey="btnPositionDescriptionReplaceCancelResource1" />
                </asp:Panel>
                                
                <AjaxControlToolkit:ModalPopupExtender ID="mpopupPositionDescriptionReplacement" runat="server" TargetControlID="ibtnReplacePositionDescription"
                                                     PopupControlID="pnlPositionDescription" BackgroundCssClass="modalBackground" CancelControlID="btnPositionDescriptionReplaceCancel" DynamicServicePath="" Enabled="True" />
            </td>
        </tr>
        <tr>
            <td   align="right" style="width: 40">
                Number of Required Publications:</td>
            <td  >
                <asp:TextBox ID="txtPublications" runat="server" meta:resourcekey="txtPublicationsResource1"></asp:TextBox>
                <AjaxControlToolkit:NumericUpDownExtender ID="numPublications" runat="server" Width="146" TargetControlID="txtPublications" Enabled="True" Maximum="1.7976931348623157E+308" Minimum="-1.7976931348623157E+308" RefValues="" ServiceDownMethod="" ServiceDownPath="" ServiceUpMethod="" Tag="" TargetButtonDownID="" TargetButtonUpID=""></AjaxControlToolkit:NumericUpDownExtender>
                
                
                <asp:CompareValidator ID="comValPublications" runat="server" ErrorMessage="* Number of Publications Can Not Be Negative" ControlToValidate="txtPublications" ValueToCompare="0" Type="Integer" Operator="GreaterThanEqual" Display="Dynamic" meta:resourcekey="comValPublicationsResource1"></asp:CompareValidator>
                <asp:RequiredFieldValidator ID="reqValPublications" runat="server" ErrorMessage="* Publications Required" ControlToValidate="txtPublications" Display="Dynamic" meta:resourcekey="reqValPublicationsResource1"></asp:RequiredFieldValidator>    
            </td>
        </tr>
        <tr>
            <td   align="right" style="width: 240">
                Number of Required References:</td>
            <td  >
                <asp:TextBox ID="txtReferences" runat="server" meta:resourcekey="txtReferencesResource1"></asp:TextBox>   
                <AjaxControlToolkit:NumericUpDownExtender ID="numReferences" runat="server" Width="146" TargetControlID="txtReferences" Enabled="True" Maximum="1.7976931348623157E+308" Minimum="-1.7976931348623157E+308" RefValues="" ServiceDownMethod="" ServiceDownPath="" ServiceUpMethod="" Tag="" TargetButtonDownID="" TargetButtonUpID=""></AjaxControlToolkit:NumericUpDownExtender>
                
                <asp:CompareValidator ID="comValReferences" runat="server" ErrorMessage="* Number of References Can Not Be Negative" ControlToValidate="txtReferences" ValueToCompare="0" Type="Integer" Operator="GreaterThanEqual" Display="Dynamic" meta:resourcekey="comValReferencesResource1"></asp:CompareValidator>
                <asp:RequiredFieldValidator ID="reqValReferences" runat="server" ErrorMessage="* Reference Required" ControlToValidate="txtReferences" Display="Dynamic" meta:resourcekey="reqValReferencesResource1"></asp:RequiredFieldValidator>                    
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 240">
                Show Education:
            </td>
            <td>
                <asp:CheckBox ID="chkShowEducation" runat="server" meta:resourcekey="chkShowEducationResource1" />
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 240">
                Show Current Position:
            </td>
            <td>
                <asp:CheckBox ID="chkShowCurrentPosition" runat="server" meta:resourcekey="chkShowCurrentPositionResource1" />
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 240" valign="top">Required File Types:</td>
            <td>
                <asp:GridView ID="gviewFileTypes" runat="server" DataKeyNames="id" AutoGenerateColumns="False" DataSourceID="ObjectDataFileTypes" BorderStyle="None" CellPadding="0" GridLines="None" Width="289px" meta:resourcekey="gviewFileTypesResource1">
                    <Columns>
                        <asp:TemplateField HeaderText="File Types" meta:resourcekey="TemplateFieldResource3">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkFileType" runat="server" Text='<%# BreakCamelCase((string)Eval("FileTypeName")) %>' Checked='<%# doesFileTypeExistInPosition((string)Eval("FileTypeName")) %>' meta:resourcekey="chkFileTypeResource1" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:GridView>                
                <asp:ObjectDataSource ID="ObjectDataFileTypes" runat="server" SelectMethod="GetAllByApplicationFileType"
                    TypeName="CAESDO.Recruitment.BLL.FileTypeBLL">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="true" Name="applicationFileType" Type="Boolean" />
                        <asp:Parameter DefaultValue="FileTypeName" Name="propertyName" Type="String" />
                        <asp:Parameter DefaultValue="true" Name="ascending" Type="Boolean" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 240">
                Allow Applications:</td>
            <td  >
                <asp:CheckBox ID="chkAllowApplications" runat="server" Checked="True" meta:resourcekey="chkAllowApplicationsResource1" />
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 240">
                Allow Faculty Review:
            </td>
            <td>
                <asp:CheckBox ID="chkAllowFaculty" runat="server" meta:resourcekey="chkAllowFacultyResource1" />
            </td>
        </tr>
        <tr>
            <td align="right" style="width:240">
                Closed:
            </td>
            <td>
                <asp:CheckBox ID="chkPositionClosed" runat="server" meta:resourcekey="chkPositionClosedResource1" />
            </td>
        </tr>
        <tr>
            <td   align="right" style="width: 240">
            </td>
            <td align="right"  >
                <br />
                <asp:Button ID="btnModifyPosition" runat="server" Text="Create!" OnClick="btnModifyPosition_Click" meta:resourcekey="btnModifyPositionResource1" /></td>
        </tr>
    </table>
 
</asp:Content>

