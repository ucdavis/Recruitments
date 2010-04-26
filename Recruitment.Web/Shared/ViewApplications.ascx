<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewApplications.ascx.cs" Inherits="CAESDO.Recruitment.Web.Shared_ViewApplications" %>

<script type="text/javascript">

    $(document).ready(function() {

        //Sort table
        $("#tblApplications").tablesorter(
        {
            sortList: [[2, 1], [0, 0]],
            cssAsc: 'headerSortUp',
            cssDesc: 'headerSortDown',
            cssHeader: 'header',
            headers: { 0: { sorter: 'link'} },
            widgets: ['zebra']
        });

        //Search any table that has the tablesorter class
        $("#tblApplications tbody tr").quicksearch({
            labelText: 'Search: ',
            attached: null,
            attachType: 'table',
            position: 'before',
            delay: 100,
            loaderText: 'Loading...',
            onAfter: function() {
                var table = this.attached;

                $(table).trigger("update");
                $(table).trigger("appendCache");

                if ($("#chkShowUnsubmitted").is(":checked")) {
                    //If the show submitted only box is checked, hide the non submitted rows
                    HideUnsubmitted();
                }
            }
        });

        //The show unsubmitted applicants checkbox should be in an initial checked state 
        $("#chkShowUnsubmitted").attr("checked", "checked");

        $("#chkShowUnsubmitted").click(function() {

            var allApplications = $("#tblApplications tbody tr");

            if ($(this).is(":checked")) {
                //Hide the unsubmitted rows
                HideUnsubmitted();
            }
            else {
                //Show all rows
                allApplications.show(0);
            }

            //$("input.qs_input").keydown();
            $("#tblApplications").trigger("applyWidgets"); //Apply the zebra stripes
        });
    });

    function HideUnsubmitted() {
        var allApplications = $("#tblApplications tbody tr");
        
        allApplications.filter(":has(td.submittedHeader:contains('False'))").hide(0); //allApplications.filter("td.submittedHeader").hide(0);
    }
        
</script>

Viewing Applicants for the
<asp:Literal ID="litPositionTitle" runat="server" />
position.
<asp:LinkButton ID="lbtnDownloadSearchPlan" runat="server" Text="[View Search Plan (PDF)]" Visible="False" OnClick="lbtnDownloadSearchPlan_Click"></asp:LinkButton>

<br />
<br />
<span style="float:right;"><input id="chkShowUnsubmitted" checked="checked" type="checkbox" /><label for="chkShowUnsubmitted">Show Submitted Only</label></span>
<asp:ListView ID="lviewApplications" runat="server" DataSourceID="ObjectDataApplications">
    <LayoutTemplate>
        <table id="tblApplications" class="tablesorter">
            <thead>
                <tr>
                    <th>
                        Applicant Name
                    </th>
                    <th>
                        Email
                    </th>
                    <th>
                        Submitted
                    </th>
                    <th>
                        Submit Date
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr id="itemPlaceholder" runat="server">
                </tr>
            </tbody>
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr style='<%# (bool)Eval("Submitted") ? "" : "display:none;" %>'> <%--Hide unsubmitted applications by default--%>
            <td>
                <asp:LinkButton ID="lbtnViewApplication" runat="server" CommandArgument='<%# Eval("id") %>'
                    Text='<%# GetNullSafeFullName((string)Eval("AssociatedProfile.FullName")) %>'
                    OnClick="lbtnViewApplication_Click"></asp:LinkButton>
            </td>
            <td>
                <%# Eval("Email") %>
            </td>
            <td class="submittedHeader">
                <%--<asp:CheckBox ID="checkbox" runat="server" Checked='<%# (bool)Eval("Submitted")  %>' onclick="javascript:return false;" />--%>
                <%# Eval("Submitted") %>
            </td>
            <td>
                <%# (bool)Eval("Submitted") ? ((DateTime)Eval("SubmitDate")).ToShortDateString() : "N/A" %>
            </td>
        </tr>
    </ItemTemplate>
    <EmptyDataTemplate>
        No Applications Found For This Position
    </EmptyDataTemplate>
</asp:ListView>
<%--<asp:GridView ID="gviewApplications" runat="server" Visible="false" AllowPaging="True" skinID="gridViewUM" GridLines="None" CellPadding="0" AutoGenerateColumns="False" 
    DataSourceID="ObjectDataApplications" EmptyDataText="No Applications Found For This Position">
        <Columns>
            <asp:TemplateField HeaderText="Applicant Name">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnViewApplication" runat="server" CommandArgument='<%# Eval("id") %>' Text='<%# GetNullSafeFullName((string)Eval("AssociatedProfile.FullName")) %>' OnClick="lbtnViewApplication_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle CssClass="paddingLeft" />
                <HeaderStyle HorizontalAlign="Left" CssClass="paddingLeft" />
            </asp:TemplateField>
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" >
                <HeaderStyle Width="100px" />
            </asp:BoundField>
            <asp:CheckBoxField DataField="Submitted" HeaderText="Submitted" SortExpression="Submitted">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="100px" />
            </asp:CheckBoxField>
        </Columns>
    
    </asp:GridView>--%>
<asp:ObjectDataSource ID="ObjectDataApplications" runat="server" SelectMethod="GetByPosition"
    TypeName="CAESDO.Recruitment.BLL.ApplicationBLL" OnSelecting="ObjectDataApplications_Selecting"
    OldValuesParameterFormatString="original_{0}">
    <SelectParameters>
        <asp:Parameter Name="position" Type="Object" />
    </SelectParameters>
</asp:ObjectDataSource>
