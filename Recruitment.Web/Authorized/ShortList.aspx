<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ShortList.aspx.cs" Inherits="CAESDO.Recruitment.Web.Authorized_ShortList" Title="Short List" Theme="MainTheme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<%--//TODO: Add drop down list that contains all positions with applications

//TODO: Add GridView with all applicants for the chosen position.  Make the applicant's name link back to their application
//      by using the ApplicationReview.aspx page (use datakeys to keep the applicationID around).--%>
    <asp:DropDownList ID="dlistPositions" runat="server" AutoPostBack="True" DataSourceID="ObjectDataPositions" DataTextField="TitleAndApplicationCount" DataValueField="ID" OnSelectedIndexChanged="dlistPositions_SelectedIndexChanged" AppendDataBoundItems="true">
        <asp:ListItem Selected="True" Value="0">Select a Position</asp:ListItem>
    </asp:DropDownList>
    
    <asp:RequiredFieldValidator id="reqValPositions" ControlToValidate="dlistPositions" ErrorMessage="*" runat="server"/>
    
    <asp:ObjectDataSource ID="ObjectDataPositions" runat="server" SelectMethod="GetAll" TypeName="CAESDO.Recruitment.Data.NHibernateDaoFactory+PositionDao"></asp:ObjectDataSource>
    <br /><br />
    <asp:GridView ID="gviewApplications" runat="server" AllowPaging="true" skinID="gridViewUM" DataKeyNames="id" GridLines="None" CellPadding="0" AutoGenerateColumns="False" DataSourceID="ObjectDataApplications" EmptyDataText="No Applications Found For This Position">
        <Columns>
            <asp:TemplateField HeaderText="Short List">
                <ItemTemplate>
                    <asp:CheckBox ID="chkShortList" runat="server" Checked='<%# Eval("ShortList") %>' />
                </ItemTemplate>
                <ItemStyle CssClass="paddingLeft" />
                <HeaderStyle HorizontalAlign="Left" CssClass="paddingLeft" />
            </asp:TemplateField>
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

    <br /><br />
    <asp:Button ID="btnUpdateShortList" runat="server" Text="Update Short List" Visible="false" OnClick="btnUpdateShortList_Click" />

</asp:Content>

