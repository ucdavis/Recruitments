<%@ Page Language="C#" AutoEventWireup="true" Inherits="CAESDO.Recruitment.Web.viewPositions" MasterPageFile="~/MasterPage.master" Theme="MainTheme" Codebehind="viewPositions.aspx.cs" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <ul style="list-style:none; margin:0; padding: 0; float: right;"><li class="viewAppsProgress"><asp:HyperLink ID="hlinkApplicationsInProgress" runat="server" NavigateUrl="~/Applicant/ViewApplicationsInProgress.aspx">View Applications In Progress</asp:HyperLink></li></ul>
    <br />To apply or view description for a position, please click on the position title.
    <div style="clear:both;">
        <br />
    </div>
    <asp:GridView ID="gViewPositions" skinID="gridViewUM" runat="server" GridLines="None" EmptyDataText="No Open Positions Found" CellPadding="0" DataKeyNames="ID" AutoGenerateColumns="False" DataSourceID="ObjectDataOpenPositions" Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="Position/Department" SortExpression="PositionTitle">
                <ItemTemplate>
                    <a href="PositionDetails.aspx?PositionID=<%# Eval("ID") %>&Title=<%# Eval("Slug") %>"><%# Eval("PositionTitle") %></a>
                    <%--<asp:LinkButton ID="lbtnPositionTitle" runat="server" CommandArgument='<%# Eval("ID") %>' Text='<%# Eval("PositionTitle") %>' OnClick="lbtnPositionTitle_Click"></asp:LinkButton>--%>
                    <br />
                    <asp:Label ID="lblDepartmentList" runat="server" Text='<%# Eval("DepartmentList") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="paddingLeft" />
                <HeaderStyle HorizontalAlign="Left" CssClass="paddingLeft" />
            </asp:TemplateField>
            <asp:BoundField DataField="DatePosted" DataFormatString="{0:d}" HeaderText="Posted"
                HtmlEncode="False" SortExpression="DatePosted"  >
                <HeaderStyle HorizontalAlign="Left" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="Deadline" DataFormatString="{0:d}" HeaderText="Review Date"
                HtmlEncode="False" SortExpression="Deadline"  >
                <HeaderStyle HorizontalAlign="Left" Width="100px" />
            </asp:BoundField>
            
<%--            <asp:TemplateField HeaderText="Modify" Visible="false">
                <ItemTemplate>
                    <asp:ImageButton ID="ibtnModifyPosition" runat="server" ImageUrl="~/Images/modify.gif" CommandArgument='<%# Eval("ID") %>' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="100px" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Applicants" SortExpression="ApplicationCount" Visible="false">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnApplicationCount" runat="server" CommandArgument='<%# Eval("ID") %>' Text='<%# Eval("ApplicationCount") %>' OnClick="lbtnApplicationCount_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="100px" />
            </asp:TemplateField>--%>
        </Columns>
    
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataOpenPositions" runat="server" SelectMethod="GetByStatusAndDepartment"
        TypeName="CAESDO.Recruitment.BLL.PositionBLL" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="false" Name="Closed" Type="Boolean" />
            <asp:Parameter DefaultValue="true" Name="AdminAccepted" Type="Boolean" />
            <asp:Parameter DefaultValue="true" Name="AllowApplications" Type="Boolean" />
            <asp:QueryStringParameter QueryStringField="DepartmentFIS" ConvertEmptyStringToNull="true" DbType="String" Name="DepartmentFIS" />
            <asp:QueryStringParameter QueryStringField="SchoolCode" ConvertEmptyStringToNull="true" DbType="String" Name="SchoolCode" />
        </SelectParameters>
    </asp:ObjectDataSource>

<br /><br />

</asp:Content>