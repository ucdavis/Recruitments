﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="CAESDO.Recruitment.Web.Shared_ViewApplications" Codebehind="ViewApplications.ascx.cs" %>

<script type="text/javascript">
    var phdInfoExpanded = false;

    $(document).ready(function() {
        //help balloon added by tyler
        $('.ApplicantViewListDateShowHideTxt').bt('This will expand the PhD information field for this applicant.  To expand this field for all applicants click on the +/- in the column header.', {
            trigger: 'hover',
            positions: 'right'
        });

        //help balloon added by tyler
        $('#ApplicantViewListShowHideHeader').bt('This will expand the PhD Information field for all applicants.  To expand this field for a certain applicant click on the +/- located to the right of the date in that row.', {
            trigger: 'hover',
            positions: 'top'
        });

        $.tablesorter.addParser({
            id: 'phdInfo',
            is: function(s) { return false; },
            format: function(s, table, cell) {
                var date = $(".ApplicantViewListDate", cell).html(); //Grab the text inside the applicant view list date class

                return $.tablesorter.formatFloat(new Date(date).getTime());
            },
            type: 'numeric'
        });

        $.tablesorter.addParser({
            id: 'dateWithNA', //A new parser is required to deal with the N/A cells
            is: function(s) { return false; },
            format: function(s, table, cell) {
                var date = $(cell).html(); //Grab the text inside the applicant view list date class

                return $.tablesorter.formatFloat(new Date(date).getTime());
            },
            type: 'numeric'
        });

        //Sort table
        $("#tblApplications").tablesorter(
        {
            sortList: [[4, 1], [0, 0]],
            cssAsc: 'headerSortUp',
            cssDesc: 'headerSortDown',
            cssHeader: 'header',
            headers: { 0: { sorter: 'link' }, 3: { sorter: 'phdInfo' }, 5: { sorter: 'dateWithNA'} },
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

        $("#ApplicantViewListShowHideHeader").click(function() {
            var additionalInfo = $(".ApplicantViewListPhDInformation");

            if (phdInfoExpanded) {
                //collapse the phdinfo
                additionalInfo.hide();
            }
            else {
                //expand the phdinfo
                additionalInfo.show();
            }

            phdInfoExpanded = !phdInfoExpanded;

            return false; //Don't bubble the event
        });

        //Expand the phd info for just this applicant
        $(".ApplicantViewListDateShowHideTxt").click(function() {

            var phdInfoCell = $(this).parents(".ApplicantViewListPhdCell");

            var phdInfoDiv = $(".ApplicantViewListPhDInformation", phdInfoCell); //Find the div to expand within this cell

            phdInfoDiv.toggle();
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
                        Current Position
                    </th>
                    <th>
                        PhD Information <span id="ApplicantViewListShowHideHeader" class="ApplicantViewListDateShowHideTxt">+/-</span>
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
            <td class="ApplicantViewListCurrentPosition">
                <%# (int)Eval("CurrentPositions.Count") > 0 ? Eval("CurrentPositions[0].Institution") + " -- " + Eval("CurrentPositions[0].Title") : string.Empty%>
            </td>
            <td class="ApplicantViewListPhdCell">
                <div class="ApplicantViewListDateShowHideDiv">
                    <span class="ApplicantViewListDate"><%# (int)Eval("Education.Count") > 0 ? ((DateTime)Eval("Education[0].Date")).ToShortDateString() : string.Empty%></span>
                    <span class="ApplicantViewListDateShowHideTxt">+/-</span>
                </div>
                <div class="ApplicantViewListPhDInformation" style="display:none;">
                    <%# (int)Eval("Education.Count") > 0 ? Eval("Education[0].Institution") : string.Empty%><br />
                    <%# (int)Eval("Education.Count") > 0 ? Eval("Education[0].Discipline") : string.Empty%><br />
                    <i>
                        <%# (int)Eval("Education.Count") > 0 && !string.IsNullOrEmpty((string) Eval("Education[0].ResearchField")) ? "Field: " + Eval("Education[0].ResearchField") : string.Empty%><br />
                        <%# (int)Eval("Education.Count") > 0 && !string.IsNullOrEmpty((string)Eval("Education[0].Advisor")) ? "Advisor: " + Eval("Education[0].Advisor") : string.Empty%>
                    </i>
                </div>
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
