<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="CAESDO.Recruitment.Web.Error" Title="Recruitments Error Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Recruitments Error Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="480">
            <tr style="background-color: #ffffff;">
                <td style="width: 17px; vertical-align: top;">
                </td>
                <td style="vertical-align: top;">
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
</body>
</html>
