<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ClosePosition.aspx.cs" Inherits="Authorized_ClosePosition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h2>
    Closing <%= CurrentPosition.PositionTitle %>
</h2>
<p>
    To close this position, please complete the <a href="http://manuals.ucdavis.edu/APM/500f.pdf">Final Recruitment Report (PDF)</a>.
</p>
<p>
    When you are finished filling out this form, save it and upload below to close the position.
</p>
<br />
<p>
    Final Recruitment Report: 
    <asp:FileUpload ID="fileFinalRecruitmentReport" runat="server" />
    <asp:RequiredFieldValidator ID="reqValFinalRecruitmentReport" runat="server" ControlToValidate="fileFinalRecruitmentReport" ErrorMessage="* Final Recruitment Report Required"></asp:RequiredFieldValidator><br />
    <asp:Label ID="lblFileError" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
</p>
<p>
    <asp:Button ID="btnClosePosition" runat="server" Text="Close Position" OnClick="ClosePosition" />
</p>

</asp:Content>