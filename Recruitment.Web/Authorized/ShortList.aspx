<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ShortList.aspx.cs" Inherits="CAESDO.Recruitment.Web.Authorized_ShortList" Title="Short List" Theme="MainTheme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
    <asp:Label ID="lblResult" runat="server" EnableViewState="false"></asp:Label><br /><br />
    
    <asp:Button ID="btnUpdateShortList" runat="server" Text="Update Short List" Visible="false" OnClick="btnUpdateShortList_Click" />
    <asp:Button ID="btnEmailReferences" runat="server" Text="Email References" Visible="false" OnClick="btnEmailReferences_Click" />
    
    <AjaxControlToolkit:ConfirmButtonExtender ID="confirmEmailReferences" runat="server" 
            ConfirmText="You are about to email all references for the Short Listed applicants" 
            TargetControlID="btnEmailReferences">
    </AjaxControlToolkit:ConfirmButtonExtender>
</asp:Content>

