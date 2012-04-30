<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="CAESDO.Recruitment.Web.Applicant_ViewApplicationsInProgress" Title="In Progress Applications" Theme="MainTheme" Codebehind="ViewApplicationsInProgress.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

Viewing Applications Currently In Progress 
    
    <br /><br />

    <asp:GridView ID="gviewApplications" runat="server" AllowPaging="True" skinID="gridViewUM" GridLines="None" CellPadding="0" AutoGenerateColumns="False" DataSourceID="ObjectDataApplications" EmptyDataText="No In Progress Applications Found">
        <Columns>
            <asp:TemplateField HeaderText="Applied Position">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnViewApplication" runat="server" Text='<%# Eval("AppliedPosition.PositionTitle") %>' CommandArgument='<%# Eval("id") %>' OnClick="lbtnViewApplication_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle CssClass="paddingLeft" />
                <HeaderStyle HorizontalAlign="Left" CssClass="paddingLeft" />
            </asp:TemplateField>
            <asp:BoundField DataField="LastUpdated" HtmlEncode="False" DataFormatString="{0:d}" HeaderText="Last Updated" SortExpression="LastUpdated" >
                <HeaderStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" >
                <HeaderStyle Width="100px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Files Uploaded">
                <ItemTemplate>
                    <asp:Label ID="lblNumFiles" runat="server" Text='<%# Eval("Files.Count") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="100px" />
            </asp:TemplateField>
        </Columns>
    
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataApplications" runat="server" SelectMethod="GetByApplicant"
        TypeName="CAESDO.Recruitment.BLL.ApplicationBLL" OnSelecting="ObjectDataApplications_Selecting" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Name="applicantProfile" Type="Object" />
            <asp:Parameter Name="submitted" Type="Boolean" DefaultValue="false" />
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>

