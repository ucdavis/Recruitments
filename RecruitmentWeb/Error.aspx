<%@ Page Language="C#" AutoEventWireup="true" Inherits="CAESDO.Recruitment.Web.Error" Title="Recruitments Error Page" Codebehind="Error.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Recruitments Error Page</title>
    <link rel="stylesheet" href="fracstyle.css" type="text/css" />
    <link rel="stylesheet" href="departmentstyles.css" type="text/css" />
</head>
<body>
    <div id="wrapper">
    <div id="header" style="height:62px;">
        <img src="Images/logo.gif" alt="UC Davis, College of Agricultural and Environmental Sciences" />
        <%--<img src="/Images/logo.gif" alt="Recruitments" style="float:left" />--%><%--<img src="~/Images/logo_ucdavis.gif" alt="UC Davis, College of Agricultural and Environmental Sciences" style="float:right" />--%>
    </div>
      <div id="content">
        <div id="breadcrumbs">&nbsp;
        </div>
        <div id="bodytext">
            <form id="form1" runat="server">
                <div>
                    <table border="0" cellpadding="0" cellspacing="0" width="480">
                        <tr style="background-color: #ffffff;">
                            <td style="width: 17px; vertical-align: top;">
                            </td>
                            <td style="vertical-align: top;">
                                <img src="Images/error404.gif" alt="404error" />
                                <br />
                                An Error Has Occured Of Type:
                                <asp:Label ID="lblErrorType" runat="server"></asp:Label>
                                <br />
                                <br />
                                Please try your request again. If problems continue contact AppRequests@caes.ucdavis.edu
                                for support.
                                <br />
                                <br />
                                <asp:HyperLink ID="hlinkHome" runat="server" NavigateUrl="~/Default.aspx">Go to the Homepage</asp:HyperLink>
                            </td>
                            <td style="width: 17px;">
                            </td>
                        </tr>
                    </table>
                </div>
             </form>
        </div>
        <br /><br />
    </div>
</div>
<div id="shadowBottom"></div><br /><br />
</body>
</html>
