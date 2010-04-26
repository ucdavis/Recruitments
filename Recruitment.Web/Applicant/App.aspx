<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="App.aspx.cs" Inherits="CAESDO.Recruitment.Web.App" Title="Untitled Page" Trace="true" Theme="MainTheme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
    <table summary="This table is for layout. The left-hand column contains the navigation bar and the right hand column contains the main text for the page." cellpadding="0" cellspacing="0" border="0" style="font-size:1.0em; width:100%;">
    <tr>
        <td colspan="2" style="height:22px; background-image:url(../Images/appmenuTop.gif);"><img src="../Images/appmenuTopLeft.gif" alt="" style="float:left;" /><img src="../Images/appmenuTopRight.gif" alt="" style="float:right;" /></td>
    </tr>
    <tr>
       <td style="background: url(../Images/appmenuLeft.gif) repeat-y; vertical-align:top; width: 203px;">
    
    <ul class="applicationMenu">
        <asp:Repeater ID="rptSteps" runat="server">
            <ItemTemplate>
               <asp:Panel ID="pnlStep" runat="server" Visible='<%# Eval("StepVisible") %>'>
                    <li class='<%# Eval("CSSClass") %>'>
                    <div class="appButton">
                        <asp:Image ID="imgStep" runat="server" ImageUrl='<%# Eval("ImgURL") %>' />
                    </div>
                    <div class="appLink">
                        <asp:LinkButton ID="lbtnStep" runat="server" Text='<%# Eval("StepName") %>' CommandArgument='<%# Eval("StepName") %>' Style="margin-left: 12px;" OnClick="lbtnStep_Click" CausesValidation="false"></asp:LinkButton>
                    </div>
                    </li>
               </asp:Panel>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    </td>
    <td style="background:url(../Images/appmenuRight.gif) repeat-y right; vertical-align:top;">
       
        <asp:Panel ID="pnlMainWindow" runat="server">
        
        <asp:MultiView ID="mviewSteps" runat="server" ActiveViewIndex="0">
        
            <asp:View ID="viewHome" runat="server">
                <span class="boxTitle">Home</span><br />
                <table class="box" style="width:500px;" cellpadding="5">
                    <tr>
                        <td colspan="2">
                            <br />
                            <asp:Label ID="lblApplicationStepStatus" runat="server" ForeColor="green" EnableViewState="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            You are applying to the <asp:Label ID="lblApplicationPositionTitle" runat="server"></asp:Label> position.
                            For full consideration, please have your application finalized by <asp:Label ID="lblApplicationDeadline" runat="server"></asp:Label>.
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"><br />
                            Please complete all tabs on the left hand side of this page. <br /><br />
                
                            When you are done with all sections, click on the finalize button to complete your application.
                            You will not be able to motify your application after you finalize.
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                        </td>
                        <td align="right">
                            <br />
                            <asp:Button ID="btnApplicationFinalize" runat="server" Text="Finalize Application" /></td>
                    </tr>
                </table>
            </asp:View>
            
            <asp:View ID="viewContactInformation" runat="server" >
                <span class="boxTitle">Contact Information</span><br />
                <table class="box" style="width:500px;" cellpadding="5">
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <td   align="right">
                            First Name:</td>
                        <td  >
                            <asp:TextBox ID="txtContactFirstName" runat="server" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqValContactFirstName" runat="server" ControlToValidate="txtContactFirstName" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Middle Name:</td>
                        <td  >
                            <asp:TextBox ID="txtContactMiddleName" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Last Name:</td>
                        <td  >
                            <asp:TextBox ID="txtContactLastName" runat="server" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqValContactLastName" runat="server" ControlToValidate="txtContactLastName" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Address 1:</td>
                        <td  >
                            <asp:TextBox ID="txtContactAddress1" runat="server" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator id="reqValContactAddress1" ControlToValidate="txtContactAddress1" ErrorMessage="*" runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Address 2:</td>
                        <td  >
                            <asp:TextBox ID="txtContactAddress2" runat="server" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            City:</td>
                        <td  >
                            <asp:TextBox ID="txtContactCity" runat="server" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator id="reqValContactCity" ControlToValidate="txtContactCity" ErrorMessage="*" runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            State:</td>
                        <td   >
                            <asp:TextBox ID="txtContactState" runat="server" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator id="reqValContactState" ControlToValidate="txtContactState" ErrorMessage="*" runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Phone Number:</td>
                        <td  >
                            <asp:TextBox ID="txtContactPhone" runat="server" MaxLength="20"></asp:TextBox>
                            <%--<AjaxControlToolkit:MaskedEditExtender ID="maskHRPhone" runat="server" TargetControlID="txtHRPhone" MaskType="Number" Mask="(999) 999-9999" ClearMaskOnLostFocus="false" Filtered="" AutoComplete="false"></AjaxControlToolkit:MaskedEditExtender>
                            --%>
                            <asp:RegularExpressionValidator ID="regValContacPhone" runat="server" ControlToValidate="txtContactPhone" ErrorMessage="*" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                        </td>
                        <td align="right">
                            <br />
                            <asp:Button ID="btnContactSave" runat="server" Text="Update" OnClick="btnContactSave_Click" /></td>
                    </tr>
                </table>
            </asp:View>
            
            <asp:View ID="viewEducationInformation" runat="server">
                <span class="boxTitle">Ph.D. Award Information</span><br />
                <table class="box" style="width:500px; " cellpadding="5">
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <td align="right">
                            Ph.D. Date:</td>
                        <td >
                            <asp:TextBox ID="txtEducationPHDDate" runat="server"></asp:TextBox> <asp:Image ID="imgEducationPHDDateCalendar" runat="server" ImageUrl="~/Images/icon.calendar.png" AlternateText="Click to show calendar" />
                            <AjaxControlToolkit:CalendarExtender ID="calEducationPHDDate" runat="server" TargetControlID="txtEducationPHDDate" PopupButtonID="imgEducationPHDDateCalendar"></AjaxControlToolkit:CalendarExtender>
                            
                            <asp:RequiredFieldValidator ID="reqValEducationPHDDate" runat="server" ControlToValidate="txtEducationPHDDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="comValEducationPHDDate" Type="Date" runat="server" Operator="DataTypeCheck" ControlToValidate="txtEducationPHDDate" ErrorMessage="*"></asp:CompareValidator>
                            
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Awarding Institute:</td>
                        <td  >
                            <asp:TextBox ID="txtEducationInstitution" runat="server" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator id="reqValEducationInstitution" ControlToValidate="txtEducationInstitution" ErrorMessage="*" runat="server"/>
                            
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Discipline:</td>
                        <td  >
                            <asp:TextBox ID="txtEducationDiscipline" runat="server" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqValEducationDiscipline" runat="server" ControlToValidate="txtEducationDiscipline" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                        </td>
                        <td align="right"  >
                            <br />
                            <asp:Button ID="btnEducationSave" runat="server" Text="Update" OnClick="btnEducationSave_Click" /></td>
                    </tr>
                </table>
            </asp:View>
        
            <asp:View ID="viewReferences" runat="server">
                <span class="boxTitle">References</span><br />
                <table class="box" style="height: 350px; width:500px;" cellpadding="5">
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ImageButton ID="lbtnReferencesAdd" runat="server" ImageURL="~/Images/addReference.gif" ImageAlign="Middle"></asp:ImageButton>
                            &nbsp;<asp:Label ID="lblReferencesRemaining" runat="server" ForeColor="Brown" EnableViewState="false"><%= NumReferencesRemainingText() %></asp:Label>
                            <br /><br />
                        </td>                       
                    </tr>
                    <tr>
                        <td colspan="2">
                        Existing References:
                            <asp:GridView ID="gviewReferences" skinID="gridViewReferences" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" EmptyDataText="No References Added" OnRowDeleting="gviewReferences_RowDeleting" OnSelectedIndexChanged="gviewReferences_SelectedIndexChanged" BorderStyle="None" CellPadding="0" GridLines="None">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" SelectText="Edit" EditText="" >
                                    <ItemStyle CssClass="paddingLeft" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="Name:">
                                    <ItemTemplate>
                                        <%# Eval("Title") %> <%# Eval("FirstName") %> <%# Eval("LastName") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Title Expertise" Visible="False">
                                    <ItemTemplate>
                                        <%# Eval("AcadTitle") %> <%# Eval("Expertise") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department Institution" Visible="False">
                                    <ItemTemplate>
                                        <%# Eval("Dept") %> <%# Eval("Institution") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address" Visible="False">
                                    <ItemTemplate>
                                        <%# Eval("Address1") %> <%# Eval("Address2") %> <br />
                                        <%# Eval("City") %> <%# Eval("State") %> <%# Eval("Zip") %> <%# Eval("Country") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Phone" DataField="Phone" Visible="False" />
                                <asp:BoundField HeaderText="Email:" DataField="Email" />
                                <asp:CommandField DeleteText="Remove" ShowDeleteButton="True" >
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>
                            </Columns>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:GridView>                           
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                     <tr>
                        <td   align="right">
                        </td>
                        <td align="right">
                            <br />
                            <asp:CheckBox ID="chkReferencesComplete" runat="server" AutoPostBack="true" TextAlign="Left" Text="Done Uploading References" OnCheckedChanged="chkReferencesComplete_CheckedChanged" />
                        </td>
                    </tr>
                </table>       
                                
                <asp:Panel ID="pnlReferencesEntry" runat="server" CssClass="modalPopup" style="display:none;">
                    <span class="modalTitle">Add/Update Reference</span>
                    <div style="height:450px; overflow:auto;">
                    <table cellpadding="5" style="width:500px;">
                        <tr>
                            <td align="right">
                                Title:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesTitle" runat="server" MaxLength="50" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                First Name:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesFirstName" runat="server" MaxLength="50" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Last Name:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesLastName" runat="server" MaxLength="50" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                Academic Title:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesAcadTitle" runat="server" MaxLength="50" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                Area of Expertise:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesExpertise" runat="server" MaxLength="50" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td   align="right">
                                Department:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesDepartment" runat="server" MaxLength="50" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td   align="right">
                                Institute:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesInstitute" runat="server" MaxLength="50" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                Address 1:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesAddress1" runat="server" MaxLength="50" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                Address 2:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesAddress2" runat="server" MaxLength="50" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                City:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesCity" runat="server" MaxLength="50" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                State:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesState" runat="server" MaxLength="50" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                Zip:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesZip" runat="server" MaxLength="20" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                Country:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesCountry" runat="server" MaxLength="50" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                Phone Number:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesPhone" runat="server" MaxLength="20" EnableViewState="false"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="regValReferencesPhone" runat="server" ControlToValidate="txtReferencesPhone" ErrorMessage="*" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                Email:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesEmail" runat="server" MaxLength="100" EnableViewState="false"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="regValReferencesEmail" runat="server" ErrorMessage="*" ControlToValidate="txtReferencesEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                            </td>
                            <td align="right"  >
                                <br />
                                <asp:Button ID="btnReferencesAddUpdate" runat="server" CommandArgument="0" Text="Add Reference" OnClick="btnReferencesAddUpdate_Click" />
                                <asp:Button ID="btnReferencesCancel" runat="server" Text="Cancel" OnClick="btnReferencesCancel_Click" />
                            </td>
                        </tr>
                    </table></div>
                </asp:Panel>
                
                <AjaxControlToolkit:ModalPopupExtender ID="mpopupReferencesEntry" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlReferencesEntry" TargetControlID="lbtnReferencesAdd"></AjaxControlToolkit:ModalPopupExtender>
                
            </asp:View>
        
            <asp:View ID="viewCurrentPosition" runat="server">
                <span class="boxTitle">Current Position</span><br />
                <table class="box" style="width:500px; " cellpadding="5">
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <td align="right">
                            Title:</td>
                        <td  >
                            <asp:TextBox ID="txtCurrentPositionTitle" runat="server" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Department:</td>
                        <td  >
                            <asp:TextBox ID="txtCurrentPositionDepartment" runat="server" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Institution:</td>
                        <td  >
                            <asp:TextBox ID="txtCurrentPositionInstitution" runat="server" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Address 1:</td>
                        <td  >
                            <asp:TextBox ID="txtCurrentPositionAddress1" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Address 2:</td>
                        <td  >
                            <asp:TextBox ID="txtCurrentPositionAddress2" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            City:</td>
                        <td  >
                            <asp:TextBox ID="txtCurrentPositionCity" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            State:</td>
                        <td  >
                            <asp:TextBox ID="txtCurrentPositionState" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Zip:</td>
                        <td  >
                            <asp:TextBox ID="txtCurrentPositionZip" runat="server" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Country:</td>
                        <td  >
                            <asp:TextBox ID="txtCurrentPositionCountry" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                        </td>
                        <td align="right"  >
                            <br />
                            <asp:Button ID="btnCurrentPositionSave" runat="server" Text="Update" OnClick="btnCurrentPositionSave_Click" /></td>
                    </tr>
                </table>
            </asp:View>
            
            <asp:View ID="viewResume" runat="server">
                 <span class="boxTitle">Resume</span><br />
                <table class="box" style="width:500px;" cellpadding="5">
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Please upload your file as a PDF Document. Maximum file size allowed is 10 MB. 
                        </td>                       
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:FileUpload ID="fileResume" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                        </td>
                        <td align="right"  >
                            <br />
                            <asp:Button ID="btnResumeUpload" runat="server" Text="Upload" OnClick="btnResumeUpload_Click" /></td>
                    </tr>
                </table>
            </asp:View>
        
            <asp:View ID="viewCoverLetter" runat="server">
                <span class="boxTitle">Cover Letter</span><br />
                <table class="box" style="width:500px; height:" cellpadding="5">
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Please upload your file as a PDF Document. Maximum file size allowed is 10 MB. 
                        </td>                       
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:FileUpload ID="fileCoverLetter" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                        </td>
                        <td align="right"  >
                            <br />
                            <asp:Button ID="btnCoverLetterUpload" runat="server" Text="Upload" OnClick="btnCoverLetterUpload_Click" /></td>
                    </tr>
                </table>
            </asp:View>
        
            <asp:View ID="viewResearchInterests" runat="server">
                <span class="boxTitle">Research Interests</span><br />
                <table class="box" style="width:500px; " cellpadding="5">
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Please upload your file as a PDF Document. Maximum file size allowed is 10 MB. 
                        </td>                       
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:FileUpload ID="fileResearchInterests" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                        </td>
                        <td align="right"  >
                            <br />
                            <asp:Button ID="btnResearchInterestsUpload" runat="server" Text="Upload" OnClick="btnResearchInterestsUpload_Click" /></td>
                    </tr>
                </table>
            </asp:View>
            
            <asp:View ID="viewTranscripts" runat="server">
                <span class="boxTitle">Transcripts</span><br />
                <table class="box" style="width:500px;" cellpadding="5">
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Please upload your file as a PDF Document. Maximum file size allowed is 10 MB. 
                        </td>                       
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:FileUpload ID="fileTranscripts" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                        </td>
                        <td align="right"  >
                            <br />
                            <asp:Button ID="btnTranscriptsUpload" runat="server" Text="Upload" OnClick="btnTranscriptsUpload_Click" /></td>
                    </tr>
                </table>
            </asp:View>
            
            <asp:View ID="viewConfidentialSurvey" runat="server">
                <span class="boxTitle">Confidential Survey</span><br />
                <table class="box" style="margin-right:30px;" cellpadding="5">
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">
                            <span style="color:#0b65c5; font-weight: bold;">
                            Sex:</span></td>
                        <td valign="top"  >
                            <asp:RadioButtonList ID="rbtnConfidentialSurveySex" runat="server" DataSourceID="ObjectDataGender" DataTextField="GenderType" DataValueField="ID">
                            </asp:RadioButtonList>
                            <asp:ObjectDataSource ID="ObjectDataGender" runat="server"
                                SelectMethod="GetAll" TypeName="CAESDO.Recruitment.Data.NHibernateDaoFactory+GenderDao">
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right" valign="top">
                            <span style="color:#0b65c5; font-weight: bold;">
                            Ethnicity:</span></td>
                        <td valign="top"  >
                            <strong>
                            American Indian</strong>
                            <br />
                                <asp:RadioButton ID="rbtnAmericanIndian" runat="server" GroupName="Ethnicity" Text="American Indian / Alaskin Native" />
                                (specify tribal affiliation: <asp:TextBox ID="txtAmericanIndian" runat="server" ></asp:TextBox>)
                            <br />
                            <br />
                            <strong>
                            Asian/Pacific Islander </strong>
                            <br />
                                <asp:RadioButton ID="rbtnChinese" runat="server" GroupName="Ethnicity" Text="Chinese/Chinese-American" /><br />
                                <asp:RadioButton ID="rbtnPakistani" runat="server" GroupName="Ethnicity" Text="East Indian/Pakistani" /><br />
                                <asp:RadioButton ID="rbtnPhilipino" runat="server" GroupName="Ethnicity" Text="Filipino/Pilipino" /><br />
                                <asp:RadioButton ID="rbtnJapanese" runat="server" GroupName="Ethnicity" Text="Japanese/Japanese-American" /><br />
                                <asp:RadioButton ID="rbtnAsian" runat="server" GroupName="Ethnicity" Text="Other Asian ( including Far East Korea, Southeast Asian or Pacific Islands, Samoa)" />
                            <br />
                            <br />
                            <strong>Black</strong>
                            <br />
                                <asp:RadioButton ID="rbtnBlack" runat="server" GroupName="Ethnicity" Text="Black/African-American" />
                            <br />
                            <br />
                            <strong>
                            Hispanic (including Black individuals whose origins are Hispanic)
                            <br />
                            </strong>
                                <asp:RadioButton ID="rbtnLatino" runat="server" GroupName="Ethnicity" Text="Latin-American/Latino (including Cuban and Puerto Rican)" /><br />
                                <asp:RadioButton ID="rbtnMexican" runat="server" GroupName="Ethnicity" Text="Mexican/Mexican-American/Chicano" /><br />
                                <asp:RadioButton ID="rbtnSpanish" runat="server" GroupName="Ethnicity" Text="Other Spanish/Spanish-American" />
                            <br />
                            <br />
                            <strong>
                            White</strong>
                            <br />
                                <asp:RadioButton ID="rbtnWhite" runat="server" GroupName="Ethnicity" Text="White/Caucasian (including the Middle East)" />
                        </td>
                    </tr>
                    <tr>
                        <td   align="right" valign="top">
                            <span style="color:#0b65c5; font-weight: bold;">
                            Recruitment Source:</span></td>
                        <td valign="top"  >
                            <asp:Repeater ID="rptRecruitmentSource" runat="server" DataSourceID="ObjectDataRecruitmentSrc">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkRecruitmentSource" runat="server" Checked="false" />
                                    <asp:Label ID="lblRecruitmentSource" runat="server" Text='<%# Eval("RecruitmentSource") %>'></asp:Label>&nbsp;&nbsp;
                                    <asp:Literal ID="litBeginSpecify" runat="server" Text="(Specify:" Visible='<%# Eval("AllowSpecify") %>'></asp:Literal>
                                    <asp:TextBox ID="txtSpecify" runat="server" MaxLength="50" Visible='<%# Eval("AllowSpecify") %>'></asp:TextBox>
                                    <asp:Literal ID="litEndSpecify" runat="server" Text=")" Visible='<%# Eval("AllowSpecify") %>'></asp:Literal>
                                </ItemTemplate>
                                <SeparatorTemplate>
                                    <br />
                                </SeparatorTemplate>
                            </asp:Repeater>
                            <asp:ObjectDataSource ID="ObjectDataRecruitmentSrc" runat="server"
                                 SelectMethod="GetAll" TypeName="CAESDO.Recruitment.Data.NHibernateDaoFactory+RecruitmentSrcDao">
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                        </td>
                        <td align="right"  >
                            <br />
                            <asp:Button ID="btnConfidentialSurveyAccept" runat="server" Text="I Accept" OnClick="btnConfidentialSurveyAccept_Click" />
                            <button type="reset" value="Reset">Reset</button>
                            </td>
                    </tr>
                </table>
            </asp:View>
            
            <asp:View ID="viewPublications" runat="server">
                <span class="boxTitle">Publications</span><br />
                <table class="box" style="width:500px; height: 350px;" cellpadding="5">
                    
                    <tr>
                        <td colspan="2">
                            <br />
                            This position description requests <asp:Literal ID="litPublicationsNum" runat="server" EnableViewState="false"></asp:Literal>  publications, although recent graduates may not have that many. Please submit as many as you have, indicating below when complete. You can return to this page to change or enter more publications later.
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Please upload your file as a PDF Document. Maximum file size allowed is 10 MB. 
                        </td>                       
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:FileUpload ID="filePublications" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                        </td>
                        <td align="right"  >
                            <asp:Button ID="btnPublicationsUpload" runat="server" Text="Upload" OnClick="btnPublicationsUpload_Click" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        <br />
                            <asp:Repeater ID="rptPublications" runat="server">
                                <HeaderTemplate>
                                    Existing Publication Files <asp:Label ID="lblPublicationsRemaining" runat="server" Text='<%# NumPublicationsRemainingText() %>' ForeColor="Brown" EnableViewState="false"></asp:Label>: <br />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    &nbsp;&nbsp;<asp:LinkButton ID="lbtnPublicationFile" runat="server" Text='<%# Eval("FileName") %>' CommandArgument='<%# Eval("ID") %>' OnClick="lbtnPublicationFile_Click"></asp:LinkButton>
                                    <asp:ImageButton ID="ibtnPublicationsRemoveFile" runat="server" CommandArgument='<%# Eval("ID") %>' OnClick="ibtnPublicationsRemoveFile_Click" AlternateText="Remove File" ImageUrl="~/Images/delete.gif" ToolTip="Remove File" />
                                </ItemTemplate>
                                <SeparatorTemplate>
                                    <br />
                                </SeparatorTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                        </td>
                        <td align="right"  >
                            <br />
                            <asp:CheckBox ID="chkPublicationsFinalize" runat="server" AutoPostBack="true" TextAlign="Left" Text="Done Uploading Publications" OnCheckedChanged="chkPublicationsFinalize_CheckedChanged" />
                            <%--<asp:Button ID="btnPublicationsUpdateStatus" runat="server" Text="Update" />--%>
                        </td>
                    </tr>
                </table>
            </asp:View>
            
            <asp:View ID="viewDissertation" runat="server">
                <span class="boxTitle">Dissertations</span><br />
                <table class="box" style="width:500px;" cellpadding="5">
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Please upload your file as a PDF Document. Maximum file size allowed is 10 MB. 
                        </td>                       
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:FileUpload ID="fileDissertation" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                        </td>
                        <td align="right"  >
                            <br />
                            <asp:Button ID="btnDissertationUpload" runat="server" Text="Upload" OnClick="btnDissertationUpload_Click" /></td>
                    </tr>
                </table>
            </asp:View>
            
            </asp:MultiView>
        
        </asp:Panel>
        </td>
    </tr>
    <tr><td colspan="2"><div style=" height:22px; background-image:url(../Images/appmenuBot.gif);"><img src="../Images/appmenuBotLeft.gif" alt="" style="float:left;" /><img src="../Images/appmenuBotRight.gif" alt="" style="float:right;" /></div>
</td></tr>
    </table>


    
  </asp:Content>

