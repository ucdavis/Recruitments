<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="viewApplications.aspx.cs" Inherits="CAESDO.Recruitment.Web.Authorized_viewApplications" Title="View Applications" Theme="MainTheme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    Viewing Applicants for the <asp:Literal ID="litPositionTitle" runat="server" /> position.
    
    <br /><br />

    <asp:GridView ID="gviewApplications" runat="server" AllowPaging="true" skinID="gridViewUM" GridLines="None" CellPadding="0" AutoGenerateColumns="False" DataSourceID="ObjectDataApplications" EmptyDataText="No Applications Found For This Position">
        <Columns>
            <asp:TemplateField HeaderText="Applicant Name">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnViewApplication" runat="server" CommandArgument='<%# Eval("id") %>' Text='<%# GetNullSafeFullName((string)Eval("AssociatedProfile.FullName")) %>' OnClick="lbtnViewApplication_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle CssClass="paddingLeft" />
                <HeaderStyle HorizontalAlign="Left" CssClass="paddingLeft" />
            </asp:TemplateField>
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" >
                <HeaderStyle Width="100px" />
            </asp:BoundField>
            <asp:CheckBoxField DataField="Submitted" HeaderText="Submitted" SortExpression="Submitted">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="100px" />
            </asp:CheckBoxField>
        </Columns>
    
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataApplications" runat="server" SelectMethod="GetApplicationsByPosition"
        TypeName="CAESDO.Recruitment.Data.NHibernateDaoFactory+ApplicationDao" OnSelecting="ObjectDataApplications_Selecting">
        <SelectParameters>
            <asp:Parameter Name="position" Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>

