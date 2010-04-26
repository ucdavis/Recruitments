<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="App.aspx.cs" Inherits="CAESDO.Recruitment.Web.App" Title="Untitled Page" Trace="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
    <div style="height:22px; background-image:url(../Images/appmenuTop.gif);"><img src="../Images/appmenuTopLeft.gif" alt="" style="float:left;" /><img src="../Images/appmenuTopRight.gif" alt="" style="float:right;" /></div>
    <div style="width:203px; float:left; background: url(../Images/appmenuLeft.gif) repeat-y; height:500px;">
    
    <ul class="applicationMenu">
        <asp:Repeater ID="rptSteps" runat="server">
            <ItemTemplate>
               <asp:Panel ID="pnlStep" runat="server" Visible='<%# Eval("StepVisible") %>'>
                    <li class='<%# Eval("CSSClass") %>'>
                    <div class="appButton">
                        <asp:Image ID="imgStep" runat="server" ImageUrl='<%# Eval("ImgURL") %>' />
                    </div>
                    <div class="appLink">
                        <asp:LinkButton ID="lbtnStep" runat="server" Text='<%# Eval("StepName") %>' CommandArgument='<%# Eval("StepName") %>' Style="margin-left: 12px;" OnClick="lbtnStep_Click"></asp:LinkButton>
                    </div>
                    </li>
               </asp:Panel>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    </div>
    <div style="background:url(../Images/appmenuRight.gif) repeat-y right; height:500px;">
        <asp:Panel ID="pnlMainWindow" runat="server">
        
        <asp:MultiView ID="mviewSteps" runat="server" ActiveViewIndex="0">
        
            <asp:View ID="viewHome" runat="server">
                Home
            </asp:View>
            
            <asp:View ID="viewContactInformation" runat="server">
                Contact Info
            </asp:View>
            
            <asp:View ID="viewEducationInformation" runat="server">
                Education Info
            </asp:View>
        
            <asp:View ID="viewReferences" runat="server">
                References
            </asp:View>
        
            <asp:View ID="viewCurrentPosition" runat="server">
                Current Position
            </asp:View>
            
            <asp:View ID="viewResume" runat="server">
                Resume
            </asp:View>
        
            <asp:View ID="viewCoverLetter" runat="server">
                Cover Letter
            </asp:View>
        
            <asp:View ID="viewResearchInterests" runat="server">
                Research Interests
            </asp:View>
            
            <asp:View ID="viewTranscripts" runat="server">
                Transcripts
            </asp:View>
            
            <asp:View ID="viewConfidentialSurvey" runat="server">
                Confidential Survey
            </asp:View>
            
        </asp:MultiView>
        
        </asp:Panel>
    </div>
    
    <div style="clear:both; height:22px; background-image:url(../Images/appmenuBot.gif);"><img src="../Images/appmenuBotLeft.gif" alt="" style="float:left;" /><img src="../Images/appmenuBotRight.gif" alt="" style="float:right;" /></div>
</asp:Content>

