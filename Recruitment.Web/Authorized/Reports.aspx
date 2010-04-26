<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="CAESDO.Recruitment.Web.Authorized_Reports" Title="Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    Report Type: <asp:DropDownList ID="dlistType" runat="server" AutoPostBack="True">
        <asp:ListItem Selected="True" Value="Interim">Interim Recruitment</asp:ListItem>
        <asp:ListItem Value="Survey">Survey Results</asp:ListItem>
    </asp:DropDownList>
    
    <br /><br />
    
    Position: <asp:DropDownList ID="dlistPositions" runat="server" DataSourceID="ObjectDataPositions" DataTextField="TitleAndApplicationCount" DataValueField="ID" AppendDataBoundItems="true">
        <asp:ListItem Selected="True" Value="0">Select a Position</asp:ListItem>
    </asp:DropDownList>
    
    <asp:RequiredFieldValidator id="reqValPositions" ControlToValidate="dlistPositions" InitialValue="0" ErrorMessage="* You Must Select A Position" runat="server"/>
    
    <asp:ObjectDataSource ID="ObjectDataPositions" runat="server" SelectMethod="GetAllPositionsByStatus" TypeName="CAESDO.Recruitment.Data.NHibernateDaoFactory+PositionDao" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="false" Name="Closed" Type="Boolean" />
            <asp:Parameter DefaultValue="true" Name="AdminAccepted" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    <br /><br />
    <asp:CheckBox ID="chkOutputFile" runat="server" Checked="true" Text="Output to Word: " TextAlign="Left" />
    <br /><br />
    <asp:Button ID="btnGenerateReport" runat="server" Text="Generate Report!" OnClick="btnGenerateReport_Click" />
    <br /><asp:Label ID="lblReportStatus" runat="server" EnableViewState="false" ForeColor="red"></asp:Label>
</asp:Content>