<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="CAESDO.Recruitment.Web.Authorized_UnsolicitedReferences" Title="Unsolicited References" EnableEventValidation="false" Theme="MainTheme" Codebehind="UnsolicitedReferences.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

Position: <asp:DropDownList ID="dlistPositions" runat="server"></asp:DropDownList> <br /><br />

Application: <asp:DropDownList ID="dlistApplications" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dlistApplications_SelectedIndexChanged"></asp:DropDownList>
<asp:RequiredFieldValidator id="reqValApplications" ControlToValidate="dlistApplications" ErrorMessage="* Select An Application" runat="server"/> <br /><br />

<AjaxControlToolkit:CascadingDropDown ID="cascadePositions" runat="server" TargetControlID="dlistPositions" 
                                        Category="Positions" PromptText="Select a Position" ServicePath="RecruitmentService.asmx" 
                                        ServiceMethod="GetPositions">
</AjaxControlToolkit:CascadingDropDown>
        
<AjaxControlToolkit:CascadingDropDown ID="cascadeApplications" runat="server" TargetControlID="dlistApplications" 
                                        Category="Applications" PromptText="Select an Application" ServicePath="RecruitmentService.asmx" 
                                        ParentControlID="dlistPositions" ServiceMethod="GetApplications">
</AjaxControlToolkit:CascadingDropDown>


<asp:GridView ID="gViewReferences" runat="server" SkinID="gridViewUserManagement" GridLines="None" CellPadding="0" DataKeyNames="id" AutoGenerateColumns="false"
            EmptyDataText="No References Found" Visible="false">
    <Columns>
        <asp:TemplateField HeaderText="Send Letter">
            <ItemTemplate>
                <asp:Button ID="sendEmail" runat="server" Text="Send Letter" CommandArgument='<%# Eval("id") %>' OnClick="sendEmail_Click" />
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" CssClass="paddingLeft" Width="90px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Unsolicited">
            <ItemTemplate>
                <asp:CheckBox ID="chkUnsolicited" runat="server" Checked='<%# Eval("UnsolicitedReference") %>' />
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:BoundField HeaderText="Reference Name" DataField="FullName"><HeaderStyle HorizontalAlign="Left" /></asp:BoundField>
        <asp:BoundField HeaderText="Email" DataField="Email"><HeaderStyle HorizontalAlign="Left" /></asp:BoundField>
        <asp:TemplateField HeaderText="Notified">
            <ItemTemplate>
                <%# Eval("EmailDate") == null ? "Not Notified" : ((DateTime)Eval("EmailDate")).ToShortDateString() %>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="File Received">
            <ItemTemplate>
                <%# Eval("ReferenceFile") == null ? false : true %>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderStyle HorizontalAlign="Center" />
        </asp:TemplateField>
    </Columns>    
</asp:GridView>

<br />
    <asp:Label ID="lblResult" runat="server" EnableViewState="false"></asp:Label>
        <AjaxControlToolkit:AnimationExtender ID="animationApplicationStatus" runat="server" TargetControlID="lblResult">
            <Animations>
                <OnLoad>
                    <Sequence>
                        <Color Duration="2"
                        StartValue="#ffff99"
                        EndValue="#FFFFFF"
                        Property="style"
                        PropertyKey="backgroundColor" />
                        <StyleAction Attribute="backgroundColor" value="" />
                    </Sequence>
                </OnLoad>
            </Animations>                            
        </AjaxControlToolkit:AnimationExtender>
    
    <br />
    
    <asp:Button ID="btnUpdateList" runat="server" Text="Update Unsolicited List" Visible="false" OnClick="btnUpdateList_Click" />
      <br />
      <br />
<hr />
    <br />
    <br />
    <%--<div style="width:818px; height:389px; background:url(../Images/envelope.jpg) no-repeat; padding:50px;">--%>
    <div class="blueletter">
       <asp:Literal ID="litEmailBody" runat="server"></asp:Literal><br />
    </div>
    <%--</div>--%>
<br /><br /><br />
</asp:Content>

