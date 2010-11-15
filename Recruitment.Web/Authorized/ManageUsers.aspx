<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ManageUsers.aspx.cs" Inherits="Authorized_ManageUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <h2>User Management</h2>
    
    <div id="loading">
        Loading...
    </div>
    <div style="width: 100%; height: 800px;" align="center">
        <iframe id="frame"  frameborder="0" 
            src='<%= ConfigurationManager.AppSettings["CatbertUserService"] %>' 
            scrolling="auto" name="frame" style="width:100%; height:100%;">
        </iframe> 
    </div>

    <script type="text/javascript">
        $(function() {
            $("#frame").load(function() {
                $("#loading").hide();
            });
        });
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFooter" Runat="Server">

</asp:Content>