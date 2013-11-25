<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="CAESDO.Recruitment.Web.App" Title="Position Application" Trace="false" Theme="MainTheme" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" Codebehind="App.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">
    
    ///Makes the applicant confirm the PHD date if it is in the future
    function confirmPHDDate()
    {
        var PHDBox = document.getElementById("<%= txtEducationPHDDate.ClientID %>");
        
        var today = new Date();
        var PHDDate = new Date(PHDBox.value);
                        
        var confirmText = "<%= GetLocalResourceObject("PhDFutureMessage") %>";
        if ( PHDDate > today )
        {
            return confirm(confirmText);
        }            
        else
            return true;
    
    }
    
    $(document).ready(function() {
        $("input[id$=Phone]").bt('Phone Number Format Examples: <br/>xxx-yyy-zzzz<br/>(xxx) yyy-zzzz<br/>+xx yyy zzz zzzz', {
            trigger: ['focus', 'blur'],
            positions: ['right']
        });
    });
    
    </script>  
    <h2 style="margin-top:0;"><asp:Label ID="lblApplicationPositionTitle2" runat="server" meta:resourcekey="lblApplicationPositionTitle2Resource1"></asp:Label> Application</h2>
     
    <table summary="This table is for layout. The left-hand column contains the navigation bar and the right hand column contains the main text for the page." cellpadding="0" cellspacing="0" border="0" style="font-size:1.0em; width:100%;">
    <tr>
        <td colspan="2" style="height:22px; background-image:url(../Images/appmenuTop.gif);"><img src="../Images/appmenuTopLeft.gif" alt="" style="float:left;" /><img src="../Images/appmenuTopRight.gif" alt="" style="float:right;" /></td>
    </tr>
    <tr>
       <td style="background: url(../Images/appmenuLeft.gif) repeat-y; vertical-align:top; width: 203px;">
    
    <ul class="applicationMenu">
        <asp:Repeater ID="rptSteps" runat="server">
            <ItemTemplate>
               <asp:Panel ID="pnlStep" runat="server" Visible='<%# Eval("StepVisible") %>' meta:resourcekey="pnlStepResource1">
                    <li class='<%# Eval("CSSClass") %>'>
                    <div class="appButton">
                        <asp:Image ID="imgStep" runat="server" ImageUrl='<%# Eval("ImgURL") %>' meta:resourcekey="imgStepResource1" />
                    </div>
                    <div class="appLink">
                        <asp:LinkButton ID="lbtnStep" runat="server" Text='<%# Eval("StepName") %>' CommandArgument='<%# Eval("StepName") %>' Style="margin-left: 12px;" OnClick="lbtnStep_Click" CausesValidation="False" meta:resourcekey="lbtnStepResource1"></asp:LinkButton>
                    </div>
                    </li>
               </asp:Panel>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    </td>
    <td style="background:url(../Images/appmenuRight.gif) repeat-y right; vertical-align:top;">
       
        <asp:Panel ID="pnlMainWindow" runat="server" meta:resourcekey="pnlMainWindowResource1">
        
        <asp:MultiView ID="mviewSteps" runat="server" ActiveViewIndex="0">
        
            <asp:View ID="viewHome" runat="server">
                <span class="boxTitle">Home</span><br />
                <table class="box" style="width:500px;" cellpadding="5">
                    <tr>
                        <td colspan="2">
                            <br />
                            <asp:Label ID="lblApplicationStepStatus" runat="server" ForeColor="Green" EnableViewState="False" meta:resourcekey="lblApplicationStepStatusResource1"></asp:Label>
                            <AjaxControlToolkit:AnimationExtender ID="animationApplicationStatus" runat="server" TargetControlID="lblApplicationStepStatus" Enabled="True">
                                <Animations>
                                    <OnLoad>
                                        <Sequence>
                                            <Color Duration="1.5"
                                            StartValue="#ffff99"
                                            EndValue="#EFEFEF"
                                            Property="style"
                                            PropertyKey="backgroundColor" />
                                            <StyleAction Attribute="backgroundColor" value="" />
                                        </Sequence>
                                    </OnLoad></Animations>                            
                            </AjaxControlToolkit:AnimationExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            You are applying to the <asp:Label ID="lblApplicationPositionTitle" runat="server" meta:resourcekey="lblApplicationPositionTitleResource1"></asp:Label> position.
                            <asp:Literal ID="litFullConsideration" runat="server" Text="For full consideration, please have your application finalized by" meta:resourcekey="litFullConsiderationResource1"></asp:Literal> 
                            <asp:Label ID="lblApplicationDeadline" runat="server" meta:resourcekey="lblApplicationDeadlineResource1"></asp:Label>.
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Contact <a href='mailto:<%= currentApplication.AppliedPosition.HREmail %>'><%= currentApplication.AppliedPosition.HREmail %></a> if you have any questions.
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Literal ID="litCompleteTabs" runat="server" Text="Please complete all tabs on the left hand side of this page." meta:resourcekey="litCompleteTabsResource1"></asp:Literal> <br /><br />
                            
                            <asp:Literal ID="litFinalize" runat="server" Text="When you are done with all sections, click on the finalize button to complete your application.
                            You will not be able to modify your application after you finalize." meta:resourcekey="litFinalizeResource1"></asp:Literal>
                            <br /><br />
                            <asp:Literal ID="litFinalizedEmail" runat="server" Text="After finalizing your application, you will receive an email confirming that your application has been completed and received."></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Literal ID="litApplicationFinalizeStatus" runat="server" EnableViewState="False" meta:resourcekey="litApplicationFinalizeStatusResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                        </td>
                        <td align="right">
                            <br />
                            <asp:Button ID="btnApplicationFinalize" runat="server" Text="Finalize Application" OnClick="btnApplicationFinalize_Click" meta:resourcekey="btnApplicationFinalizeResource1" /></td>
                    </tr>
                </table>
            </asp:View>
            
            <asp:View ID="viewContactInformation" runat="server" >
                <span class="boxTitle">Primary Contact Information</span><br />
                <table class="box" style="width:500px;" cellpadding="5">
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Literal ID="litContactInfo" runat="server" Text="Enter the information of where you would like to be contacted regarding this position" meta:resourcekey="litContactInfoResource1"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            First Name:</td>
                        <td  >
                            <asp:TextBox ID="txtContactFirstName" runat="server" MaxLength="50" meta:resourcekey="txtContactFirstNameResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqValContactFirstName" runat="server" ControlToValidate="txtContactFirstName" ErrorMessage="* First Name Required" meta:resourcekey="reqValContactFirstNameResource1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Middle Name:</td>
                        <td  >
                            <asp:TextBox ID="txtContactMiddleName" runat="server" MaxLength="50" meta:resourcekey="txtContactMiddleNameResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Last Name:</td>
                        <td  >
                            <asp:TextBox ID="txtContactLastName" runat="server" MaxLength="50" meta:resourcekey="txtContactLastNameResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqValContactLastName" runat="server" ControlToValidate="txtContactLastName" ErrorMessage="* Last Name Required" meta:resourcekey="reqValContactLastNameResource1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Address 1:</td>
                        <td  >
                            <asp:TextBox ID="txtContactAddress1" runat="server" MaxLength="100" meta:resourcekey="txtContactAddress1Resource1"></asp:TextBox>
                            <asp:RequiredFieldValidator id="reqValContactAddress1" ControlToValidate="txtContactAddress1" ErrorMessage="* Address Required" runat="server" meta:resourcekey="reqValContactAddress1Resource1"/>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Address 2:</td>
                        <td  >
                            <asp:TextBox ID="txtContactAddress2" runat="server" MaxLength="100" meta:resourcekey="txtContactAddress2Resource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            City:</td>
                        <td  >
                            <asp:TextBox ID="txtContactCity" runat="server" MaxLength="50" meta:resourcekey="txtContactCityResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator id="reqValContactCity" ControlToValidate="txtContactCity" ErrorMessage="* City Required" runat="server" meta:resourcekey="reqValContactCityResource1"/>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            State:</td>
                        <td   >
                            <asp:TextBox ID="txtContactState" runat="server" MaxLength="50" meta:resourcekey="txtContactStateResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator id="reqValContactState" ControlToValidate="txtContactState" ErrorMessage="* State Required" runat="server" meta:resourcekey="reqValContactStateResource1"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Zip
                        </td>
                        <td>
                            <asp:TextBox ID="txtContactZip" runat="server" MaxLength="20" meta:resourcekey="txtContactZipResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Country
                        </td>
                        <td>
                            <asp:TextBox ID="txtContactCountry" runat="server" MaxLength="50" meta:resourcekey="txtContactCountryResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Phone Number:</td>
                        <td  >
                            <asp:TextBox ID="txtContactPhone" runat="server" MaxLength="20" meta:resourcekey="txtContactPhoneResource1"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="regValContacPhone" runat="server" ControlToValidate="txtContactPhone" ErrorMessage="* Phone Required" ValidationExpression="((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}" meta:resourcekey="regValContacPhoneResource1"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Email
                        </td>
                        <td>
                            <asp:TextBox ID="txtContactEmail" runat="server" MaxLength="50" meta:resourcekey="txtContactEmailResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator id="reqValContactEmail" runat="server" ControlToValidate="txtContactEmail" ErrorMessage="* Email Required" Display="Dynamic" meta:resourcekey="reqValContactEmailResource1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="regValContactEmail" runat="server" ControlToValidate="txtContactEmail" ErrorMessage="* Email Format Not Recognized" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" meta:resourcekey="regValContactEmailResource1"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                        </td>
                        <td align="right">
                            <br />
                            <asp:Button ID="btnContactSave" runat="server" Text="Update" OnClick="btnContactSave_Click" meta:resourcekey="btnContactSaveResource1" /></td>
                    </tr>
                </table>
            </asp:View>

            <asp:View ID="viewApplicationForm" runat="server">
                <span class="boxTitle">Application Form</span><br />
                <table class="box" style="width:500px;" cellpadding="5">
                    <tr>
                        <td colspan="2"><br />Please download the application from <a href="#">this link (app.pdf)</a> and fill it out, sign it, and then upload it here when complete.</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Literal ID="litTranscriptsPDF" runat="server" Text="<%$ Resources:PDFWarning %>"></asp:Literal> 
                        </td>                       
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:FileUpload ID="fileTranscripts" runat="server" meta:resourcekey="fileTranscriptsResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                        </td>
                        <td align="right"  >
                            <br />
                            <asp:Button ID="btnTranscriptsUpload" runat="server" Text="Upload" OnClick="btnTranscriptsUpload_Click" meta:resourcekey="btnTranscriptsUploadResource1" /></td>
                    </tr>
                </table>
            </asp:View>
            
            <asp:View ID="viewEducationInformation" runat="server">
                <span class="boxTitle">Terminal Degree Information</span><br />
                <table class="box" style="width:500px; " cellpadding="5">
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <td align="right">
                            Terminal Degree Date:</td>
                        <td >
                            <asp:TextBox ID="txtEducationPHDDate" runat="server" meta:resourcekey="txtEducationPHDDateResource1"></asp:TextBox> <asp:Image ID="imgEducationPHDDateCalendar" runat="server" ImageUrl="~/Images/icon.calendar.png" AlternateText="Click to show calendar" meta:resourcekey="imgEducationPHDDateCalendarResource1" />
                            <AjaxControlToolkit:CalendarExtender ID="calEducationPHDDate" runat="server" TargetControlID="txtEducationPHDDate" PopupButtonID="imgEducationPHDDateCalendar" Enabled="True"></AjaxControlToolkit:CalendarExtender>
                            
                            <asp:RequiredFieldValidator ID="reqValEducationPHDDate" runat="server" ControlToValidate="txtEducationPHDDate" ErrorMessage="* Terminal Degree Date Required" Display="Dynamic" meta:resourcekey="reqValEducationPHDDateResource1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="regExprEducationPHDDate" runat="server" ControlToValidate="txtEducationPHDDate" ErrorMessage="* Terminal Degree Date Must Be In mm/dd/yyyy Format" Display="dynamic" ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$"></asp:RegularExpressionValidator>
                            <%--<asp:CompareValidator ID="comValEducationPHDDate" Type="Date" runat="server" Operator="DataTypeCheck" ControlToValidate="txtEducationPHDDate" ErrorMessage="* PH.D. Date Must Be In mm/dd/yyyy Format" Display="Dynamic" meta:resourcekey="comValEducationPHDDateResource1"></asp:CompareValidator>--%>
                            
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Awarding Institute:</td>
                        <td  >
                            <asp:TextBox ID="txtEducationInstitution" runat="server" MaxLength="100" meta:resourcekey="txtEducationInstitutionResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator id="reqValEducationInstitution" ControlToValidate="txtEducationInstitution" ErrorMessage="* Institute Required" runat="server" meta:resourcekey="reqValEducationInstitutionResource1"/>
                            
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Discipline:</td>
                        <td  >
                            <asp:TextBox ID="txtEducationDiscipline" runat="server" MaxLength="50" meta:resourcekey="txtEducationDisciplineResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqValEducationDiscipline" runat="server" ControlToValidate="txtEducationDiscipline" ErrorMessage="* Discipline Required" meta:resourcekey="reqValEducationDisciplineResource1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Research Field:</td>
                        <td  >
                            <asp:TextBox ID="txtEducationResearch" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Advisor:</td>
                        <td  >
                            <asp:TextBox ID="txtEducationAdvisor" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                        </td>
                        <td align="right"  >
                            <br />
                            <asp:Button ID="btnEducationSave" runat="server" Text="Update" OnClientClick="return confirmPHDDate();" OnClick="btnEducationSave_Click" meta:resourcekey="btnEducationSaveResource1" /></td>
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
                        <td colspan="2">Extramural reviewers should be people from outside UC Davis.
                        Letters will be requested from the references you have provided at a later date in the process.
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ImageButton ID="lbtnReferencesAdd" runat="server" ImageURL="~/Images/addReference.gif" ImageAlign="Middle" meta:resourcekey="lbtnReferencesAddResource1"></asp:ImageButton>
                            &nbsp;<asp:Label ID="lblReferencesRemaining" runat="server" ForeColor="Brown" EnableViewState="true" meta:resourcekey="lblReferencesRemainingResource1"></asp:Label>
                            <br /><br />
                        </td>                       
                    </tr>
                    <tr>
                        <td colspan="2">
                        Existing References:
                            <asp:GridView ID="gviewReferences" skinID="gridViewReferences" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" EmptyDataText="No References Added" OnRowDeleting="gviewReferences_RowDeleting" OnSelectedIndexChanged="gviewReferences_SelectedIndexChanged" BorderStyle="None" CellPadding="0" GridLines="None" meta:resourcekey="gviewReferencesResource1">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" SelectText="Edit" EditText="" meta:resourcekey="CommandFieldResource1" >
                                    <ItemStyle CssClass="paddingLeft" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="Name:" meta:resourcekey="TemplateFieldResource1">
                                    <ItemTemplate>
                                        <%# Eval("Title") %> <%# Eval("FirstName") %> <%# Eval("LastName") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Title Expertise" Visible="False" meta:resourcekey="TemplateFieldResource2">
                                    <ItemTemplate>
                                        <%# Eval("AcadTitle") %> <%# Eval("Expertise") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department Institution" Visible="False" meta:resourcekey="TemplateFieldResource3">
                                    <ItemTemplate>
                                        <%# Eval("Dept") %> <%# Eval("Institution") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address" Visible="False" meta:resourcekey="TemplateFieldResource4">
                                    <ItemTemplate>
                                        <%# Eval("Address1") %> <%# Eval("Address2") %> <br />
                                        <%# Eval("City") %> <%# Eval("State") %> <%# Eval("Zip") %> <%# Eval("Country") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Phone" DataField="Phone" Visible="False" meta:resourcekey="BoundFieldResource1" />
                                <asp:TemplateField HeaderText="Email:" meta:resourcekey="TemplateFieldResource5">
                                    <ItemTemplate>
                                        <%# Eval("Email") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Email:" DataField="Email" Visible="False" meta:resourcekey="BoundFieldResource2" />
                                <asp:CommandField DeleteText="Remove" ShowDeleteButton="True" meta:resourcekey="CommandFieldResource2" >
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
                            <asp:CheckBox ID="chkReferencesComplete" runat="server" AutoPostBack="True" TextAlign="Left" Text="Done Uploading References" OnCheckedChanged="chkReferencesComplete_CheckedChanged" meta:resourcekey="chkReferencesCompleteResource1" />
                        </td>
                    </tr>
                </table>       
                                
                <asp:Panel ID="pnlReferencesEntry" runat="server" CssClass="modalPopup" style="display:none;" meta:resourcekey="pnlReferencesEntryResource1">
                    <span class="modalTitle">Add/Update Reference</span>
                    <div style="height:550px; width: 460px; overflow:auto;">
                    <table cellpadding="5" id="App_ReferencesEntry">
                        <tr>
                            <td align="right">
                                Title:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesTitle" runat="server" MaxLength="50" EnableViewState="False" meta:resourcekey="txtReferencesTitleResource1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                First Name:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesFirstName" runat="server" MaxLength="50" EnableViewState="False" meta:resourcekey="txtReferencesFirstNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator id="reqValReferencesFirstName" ControlToValidate="txtReferencesFirstName" ValidationGroup="References" ErrorMessage="* First Name Required" runat="server" meta:resourcekey="reqValReferencesFirstNameResource1"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Last Name:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesLastName" runat="server" MaxLength="50" EnableViewState="False" meta:resourcekey="txtReferencesLastNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator id="reqValReferencesLastName" ControlToValidate="txtReferencesLastName" ValidationGroup="References" ErrorMessage="* Last Name Required" runat="server" meta:resourcekey="reqValReferencesLastNameResource1"/>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                Academic / Professional Title:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesAcadTitle" runat="server" MaxLength="50" EnableViewState="False" meta:resourcekey="txtReferencesAcadTitleResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator id="reqValReferencesAcadTitle" ControlToValidate="txtReferencesAcadTitle" ValidationGroup="References" ErrorMessage="* Academic/Professional Title Required" runat="server" meta:resourcekey="reqValReferencesAcadTitleResource1"/>                                
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                Institute / Organization:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesInstitute" runat="server" MaxLength="50" EnableViewState="False" meta:resourcekey="txtReferencesInstituteResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator id="reqValReferencesInstitute" ControlToValidate="txtReferencesInstitute" ValidationGroup="References" ErrorMessage="* Institute/Organization Required" runat="server" meta:resourcekey="reqValReferencesInstituteResource1"/>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                Department:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesDepartment" runat="server" MaxLength="50" EnableViewState="False" meta:resourcekey="txtReferencesDepartmentResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator id="reqValReferencesDepartment" ControlToValidate="txtReferencesDepartment" ValidationGroup="References" ErrorMessage="* Department Required" runat="server" meta:resourcekey="reqValReferencesDepartmentResource1"/>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                Area of Expertise:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesExpertise" runat="server" MaxLength="50" EnableViewState="False" meta:resourcekey="txtReferencesExpertiseResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator id="reqValReferencesExpertise" ControlToValidate="txtReferencesExpertise" ValidationGroup="References" ErrorMessage="* Expertise Required" runat="server" meta:resourcekey="reqValReferencesExpertiseResource1"/>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                Address 1:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesAddress1" runat="server" MaxLength="50" EnableViewState="False" meta:resourcekey="txtReferencesAddress1Resource1"></asp:TextBox>
                                <asp:RequiredFieldValidator id="reqValReferencesAddress1" ControlToValidate="txtReferencesAddress1" ValidationGroup="References" ErrorMessage="* Address Required" runat="server" meta:resourcekey="reqValReferencesAddress1Resource1"/>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                Address 2:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesAddress2" runat="server" MaxLength="50" EnableViewState="False" meta:resourcekey="txtReferencesAddress2Resource1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                City:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesCity" runat="server" MaxLength="50" EnableViewState="False" meta:resourcekey="txtReferencesCityResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator id="reqValReferencesCity" ControlToValidate="txtReferencesCity" ValidationGroup="References" ErrorMessage="* City Required" runat="server" meta:resourcekey="reqValReferencesCityResource1"/>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                State:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesState" runat="server" MaxLength="50" EnableViewState="False" meta:resourcekey="txtReferencesStateResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator id="reqValReferencesState" ControlToValidate="txtReferencesState" ValidationGroup="References" ErrorMessage="* State Required" runat="server" meta:resourcekey="reqValReferencesStateResource1"/>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                Zip:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesZip" runat="server" MaxLength="20" EnableViewState="False" meta:resourcekey="txtReferencesZipResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator id="reqValReferencesZip" ControlToValidate="txtReferencesZip" ValidationGroup="References" ErrorMessage="* Zip Required" runat="server" meta:resourcekey="reqValReferencesZipResource1"/>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                Country:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesCountry" runat="server" MaxLength="50" EnableViewState="False" meta:resourcekey="txtReferencesCountryResource1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                Phone Number:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesPhone" runat="server" MaxLength="20" EnableViewState="False" meta:resourcekey="txtReferencesPhoneResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator id="reqValReferencesPhone" ControlToValidate="txtReferencesPhone" ValidationGroup="References" ErrorMessage="* Phone Required" runat="server" Display="Dynamic" meta:resourcekey="reqValReferencesPhoneResource1"/>
                                <asp:RegularExpressionValidator ID="regValReferencesPhone" runat="server" ControlToValidate="txtReferencesPhone" ValidationGroup="References" ErrorMessage="* Phone Format Not Valid" ValidationExpression="((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}" Display="Dynamic" meta:resourcekey="regValReferencesPhoneResource1"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                Email:</td>
                            <td  >
                                <asp:TextBox ID="txtReferencesEmail" runat="server" MaxLength="100" EnableViewState="False" meta:resourcekey="txtReferencesEmailResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator id="reqValReferencesEmail" ControlToValidate="txtReferencesEmail" ValidationGroup="References" ErrorMessage="* Email Required" runat="server" Display="Dynamic" meta:resourcekey="reqValReferencesEmailResource1"/>
                                <asp:RegularExpressionValidator ID="regValReferencesEmail" runat="server" ValidationGroup="References" ErrorMessage="* Email Format Not Required" ControlToValidate="txtReferencesEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" meta:resourcekey="regValReferencesEmailResource1"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                            </td>
                            <td>
                                <br />
                                <asp:Button ID="btnReferencesAddUpdate" runat="server" CommandArgument="0" Text="Add Reference" ValidationGroup="References" OnClick="btnReferencesAddUpdate_Click" meta:resourcekey="btnReferencesAddUpdateResource1" />
                                <asp:Button ID="btnReferencesCancel" runat="server" Text="Cancel" CausesValidation="False" OnClick="btnReferencesCancel_Click" meta:resourcekey="btnReferencesCancelResource1" />
                            </td>
                        </tr>
                    </table></div>
                </asp:Panel>
                
                <AjaxControlToolkit:ModalPopupExtender ID="mpopupReferencesEntry" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlReferencesEntry" TargetControlID="lbtnReferencesAdd" DynamicServicePath="" Enabled="True"></AjaxControlToolkit:ModalPopupExtender>
                
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
                            <asp:TextBox ID="txtCurrentPositionTitle" runat="server" MaxLength="100" meta:resourcekey="txtCurrentPositionTitleResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Institution/Organization:</td>
                        <td  >
                            <asp:TextBox ID="txtCurrentPositionInstitution" runat="server" MaxLength="100" meta:resourcekey="txtCurrentPositionInstitutionResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Department:</td>
                        <td  >
                            <asp:TextBox ID="txtCurrentPositionDepartment" runat="server" MaxLength="100" meta:resourcekey="txtCurrentPositionDepartmentResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Address 1:</td>
                        <td  >
                            <asp:TextBox ID="txtCurrentPositionAddress1" runat="server" MaxLength="50" meta:resourcekey="txtCurrentPositionAddress1Resource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Address 2:</td>
                        <td  >
                            <asp:TextBox ID="txtCurrentPositionAddress2" runat="server" MaxLength="50" meta:resourcekey="txtCurrentPositionAddress2Resource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            City:</td>
                        <td  >
                            <asp:TextBox ID="txtCurrentPositionCity" runat="server" MaxLength="50" meta:resourcekey="txtCurrentPositionCityResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            State:</td>
                        <td  >
                            <asp:TextBox ID="txtCurrentPositionState" runat="server" MaxLength="50" meta:resourcekey="txtCurrentPositionStateResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Zip:</td>
                        <td  >
                            <asp:TextBox ID="txtCurrentPositionZip" runat="server" MaxLength="20" meta:resourcekey="txtCurrentPositionZipResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Country:</td>
                        <td  >
                            <asp:TextBox ID="txtCurrentPositionCountry" runat="server" MaxLength="50" meta:resourcekey="txtCurrentPositionCountryResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                        </td>
                        <td align="right"  >
                            <br />
                            <asp:Button ID="btnCurrentPositionSave" runat="server" Text="Update" OnClick="btnCurrentPositionSave_Click" meta:resourcekey="btnCurrentPositionSaveResource1" /></td>
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
                            <asp:Literal ID="litResumePDF" runat="server" Text="<%$ Resources:PDFWarning %>" ></asp:Literal>
                        </td>                       
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:FileUpload ID="fileResume" runat="server" meta:resourcekey="fileResumeResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                        </td>
                        <td align="right"  >
                            <br />
                            <asp:Button ID="btnResumeUpload" runat="server" Text="Upload" OnClick="btnResumeUpload_Click" meta:resourcekey="btnResumeUploadResource1" /></td>
                    </tr>
                </table>
            </asp:View>
            
            <asp:View ID="viewCV" runat="server">
                 <span class="boxTitle">CV</span><br />
                <table class="box" style="width:500px;" cellpadding="5">
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Literal ID="litCVPDF" runat="server" Text="<%$ Resources:PDFWarning %>"></asp:Literal>
                        </td>                       
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:FileUpload ID="fileCV" runat="server" meta:resourcekey="fileCVResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                        </td>
                        <td align="right"  >
                            <br />
                            <asp:Button ID="btnCVUpload" runat="server" Text="Upload" OnClick="btnCVUpload_Click" meta:resourcekey="btnCVUploadResource1" /></td>
                    </tr>
                </table>
            </asp:View>
        
            <asp:View ID="viewStatementOfPurpose" runat="server">
                <span class="boxTitle">Statement of Purpose</span><br />
                <table class="box" style="width:500px;" cellpadding="5">
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
<%--                    <tr>
                        <td colspan="2" align="left">
                            <asp:CheckBox ID="chkCoverLetterOption" runat="server" Text=" I do not wish to upload a cover letter." AutoPostBack="True" OnCheckedChanged="chkCoverLetterOption_CheckedChanged" Font-Bold="True" meta:resourcekey="chkCoverLetterOptionResource1" />
                        </td>
                    </tr>--%>
                    <asp:CheckBox runat="server" ID="chkCoverLetterOption" Checked="false" Visible="false"/>
                    <tr>
                        <td colspan="2">
                            <asp:Literal ID="litCoverLetterPDF" runat="server" Text="<%$ Resources:PDFWarning %>"></asp:Literal>
                        </td>                       
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:FileUpload ID="fileCoverLetter" runat="server" meta:resourcekey="fileCoverLetterResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                        </td>
                        <td align="right"  >
                            <br />
                            <asp:Button ID="btnCoverLetterUpload" runat="server" Text="Upload" OnClick="btnCoverLetterUpload_Click" meta:resourcekey="btnCoverLetterUploadResource1" /></td>
                    </tr>
                </table>
            </asp:View>
        
            <asp:View ID="viewResearchProposal" runat="server">
                <span class="boxTitle">Research Proposal</span><br />
                <table class="box" style="width:500px; " cellpadding="5">
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                           <asp:Literal ID="litResearchPDF" runat="server" Text="<%$ Resources:PDFWarning %>"></asp:Literal>
                        </td>                       
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:FileUpload ID="fileResearchInterests" runat="server" meta:resourcekey="fileResearchInterestsResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                        </td>
                        <td align="right"  >
                            <br />
                            <asp:Button ID="btnResearchInterestsUpload" runat="server" Text="Upload" OnClick="btnResearchInterestsUpload_Click" meta:resourcekey="btnResearchInterestsUploadResource1" /></td>
                    </tr>
                </table>
            </asp:View>
            
            <asp:View ID="viewExtensionInterests" runat="server">
                <span class="boxTitle">Extension Interests</span><br />
                <table class="box" style="width:500px; " cellpadding="5">
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Literal ID="litExtensionPDF" runat="server" Text="<%$ Resources:PDFWarning %>"></asp:Literal> 
                        </td>                       
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:FileUpload ID="fileExtensionInterests" runat="server" meta:resourcekey="fileExtensionInterestsResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                        </td>
                        <td align="right"  >
                            <br />
                            <asp:Button ID="btnExtensionInterestsUpload" runat="server" Text="Upload" OnClick="btnExtensionInterestsUpload_Click" meta:resourcekey="btnExtensionInterestsUploadResource1" /></td>
                    </tr>
                </table>
            </asp:View>
            
            <asp:View ID="viewTeachingInterests" runat="server">
                <span class="boxTitle">Teaching Interests</span><br />
                <table class="box" style="width:500px; " cellpadding="5">
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Literal ID="litTeachingPDF" runat="server" Text="<%$ Resources:PDFWarning %>"></asp:Literal>
                        </td>                       
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:FileUpload ID="fileTeachingInterests" runat="server" meta:resourcekey="fileTeachingInterestsResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                        </td>
                        <td align="right"  >
                            <br />
                            <asp:Button ID="btnTeachingInterestsUpload" runat="server" Text="Upload" OnClick="btnTeachingInterestsUpload_Click" meta:resourcekey="btnTeachingInterestsUploadResource1" /></td>
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
                        <td colspan="2">
                            <asp:Panel ID="pnlConfidentialSurveyHolder" runat="server" meta:resourcekey="pnlConfidentialSurveyHolderResource1">
                                <span style="font-weight: bold;">Confidential Survey Legal Notification</span>                               
                                <br />
                                <asp:ImageButton ID="CSNImage" runat="server" ImageUrl="~/Images/show_details.jpg" AlternateText="Show Details" meta:resourcekey="CSNImageResource1" />
                            </asp:Panel>
                                                        
                            <asp:Panel ID="pnlConfidentialSurveyNotification" runat="server" Height="0px" meta:resourcekey="pnlConfidentialSurveyNotificationResource1" >
                                <div id="legalNote">
                                    <%= GetLocalResourceObject("ConfidentialSurveyNotificationText") %>
                                    <br />
                                </div>
                            </asp:Panel>
                            
                            <AjaxControlToolkit:CollapsiblePanelExtender ID="collapseConfidentialSurveyNotification" runat="server" TargetControlID="pnlConfidentialSurveyNotification"
                                                                         Collapsed="True" CollapseControlID="pnlConfidentialSurveyHolder" ExpandControlID="pnlConfidentialSurveyHolder"
                                                                           ImageControlID="CSNImage" ExpandedImage="../Images/hidedetails.jpg" CollapsedImage="../Images/show_details.jpg" SuppressPostBack="True" Enabled="True"></AjaxControlToolkit:CollapsiblePanelExtender>
                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="font-style:italic;">
                            <asp:Literal ID="litIAgreeWarning" runat="server" Text="Note: While you need not fill out any fields, you must read and press the 'I agree' button for your application to be considered complete. 
                            Once you select an answer button, you can clear it by pressing the reset button below. 
                            If you complete the form but later decide you do not want to provide this information, you can return to this form and resubmit it, leaving the desired information blank." meta:resourcekey="litIAgreeWarningResource1"></asp:Literal> <br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">
                            <span style="color:#0b65c5; font-weight: bold;">
                            Sex:</span></td>
                        <td valign="top"  >
                            <asp:RadioButtonList ID="rbtnConfidentialSurveySex" runat="server" DataSourceID="ObjectDataGender" DataTextField="GenderType" DataValueField="ID" meta:resourcekey="rbtnConfidentialSurveySexResource1">
                            </asp:RadioButtonList>
                            <asp:ObjectDataSource ID="ObjectDataGender" runat="server"
                                SelectMethod="GetAll" TypeName="CAESDO.Recruitment.BLL.GenderBLL" OldValuesParameterFormatString="original_{0}">
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
                                <asp:RadioButton ID="rbtnAmericanIndian" runat="server" GroupName="Ethnicity" Text="American Indian / Alaskan Native" meta:resourcekey="rbtnAmericanIndianResource1" />
                                (specify tribal affiliation: <asp:TextBox ID="txtAmericanIndian" runat="server" meta:resourcekey="txtAmericanIndianResource1" ></asp:TextBox>)
                            <br />
                            <br />
                            <strong>
                            Asian/Pacific Islander </strong>
                            <br />
                                <asp:RadioButton ID="rbtnChinese" runat="server" GroupName="Ethnicity" Text="Chinese/Chinese-American" meta:resourcekey="rbtnChineseResource1" /><br />
                                <asp:RadioButton ID="rbtnPakistani" runat="server" GroupName="Ethnicity" Text="East Indian/Pakistani" meta:resourcekey="rbtnPakistaniResource1" /><br />
                                <asp:RadioButton ID="rbtnPhilipino" runat="server" GroupName="Ethnicity" Text="Filipino/Pilipino" meta:resourcekey="rbtnPhilipinoResource1" /><br />
                                <asp:RadioButton ID="rbtnJapanese" runat="server" GroupName="Ethnicity" Text="Japanese/Japanese-American" meta:resourcekey="rbtnJapaneseResource1" /><br />
                                <asp:RadioButton ID="rbtnAsian" runat="server" GroupName="Ethnicity" Text="Other Asian ( including Far East Korea, Southeast Asian or Pacific Islands, Samoa)" meta:resourcekey="rbtnAsianResource1" />
                            <br />
                            <br />
                            <strong>Black</strong>
                            <br />
                                <asp:RadioButton ID="rbtnBlack" runat="server" GroupName="Ethnicity" Text="Black/African-American" meta:resourcekey="rbtnBlackResource1" />
                            <br />
                            <br />
                            <strong>
                            Hispanic (including Black individuals whose origins are Hispanic)
                            <br />
                            </strong>
                                <asp:RadioButton ID="rbtnLatino" runat="server" GroupName="Ethnicity" Text="Latin-American/Latino (including Cuban and Puerto Rican)" meta:resourcekey="rbtnLatinoResource1" /><br />
                                <asp:RadioButton ID="rbtnMexican" runat="server" GroupName="Ethnicity" Text="Mexican/Mexican-American/Chicano" meta:resourcekey="rbtnMexicanResource1" /><br />
                                <asp:RadioButton ID="rbtnSpanish" runat="server" GroupName="Ethnicity" Text="Other Spanish/Spanish-American" meta:resourcekey="rbtnSpanishResource1" />
                            <br />
                            <br />
                            <strong>
                            White</strong>
                            <br />
                                <asp:RadioButton ID="rbtnWhite" runat="server" GroupName="Ethnicity" Text="White/Caucasian (including the Middle East)" meta:resourcekey="rbtnWhiteResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td   align="right" valign="top">
                            <span style="color:#0b65c5; font-weight: bold;">
                            Recruitment Source:</span></td>
                        <td valign="top"  >
                            <asp:Repeater ID="rptRecruitmentSource" runat="server" DataSourceID="ObjectDataRecruitmentSrc">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkRecruitmentSource" runat="server" meta:resourcekey="chkRecruitmentSourceResource1" />
                                    <asp:Label ID="lblRecruitmentSource" runat="server" Text='<%# Eval("RecruitmentSource") %>' meta:resourcekey="lblRecruitmentSourceResource1"></asp:Label>&nbsp;&nbsp;
                                    <asp:Literal ID="litBeginSpecify" runat="server" Text="(Specify:" Visible='<%# Eval("AllowSpecify") %>' meta:resourcekey="litBeginSpecifyResource1"></asp:Literal>
                                    <asp:TextBox ID="txtSpecify" runat="server" MaxLength="50" Visible='<%# Eval("AllowSpecify") %>' meta:resourcekey="txtSpecifyResource1"></asp:TextBox>
                                    <asp:Literal ID="litEndSpecify" runat="server" Text=")" Visible='<%# Eval("AllowSpecify") %>' meta:resourcekey="litEndSpecifyResource1"></asp:Literal>
                                </ItemTemplate>
                                <SeparatorTemplate>
                                    <br />
                                </SeparatorTemplate>
                            </asp:Repeater>
                            <asp:ObjectDataSource ID="ObjectDataRecruitmentSrc" runat="server"
                                 SelectMethod="GetAll" TypeName="CAESDO.Recruitment.BLL.RecruitmentSrcBLL" OldValuesParameterFormatString="original_{0}">
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                        </td>
                        <td align="right"  >
                            <br />
                            <asp:Button ID="btnConfidentialSurveyAccept" runat="server" Text="I Agree" OnClick="btnConfidentialSurveyAccept_Click" meta:resourcekey="btnConfidentialSurveyAcceptResource1" />
                            <button type="reset" value="Reset">Reset</button>
                            </td>
                    </tr>
                </table>
            </asp:View>
            
            <asp:View ID="viewTestimonials" runat="server">
                <span class="boxTitle">Eduational Testimonials</span><br />
                <table class="box" style="width:500px; height: 350px;" cellpadding="5">
                    
                    <tr>
                        <td colspan="2">
                            <br />
                            This position requests
                            <asp:Literal ID="litPublicationsNum" runat="server" EnableViewState="False" meta:resourcekey="litPublicationsNumResource1"></asp:Literal>
                            educational testimonials. Please submit as many as you have, indicating below when complete. You can return to this page to change or enter more testimonials later.
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Literal ID="litPublicationsPDF" runat="server" Text="<%$ Resources:PDFWarning %>"></asp:Literal>
                        </td>                       
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:FileUpload ID="filePublications" runat="server" meta:resourcekey="filePublicationsResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                        </td>
                        <td align="right"  >
                            <asp:Button ID="btnPublicationsUpload" runat="server" Text="Upload" OnClick="btnPublicationsUpload_Click" meta:resourcekey="btnPublicationsUploadResource1" /></td>
                    </tr>
                    <tr>
                        <td colspan="2"><asp:Label ID="litPublicationsFileTypeWarning" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Repeater ID="rptPublications" runat="server">
                                <HeaderTemplate>
                                    Existing Testimonials <asp:Label ID="lblPublicationsRemaining" runat="server" Text='<%# NumPublicationsRemainingText() %>' ForeColor="Brown" EnableViewState="False" meta:resourcekey="lblPublicationsRemainingResource1"></asp:Label>: <br />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    &nbsp;&nbsp;<asp:LinkButton ID="lbtnPublicationFile" runat="server" Text='<%# Eval("FileName") %>' CommandArgument='<%# Eval("ID") %>' OnClick="lbtnPublicationFile_Click" meta:resourcekey="lbtnPublicationFileResource1"></asp:LinkButton>
                                    <asp:ImageButton ID="ibtnPublicationsRemoveFile" runat="server" CommandArgument='<%# Eval("ID") %>' OnClick="ibtnPublicationsRemoveFile_Click" AlternateText="Remove File" ImageUrl="~/Images/delete.gif" ToolTip="Remove File" meta:resourcekey="ibtnPublicationsRemoveFileResource1" />
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
                            <asp:CheckBox ID="chkPublicationsFinalize" runat="server" AutoPostBack="True" TextAlign="Left" Text="Done Uploading Testimonials" OnCheckedChanged="chkPublicationsFinalize_CheckedChanged" meta:resourcekey="chkPublicationsFinalizeResource1" />
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
                            <asp:Literal ID="litDissertationPDF" runat="server" Text="<%$ Resources:PDFWarning %>"></asp:Literal>
                        </td>                       
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:FileUpload ID="fileDissertation" runat="server" meta:resourcekey="fileDissertationResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                        </td>
                        <td align="right"  >
                            <br />
                            <asp:Button ID="btnDissertationUpload" runat="server" Text="Upload" OnClick="btnDissertationUpload_Click" meta:resourcekey="btnDissertationUploadResource1" /></td>
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

<asp:Content ContentPlaceHolderID="cphFooter" runat="server" ID="cphAppFooter">

<div id="footer" style="text-align: center">
    <div>
            <p>
                CAESDO Recruitments Version: <%= GetAssemblyVersion() %>
                <br />
                Developed By The College Of Agricultural And Environmental Science Dean's Office
            </p>
        </div>
    </div>
</asp:Content>