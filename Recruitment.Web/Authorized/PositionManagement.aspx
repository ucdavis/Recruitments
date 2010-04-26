<%@ Page AutoEventWireup="true" CodeFile="PositionManagement.aspx.cs" Inherits="CAESDO.Recruitment.Web.PositionManagement" Theme="MainTheme" ValidateRequest="false" Language="C#" MasterPageFile="~/MasterPage.master" Title="Position Management"  Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <script type="text/javascript" language="javascript">
        
        function InsertText(txtID, text)
        {
            var parent = FTB_API[txtID].GetParentElement();
            
            if ( parent.tagName == 'HTML' )
            {                
                var currentHTML = FTB_API[txtID].GetHtml();
                
                FTB_API[txtID].SetHtml(currentHTML + text);                
            }
            else if ( parent.tagName == "BR" )
            {
                return; //Don't do anything with BR's, they aren't formatted correctly
            }
            else            
            {
                FTB_API[txtID].InsertHtml(text);
            }   
        }
        
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
                    <asp:DropDownList ID="dlistDepartment" runat="server" DataSourceID="ObjectDataUnits" DataTextField="ShortName" DataValueField="FISCode" meta:resourcekey="dlistDepartmentResource1"></asp:DropDownList>
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
                    TypeName="CAESDO.Recruitment.Data.NHibernateDaoFactory+UnitDao">
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
            <td align="right" valign="top" style="width: 240">
                Summary:</td>
            <td  >
                <asp:TextBox ID="txtShortDescription" runat="server" Height="255px" Rows="4" TextMode="MultiLine"
                    Width="502px" meta:resourcekey="txtShortDescriptionResource1"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" valign="top" style="width: 240">
                Reference Template:
            </td>
            <td>
                Template Fields: 
                    <a href="javascript:InsertText('<%= ftxtReferenceTemplate.ClientID %>', '<%= GetLocalResourceObject("ReferenceLastName.Value") %>');" ><%= GetLocalResourceObject("ReferenceLastName.Text") %></a>,
                    <a href="javascript:InsertText('<%= ftxtReferenceTemplate.ClientID %>', '<%= GetLocalResourceObject("ApplicantName.Value") %>');" ><%= GetLocalResourceObject("ApplicantName.Text") %></a>,
                    <a href="javascript:InsertText('<%= ftxtReferenceTemplate.ClientID %>', '<%= GetLocalResourceObject("Deadline.Value") %>');" ><%= GetLocalResourceObject("Deadline.Text") %></a>,
                    <a href="javascript:InsertText('<%= ftxtReferenceTemplate.ClientID %>', '<%= GetLocalResourceObject("PositionTitle.Value") %>');" ><%= GetLocalResourceObject("PositionTitle.Text") %></a>,
                    <a href="javascript:InsertText('<%= ftxtReferenceTemplate.ClientID %>', '<%= GetLocalResourceObject("RecruitmentAdminName.Value") %>');" ><%= GetLocalResourceObject("RecruitmentAdminName.Text") %></a>,
                    <a href="javascript:InsertText('<%= ftxtReferenceTemplate.ClientID %>', '<%= GetLocalResourceObject("PrimaryDepartment.Value") %>');" ><%= GetLocalResourceObject("PrimaryDepartment.Text") %></a>,
                    <a href="javascript:InsertText('<%= ftxtReferenceTemplate.ClientID %>', '<%= GetLocalResourceObject("ConfidentialityStatement.Value") %>');" ><%= GetLocalResourceObject("ConfidentialityStatement.Text") %></a>
                
                <FTB:FreeTextBox ID="ftxtReferenceTemplate" runat="server" Width="500px" Height="300px" EnableHtmlMode="False" AllowHtmlMode="False" AssemblyResourceHandlerPath="" AutoConfigure="" AutoGenerateToolbarsFromString="True" AutoHideToolbar="True" AutoParseStyles="True" BackColor="158, 190, 245" BaseUrl="" BreakMode="Paragraph" ButtonDownImage="False" ButtonFileExtention="gif" ButtonFolder="Images" ButtonHeight="20" ButtonImagesLocation="InternalResource" ButtonOverImage="False" ButtonPath="" ButtonSet="Office2003" ButtonWidth="21" ClientSideTextChanged="" ConvertHtmlSymbolsToHtmlCodes="False" DesignModeBodyTagCssClass="" DesignModeCss="" DisableIEBackButton="False" DownLevelCols="50" DownLevelMessage="" DownLevelMode="TextArea" DownLevelRows="10" EditorBorderColorDark="128, 128, 128" EditorBorderColorLight="128, 128, 128" EnableSsl="False" EnableToolbars="True" Focus="False" FormatHtmlTagsToXhtml="True" GutterBackColor="129, 169, 226" GutterBorderColorDark="128, 128, 128" GutterBorderColorLight="255, 255, 255" HelperFilesParameters="" HelperFilesPath="" HtmlModeCss="" HtmlModeDefaultsToMonoSpaceFont="True" ImageGalleryPath="~/images/" ImageGalleryUrl="ftb.imagegallery.aspx?rif={0}&cif={0}" InstallationErrorMessage="InlineMessage" JavaScriptLocation="InternalResource" Language="en-US" PasteMode="Default" ReadOnly="False" RemoveScriptNameFromBookmarks="True" RemoveServerNameFromUrls="True" RenderMode="NotSet" ScriptMode="External" ShowTagPath="False" SslUrl="/." StartMode="DesignMode" StripAllScripting="False" SupportFolder="/aspnet_client/FreeTextBox/" TabIndex="-1" TabMode="InsertSpaces" Text="" TextDirection="LeftToRight" ToolbarBackColor="Transparent" ToolbarBackgroundImage="True" ToolbarImagesLocation="InternalResource" ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline,Strikethrough;Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;CreateLink,Unlink,InsertImage,InsertRule|Cut,Copy,Paste;Undo,Redo,Print" ToolbarStyleConfiguration="NotSet" UpdateToolbar="True" UseToolbarBackGroundImage="True">
                </FTB:FreeTextBox>
                <asp:RequiredFieldValidator id="reqValReferenceTemplate" ControlToValidate="ftxtReferenceTemplate" ErrorMessage="* Reference Template Required" runat="server" meta:resourcekey="reqValReferenceTemplateResource1"/>
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
            <td   align="right" style="width: 240">
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
                    TypeName="CAESDO.Recruitment.Data.NHibernateDaoFactory+FileTypeDao">
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

