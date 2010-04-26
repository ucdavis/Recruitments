<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmailTemplates.aspx.cs" Inherits="CAESDO.Recruitment.Web.Authorized_EmailTemplates" Title="Send Templated Emails" Theme="MainTheme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:DropDownList ID="dlistApplicants" runat="server" AutoPostBack="True" DataSourceID="ObjectDataPositions" DataTextField="TitleAndApplicationCount" DataTextFormatString="{0}" DataValueField="ID" AppendDataBoundItems="True">
        <asp:ListItem Selected="True" Value="0">Select a Position</asp:ListItem>
    </asp:DropDownList>
    <asp:ObjectDataSource ID="ObjectDataPositions" runat="server"
        SelectMethod="GetAllPositionsByStatus" TypeName="CAESDO.Recruitment.Data.NHibernateDaoFactory+PositionDao" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="false" Name="Closed" Type="Boolean" />
            <asp:Parameter DefaultValue="true" Name="AdminAccepted" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    <br /><br />
    
    <asp:GridView ID="gViewApplications" SkinID="gridViewUserManagement" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" DataSourceID="ObjectDataApplications" BorderStyle="None" CellPadding="0" GridLines="None">
        <Columns>
            <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <asp:CheckBox ID="chkEmailApplicant" runat="server" />
                </ItemTemplate>
                <ItemStyle CssClass="paddingLeft" />
                <HeaderStyle CssClass="paddingLeft" Width="75px" />
            </asp:TemplateField>
        
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <%# Eval("AssociatedProfile.FullName") %>
                </ItemTemplate>
                <HeaderStyle Width="450px" />
            </asp:TemplateField>
            
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="Submitted" HeaderText="Submitted" SortExpression="Submitted" />
            
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataApplications" runat="server" SelectMethod="GetApplicationsByPosition"
        TypeName="CAESDO.Recruitment.Data.NHibernateDaoFactory+ApplicationDao">
        <SelectParameters>
            <asp:ControlParameter ControlID="dlistApplicants" Name="positionID" PropertyName="SelectedValue"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    <br />
    <asp:Button ID="btnSendEmail" runat="server" Text="Send Reminder Emails" /><br />
    <br />
<hr />
    <br />
    <br />
    <div style="width:818px; height:389px; background:url(../Images/envelope.jpg) no-repeat; padding:50px;">
    <div class="blueletter">
       <asp:Literal ID="litEmailBody" runat="server"></asp:Literal><br />
    </div>
    </div>
</asp:Content>

