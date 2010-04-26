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
            <table cellpadding="0" cellspacing="0" style="width:100%;">
                <tr class="headingBlue">
                    <td>Position/Department:</td>
                    <td style="width: 100px">Modify:</td>
                    <td style="width: 100px">Delete:</td>
                    <td style="width: 100px">Applicants:</td>
                </tr>
                <tr style="background-color:#eeeeee; height: 60px;">
                    <td style="border-top: dotted 1px #333333;">&nbsp;</td>
                    <td style="border-top: dotted 1px #333333;">&nbsp;</td>
                    <td style="border-top: dotted 1px #333333;">&nbsp;</td>
                    <td style="border-top: dotted 1px #333333;">&nbsp;</td>
                </tr>
                 <tr style="height: 60px;">
                    <td style="border-top: dotted 1px #333333;">&nbsp;</td>
                    <td style="border-top: dotted 1px #333333;">&nbsp;</td>
                    <td style="border-top: dotted 1px #333333;">&nbsp;</td>
                    <td style="border-top: dotted 1px #333333;">&nbsp;</td>
                </tr>
                <tr style="background-color:#eeeeee; height: 60px;">
                    <td style="border-top: dotted 1px #333333;">&nbsp;</td>
                    <td style="border-top: dotted 1px #333333;">&nbsp;</td>
                    <td style="border-top: dotted 1px #333333;">&nbsp;</td>
                    <td style="border-top: dotted 1px #333333;">&nbsp;</td>
                </tr>
            </table>
        </div>
    </div>
</div>
</form>
</body>
</html>
