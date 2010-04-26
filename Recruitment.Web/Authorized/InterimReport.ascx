<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InterimReport.ascx.cs" Inherits="CAESDO.Recruitment.Web.Authorized_InterimReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Interim Recruitment Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <p align="right">EXHIBIT E</p>
 <p align="right">
     SECTION UCD-500</p>
 <p align="right">Academic Personnel Manual</p>
 <p align="center">INTERIM RECRUITMENT REPORT ON APPLICANT POOLS</p>
 <table width="100%"  border="0">
   <tr>
     <td>Department: <strong>
         <asp:Label ID="lblDepartment" runat="server" EnableViewState="False"></asp:Label></strong></td>
     <td>&nbsp;</td>
     <td>College/School: <strong>Agricultural and Environmental Sciences</strong> </td>
   </tr>
   <tr>
     <td>Recruitment for the position of <strong>
         <asp:Label ID="lblPosition" runat="server" EnableViewState="False"></asp:Label></strong></td>
     <td>&nbsp;</td>
     <td>Position #: <strong>
         <asp:Label ID="lblPosNum" runat="server" EnableViewState="False"></asp:Label></strong> </td>
   </tr>
   <tr>
     <td colspan="3">Title and rank: </td>
   </tr>
   <tr>
     <td colspan="3">Was approval granted to upgrade the position for tenure level appointments? Yes ____ No ____ </td>
   </tr>
   <tr>
     <td>Date search initiated:</td>
     <td>&nbsp;</td>
     <td>Search closing: 
         <strong><asp:Label ID="lblDeadline" runat="server" EnableViewState="False"></asp:Label></strong></td>
   </tr>
 </table>
 <p align="left">As stated in the search plan, is there an affirmative action hiring goal for this position? Yes ____ No _____ </p>
 <p>A. Indicate the composition of the applicant pool at the close of the search.</p>
 <p align="center"><u>Composition of the Applicant Pool</u></p>
 <asp:GridView ID="gviewSexEthnicity" runat="server" AutoGenerateColumns="False" HorizontalAlign="Left" EnableViewState="False">
            <Columns>
                <asp:BoundField DataField="Gender" ReadOnly="True" SortExpression="Gender" />
                <asp:BoundField DataField="AmericanIndianCount" HeaderText="American Indian" ReadOnly="True"
                    SortExpression="AmericanIndianCount">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="AsianCount" HeaderText="Asian/ Asian American"
                    ReadOnly="True" SortExpression="AsianCount">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="BlackCount" HeaderText="Black/ African American"
                    ReadOnly="True" SortExpression="BlackCount">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ChicanoCount" HeaderText="Chicano/ Latino/ Hispanic"
                    ReadOnly="True" SortExpression="ChicanoCount">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="WhiteCount" HeaderText="White" ReadOnly="True" SortExpression="WhiteCount">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="UnidentifiedCount" HeaderText="Unidentified" ReadOnly="True"
                    SortExpression="UnidentifiedCount">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="TotalCount" HeaderText="Total" ReadOnly="True" SortExpression="TotalCount">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        &nbsp;
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        B. Based on the selection criteria, provide the reason each candidate was not selected
        for interview.<br />
        <table width="80%">
            <tr>
            <td>&nbsp;&nbsp;</td>
            <td><asp:GridView ID="gviewApplicants" runat="server" Width="90%" AutoGenerateColumns="False" EnableViewState="False">
            <Columns>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <%# GetNullSafeName((CAESDO.Recruitment.Core.Domain.Profile)Eval("AssociatedProfile")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Comments">
                    <ItemTemplate>
                        &nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView></td>
            </tr>
        </table>
        
        <br />
        C. Indicate the composition of the pool of applicants you plant to interview.<p align="center">
            <u>Composition of the Pool Selected for Interview</u></p>
            
            <asp:GridView ID="gviewInterviewSexEthnicity" runat="server" AutoGenerateColumns="False" HorizontalAlign="Left" EnableViewState="False">
            <Columns>
                <asp:BoundField DataField="Gender" ReadOnly="True" SortExpression="Gender" />
                <asp:BoundField DataField="AmericanIndianCount" HeaderText="American Indian" ReadOnly="True"
                    SortExpression="AmericanIndianCount">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="AsianCount" HeaderText="Asian/ Asian American"
                    ReadOnly="True" SortExpression="AsianCount">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="BlackCount" HeaderText="Black/ African American"
                    ReadOnly="True" SortExpression="BlackCount">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ChicanoCount" HeaderText="Chicano/ Latino/ Hispanic"
                    ReadOnly="True" SortExpression="ChicanoCount">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="WhiteCount" HeaderText="White" ReadOnly="True" SortExpression="WhiteCount">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="UnidentifiedCount" HeaderText="Unidentified" ReadOnly="True"
                    SortExpression="UnidentifiedCount">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="TotalCount" HeaderText="Total" ReadOnly="True" SortExpression="TotalCount">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        D. List the names of the persons selected for interview and attach a copy of their
        curriculum vitae.<br />
        
        <ul>
            <asp:Repeater ID="rptInterviewApplicants" runat="server">
                <ItemTemplate>
                    <li> <%# GetNullSafeName((CAESDO.Recruitment.Core.Domain.Profile)Eval("AssociatedProfile"))%> </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
        
        <br />
        <br />
        E. Provide written justification for continuing the search if applicant pool/pool
        select for interview is no sufficiently diverse.<br />
        <br />
        <br />
        <br />
        <br />
        <table>
            <tr>
                <td colspan="2"><hr style="width: 380px; text-align: left;" /></td>
                <td>&nbsp;</td>
                <td colspan="2"><hr style="width: 380px; text-align: left;" /></td>
            </tr>
            <tr>
                <td align="left">Department Chair</td>
                <td align="right">Date</td>
                <td>&nbsp;</td>
                <td align="left">Dean</td>
                <td align="right">Date</td>
            </tr>
        </table>
        <br />
    </div>
        
    </form>
</body>
</html>
