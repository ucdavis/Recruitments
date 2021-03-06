<%@ Page Language="C#" AutoEventWireup="true" Inherits="CAESDO.Recruitment.Web.ViewPositionsPending" MasterPageFile="~/MasterPage.master" Theme="MainTheme" Codebehind="ViewPositionsPending.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ImageButton ID="ibtnCreatePosition" runat="server" ImageUrl="~/Images/ibCreatePosition.gif" PostBackUrl="~/Authorized/PositionManagement.aspx" /><br />
    <br />

    <asp:GridView ID="gViewPositions" skinID="gridViewUM" runat="server" GridLines="None" EmptyDataText="No Pending Positions Found" CellPadding="0" DataKeyNames="ID" AutoGenerateColumns="False" DataSourceID="ObjectDataOpenPositions" Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="Position/Department" SortExpression="PositionTitle">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnPositionTitle" runat="server" CommandArgument='<%# Eval("ID") %>' Text='<%# Bind("PositionTitle") %>' OnClick="lbtnPositionTitle_Click"></asp:LinkButton>
                    <br />
                    <asp:Label ID="lblDepartmentList" runat="server" Text='<%# Bind("DepartmentList") %>'></asp:Label>
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
            
            <asp:TemplateField HeaderText="Modify" Visible="False">
                <ItemTemplate>
                    <asp:ImageButton ID="ibtnModifyPosition" runat="server" ImageUrl="~/Images/modify.gif" CommandArgument='<%# Eval("ID") %>' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="100px" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Accept">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnAccept" runat="server" Text="Accept Position" CommandArgument='<%# Eval("ID") %>' OnClick="lbtnAccept_Click"></asp:LinkButton>
                    <AjaxControlToolkit:ConfirmButtonExtender ID="confirmAccept" runat="server" ConfirmText='<%# "You are about to accept the " + Eval("PositionTitle") + "." %>' TargetControlID="lbtnAccept"></AjaxControlToolkit:ConfirmButtonExtender>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="100px" />
            </asp:TemplateField>
        </Columns>
    
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataOpenPositions" runat="server" SelectMethod="GetByStatus"
        TypeName="CAESDO.Recruitment.BLL.PositionBLL" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="false" Name="Closed" Type="Boolean" />
            <asp:Parameter DefaultValue="false" Name="AdminAccepted" Type="Boolean" />
            <asp:Parameter Name="AllowApplications" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>

<br /><br />

</asp:Content>