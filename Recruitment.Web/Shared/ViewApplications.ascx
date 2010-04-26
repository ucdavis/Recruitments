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
                headers: { 2: { sorter: 'checkbox' }, 0: { sorter: 'link'} },
                widgets: ['zebra']
            });

        //The show unsubmitted applicants checkbox should be in an initial unchecked state 
        $("#chkShowUnsubmitted").removeAttr("checked");

        $("#chkShowUnsubmitted").click(function() {
            var allApplications = $("#tblApplications tbody tr");

            if ($(this).is(":checked")) {
                //Checkbox is checked, show all rows
                allApplications.show(0);
            }
            else {
                //Hide the unsubmitted rows
                allApplications.not(":has(:checked)").hide(0);
            }

            $("#tblApplications").trigger("applyWidgets"); //Apply the zebra stripes
        });
    });
        
</script>

Viewing Applicants for the
<asp:Literal ID="litPositionTitle" runat="server" />
position.
<br />
<br />
<span style="float:right;"><input id="chkShowUnsubmitted" type="checkbox" /> Show All Applicants</span>
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
            <td>
                <asp:CheckBox ID="checkbox" runat="server" Checked='<%# (bool)Eval("Submitted")  %>' onclick="javascript:return false;" />
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
