<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApplicationsList.aspx.cs" Inherits="CAESDO.Recruitment.Web.Authorized_ApplicationsList" Title="Applications List" Theme="MainTheme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:DropDownList ID="dlistPositions" runat="server" AutoPostBack="True" DataSourceID="ObjectDataPositions" DataTextField="TitleAndApplicationCount" DataValueField="ID" OnSelectedIndexChanged="dlistPositions_SelectedIndexChanged" AppendDataBoundItems="true">
        <asp:ListItem Selected="True" Value="0">Select a Position</asp:ListItem>
    </asp:DropDownList>
    
    <asp:RequiredFieldValidator id="reqValPositions" ControlToValidate="dlistPositions" ErrorMessage="*" runat="server"/>
    
    <asp:ObjectDataSource ID="ObjectDataPositions" runat="server" SelectMethod="GetAllPositionsByStatus" TypeName="CAESDO.Recruitment.Data.NHibernateDaoFactory+PositionDao" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="false" Name="Closed" Type="Boolean" />
            <asp:Parameter DefaultValue="true" Name="AdminAccepted" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br /><br />
    <asp:GridView ID="gviewApplications" runat="server" AllowPaging="True" skinID="gridViewShortList" DataKeyNames="id" GridLines="None" CellPadding="0" AutoGenerateColumns="False" DataSourceID="ObjectDataApplications" EmptyDataText="No Applications Found For This Position">
        <Columns>
            <asp:TemplateField HeaderText="Interview">
                <ItemTemplate>
                    <asp:CheckBox ID="chkShortList" runat="server" Checked='<%# Eval("ShortList") %>' />
                </ItemTemplate>
                <ItemStyle CssClass="paddingLeft2" HorizontalAlign="Left" />
                <HeaderStyle HorizontalAlign="Left" CssClass="paddingLeft" Width="15%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Get References">
                <ItemTemplate>
                    <asp:CheckBox ID="chkReferences" runat="server" Checked='<%# Eval("GetReferences") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="No Consideration">
                <ItemTemplate>
                    <asp:CheckBox ID="chkNoConsideration" runat="server" Checked='<%# Eval("NoConsideration") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Applicant Name">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnViewApplication" runat="server" CommandArgument='<%# Eval("id") %>' Text='<%# GetNullSafeFullName((string)Eval("AssociatedProfile.FullName")) %>' OnClick="lbtnViewApplication_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle CssClass="paddingLeft" />
                <HeaderStyle HorizontalAlign="Left" CssClass="paddingLeft" Width="30%" />
            </asp:TemplateField>
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" >
                <HeaderStyle Width="40%" />
            </asp:BoundField>
            <asp:CheckBoxField DataField="Submitted" HeaderText="Submitted" SortExpression="Submitted">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="15%" HorizontalAlign="Center" />
            </asp:CheckBoxField>
        </Columns>    
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
    
    <asp:ObjectDataSource ID="ObjectDataApplications" runat="server" SelectMethod="GetApplicationsByPosition"
        TypeName="CAESDO.Recruitment.Data.NHibernateDaoFactory+ApplicationDao" OnSelecting="ObjectDataApplications_Selecting">
        <SelectParameters>
            <asp:Parameter Name="position" Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    <br /><br />
    <asp:Label ID="lblResult" runat="server" EnableViewState="false"></asp:Label><br /><br />
    
    <asp:Button ID="btnUpdateList" runat="server" Text="Update Applications List" Visible="false" OnClick="btnUpdateList_Click" />
    <asp:Button ID="btnEmailReferences" runat="server" Text="Email References" Visible="false" OnClick="btnEmailReferences_Click" />
    
    <AjaxControlToolkit:ConfirmButtonExtender ID="confirmEmailReferences" runat="server" 
            ConfirmText="You are about to email all references for the Short Listed applicants" 
            TargetControlID="btnEmailReferences">
    </AjaxControlToolkit:ConfirmButtonExtender>
</asp:Content>

