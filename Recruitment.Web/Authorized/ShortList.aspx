<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ShortList.aspx.cs" Inherits="CAESDO.Recruitment.Web.Authorized_ShortList" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<%--//TODO: Add drop down list that contains all positions with applications

//TODO: Add GridView with all applicants for the chosen position.  Make the applicant's name link back to their application
//      by using the ApplicationReview.aspx page (use datakeys to keep the applicationID around).--%>
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" DataSourceID="ObjectDataPositions" DataTextField="TitleAndApplicationCount" DataValueField="ID">
    </asp:DropDownList>
    <asp:ObjectDataSource ID="ObjectDataPositions" runat="server" SelectMethod="GetAll" TypeName="CAESDO.Recruitment.Data.NHibernateDaoFactory+PositionDao"></asp:ObjectDataSource>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataApplications">
        <Columns>
            <asp:CheckBoxField DataField="Submitted" HeaderText="Submitted" SortExpression="Submitted" />
            <asp:CheckBoxField DataField="ReferencesComplete" HeaderText="ReferencesComplete"
                SortExpression="ReferencesComplete" />
            <asp:CheckBoxField DataField="Tracked" HeaderText="Tracked" SortExpression="Tracked" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:CheckBoxField DataField="TrackProperties" HeaderText="TrackProperties" SortExpression="TrackProperties" />
            <asp:BoundField DataField="SubmitDate" HeaderText="SubmitDate" SortExpression="SubmitDate" />
            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" />
            <asp:CheckBoxField DataField="PublicationsComplete" HeaderText="PublicationsComplete"
                SortExpression="PublicationsComplete" />
            <asp:BoundField DataField="LastUpdated" HeaderText="LastUpdated" SortExpression="LastUpdated" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataApplications" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetApplicationsByPosition" TypeName="CAESDO.Recruitment.Data.NHibernateDaoFactory+ApplicationDao">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList1" Name="positionID" PropertyName="SelectedValue"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>

