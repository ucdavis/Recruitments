<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BiographicalSelection.aspx.cs" Inherits="CAESDO.Recruitment.Web.Review_BiographicalSelection" Title="Biographical Data Spreadsheet" EnableEventValidation="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

Position:
    <asp:DropDownList ID="dlistPositions" runat="server">
    </asp:DropDownList>
    <asp:RequiredFieldValidator id="reqValPositions" ControlToValidate="dlistPositions" ErrorMessage="* Position Required" runat="server"/><br />
    <br />
    
    <asp:CheckBox ID="chkOutputToFile" runat="server" Text="Output To Excel File" Checked="true" />
    <br /><br />
    
    <asp:Button ID="btnDisplayReport" runat="server" Text="Open Spreadsheet" OnClick="btnDisplayReport_Click" />    
    <br /><asp:Label ID="lblReportStatus" runat="server" EnableViewState="false" ForeColor="red"></asp:Label>
    
    <AjaxControlToolkit:CascadingDropDown ID="cascadePositions" runat="server" Category="Positions"
        PromptText="Select a Position" ServiceMethod="GetPositionsForCommitteeOnly" ServicePath="RecruitmentCommitteeService.asmx"
        TargetControlID="dlistPositions">
    </AjaxControlToolkit:CascadingDropDown>

</asp:Content>

