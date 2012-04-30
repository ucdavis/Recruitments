<%@ Control Language="C#" AutoEventWireup="true" Inherits="CAESDO.Recruitment.Web.Authorized_RecruitmentSources" Codebehind="RecruitmentSources.ascx.cs" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Recruitment Sources Survey Results</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gviewSourcesCount" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="PublicationCount" HeaderText="Publication Advertisement" />
                <asp:BoundField DataField="ProfessionalOrgCount" HeaderText="Professional Organization" />
                <asp:BoundField DataField="UCDAnnouncementCount" HeaderText="UCD Position Announcement" />
                <asp:BoundField DataField="InquiryCount" HeaderText="General Inquiry" />
                <asp:BoundField DataField="FriendCount" HeaderText="Friend" />
                <asp:BoundField DataField="OtherCount" HeaderText="Other" />
                <asp:BoundField DataField="TotalCount" HeaderText="Total Applications" />
            </Columns>
        </asp:GridView>
        
        <br />
        <br />
        
        <asp:GridView ID="gviewSourcesText" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField HeaderText="Publication Advertisement Sources:" DataField="PublicationSources" HtmlEncode="false" />
                <asp:BoundField HeaderText="Professional Organization Sources:" DataField="ProfessionalOrgSources" HtmlEncode="false" />
                <asp:BoundField HeaderText="Other Sources:" DataField="OtherSources" HtmlEncode="false" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>