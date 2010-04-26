<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmailTemplates.aspx.cs" Inherits="CAESDO.Recruitment.Web.Authorized_EmailTemplates" Title="Send Templated Emails" Theme="MainTheme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <script type="text/javascript">

        $(document).ready(function() {
            //Sort table
            $("#tblApplications").tablesorter(
            {
                sortList: [[3, 1], [1, 0]],
                cssAsc: 'headerSortUp',
                cssDesc: 'headerSortDown',
                cssHeader: 'header',
                headers: { 0: { sorter: false} },
                widgets: ['zebra']
            });
        });
        
    </script>

    <asp:DropDownList ID="dlistApplicants" runat="server" AutoPostBack="True" DataSourceID="ObjectDataPositions" DataTextField="TitleAndApplicationCount" DataTextFormatString="{0}" DataValueField="ID" AppendDataBoundItems="True">
        <asp:ListItem Selected="True" Value="0">Select a Position</asp:ListItem>
    </asp:DropDownList>
    <asp:ObjectDataSource ID="ObjectDataPositions" runat="server"
        SelectMethod="GetByStatus" TypeName="CAESDO.Recruitment.BLL.PositionBLL" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="false" Name="Closed" Type="Boolean" />
            <asp:Parameter DefaultValue="true" Name="AdminAccepted" Type="Boolean" />
            <asp:Parameter Name="AllowApplications" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    <br /><br />
    
    <asp:ListView ID="lviewApplications" runat="server" DataSourceID="ObjectDataApplications" DataKeyNames="id">
        <LayoutTemplate>
        <table id="tblApplications" class="tablesorter">
            <thead>
                <tr>
                    <th>
                        Email?
                    </th>
                    <th>
                        Name
                    </th>
                    <th>
                        Email
                    </th>
                    <th>
                        Submitted
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr id="itemPlaceholder" runat="server"></tr>
            </tbody>
        </table>
        </LayoutTemplate>
        <ItemTemplate>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkEmailApplicant" runat="server" />
                    </td>
                    <td>
                        <%# GetNullSafeFullName((string)Eval("AssociatedProfile.FullName")) %>
                    </td>
                    <td>
                        <%# Eval("Email") %>
                    </td>
                    <td>
                        <%# Eval("Submitted") %>
                    </td>
                </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            No Applications Found For This Position
        </EmptyDataTemplate>
    </asp:ListView>
    
    <%--<asp:GridView ID="gViewApplications" SkinID="gridViewUserManagement" runat="server" 
            DataKeyNames="id" AutoGenerateColumns="False" DataSourceID="ObjectDataApplications" BorderStyle="None" CellPadding="0" GridLines="None">
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
                    <%# GetNullSafeFullName((string)Eval("AssociatedProfile.FullName")) %>
                </ItemTemplate>
                <HeaderStyle Width="450px" />
            </asp:TemplateField>
            
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="Submitted" HeaderText="Submitted" SortExpression="Submitted" />
            
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    
    </asp:GridView>--%>
    <asp:ObjectDataSource ID="ObjectDataApplications" runat="server" SelectMethod="GetByPositionID"
        TypeName="CAESDO.Recruitment.BLL.ApplicationBLL" OldValuesParameterFormatString="original_{0}">
         <SelectParameters>
            <asp:ControlParameter ControlID="dlistApplicants" Name="positionID" PropertyName="SelectedValue"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    <br />
    <asp:Button ID="btnSendEmail" runat="server" Text="Send Reminder Emails" OnClick="btnSendEmail_Click" /><br />
    <asp:Label ID="lblSentEmail" runat="server" ForeColor="green" EnableViewState="false"></asp:Label>
        <AjaxControlToolkit:AnimationExtender ID="animationApplicationStatus" runat="server" TargetControlID="lblSentEmail">
            <Animations>
                <OnLoad>
                    <Sequence>
                        <Color Duration="2"
                        StartValue="#ffff99"
                        EndValue="#FFFFFF"
                        Property="style"
                        PropertyKey="backgroundColor" />
                        <StyleAction Attribute="backgroundColor" value="" />
                    </Sequence>
                </OnLoad>
            </Animations>                            
        </AjaxControlToolkit:AnimationExtender>
                            
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

