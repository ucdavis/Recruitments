<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmailTemplates.aspx.cs" Inherits="CAESDO.Recruitment.Web.Authorized_EmailTemplates" Title="Send Templated Emails" Theme="MainTheme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <script type="text/javascript">

        $(document).ready(function() {
            //Sort table
            $("#tblApplications").tablesorter(
            {
                sortList: [[3, 1], [1, 0]],
                cssAsc: 'headerSortUp',
                cssDesc: 'headerSortDown',
                cssHeader: 'header',
                headers: { 0: { sorter: false} },
                widgets: ['zebra']
            });
        });
        
    </script>

    <asp:DropDownList ID="dlistApplicants" runat="server" AutoPostBack="True" DataSourceID="ObjectDataPositions" DataTextField="TitleAndApplicationCount" DataTextFormatString="{0}" DataValueField="ID" AppendDataBoundItems="True">
        <asp:ListItem Selected="True" Value="0">Select a Position</asp:ListItem>
    </asp:DropDownList>
    <asp:ObjectDataSource ID="ObjectDataPositions" runat="server"
        SelectMethod="GetByStatus" TypeName="CAESDO.Recruitment.BLL.PositionBLL" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="false" Name="Closed" Type="Boolean" />
            <asp:Parameter DefaultValue="true" Name="AdminAccepted" Type="Boolean" />
            <asp:Parameter Name="AllowApplications" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    <br /><br />
    
    <asp:ListView ID="lviewApplications" runat="server" 
        DataSourceID="ObjectDataApplications" DataKeyNames="id" 
        ondatabound="lviewApplications_DataBound">
        <LayoutTemplate>
        <table id="tblApplications" class="tablesorter tablesearch">
            <thead>
                <tr>
                    <th>
                        Email?
                    </th>
                    <th>
                        Name
                    </th>
                    <th>
                        Email
                    </th>
                    <th>
                        Submitted
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr id="itemPlaceholder" runat="server"></tr>
            </tbody>
        </table>
        </LayoutTemplate>
        <ItemTemplate>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkEmailApplicant" runat="server" />
                    </td>
                    <td>
                        <%# GetNullSafeFullName((string)Eval("AssociatedProfile.FullName")) %>
                    </td>
                    <td>
                        <%# Eval("Email") %>
                    </td>
                    <td>
                        <%# Eval("Submitted") %>
                    </td>
                </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            No Applications Found For This Position
        </EmptyDataTemplate>
    </asp:ListView>
    
    <%--<asp:GridView ID="gViewApplications" SkinID="gridViewUserManagement" runat="server" 
            DataKeyNames="id" AutoGenerateColumns="False" DataSourceID="ObjectDataApplications" BorderStyle="None" CellPadding="0" GridLines="None">
        <Columns>
            <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <asp:CheckBox ID="chkEmailApplicant" runat="server" />
                </ItemTemplate>
                <ItemStyle CssClass="paddingLeft" />
                <HeaderStyle CssClass="paddingLeft" Width="75px" />
            </asp:TemplateField>
        
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <%# GetNullSafeFullName((string)Eval("AssociatedProfile.FullName")) %>
                </ItemTemplate>
                <HeaderStyle Width="450px" />
            </asp:TemplateField>
            
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="Submitted" HeaderText="Submitted" SortExpression="Submitted" />
            
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    
    </asp:GridView>--%>
    <asp:ObjectDataSource ID="ObjectDataApplications" runat="server" SelectMethod="GetByPositionID"
        TypeName="CAESDO.Recruitment.BLL.ApplicationBLL" OldValuesParameterFormatString="original_{0}">
         <SelectParameters>
            <asp:ControlParameter ControlID="dlistApplicants" Name="positionID" PropertyName="SelectedValue"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    <br />
    <asp:Panel ID="pnlApplicationsExist" runat="server" Visible="false">
    <asp:Button ID="btnSendEmail" runat="server" Text="Send Reminder Emails" OnClick="btnSendEmail_Click" /><br />
    <asp:Button ID="btnSendTemplate" runat="server" Text="Send Template Emails" /><br />
    <asp:Label ID="lblSentEmail" runat="server" ForeColor="green" EnableViewState="false"></asp:Label>
        <AjaxControlToolkit:AnimationExtender ID="animationApplicationStatus" runat="server" TargetControlID="lblSentEmail">
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
    <hr />
    <br />
    
    Template Type: 
    <asp:DropDownList ID="dlistEmailTemplates" runat="server" 
            DataSourceID="odsTemplateTypes" DataTextField="Type" DataValueField="ID">
    </asp:DropDownList>
    
    <asp:ObjectDataSource ID="odsTemplateTypes" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetEmailTemplates" 
        TypeName="CAESDO.Recruitment.BLL.TemplateTypeBLL"></asp:ObjectDataSource>
    
    <div style="width:818px; height:389px; background:url(../Images/envelope.jpg) no-repeat; padding:50px;">
    
    <script src="../JS/tiny_mce/tiny_mce.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        var refTemplateEditor = null;

        tinyMCE.init({
            mode: "specific_textareas",
            editor_selector: "richTextEditor", //Just use textareas with the richTextEditor class applied
            theme: "advanced",
            skin: "o2k7",
            plugins: "paste",

            theme_advanced_buttons2: "cut,copy,pastetext,pasteword,|,bullist,numlist,|,undo,redo,|,link,unlink,anchor,image,cleanup,|,forecolor,backcolor",
            theme_advanced_toolbar_location: "top",

            setup: function(ed) {
                refTemplateEditor = ed;
            }
        });

        function InsertTemplateText(text) {
            refTemplateEditor.focus();
            refTemplateEditor.selection.setContent(text);
        }

        //////// Letter Templates
/*
        function TemplateSelected(ddl) {
            // get the editor object
            var mce = tinyMCE.get("LetterTemplate");

            if ($(ddl).val() != "-1") {
                // show the update state               
                mce.setProgressState(1);

                // get the template text
                ScriptService.GetTemplate($(ddl).val(), TemplateSelectedOnComplete, null, mce);
            }
            else {
                // clear the text
                mce.setContent("");
            }
        }

        function TemplateSelectedOnComplete(result, context) {
            context.setProgressState(0);
            context.setContent(result);
        }
*/
    </script>
    
    <div class="blueletter">
       <asp:TextBox ID="txtEmailTemplate" runat="server" CssClass="richTextEditor" TextMode="MultiLine" Width="100%"></asp:TextBox>
       <br />
    </div>
    </div>
    <br />
    <div style="width:818px; height:389px; background:url(../Images/envelope.jpg) no-repeat; padding:50px;">
    <div class="blueletter">
       <asp:Literal ID="litEmailBody" runat="server"></asp:Literal><br />
    </div>
    </div>
    </asp:Panel>

</asp:Content>

