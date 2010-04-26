<%@ Page Language="C#" AutoEventWireup="true" CodeFile="viewPositions.aspx.cs" Inherits="CAESDO.Recruitment.Web.viewPositions" MasterPageFile="~/MasterPage.master" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ImageButton ID="ibtnCreatePosition" runat="server" ImageUrl="~/Images/ibCreatePosition.gif" /><br />
    <br />

    <asp:GridView ID="gViewPositions" runat="server" GridLines="none" CellPadding="2" CellSpacing="16" DataKeyNames="ID" AutoGenerateColumns="False" DataSourceID="ObjectDataOpenPositions">
        <Columns>
            <asp:TemplateField HeaderText="Position/Department" SortExpression="PositionTitle">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnPositionTitle" runat="server" CommandArgument='<%# Eval("ID") %>' Text='<%# Bind("PositionTitle") %>'></asp:LinkButton>
                    <br />
                    <asp:Label ID="lblDepartmentList" runat="server" Text='<%# Bind("DepartmentList") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Deadline" DataFormatString="{0:d}" HeaderText="Deadline"
                HtmlEncode="False" SortExpression="Deadline" />
            
            <asp:TemplateField HeaderText="Modify" ItemStyle-HorizontalAlign="center">
                <ItemTemplate>
                    <asp:ImageButton ID="ibtnModifyPosition" runat="server" ImageUrl="~/Images/modify.gif" CommandArgument='<%# Eval("ID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Applicants" SortExpression="ApplicationCount" ItemStyle-HorizontalAlign="center">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnApplicationCount" runat="server" CommandArgument='<%# Eval("ID") %>' Text='<%# Eval("ApplicationCount") %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataOpenPositions" runat="server" SelectMethod="GetAllPositionsByStatus"
        TypeName="CAESDO.Recruitment.Data.NHibernateDaoFactory+PositionDao">
        <SelectParameters>
            <asp:Parameter DefaultValue="false" Name="Closed" Type="Boolean" />
            <asp:Parameter DefaultValue="true" Name="AdminAccepted" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>

<br /><br />

</asp:Content>