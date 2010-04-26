<%@ Page AutoEventWireup="true" CodeFile="addPosition.aspx.cs" Inherits="CAESDO.Recruitment.Web.addPosition" Language="C#" MasterPageFile="~/MasterPage.master" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <span class="boxTitle"><asp:Image ID="imgProfile" runat="server" EnableViewState="false" ImageUrl="~/Images/profile_sm.gif" style="vertical-align:middle;" AlternateText="" /> Create Position</span><br />
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
                Deadline:</td>
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
                <asp:DropDownList ID="dlistDepartment" runat="server" DataSourceID="ObjectDataUnits" DataTextField="ShortName" DataValueField="FISCode"></asp:DropDownList><asp:ObjectDataSource ID="ObjectDataUnits" runat="server" SelectMethod="GetAll"
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
                <asp:TextBox ID="txtHRRep" runat="server" MaxLength="100"></asp:TextBox></td>
        </tr>
        <tr>
            <td   align="right">
                HR Phone Number:</td>
            <td  >
                <asp:TextBox ID="txtHRPhone" runat="server" MaxLength="13"></asp:TextBox>
                <%--<AjaxControlToolkit:MaskedEditExtender ID="maskHRPhone" runat="server" TargetControlID="txtHRPhone" MaskType="Number" Mask="(999) 999-9999" ClearMaskOnLostFocus="false" Filtered="" AutoComplete="false"></AjaxControlToolkit:MaskedEditExtender>
                --%>
                <asp:RegularExpressionValidator ID="regValHRPhone" runat="server" ControlToValidate="txtHRPhone" ErrorMessage="*" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                HR Email:
            </td>
            <td>
                <asp:TextBox ID="txtHREmail" runat="server" MaxLength="100"></asp:TextBox>
                
                <asp:RegularExpressionValidator ID="regValHREmail" runat="server" ErrorMessage="*" ControlToValidate="txtHREmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
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
            <td align="right" valign="top">
                Summary:</td>
            <td  >
                <asp:TextBox ID="txtShortDescription" runat="server" Height="90px" Rows="4" TextMode="MultiLine"
                    Width="233px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right">
                Full job description (PDF):</td>
            <td  >
                <asp:FileUpload ID="filePositionDescription" runat="server" />
                
                <asp:RequiredFieldValidator ID="reqValPositionDescription" runat="server" ControlToValidate="filePositionDescription" ErrorMessage="*"></asp:RequiredFieldValidator>
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
                <asp:Button ID="btnCreatePosition" runat="server" Text="Create!" OnClick="btnCreatePosition_Click" /></td>
        </tr>
    </table>
 
</asp:Content>

