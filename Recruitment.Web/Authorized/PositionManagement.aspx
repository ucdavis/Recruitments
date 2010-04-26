<%@ Page AutoEventWireup="true" CodeFile="PositionManagement.aspx.cs" Inherits="CAESDO.Recruitment.Web.PositionManagement" ValidateRequest="false" Language="C#" MasterPageFile="~/MasterPage.master" Title="Position Management" %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <script type="text/javascript" language="javascript">
        
        function InsertText(txtID, text)
        {
            FTB_API[txtID].InsertHtml(text);
        }
        
    </script>

    <span class="boxTitle"><asp:Image ID="imgProfile" runat="server" EnableViewState="false" ImageUrl="~/Images/profile_sm.gif" style="vertical-align:middle;" AlternateText="" /><asp:Literal ID="litPositionState" runat="server" Text="Create Position" EnableViewState="false"></asp:Literal></span><br />
    <table class="box" style="width:550px; height: 350px;" cellpadding="5">
        <tr>
            <td colspan="2"><br /></td>
        </tr>
        <tr>
            <td   align="right">
                Position Title:</td>
            <td  >
                <asp:TextBox ID="txtPositionTitle" runat="server" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqValPositionTitle" runat="server" ControlToValidate="txtPositionTitle" ErrorMessage="*"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td   align="right">
                Position Number:</td>
            <td  >
                <asp:TextBox ID="txtPositionNumber" runat="server" MaxLength="20"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right">
                Review Date:</td>
            <td >
                <asp:TextBox ID="txtDeadline" runat="server"></asp:TextBox> <asp:Image ID="imgDeadlineCalendar" runat="server" ImageUrl="~/Images/icon.calendar.png" AlternateText="Click to show calendar" />
                <AjaxControlToolkit:CalendarExtender ID="calDeadline" runat="server" TargetControlID="txtDeadline" PopupButtonID="imgDeadlineCalendar"></AjaxControlToolkit:CalendarExtender>
                
                <asp:RequiredFieldValidator ID="reqValDeadline" runat="server" ControlToValidate="txtDeadline" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="comValDeadline" Type="Date" runat="server" Operator="DataTypeCheck" ControlToValidate="txtDeadline" ErrorMessage="*"></asp:CompareValidator>
                
            </td>
        </tr>
        <tr>
            <td   align="right">
                Department:</td>
            <td  >
                <Ajax:UpdatePanel ID="updateDepartments" runat="server" UpdateMode="conditional">
                <ContentTemplate>
                    <asp:DropDownList ID="dlistDepartment" runat="server" DataSourceID="ObjectDataUnits" DataTextField="ShortName" DataValueField="FISCode"></asp:DropDownList>
                    <asp:LinkButton ID="lbtnAddDepartment" runat="server" Text="Add Department" CausesValidation="False" OnClick="lbtnAddDepartment_Click"></asp:LinkButton>
                    <br />
                    
                    <asp:GridView ID="gviewDepartments" runat="server" DataKeyNames="DepartmentFIS" AutoGenerateColumns="False" OnRowDeleting="gviewDepartments_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="Primary">
                            <ItemTemplate>
                                <asp:CheckBox ID="cboxPrimary" runat="server" Checked='<%# Eval("PrimaryDept") %>' AutoPostBack="true" OnCheckedChanged="cboxPrimary_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department Name">
                            <ItemTemplate>
                                <asp:Literal ID="litDepartmentName" runat="server" Text='<%# Eval("Unit.ShortName") %>'></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField DeleteText="Remove" ShowDeleteButton="True" />
                    </Columns>                    
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
            <td   align="right">
                HR Representative:</td>
            <td  >
                <asp:TextBox ID="txtHRRep" runat="server" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator id="reqValHRRep" ControlToValidate="txtHRRep" ErrorMessage="*" runat="server"/>
            </td>
        </tr>
        <tr>
            <td   align="right">
                HR Phone Number:</td>
            <td  >
                <asp:TextBox ID="txtHRPhone" runat="server" MaxLength="13"></asp:TextBox>                
                <asp:RegularExpressionValidator ID="regValHRPhone" runat="server" ControlToValidate="txtHRPhone" ErrorMessage="*" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                HR Email:
            </td>
            <td>
                <asp:TextBox ID="txtHREmail" runat="server" MaxLength="100"></asp:TextBox>
                
                <asp:RequiredFieldValidator id="reqValHREmail" ControlToValidate="txtHREmail" ErrorMessage="*" runat="server"/>                
                <asp:RegularExpressionValidator ID="regValHREmail" runat="server" ErrorMessage="*" ControlToValidate="txtHREmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                Summary:</td>
            <td  >
                <asp:TextBox ID="txtShortDescription" runat="server" Height="90px" Rows="4" TextMode="MultiLine"
                    Width="233px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" valign="top">
                Reference Template:
            </td>
            <td>
                Template Fields: 
                    <a href="javascript:InsertText('<%= ftxtReferenceTemplate.ClientID %>', '{ReferenceName}');" >Reference Name</a>,
                    <a href="javascript:InsertText('<%= ftxtReferenceTemplate.ClientID %>', '{ApplicantName}');" >Applicant Name</a>,
                    <a href="javascript:InsertText('<%= ftxtReferenceTemplate.ClientID %>', '{Deadline}');" >Deadline</a>,
                    <a href="javascript:InsertText('<%= ftxtReferenceTemplate.ClientID %>', '{Position Title}');" >Position Title</a>
                
                <FTB:FreeTextBox ID="ftxtReferenceTemplate" runat="server" Width="500px" Height="300px">
                </FTB:FreeTextBox>
                <asp:RequiredFieldValidator id="reqValReferenceTemplate" ControlToValidate="ftxtReferenceTemplate" ErrorMessage="*" runat="server"/>
            </td>
        </tr>
        <tr>
            <td align="right">
                Full job description (PDF):</td>
            <td  >
                <asp:LinkButton ID="lbtnDownloadPositionDescription" runat="server" Text="Download Existing File" Visible="false"></asp:LinkButton>
                <asp:Literal ID="litDownloadPositionDescription" runat="server" Visible="false"><br /><br /></asp:Literal>
                <asp:FileUpload ID="filePositionDescription" runat="server" Visible="true" />                
                <asp:RequiredFieldValidator ID="reqValPositionDescription" runat="server" ControlToValidate="filePositionDescription" ErrorMessage="*" Visible="true"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td   align="right">
                Number of Required Publications:</td>
            <td  >
                <asp:TextBox ID="txtPublications" runat="server"></asp:TextBox>
                <AjaxControlToolkit:NumericUpDownExtender ID="numPublications" runat="server" Width="146" TargetControlID="txtPublications"></AjaxControlToolkit:NumericUpDownExtender>
                
                <asp:RequiredFieldValidator ID="reqValPublications" runat="server" ErrorMessage="*" ControlToValidate="txtPublications"></asp:RequiredFieldValidator>    
                <asp:CompareValidator ID="comValPublications" runat="server" ErrorMessage="*" ControlToValidate="txtPublications" ValueToCompare="0" Type="Integer" Operator="greaterThanEqual"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td   align="right">
                Number of Required References:</td>
            <td  >
                <asp:TextBox ID="txtReferences" runat="server"></asp:TextBox>   
                <AjaxControlToolkit:NumericUpDownExtender ID="numReferences" runat="server" Width="146" TargetControlID="txtReferences"></AjaxControlToolkit:NumericUpDownExtender>
                
                <asp:RequiredFieldValidator ID="reqValReferences" runat="server" ErrorMessage="*" ControlToValidate="txtReferences"></asp:RequiredFieldValidator>    
                <asp:CompareValidator ID="comValReferences" runat="server" ErrorMessage="*" ControlToValidate="txtReferences" ValueToCompare="0" Type="Integer" Operator="GreaterThanEqual"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td align="right">Required File Types:</td>
            <td>
                <asp:GridView ID="gviewFileTypes" runat="server" DataKeyNames="id" AutoGenerateColumns="False" DataSourceID="ObjectDataFileTypes">
                    <Columns>
                        <asp:TemplateField HeaderText="File Types">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkFileType" runat="server" Text='<%# BreakCamelCase((string)Eval("FileTypeName")) %>' Checked='<%# doesFileTypeExistInPosition((string)Eval("FileTypeName")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>                
                <asp:ObjectDataSource ID="ObjectDataFileTypes" runat="server" SelectMethod="GetAll"
                    TypeName="CAESDO.Recruitment.Data.NHibernateDaoFactory+FileTypeDao">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="FileTypeName" Name="propertyName" Type="String" />
                        <asp:Parameter DefaultValue="true" Name="ascending" Type="Boolean" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td align="right">
                Allow Applications:</td>
            <td  >
                <asp:CheckBox ID="chkAllowApplications" runat="server" Checked="true" />
            </td>
        </tr>
        <tr>
            <td   align="right">
            </td>
            <td align="right"  >
                <br />
                <asp:Button ID="btnModifyPosition" runat="server" Text="Create!" OnClick="btnModifyPosition_Click" /></td>
        </tr>
    </table>
 
</asp:Content>

