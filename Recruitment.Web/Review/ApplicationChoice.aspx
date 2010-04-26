<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApplicationChoice.aspx.cs" Inherits="CAESDO.Recruitment.Web.Review_ApplicationChoice" Title="Application Choice" Theme="MainTheme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
<asp:GridView ID="gViewPositions" skinID="gridViewUM" runat="server" GridLines="None" EmptyDataText="No Positions Found" CellPadding="0" DataKeyNames="ID" AutoGenerateColumns="False" DataSourceID="ObjectDataOpenPositions" Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="Position/Department" SortExpression="PositionTitle">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnPositionTitle" runat="server" CommandArgument='<%# Eval("ID") %>' Text='<%# Bind("PositionTitle") %>' OnClick="lbtnPositionTitle_Click"></asp:LinkButton>
                    <br />
                    <asp:Label ID="lblDepartmentList" runat="server" Text='<%# Eval("DepartmentList") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="paddingLeft" />
                <HeaderStyle HorizontalAlign="Left" CssClass="paddingLeft" />
            </asp:TemplateField>
            
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
            
            <asp:TemplateField HeaderText="Applicants" SortExpression="ApplicationCount">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnApplicationCount" runat="server" CommandArgument='<%# Eval("ID") %>' Text='<%# Eval("ApplicationCount") %>' OnClick="lbtnApplicationCount_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="100px" />
            </asp:TemplateField>
        </Columns>
    
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataOpenPositions" runat="server" SelectMethod="GetAllPositionsByStatusForCommittee"
        TypeName="CAESDO.Recruitment.BLL.PositionBLL" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="false" Name="Closed" Type="Boolean" />
            <asp:Parameter DefaultValue="true" Name="AdminAccepted" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>

