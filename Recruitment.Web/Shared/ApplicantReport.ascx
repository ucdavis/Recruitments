<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ApplicantReport.ascx.cs" Inherits="CAESDO.Recruitment.Web.Shared_ApplicantReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Applicant Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gviewApplications" runat="server" AutoGenerateColumns="false" DataKeyNames="id">
            <Columns>
            //select FirstName, MiddleName, LastName, Email, Address1, Address2, City, State, Zip, Country, CountryCode, Phone
                <asp:TemplateField HeaderText="First Name">
                    <ItemTemplate>
                        <%# Eval("AssociatedProfile.FirstName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Middle Name">
                    <ItemTemplate>
                        <%# Eval("AssociatedProfile.MiddleName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Last Name">
                    <ItemTemplate>
                        <%# Eval("AssociatedProfile.LastName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Email">
                    <ItemTemplate>
                        <%# Eval("Email") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Address1">
                    <ItemTemplate>
                        <%# Eval("Email") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Address2">
                    <ItemTemplate>
                        <%# Eval("Email") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="City">
                    <ItemTemplate>
                        <%# Eval("Email") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="State">
                    <ItemTemplate>
                        <%# Eval("Email") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Zip">
                    <ItemTemplate>
                        <%# Eval("Email") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Country">
                    <ItemTemplate>
                        <%# Eval("Email") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CountryCode">
                    <ItemTemplate>
                        <%# Eval("Email") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Phone">
                    <ItemTemplate>
                        <%# Eval("Email") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <%# (bool)Eval("Submitted") ? "Submitted" : "Incomplete"  %><br />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>  
        </asp:GridView>
    </div>
    </form>
</body>
</html>
