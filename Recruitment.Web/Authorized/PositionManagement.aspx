<%@ Page AutoEventWireup="true" CodeFile="PositionManagement.aspx.cs" Inherits="CAESDO.Recruitment.Web.PositionManagement" Theme="MainTheme" ValidateRequest="false" Language="C#" MasterPageFile="~/MasterPage.master" Title="Position Management"  %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <script type="text/javascript" language="javascript">
        
        function InsertText(txtID, text)
        {
            var parent = FTB_API[txtID].GetParentElement();
            
            if ( parent.tagName == 'BODY' )
            {
                FTB_API[txtID].InsertHtml(text);
            }
            else if ( parent.tagName == "BR" )
            {
                return; //Don't do anything with BR's, they aren't formatted correctly
            }
            else
            {
                var currentHTML = FTB_API[txtID].GetHtml();
                
                FTB_API[txtID].SetHtml(currentHTML + text);                
            }   
        }
        
    </script>
    <span class="boxTitle"><asp:Image ID="imgProfile" runat="server" EnableViewState="false" ImageUrl="~/Images/profile_sm.gif" style="vertical-align:middle;" AlternateText="" /><asp:Literal ID="litPositionState" runat="server" Text="Create Position" EnableViewState="false"></asp:Literal></span><br />
    <table class="box" style="width:690px;" cellpadding="5">
        <tr>
            <td colspan="2"><br /></td>
        </tr>
        <tr>
            <td   align="right" style="width: 240">
                Position Title:</td>
            <td  >
                <asp:TextBox ID="txtPositionTitle" runat="server" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqValPositionTitle" runat="server" ControlToValidate="txtPositionTitle" ErrorMessage="* Position Title Required"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td   align="right" style="width: 240">
                Position Number:</td>
            <td  >
                <asp:TextBox ID="txtPositionNumber" runat="server" MaxLength="20"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 240" valign="top">
                Review Date:</td>
            <td >
                <asp:TextBox ID="txtDeadline" runat="server"></asp:TextBox> <asp:Image ID="imgDeadlineCalendar" runat="server" ImageUrl="~/Images/icon.calendar.png" AlternateText="Click to show calendar" />
                <AjaxControlToolkit:CalendarExtender ID="calDeadline" runat="server" TargetControlID="txtDeadline" PopupButtonID="imgDeadlineCalendar"></AjaxControlToolkit:CalendarExtender>
                
                <asp:RequiredFieldValidator ID="reqValDeadline" runat="server" ControlToValidate="txtDeadline" ErrorMessage="* Review Date Required" Display="dynamic"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="comValDeadline" Type="Date" runat="server" Operator="DataTypeCheck" ControlToValidate="txtDeadline" ErrorMessage="* Review Date Format Not Valid" Display="dynamic"></asp:CompareValidator>
                
            </td>
        </tr>
        <tr>
            <td   align="right" style="width: 240" valign="top">
                Department:</td>
            <td  >
                <Ajax:UpdatePanel ID="updateDepartments" runat="server" UpdateMode="conditional">
                <ContentTemplate>
                    <asp:DropDownList ID="dlistDepartment" runat="server" DataSourceID="ObjectDataUnits" DataTextField="ShortName" DataValueField="FISCode"></asp:DropDownList>
                    <asp:LinkButton ID="lbtnAddDepartment" runat="server" Text="Add Department" CausesValidation="False" OnClick="lbtnAddDepartment_Click"></asp:LinkButton>
                    <br />
                    <br />
                    
                    <asp:GridView ID="gviewDepartments" runat="server" DataKeyNames="DepartmentFIS" AutoGenerateColumns="False" OnRowDeleting="gviewDepartments_RowDeleting" Width="346px" SkinID="gridViewPosManage">
                    <Columns>
                        <asp:TemplateField HeaderText="Home Dept">
                            <ItemTemplate>
                                <asp:CheckBox ID="cboxPrimary" runat="server" Checked='<%# Eval("PrimaryDept") %>' AutoPostBack="true" OnCheckedChanged="cboxPrimary_CheckedChanged" />
                            </ItemTemplate>
                            <ItemStyle CssClass="paddingLeft" />
                            <HeaderStyle CssClass="paddingLeft" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department Name">
                            <ItemTemplate>
                                <asp:Literal ID="litDepartmentName" runat="server" Text='<%# Eval("Unit.ShortName") %>'></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField DeleteText="Remove" ShowDeleteButton="True" />
                    </Columns>                    
                        <HeaderStyle HorizontalAlign="Left" CssClass="paddingRight" />
                        <RowStyle CssClass="paddingRight" />
                    </asp:GridView>
                    <asp:Label ID="lblPrimaryDeptErrorMessage" runat="server" EnableViewState="false" ForeColor="red" Text=""></asp:Label>
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
                <asp:TextBox ID="txtHRRep" runat="server" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator id="reqValHRRep" ControlToValidate="txtHRRep" ErrorMessage="* Recruitment Reprenentative Required" runat="server"/>
            </td>
        </tr>
        <tr>
            <td   align="right" style="width: 240">
                Recruitment Rep Phone Number:</td>
            <td  >
                <asp:TextBox ID="txtHRPhone" runat="server" MaxLength="13"></asp:TextBox>                
                <asp:RegularExpressionValidator ID="regValHRPhone" runat="server" ControlToValidate="txtHRPhone" ErrorMessage="* Phone Number Required" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 240">
                Recruitment Rep Email:
            </td>
            <td>
                <asp:TextBox ID="txtHREmail" runat="server" MaxLength="100"></asp:TextBox>
                
                <asp:RequiredFieldValidator id="reqValHREmail" ControlToValidate="txtHREmail" ErrorMessage="* Email Required" runat="server" Display="dynamic"/>                
                <asp:RegularExpressionValidator ID="regValHREmail" runat="server" ErrorMessage="* Email Format Not Valid" ControlToValidate="txtHREmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top" style="width: 240">
                Summary:</td>
            <td  >
                <asp:TextBox ID="txtShortDescription" runat="server" Height="255px" Rows="4" TextMode="MultiLine"
                    Width="502px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" valign="top" style="width: 240">
                Reference Template:
            </td>
            <td>
                Template Fields: 
                    <a href="javascript:InsertText('<%= ftxtReferenceTemplate.ClientID %>', '{ReferenceName}');" >Reference Name</a>,
                    <a href="javascript:InsertText('<%= ftxtReferenceTemplate.ClientID %>', '{ApplicantName}');" >Applicant Name</a>,
                    <a href="javascript:InsertText('<%= ftxtReferenceTemplate.ClientID %>', '{Deadline}');" >Deadline</a>,
                    <a href="javascript:InsertText('<%= ftxtReferenceTemplate.ClientID %>', '{PositionTitle}');" >Position Title</a>
                    <a href="javascript:InsertText('<%= ftxtReferenceTemplate.ClientID %>', '<%= ConfidentialityStatement() %>');" >Confidentiality Statement</a>
                
                <FTB:FreeTextBox ID="ftxtReferenceTemplate" runat="server" Width="500px" Height="300px" EnableHtmlMode="true">
                </FTB:FreeTextBox>
                <asp:RequiredFieldValidator id="reqValReferenceTemplate" ControlToValidate="ftxtReferenceTemplate" ErrorMessage="* Reference Template Required" runat="server"/>
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 240">
                Full job description (PDF):</td>
            <td  >
                <asp:LinkButton ID="lbtnDownloadPositionDescription" runat="server" Text="Download Existing File" Visible="false" OnClick="lbtnDownloadPositionDescription_Click"></asp:LinkButton>
                <asp:ImageButton ID="ibtnReplacePositionDescription" runat="server" ImageUrl="~/Images/delete.gif" AlternateText="[Replace]" Visible="false" />
                <asp:Literal ID="litDownloadPositionDescription" runat="server" Visible="false"><br /><br /></asp:Literal>
                <asp:FileUpload ID="filePositionDescription" runat="server" Visible="true" />                
                <asp:RequiredFieldValidator ID="reqValPositionDescription" runat="server" ControlToValidate="filePositionDescription" ErrorMessage="* Job Description Required" Visible="true"></asp:RequiredFieldValidator>
                                                                     
                <asp:Panel ID="pnlPositionDescription" runat="server" CssClass="modalPopup" style="display:none;">
                    Replace Existing Position Description: <br /><br />
                    <asp:FileUpload ID="filePositionDescriptionReplace" runat="server" Visible="true" />
                    <asp:RequiredFieldValidator ID="reqValPositionDescriptionReplace" runat="server" ControlToValidate="filePositionDescriptionReplace" ErrorMessage="* Position Description Required" ValidationGroup="Replace" Visible="true"></asp:RequiredFieldValidator>
                    <br /><br />
                    <asp:Button ID="btnPositionDescriptionReplace" runat="server" Text="Upload" ValidationGroup="Replace" OnClick="btnPositionDescriptionReplace_Click" />
                    <asp:Button ID="btnPositionDescriptionReplaceCancel" runat="server" CausesValidation="false" Text="Cancel" />
                </asp:Panel>
                                
                <AjaxControlToolkit:ModalPopupExtender ID="mpopupPositionDescriptionReplacement" runat="server" TargetControlID="ibtnReplacePositionDescription"
                                                     PopupControlID="pnlPositionDescription" BackgroundCssClass="modalBackground" CancelControlID="btnPositionDescriptionReplaceCancel" />
            </td>
        </tr>
        <tr>
            <td   align="right" style="width: 240">
                Number of Required Publications:</td>
            <td  >
                <asp:TextBox ID="txtPublications" runat="server"></asp:TextBox>
                <AjaxControlToolkit:NumericUpDownExtender ID="numPublications" runat="server" Width="146" TargetControlID="txtPublications"></AjaxControlToolkit:NumericUpDownExtender>
                
                
                <asp:CompareValidator ID="comValPublications" runat="server" ErrorMessage="* Number of Publications Can Not Be Negative" ControlToValidate="txtPublications" ValueToCompare="0" Type="Integer" Operator="greaterThanEqual" Display="dynamic"></asp:CompareValidator>
                <asp:RequiredFieldValidator ID="reqValPublications" runat="server" ErrorMessage="* Publications Required" ControlToValidate="txtPublications" Display="dynamic"></asp:RequiredFieldValidator>    
            </td>
        </tr>
        <tr>
            <td   align="right" style="width: 240">
                Number of Required References:</td>
            <td  >
                <asp:TextBox ID="txtReferences" runat="server"></asp:TextBox>   
                <AjaxControlToolkit:NumericUpDownExtender ID="numReferences" runat="server" Width="146" TargetControlID="txtReferences"></AjaxControlToolkit:NumericUpDownExtender>
                
                <asp:CompareValidator ID="comValReferences" runat="server" ErrorMessage="* Number of References Can Not Be Negative" ControlToValidate="txtReferences" ValueToCompare="0" Type="Integer" Operator="GreaterThanEqual" Display="dynamic"></asp:CompareValidator>
                <asp:RequiredFieldValidator ID="reqValReferences" runat="server" ErrorMessage="* Reference Required" ControlToValidate="txtReferences" Display="dynamic"></asp:RequiredFieldValidator>                    
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 240">
                Show Education:
            </td>
            <td>
                <asp:CheckBox ID="chkShowEducation" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 240">
                Show Current Position:
            </td>
            <td>
                <asp:CheckBox ID="chkShowCurrentPosition" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 240" valign="top">Required File Types:</td>
            <td>
                <asp:GridView ID="gviewFileTypes" runat="server" DataKeyNames="id" AutoGenerateColumns="False" DataSourceID="ObjectDataFileTypes" BorderStyle="None" CellPadding="0" GridLines="None" Width="289px">
                    <Columns>
                        <asp:TemplateField HeaderText="File Types">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkFileType" runat="server" Text='<%# BreakCamelCase((string)Eval("FileTypeName")) %>' Checked='<%# doesFileTypeExistInPosition((string)Eval("FileTypeName")) %>' />
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
                <asp:CheckBox ID="chkAllowApplications" runat="server" Checked="true" />
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 240">
                Allow Faculty Review:
            </td>
            <td>
                <asp:CheckBox ID="chkAllowFaculty" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right" style="width:240">
                Closed:
            </td>
            <td>
                <asp:CheckBox ID="chkPositionClosed" runat="server" />
            </td>
        </tr>
        <tr>
            <td   align="right" style="width: 240">
            </td>
            <td align="right"  >
                <br />
                <asp:Button ID="btnModifyPosition" runat="server" Text="Create!" OnClick="btnModifyPosition_Click" /></td>
        </tr>
    </table>
 
</asp:Content>

