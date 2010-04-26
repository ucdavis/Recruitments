<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="App.aspx.cs" Inherits="CAESDO.Recruitment.Web.App" Title="Untitled Page" Trace="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
    <div style="height:22px; background-image:url(../Images/appmenuTop.gif);"><img src="../Images/appmenuTopLeft.gif" alt="" style="float:left;" /><img src="../Images/appmenuTopRight.gif" alt="" style="float:right;" /></div>
    <div style="width:203px; float:left; background: url(../Images/appmenuLeft.gif) repeat-y; height:500px;">
    
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
    </div>
    <div style="background:url(../Images/appmenuRight.gif) repeat-y right; height:500px;">
        <asp:Panel ID="pnlMainWindow" runat="server">
        
        <asp:MultiView ID="mviewSteps" runat="server" ActiveViewIndex="0">
        
            <asp:View ID="viewHome" runat="server">
                Home
            </asp:View>
            
            <asp:View ID="viewContactInformation" runat="server">
                <span class="boxTitle"><asp:Image ID="imgContactProfile" runat="server" EnableViewState="false" ImageUrl="~/Images/profile_sm.gif" style="vertical-align:middle;" AlternateText="" />Contact Information</span><br />
                <table class="box" style="width:500px; height: 350px;" cellpadding="5">
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
                        <td  >
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
                        <td align="right"  >
                            <br />
                            <asp:Button ID="btnContactSave" runat="server" Text="Update" /></td>
                    </tr>
                </table>
            </asp:View>
            
            <asp:View ID="viewEducationInformation" runat="server">
                <span class="boxTitle"><asp:Image ID="imgEducation" runat="server" EnableViewState="false" ImageUrl="~/Images/profile_sm.gif" style="vertical-align:middle;" AlternateText="" />Ph.D. Award Information</span><br />
                <table class="box" style="width:500px; height: 350px;" cellpadding="5">
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
                            <asp:Button ID="btnEducation" runat="server" Text="Update" /></td>
                    </tr>
                </table>
            </asp:View>
        
            <asp:View ID="viewReferences" runat="server">
                References
            </asp:View>
        
            <asp:View ID="viewCurrentPosition" runat="server">
                <span class="boxTitle"><asp:Image ID="imgCurrentPosition" runat="server" EnableViewState="false" ImageUrl="~/Images/profile_sm.gif" style="vertical-align:middle;" AlternateText="" />Current Position</span><br />
                <table class="box" style="width:500px; height: 350px;" cellpadding="5">
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
                            <asp:Button ID="btnCurrentPosition" runat="server" Text="Update" /></td>
                    </tr>
                </table>
            </asp:View>
            
            <asp:View ID="viewResume" runat="server">
                Resume
            </asp:View>
        
            <asp:View ID="viewCoverLetter" runat="server">
                Cover Letter
            </asp:View>
        
            <asp:View ID="viewResearchInterests" runat="server">
                Research Interests
            </asp:View>
            
            <asp:View ID="viewTranscripts" runat="server">
                Transcripts
            </asp:View>
            
            <asp:View ID="viewConfidentialSurvey" runat="server">
                <span class="boxTitle"><asp:Image ID="imgConfidentialSurvey" runat="server" EnableViewState="false" ImageUrl="~/Images/profile_sm.gif" style="vertical-align:middle;" AlternateText="" />Confidential Survey</span><br />
                <table class="box" style="width:500px; height: 350px;" cellpadding="5">
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <td align="right">
                            Sex:</td>
                        <td  >
                            <asp:RadioButtonList ID="rbtnConfidentialSurveySex" runat="server" DataSourceID="ObjectDataGender" DataTextField="GenderType" DataValueField="ID">
                            </asp:RadioButtonList>
                            <asp:ObjectDataSource ID="ObjectDataGender" runat="server"
                                SelectMethod="GetAll" TypeName="CAESDO.Recruitment.Data.NHibernateDaoFactory+GenderDao">
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Ethnicity:</td>
                        <td  >
                            <asp:Repeater ID="reptConfidentialSurveyEthnicity" runat="server" DataSourceID="ObjectDataEthnicity">
                                <ItemTemplate>    
                                    <asp:RadioButton ID="rbtnConfidentialSurveyEthnicity" runat="server" Text='<%# Eval("EthnicityValue") %>' />
                                </ItemTemplate>
                                <SeparatorTemplate>
                                    <br />
                                </SeparatorTemplate>
                            </asp:Repeater>
                            <asp:ObjectDataSource ID="ObjectDataEthnicity" runat="server"
                                SelectMethod="GetAll" TypeName="CAESDO.Recruitment.Data.NHibernateDaoFactory+EthnicityDao">
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Institution:</td>
                        <td  >
                            <asp:TextBox ID="TextBox3" runat="server" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                        </td>
                        <td align="right"  >
                            <br />
                            <asp:Button ID="Button1" runat="server" Text="Update" /></td>
                    </tr>
                </table>
            </asp:View>
            
        </asp:MultiView>
        
        </asp:Panel>
    </div>
    
    <div style="clear:both; height:22px; background-image:url(../Images/appmenuBot.gif);"><img src="../Images/appmenuBotLeft.gif" alt="" style="float:left;" /><img src="../Images/appmenuBotRight.gif" alt="" style="float:right;" /></div>
</asp:Content>

