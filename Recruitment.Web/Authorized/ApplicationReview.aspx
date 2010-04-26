<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApplicationReview.aspx.cs" Inherits="CAESDO.Recruitment.Web.Authorized_ApplicationReview" Title="Application Review" Theme="MainTheme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

            <asp:Panel ID="pnlContactInformation" runat="server" >
                <span class="boxTitle">Contact Information</span><br />
                <table class="box" style="width:500px;" cellpadding="5">
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <td   align="right">
                            First Name:</td>
                        <td  >
                            <asp:TextBox ReadOnly="true" ID="txtContactFirstName" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Middle Name:</td>
                        <td  >
                            <asp:TextBox ReadOnly="true" ID="txtContactMiddleName" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Last Name:</td>
                        <td  >
                            <asp:TextBox ReadOnly="true" ID="txtContactLastName" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Address 1:</td>
                        <td  >
                            <asp:TextBox ReadOnly="true" ID="txtContactAddress1" runat="server" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Address 2:</td>
                        <td  >
                            <asp:TextBox ReadOnly="true" ID="txtContactAddress2" runat="server" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            City:</td>
                        <td  >
                            <asp:TextBox ReadOnly="true" ID="txtContactCity" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            State:</td>
                        <td   >
                            <asp:TextBox ReadOnly="true" ID="txtContactState" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Phone Number:</td>
                        <td  >
                            <asp:TextBox ReadOnly="true" ID="txtContactPhone" runat="server" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            
            <asp:Panel ID="pnlEducationInformation" runat="server">
                <span class="boxTitle">Ph.D. Award Information</span><br />
                <table class="box" style="width:500px; " cellpadding="5">
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <td align="right">
                            Ph.D. Date:</td>
                        <td >
                            <asp:TextBox ReadOnly="true" ID="txtEducationPHDDate" runat="server"></asp:TextBox> 
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Awarding Institute:</td>
                        <td  >
                            <asp:TextBox ReadOnly="true" ID="txtEducationInstitution" runat="server" MaxLength="100"></asp:TextBox>                
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Discipline:</td>
                        <td  >
                            <asp:TextBox ReadOnly="true" ID="txtEducationDiscipline" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            
             <asp:Panel ID="pnlCurrentPosition" runat="server">
                <span class="boxTitle">Current Position</span><br />
                <table class="box" style="width:500px; " cellpadding="5">
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <td align="right">
                            Title:</td>
                        <td  >
                            <asp:TextBox ReadOnly="true" ID="txtCurrentPositionTitle" runat="server" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Department:</td>
                        <td  >
                            <asp:TextBox ReadOnly="true" ID="txtCurrentPositionDepartment" runat="server" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Institution:</td>
                        <td  >
                            <asp:TextBox ReadOnly="true" ID="txtCurrentPositionInstitution" runat="server" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Address 1:</td>
                        <td  >
                            <asp:TextBox ReadOnly="true" ID="txtCurrentPositionAddress1" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Address 2:</td>
                        <td  >
                            <asp:TextBox ReadOnly="true" ID="txtCurrentPositionAddress2" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            City:</td>
                        <td  >
                            <asp:TextBox ReadOnly="true" ID="txtCurrentPositionCity" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            State:</td>
                        <td  >
                            <asp:TextBox ReadOnly="true" ID="txtCurrentPositionState" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Zip:</td>
                        <td  >
                            <asp:TextBox ReadOnly="true" ID="txtCurrentPositionZip" runat="server" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td   align="right">
                            Country:</td>
                        <td  >
                            <asp:TextBox ReadOnly="true" ID="txtCurrentPositionCountry" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        
            <asp:Panel ID="pnlReferences" runat="server">
                <span class="boxTitle">References</span><br />
                <table class="box" style="width:500px;" cellpadding="5">
                    <tr>
                        <td colspan="2"><asp:Button ID="btnReferencesShow" runat="server" style="display:none; visibility:hidden;" /><br /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        Existing References:
                            <asp:GridView ID="gviewReferences" skinID="gridViewReferences" runat="server" DataKeyNames="ID" OnSelectedIndexChanged="gviewReferences_SelectedIndexChanged" AutoGenerateColumns="False" EmptyDataText="No References Added" BorderStyle="None" CellPadding="0" GridLines="None">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" SelectText="View" EditText="" >
                                    <ItemStyle CssClass="paddingLeft" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="Name:">
                                    <ItemTemplate>
                                        <%# Eval("Title") %> <%# Eval("FirstName") %> <%# Eval("LastName") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Title Expertise" Visible="False">
                                    <ItemTemplate>
                                        <%# Eval("AcadTitle") %> <%# Eval("Expertise") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department Institution" Visible="False">
                                    <ItemTemplate>
                                        <%# Eval("Dept") %> <%# Eval("Institution") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address" Visible="False">
                                    <ItemTemplate>
                                        <%# Eval("Address1") %> <%# Eval("Address2") %> <br />
                                        <%# Eval("City") %> <%# Eval("State") %> <%# Eval("Zip") %> <%# Eval("Country") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Phone" DataField="Phone" Visible="False" />
                                <asp:BoundField HeaderText="Email:" DataField="Email" />
                            </Columns>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:GridView>                           
                        </td>
                    </tr>
                </table>
                
                <asp:Panel ID="pnlReferencesEntry" runat="server" CssClass="modalPopup" style="display:none;">
                    <span class="modalTitle">View Reference</span>
                    <div style="height:450px; overflow:auto;">
                    <table cellpadding="5" style="width:500px;">
                        <tr>
                            <td align="right">
                                Title:</td>
                            <td  >
                                <asp:TextBox ReadOnly="true" ID="txtReferencesTitle" runat="server" MaxLength="50" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                First Name:</td>
                            <td  >
                                <asp:TextBox ReadOnly="true" ID="txtReferencesFirstName" runat="server" MaxLength="50" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Last Name:</td>
                            <td  >
                                <asp:TextBox ReadOnly="true" ID="txtReferencesLastName" runat="server" MaxLength="50" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                Academic Title:</td>
                            <td  >
                                <asp:TextBox ReadOnly="true" ID="txtReferencesAcadTitle" runat="server" MaxLength="50" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                Area of Expertise:</td>
                            <td  >
                                <asp:TextBox ReadOnly="true" ID="txtReferencesExpertise" runat="server" MaxLength="50" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td   align="right">
                                Department:</td>
                            <td  >
                                <asp:TextBox ReadOnly="true" ID="txtReferencesDepartment" runat="server" MaxLength="50" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td   align="right">
                                Institute:</td>
                            <td  >
                                <asp:TextBox ReadOnly="true" ID="txtReferencesInstitute" runat="server" MaxLength="50" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                Address 1:</td>
                            <td  >
                                <asp:TextBox ReadOnly="true" ID="txtReferencesAddress1" runat="server" MaxLength="50" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                Address 2:</td>
                            <td  >
                                <asp:TextBox ReadOnly="true" ID="txtReferencesAddress2" runat="server" MaxLength="50" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                City:</td>
                            <td  >
                                <asp:TextBox ReadOnly="true" ID="txtReferencesCity" runat="server" MaxLength="50" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                State:</td>
                            <td  >
                                <asp:TextBox ReadOnly="true" ID="txtReferencesState" runat="server" MaxLength="50" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                Zip:</td>
                            <td  >
                                <asp:TextBox ReadOnly="true" ID="txtReferencesZip" runat="server" MaxLength="20" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                Country:</td>
                            <td  >
                                <asp:TextBox ReadOnly="true" ID="txtReferencesCountry" runat="server" MaxLength="50" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                Phone Number:</td>
                            <td  >
                                <asp:TextBox ReadOnly="true" ID="txtReferencesPhone" runat="server" MaxLength="20" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                                Email:</td>
                            <td  >
                                <asp:TextBox ReadOnly="true" ID="txtReferencesEmail" runat="server" MaxLength="100" EnableViewState="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td   align="right">
                            </td>
                            <td align="right"  >
                                <br />
                                <asp:Button ID="btnReferencesCancel" runat="server" Text="Close" />
                            </td>
                        </tr>
                    </table></div>
                </asp:Panel>
                
                <AjaxControlToolkit:ModalPopupExtender ID="mpopupReferencesEntry" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnReferencesCancel" PopupControlID="pnlReferencesEntry" TargetControlID="btnReferencesShow"></AjaxControlToolkit:ModalPopupExtender>
                
            </asp:Panel>
            
            <asp:Panel ID="pnlFiles" runat="server">
                 <span class="boxTitle">Files</span><br />
                <table class="box" style="width:500px;" cellpadding="5">
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gviewFiles" runat="server" skinID="gridViewReferences" EmptyDataText="No Files Found" AutoGenerateColumns="false" BorderStyle="None" CellPadding="0" GridLines="None">
                                <Columns>
                                    <asp:TemplateField HeaderText="File Type">
                                        <ItemTemplate>
                                            <%# BreakCamelCase(Eval("FileType.FileTypeName") as string)%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="File">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnFileDownload" runat="server" Text='<%# Eval("FileName") %>' CommandArgument='<%# Eval("ID") %>' OnClick="lbtnFileDownload_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                
                <asp:Button ID="btnDownloadAll" runat="server" Text="Download All Files" OnClick="btnDownloadAll_Click" />
            </asp:Panel>          
            
            <asp:Panel ID="pnlPublications" runat="server" Visible="false">
                <span class="boxTitle">Publications</span><br />
                <table class="box" style="width:500px;" cellpadding="5">
                    
                    <tr>
                        <td colspan="2">
                        <br />
                            <asp:Repeater ID="rptPublications" runat="server">
                                <ItemTemplate>
                                    &nbsp;&nbsp;<asp:LinkButton ID="lbtnPublicationFile" runat="server" Text='<%# Eval("FileName") %>' CommandArgument='<%# Eval("ID") %>' OnClick="lbtnPublicationFile_Click"></asp:LinkButton>
                                </ItemTemplate>
                                <SeparatorTemplate>
                                    <br />
                                </SeparatorTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            
</asp:Content>

