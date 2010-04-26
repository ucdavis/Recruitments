<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Help_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>Recruitments  Version 2 – Updates and New Features</h1>
<h2>Table of Contents</h2>
<ul id="square_li">
<li><a href="#PDFSearchPlan">PDF search plan</a></li>
<li><a href="#EmailApplicants">Send template emails to applicants</a></li>
<li>Imporved email text editor</li>
<li>Filter position list</li>
<li>RSS feeds
    <ul><li>iGoogle</li>
    <li>Netvibes</li></ul></li>
<li>New list features</li>
<li>Help balloons</li>
<li>Friendly messages to references</li>
<li>New Fileds for education information</li>
<li>Sortable submit date</li>
<li>Biographical spreadsheet</li>
<li>Additional references</li>
<li>Recruitment manager contact email</li>
<li>Reference Template
    <ul><li>Reference Title</li></ul></li>
<li>"My Applications" changed to "In Progress Applications"</li>
<li>Submitted applications</li>    
</ul>

<p><br /><br /></p>

<ul id="help_main_li">
  <li><a name="PDFSearchPlan"><h3>PDF Search Plan<br /></h3></a>
    All new and updated positions  are required to include a PDF search plan.<br /><img src="search_plan.jpg" alt="" width="541" height="117" />
  <ul>
    <li>When viewing the applicant list for a position  (as an admin or committee member), you can view the search plan by clicking on  the provided link. <img src="view_search_plan.jpg" alt="" width="527" height="108" /></li>
  </ul></li>
  <li><a name="EmailApplicants"><h3>Send template emails to applicants<br /></h3></a>
    You can now  send an ad-hoc list of applicants any formatted email directly from the  system.  There will be starter templates  to choose from, or you can create your own.<br /><img src="email_templates.jpg" alt="" width="628" height="400" /></li>
  <li>Improved Email Text Editor – You can now preview  letters with example data and insert text from Microsoft Word (see example):<br /><img src="external_editor.jpg" alt="" width="663" height="278" /></li>
  <li>Filter Positions List – You can filter the list  of all positions by either department or school.  If you would like to just list the positions  in Human &amp; Community Development, you can append ‘?DepartmentFIS=AHCD’ to  the view positions Url.<br /><img src="filter_positions_list.jpg" alt="" width="690" height="311" /></li>
  <li>RSS Feeds – The main view positions page can now  be consumed as an RSS feed in any feed reader (like iGoogle/Google reader) or on  any page that can consume feeds (like a department homepage).  The feed can be filtered by unit or school.</li>
  <li>iGoogle Feed Reader<br /><img src="google_reader.jpg" alt="" width="569" height="280" /></li>
  <li>Netvibes Start Page<br /><img src="netvibes.jpg" alt="" width="615" height="270" /></li>
  <li>New list features – The applicant lists can all  now be sorted by any column and filtered by text search.  Also you can toggle whether to show submitted  applications only with the click of a button:<br /><img src="new_list_features.jpg" alt="" width="742" height="196" /></li>
  <li>Help Balloons – Popup tips have been added to  portions of the site.  Whenever you see a  question mark, you can hover or click on it to get some helpful contextual  information.  Some popup tips appear  automatically when filling in form properties (see below):<br /><img src="help_balloons_1.jpg" alt="" width="495" height="159" /><br /><img src="help_balloons_2.jpg" alt="" width="631" height="183" /></li>
  <li>Friendly Messages to References:  When uploading a reference letter, references  will get better information about what is going on if the wrong url was used or  once a letter is uploaded.  Also, a  reference will get a confirmation email after uploading assuring them that the  letter was properly received.<br /><img src="reference_letter_fail.jpg" alt="" width="442" height="195" /><img src="reference_letter_success.jpg" alt="" width="437" height="173" /></li>
  <li>Education information now contains the  non-required ‘Research Field’ and ‘Advisor’.   This information will also be available in the updated biographical  spreadsheet. <img src="terminal_degree_information.jpg" alt="" width="427" height="305" /></li>
  <li>Submit Date now included on the view  applications pages, and is sortable.<br /><img src="submit_date.jpg" alt="" width="485" height="324" /></li>
  <li>Biographical spreadsheet – The biographical data  spreadsheet available to committee members has been reformatted to include new  columns and can also now be sorted from within Excel.</li>
  <li>Additional references -- A new file type called  “Additional References” has been created which will allow you to upload any  desired information about references which were not included in the original  application.  This document will be kept  along with the application and can be viewed along with the application packet.<img src="additional_references.jpg" alt="" width="483" height="264" /></li>
  <li>The Recruitment Manager contact email is now  shown on the homepage of every application so an applicant can contact the  correct person for help if they have any questions.<img src="recruitment_manager_email.jpg" alt="" width="580" height="207" /></li>
  <li>Reference Template – The Upload Link can be  placed at any desired position within the reference template by clicking on the  “Upload Link” template field.  If no  upload link is found, the default behavior is to add the link at the bottom of  the email.</li>
      <ul>
        <li>Reference Title is also now available as a field  (which is entered by the applicant)<br /><img src="reference_template.jpg" alt="" width="235" height="312" /></li>
      </ul>
  <li>“My Applications” has been changed to “In  Progress Applications” in the applicant drop-down options menu.</li>
  <li>Submitted applications now automatically receive  a confirmation email from the system.</li>
</ul>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFooter" Runat="Server">
</asp:Content>

