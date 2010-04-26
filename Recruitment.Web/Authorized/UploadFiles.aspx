<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="UploadFiles.aspx.cs" Inherits="CAESDO.Recruitment.Web.Authorized_UploadFiles" Title="Upload Files" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    Position: <asp:DropDownList ID="dlistPositions" runat="server"></asp:DropDownList>
    <br />
    Applicant: <asp:DropDownList ID="dlistApplications" runat="server"></asp:DropDownList>
    <br />
    File Type: <asp:DropDownList ID="dlistFileTypes" runat="server"></asp:DropDownList>
    <asp:RequiredFieldValidator id="reqValFileType" ControlToValidate="dlistFileTypes" ErrorMessage="* You must select a fileType" runat="server"/>
        
    <br /><br />
    <table class="box" style="width:500px;" cellpadding="5">
        <tr>
            <td colspan="2"><br />
                <asp:Label ID="lblStatus" runat="server" EnableViewState="False" ForeColor="Green"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="2">
                Please upload your file as a PDF Document. Maximum file size allowed is 10 MB. 
            </td>                       
        </tr>
        <tr>
            <td colspan="2">
                <asp:FileUpload ID="fileUpload" runat="server" />
            </td>
        </tr>
        <tr>
            <td   align="right">
            </td>
            <td align="right"  >
                <br />
                <asp:Button ID="btnfileUpload" runat="server" Text="Upload" OnClick="btnfileUpload_Click" /></td>
        </tr>
    </table>    

    <AjaxControlToolkit:CascadingDropDown ID="cascadePositions" runat="server" TargetControlID="dlistPositions" 
                                             Category="Positions" PromptText="Select a Position" ServicePath="RecruitmentService.asmx" 
                                             ServiceMethod="GetPositions">
    </AjaxControlToolkit:CascadingDropDown>
    
    <AjaxControlToolkit:CascadingDropDown ID="cascadeApplications" runat="server" TargetControlID="dlistApplications" 
                                             Category="Applications" PromptText="Select an Application" ServicePath="RecruitmentService.asmx" 
                                             ParentControlID="dlistPositions" ServiceMethod="GetApplications">
    </AjaxControlToolkit:CascadingDropDown>
    
    <AjaxControlToolkit:CascadingDropDown ID="cascadeFileTypes" runat="server" TargetControlID="dlistFileTypes"
                                             Category="FileTypes" PromptText="Select a File Type" ServicePath="RecruitmentService.asmx"
                                             ParentControlID="dlistApplications" ServiceMethod="GetFileTypesNoLettersOfRec">
    </AjaxControlToolkit:CascadingDropDown>
    
</asp:Content>

