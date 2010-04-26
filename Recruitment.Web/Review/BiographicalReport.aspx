<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BiographicalReport.aspx.cs" Inherits="CAESDO.Recruitment.Web.Review_BiographicalReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Biographical Data Spreadsheet</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gViewBiographicalData" runat="server" AutoGenerateColumns="false" DataKeyNames="id" OnRowDataBound="gViewBiographicalData_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <%# Eval("AssociatedProfile.LastName")%>, <%# Eval("AssociatedProfile.FirstName")%> <%# string.IsNullOrEmpty((string)Eval("AssociatedProfile.MiddleName")) ? string.Empty : Eval("AssociatedProfile.MiddleName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Title" Visible="false">
                    <ItemTemplate>
                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ph.D. Info">
                    <ItemTemplate>
                        <%# (int)Eval("Education.Count") > 0 ? GetPHDAwardedString((DateTime)Eval("Education[0].Date")) : string.Empty%> <br />
                        <%# (int)Eval("Education.Count") > 0 ? Eval("Education[0].Institution") : string.Empty%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Discipline">
                    <ItemTemplate>
                        <%# (int)Eval("Education.Count") > 0 ? Eval("Education[0].Discipline") : string.Empty%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Current Employement">
                    <ItemTemplate>
                        <%# (int)Eval("CurrentPositions.Count") > 0 ? GetEmploymentString((CAESDO.Recruitment.Core.Domain.CurrentPosition)Eval("CurrentPositions[0]")) : string.Empty %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Comments">
                    <ItemTemplate>
                        <%# (bool)Eval("Submitted") ? "Submitted" : "Incomplete"  %><br />
                        <asp:Literal ID="litMissingSteps" runat="server" EnableViewState="false"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>  
        </asp:GridView>
    </div>
    </form>
</body>
</html>
