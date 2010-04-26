<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApplicationChoice.aspx.cs" Inherits="CAESDO.Recruitment.Web.Review_ApplicationChoice" Title="Application Choice" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    Position:
    <asp:DropDownList ID="dlistPositions" runat="server">
    </asp:DropDownList><br />
    <br />

    Application:
    <asp:DropDownList ID="dlistApplications" runat="server">
    </asp:DropDownList><br />
    <br />
    
    <asp:Button ID="btnApplicationReview" runat="server" Text="Review Application" OnClick="btnApplicationReview_Click" />
    
    <AjaxControlToolkit:CascadingDropDown ID="cascadePositions" runat="server" Category="Positions"
        PromptText="Select a Position" ServiceMethod="GetPositionsForCommittee" ServicePath="RecruitmentCommitteeService.asmx"
        TargetControlID="dlistPositions">
    </AjaxControlToolkit:CascadingDropDown>
    
    <AjaxControlToolkit:CascadingDropDown ID="cascadeApplications" runat="server" Category="Applications"
        ParentControlID="dlistPositions" PromptText="Select an Application" ServiceMethod="GetApplications"
        ServicePath="RecruitmentCommitteeService.asmx" TargetControlID="dlistApplications">
    </AjaxControlToolkit:CascadingDropDown>    

</asp:Content>

