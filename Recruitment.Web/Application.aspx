<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="application.aspx.cs" Inherits="Default2" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="height:22px; background-image:url(Images/appmenuTop.gif);"><img src="Images/appmenuTopLeft.gif" alt="" style="float:left;" /><img src="Images/appmenuTopRight.gif" alt="" style="float:right;" /></div>
    <div style="width:203px; float:left; background: url(Images/appmenuLeft.gif) repeat-y; height:500px;">
    <ul class="applicationMenu">
        <li class="unselected"><div class="appButton"><asp:Image ID="X1" runat="server" ImageUrl="~/Images/appmenuX.gif" /> </div><div class="appLink"><asp:LinkButton ID="linkButHome" runat="server" style="margin-left:12px;">Home</asp:LinkButton></div></li>
        <li class="selected"><div class="appButton"><asp:Image ID="X2" runat="server" ImageUrl="~/Images/appmenuCheck.gif" /> </div><div class="appLink"><asp:LinkButton ID="LinkButContact" runat="server" style="margin-left:12px;">Contact Information</asp:LinkButton></div></li>
        <li class="unselected"><div class="appButton"><asp:Image ID="X3" runat="server" ImageUrl="~/Images/appmenuX.gif" /> </div><div class="appLink"><asp:LinkButton ID="LinkButEducation" runat="server" style="margin-left:12px;">Education Information</asp:LinkButton></div></li>
        <li class="unselected"><div class="appButton"><asp:Image ID="X4" runat="server" ImageUrl="~/Images/appmenuX.gif" /> </div><div class="appLink"><asp:LinkButton ID="LinkButReferences" runat="server" style="margin-left:12px;">References</asp:LinkButton></div></li>
        <li class="unselected"><div class="appButton"><asp:Image ID="X5" runat="server" ImageUrl="~/Images/appmenuX.gif" /> </div><div class="appLink"><asp:LinkButton ID="LinkButResume" runat="server" style="margin-left:12px;">Resume</asp:LinkButton></div></li>
        <li class="unselected"><div class="appButton"><asp:Image ID="X6" runat="server" ImageUrl="~/Images/appmenuX.gif" /> </div><div class="appLink"><asp:LinkButton ID="LinkButCover" runat="server" style="margin-left:12px;">Cover Letter</asp:LinkButton></div></li>
        <li class="unselected"><div class="appButton"><asp:Image ID="X7" runat="server" ImageUrl="~/Images/appmenuX.gif" /> </div><div class="appLink"><asp:LinkButton ID="LinkButResearch" runat="server" style="margin-left:12px;">Research Interests</asp:LinkButton></div></li>
        <li class="unselected"><div class="appButton"><asp:Image ID="X8" runat="server" ImageUrl="~/Images/appmenuX.gif" /> </div><div class="appLink"><asp:LinkButton ID="LinkButTranscripts" runat="server" style="margin-left:12px;">Transcripts</asp:LinkButton></div></li>
        <li class="unselected"><div class="appButton"><asp:Image ID="X9" runat="server" ImageUrl="~/Images/appmenuX.gif" /> </div><div class="appLink"><asp:LinkButton ID="LinkButPublications" runat="server" style="margin-left:12px;">Publications</asp:LinkButton></div></li>
        <li class="unselected"><div class="appButton"><asp:Image ID="X10" runat="server" ImageUrl="~/Images/appmenuX.gif" /> </div><div class="appLink"><asp:LinkButton ID="LinkButConfidential" runat="server" style="margin-left:12px;">Confidential Survey</asp:LinkButton></div></li>
    </ul>
    </div>
    <div style="background:url(Images/appmenuRight.gif) repeat-y right; height:500px;">
        <asp:Panel ID="Panel1" runat="server">
         
        </asp:Panel>
    </div>
    
    <div style="clear:both; height:22px; background-image:url(Images/appmenuBot.gif);"><img src="Images/appmenuBotLeft.gif" alt="" style="float:left;" /><img src="Images/appmenuBotRight.gif" alt="" style="float:right;" /></div>
</asp:Content>

