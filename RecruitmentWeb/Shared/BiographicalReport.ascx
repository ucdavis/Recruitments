<%@ Control Language="C#" AutoEventWireup="true" Inherits="CAESDO.Recruitment.Web.Review_BiographicalReport" Codebehind="BiographicalReport.ascx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Biographical Data Spreadsheet</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gViewBiographicalData" runat="server" AutoGenerateColumns="false" DataKeyNames="id" OnRowDataBound="gViewBiographicalData_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <%# GetNullSafeName((CAESDO.Recruitment.Core.Domain.Profile)Eval("AssociatedProfile")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Title" Visible="false">
                    <ItemTemplate>                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PhD Award Date">
                    <ItemTemplate>
                        <%# (int)Eval("Education.Count") > 0 ? ((DateTime)Eval("Education[0].Date")).ToShortDateString() : string.Empty%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PhD Institution">
                    <ItemTemplate>
                        <%# (int)Eval("Education.Count") > 0 ? Eval("Education[0].Institution") : string.Empty%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PhD Discipline">
                    <ItemTemplate>
                        <%# (int)Eval("Education.Count") > 0 ? Eval("Education[0].Discipline") : string.Empty%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PhD ResearchField">
                    <ItemTemplate>
                        <%# (int)Eval("Education.Count") > 0 ? Eval("Education[0].ResearchField") : string.Empty%>
                    </ItemTemplate>
                </asp:TemplateField>                
                <asp:TemplateField HeaderText="PhD Advisor">
                    <ItemTemplate>
                        <%# (int)Eval("Education.Count") > 0 ? Eval("Education[0].Advisor") : string.Empty%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Current Position">
                    <ItemTemplate>
                        <%# (int)Eval("CurrentPositions.Count") > 0 ? Eval("CurrentPositions[0].Title") + " at " + Eval("CurrentPositions[0].Institution") : string.Empty%>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="Current Employement" Visible="false">
                    <ItemTemplate>
                        <%# (int)Eval("CurrentPositions.Count") > 0 ? GetEmploymentString((CAESDO.Recruitment.Core.Domain.CurrentPosition)Eval("CurrentPositions[0]")) : string.Empty %>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <%# (bool)Eval("Submitted") ? "Submitted" : "Incomplete"  %><br />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Comments">
                    <ItemTemplate>
                        <asp:Literal ID="litMissingSteps" runat="server" EnableViewState="false"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>  
        </asp:GridView>
    </div>
    </form>
</body>
</html>
