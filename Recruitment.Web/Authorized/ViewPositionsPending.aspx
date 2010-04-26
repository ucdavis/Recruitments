<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewPositionsPending.aspx.cs" Inherits="CAESDO.Recruitment.Web.ViewPositionsPending" MasterPageFile="~/MasterPage.master" Theme="MainTheme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ImageButton ID="ibtnCreatePosition" runat="server" ImageUrl="~/Images/ibCreatePosition.gif" PostBackUrl="~/Authorized/addPosition.aspx" /><br />
    <br />

    <asp:GridView ID="gViewPositions" skinID="gridViewUM" runat="server" GridLines="None" CellPadding="0" DataKeyNames="ID" AutoGenerateColumns="False" DataSourceID="ObjectDataOpenPositions" Width="100%">
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
            <asp:BoundField DataField="Deadline" DataFormatString="{0:d}" HeaderText="Deadline"
                HtmlEncode="False" SortExpression="Deadline"  >
                <HeaderStyle HorizontalAlign="Left" Width="100px" />
            </asp:BoundField>
            
            <asp:TemplateField HeaderText="Modify">
                <ItemTemplate>
                    <asp:ImageButton ID="ibtnModifyPosition" runat="server" ImageUrl="~/Images/modify.gif" CommandArgument='<%# Eval("ID") %>' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="100px" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Accept">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnAccept" runat="server" Text="Accept Position" CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="100px" />
            </asp:TemplateField>
        </Columns>
    
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataOpenPositions" runat="server" SelectMethod="GetAllPositionsByStatus"
        TypeName="CAESDO.Recruitment.Data.NHibernateDaoFactory+PositionDao">
        <SelectParameters>
            <asp:Parameter DefaultValue="false" Name="Closed" Type="Boolean" />
            <asp:Parameter DefaultValue="false" Name="AdminAccepted" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>

<br /><br />

</asp:Content>