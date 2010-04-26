<%@ Page Language="C#" AutoEventWireup="true" CodeFile="viewPositions.aspx.cs" Inherits="viewPositions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Recruitments</title>
     <link rel="stylesheet" href="fracstyle.css" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
<div id="wrapper">
    <div id="header" style="height:62px;">
        <img src="Images/logo.gif" alt="Recruitments" style="float:left" /><img src="Images/logo_ucdavis.gif" alt="UC Davis, College of Agricultural and Environmental Sciences" style="float:right" />
    </div>
    <div id="content">
        <div id="breadcrumbs">
            <table cellpadding="5" cellspacing="0" style="width:100%;">
                <tr>
                    <td>Home</td>
                    <td style="width:250px; text-align: right;">|&nbsp;&nbsp;&nbsp; Welcome Iglasias, Enrique! <img src="Images/downarrow.gif" alt="down arrow" /></td>
                </tr>
            </table>
        </div>
        <div id="bodytext">
            <asp:ImageButton ID="ibCreatePosition" runat="server" ImageUrl="~/Images/ibCreatePosition.gif" /><br />
            <br />
            
            <!--Can replace this with gridview or something else-->
            <table cellpadding="0" cellspacing="0" style="width:100%;" class="dottedBotBorder">
                <tr style="height: 40px;" class="headingBlue">
                    <td>Position/Department:</td>
                    <td style="width: 100px; text-align: center;">Modify:</td>
                    <td style="width: 100px; text-align: center;">Delete:</td>
                    <td style="width: 100px; text-align: center;">Applicants:</td>
                </tr>
                <tr style="background-color:#eeeeee; height: 60px;">
                    <td style="padding-left: 10px;">
                        <a href="#">Assistant Professor and Assistant Quantitative Geneticist</a><br />
                        Agricultural Experiment Station Department of Plant Sciences
                    </td>
                    <td align="center"><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/modify.gif" /></td>
                    <td align="center"><asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/delete.gif" /></td>
                    <td align="center"><a href="#">15</a></td>
                </tr>
                <tr style="height: 60px;">
                    <td style="padding-left: 10px;">
                        <a href="#">Assistant Professor of Metabolomics (AP #06-01)</a><br />
                         Nutrition, Food Science and Technology
                    </td>
                    <td align="center"><asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Images/modify.gif" /></td>
                    <td align="center" ><asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/Images/delete.gif" /></td>
                    <td align="center"><a href="#">15</a></td>
                </tr>
                <tr style="background-color:#eeeeee; height: 60px;">
                     <td style="padding-left: 10px;">
                        <a href="#">Assistant Professor of Human Development (AP #06-20)</a><br />
                        Center for Mind and Brain
                    </td>
                    <td align="center"><asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/modify.gif" /></td>
                    <td align="center"><asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/delete.gif" /></td>
                    <td align="center"><a href="#">15</a></td>
                </tr>
                <tr style="height: 60px;">
                     <td style="padding-left: 10px;">
                        <a href="#">Assistant Professor of American Studies and of Food Science and Technology</a><br />
                        American Studies Program, Department of Food Science and Technology
                    </td>
                    <td align="center"><asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/Images/modify.gif" /></td>
                    <td align="center" ><asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/Images/delete.gif" /></td>
                    <td align="center"><a href="#">15</a></td>
                </tr>
                <tr style="background-color:#eeeeee; height: 60px;">
                     <td style="padding-left: 10px;">
                        <a href="#">Assistant Professor: Agricultural Sustainability and Society Position</a><br />
                        Community Studies and Development Program
                    </td>
                    <td align="center"><asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/modify.gif" /></td>
                    <td align="center"><asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Images/delete.gif" /></td>
                    <td align="center"><a href="#">15</a></td>
                </tr>
                             <tr style="height: 60px;">
                     <td style="padding-left: 10px;">
                        <a href="#">Assistant Professor in Plant Sciences: Plant Biologist</a><br />
                        Department of Plant Sciences
                    </td>
                    <td align="center"><asp:ImageButton ID="ImageButton11" runat="server" ImageUrl="~/Images/modify.gif" /></td>
                    <td align="center" ><asp:ImageButton ID="ImageButton12" runat="server" ImageUrl="~/Images/delete.gif" /></td>
                    <td align="center"><a href="#">15</a></td>
                </tr>
                <tr style="background-color:#eeeeee; height: 60px;">
                     <td style="padding-left: 10px;">
                        <a href="#">Head Librarian, AREL</a><br />
                        Agricultural and Resource Economics Library
                    </td>
                    <td align="center"><asp:ImageButton ID="ImageButton13" runat="server" ImageUrl="~/Images/modify.gif" /></td>
                    <td align="center"><asp:ImageButton ID="ImageButton14" runat="server" ImageUrl="~/Images/delete.gif" /></td>
                    <td align="center"><a href="#">15</a></td>
                </tr>
            </table>
        </div>
    </div>
</div>
</form>
</body>
</html>
