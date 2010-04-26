<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UnsolicitedReferences.aspx.cs" Inherits="CAESDO.Recruitment.Web.Authorized_UnsolicitedReferences" Title="Unsolicited References" EnableEventValidation="false" Theme="MainTheme" %>
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


<asp:GridView ID="gViewReferences" runat="server" SkinID="gridViewUserManagement" DataKeyNames="id" AutoGenerateColumns="false"
            EmptyDataText="No References Found" Visible="false">
    <Columns>
        <asp:TemplateField HeaderText="Unsolicited">
            <ItemTemplate>
                <asp:CheckBox ID="chkUnsolicited" runat="server" Checked='<%# Eval("UnsolicitedReference") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="Reference Name" DataField="FullName" />
        <asp:BoundField HeaderText="Email" DataField="Email" />
        <asp:TemplateField HeaderText="Notified">
            <ItemTemplate>
                <%# Eval("EmailDate") == null ? "Not Notified" : ((DateTime)Eval("EmailDate")).ToShortDateString() %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="File Received">
            <ItemTemplate>
                <%# Eval("ReferenceFile") == null ? false : true %>
            </ItemTemplate>
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

</asp:Content>

