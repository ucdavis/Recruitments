<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="V2Updates.aspx.cs" Inherits="Help_V2Updates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1><a name="top">Recruitments  Version 2 – Updates and New Features</a></h1>
<h2>Table of Contents</h2>
<ul id="square_li">
<li><a href="#PDFSearchPlan">PDF search plan</a></li>
<li><a href="#EmailApplicants">Send template emails to applicants</a></li>
<li><a href="#TextEditor">Imporved email text editor</a></li>
<li><a href="#FilterList">Filter position list</a></li>
<li><a href="#RSS">RSS feeds</a>
    <ul><li><a href="#RSS_iGoogle">iGoogle</a></li>
    <li><a href="#RSS_Netvibes">Netvibes</a></li></ul></li>
<li><a href="#ListFeatures">New list features</a></li>
<li><a href="#HelpBalloons">Help balloons</a></li>
<li><a href="#MessagesReferences">Friendly messages to references</a></li>
<li><a href="#EducationInformation">New Fields for education information</a></li>
<li><a href="#SubmitDate">Sortable submit date</a></li>
<li><a href="#BiographicalSpreadsheet">Biographical spreadsheet</a></li>
<li><a href="#ImprovedApplicationsView">Improved Applications View</a></li>
<li><a href="#AdditionalReferences">Additional references</a></li>
<li><a href="#RecruitmentManager">Recruitment manager contact email</a></li>
<li><a href="#ReferenceTemplate">Reference Template</a>
    <ul><li><a href="#ReferenceTitle">Reference Title</a></li></ul></li>
<li><a href="#MyApplicationsInProgressApplications">"My Applications" changed to "In Progress Applications"</a></li>
<li><a href="#SubmittedApplications">Submitted applications</a></li>    
</ul>

<p><br /><br /></p>

<ul id="help_main_li">
  <li><a name="PDFSearchPlan"><h3>PDF Search Plan<br /></h3></a>
    All new and updated positions  are required to include a PDF search plan.<br /><img src="search_plan.jpg" alt="" width="541" height="117" /><h4 class="help_back_to_top"><a href="#top">Back to Top</a></h4>
  <ul>
    <li>When viewing the applicant list for a position  (as an admin or committee member), you can view the search plan by clicking on  the provided link. <img src="view_search_plan.jpg" alt="" width="527" height="108" /><h4 class="help_back_to_top"><a href="#top">Back to Top</a></h4></li>
  </ul></li>
  <li><a name="EmailApplicants"><h3>Send template emails to applicants<br /></h3></a>
    You can now  send an ad-hoc list of applicants any formatted email directly from the  system.  There will be starter templates  to choose from (more coming later) or you can create your own.<br /><img src="email_templates.jpg" alt="" width="628" height="400" /><h4 class="help_back_to_top"><a href="#top">Back to Top</a></h4></li>
  <li><a name="TextEditor"><h3>Improved Email Text Editor<br /></h3></a>
    You can now preview  letters with example data and insert text from Microsoft Word (see example):<br /><img src="external_editor.jpg" alt="" width="663" height="278" /><h4 class="help_back_to_top"><a href="#top">Back to Top</a></h4></li>
  <li><a name="FilterList"><h3>Filter Positions List<br /></h3></a>
    You can filter the list  of all positions by either department or school.  If you would like to just list the positions  in Human &amp; Community Development, you can append ‘?DepartmentFIS=AHCD’ to  the view positions Url. Example: https://secure.caes.ucdavis.edu/Recruitment/viewPositions.aspx?DepartmentFIS=AHCD <br /><img src="filter_positions_list.jpg" alt="" width="690" height="311" /><h4 class="help_back_to_top"><a href="#top">Back to Top</a></h4></li>
  <li><a name="RSS"><h3>RSS Feeds<br /></h3></a>
    The main view positions page can now  be consumed as an RSS feed in any feed reader (like iGoogle/Google reader) or on  any page that can consume feeds (like a department homepage).  The feed can be filtered by unit or school.
    <ul>
    <li><a name="RSS_iGoogle"><h3>iGoogle Feed Reader<br /></h3></a><img src="google_reader.jpg" alt="" width="569" height="280" /><h4 class="help_back_to_top"><a href="#top">Back to Top</a></h4></li>
    <li><a name="RSS_Netvibes"><h3>Netvibes Start Page<br /></h3></a><img src="netvibes.jpg" alt="" width="615" height="270" /><h4 class="help_back_to_top"><a href="#top">Back to Top</a></h4></li>
    </ul></li>
  <li><a name="ListFeatures"><h3>New list features</br /></h3></a>
    The applicant lists can all  now be sorted by any column and filtered by text search.  Also you can toggle whether to show submitted  applications only with the click of a button:<br /><img src="new_list_features.jpg" alt="" width="742" height="196" /><h4 class="help_back_to_top"><a href="#top">Back to Top</a></h4></li>
  <li><a name="HelpBalloons"><h3>Help Balloons<br /></h3></a>
    Popup tips have been added to  portions of the site.  Whenever you see a  question mark, you can hover or click on it to get some helpful contextual  information.  Some popup tips appear  automatically when filling in form properties (see below):<br /><img src="help_balloons_1.jpg" alt="" width="495" height="159" /><br /><img src="help_balloons_2.jpg" alt="" width="631" height="183" /><h4 class="help_back_to_top"><a href="#top">Back to Top</a></h4></li>
  <li><a name="MessagesReferences"><h3>Friendly Messages to References<br /></h3></a>
    When uploading a reference letter, references  will get better information about what is going on if the wrong url was used or  once a letter is uploaded.  Also, a  reference will get a confirmation email after uploading assuring them that the  letter was properly received.<br /><img src="reference_letter_fail.jpg" alt="" width="442" height="195" /><img src="reference_letter_success.jpg" alt="" width="437" height="173" /><h4 class="help_back_to_top"><a href="#top">Back to Top</a></h4></li>
  <li><a name="EducationInformation"><h3>Education Information<br /></h3></a>
    Education information now contains the  non-required ‘Research Field’ and ‘Advisor’.   This information will also be available in the updated biographical  spreadsheet. <img src="terminal_degree_information.jpg" alt="" width="427" height="305" /><h4 class="help_back_to_top"><a href="#top">Back to Top</a></h4></li>
  <li><a name="SubmitDate"><h3>Submit Date<br /></h3></a>
    Submit Date now included on the view  applications pages, and is sortable.<br /><img src="submit_date.jpg" alt="" width="485" height="324" /><h4 class="help_back_to_top"><a href="#top">Back to Top</a></h4></li>
  <li><a name="BiographicalSpreadsheet"><h3>Biographical spreadsheet<br /></h3></a>
    The biographical data  spreadsheet available to committee members has been reformatted to include new  columns and can also now be sorted from within Excel.<br /><img src="excel.jpg" /><h4 class="help_back_to_top"><a href="#top">Back to Top</a></h4></li>
  <li><a name="ImprovedApplicationsView"><h3>Improved Applications View<br /></h3></a>
  The applications view (for admins or committee members) now shows the current position and PhD Information for each applicant.  The PhD Information by default shows just the PhD award date and is sortable by this date.  By clicking +/- buttons you can choose to show or hide additional PhD information (like institution, field, etc.)<br /><img src="improved_app_view.jpg" /><h4 class="help_back_to_top"><a href="#top">Back to Top</a></h4>.</li>
  <li><a name="AdditionalReferences"><h3>Additional references<br /></h3></a>
    A new file type called  “Additional References” has been created which will allow you to upload any  desired information about references which were not included in the original  application.  This PDF document will be kept  along with the application and can be viewed along with the application packet.<img src="additional_references.jpg" alt="" width="483" height="264" /><h4 class="help_back_to_top"><a href="#top">Back to Top</a></h4></li>
  <li><a name="RecruitmentManager"><h3>Recruitment manager email<br /></h3></a>
    The Recruitment Manager contact email is now  shown on the homepage of every application so an applicant can easily send an email to the correct person if they have any questions.<br /><img src="recruitment_manager_email.jpg" alt="" width="580" height="207" /><h4 class="help_back_to_top"><a href="#top">Back to Top</a></h4></li>
  <li><a name="ReferenceTemplate"><h3>Reference Template<br /></h3></a>
    The Upload Link can be  placed at any desired position within the reference template by clicking on the  “Upload Link” template field.  If no  upload link is found, the default behavior is to add the link at the bottom of  the email.
      <ul>
        <li><a name="ReferenceTitle"><h3>Reference Title<br /></h3></a>
            Reference Title is also now available as a field  (which is entered by the applicant)<br /><img src="reference_template.jpg" alt="" width="235" height="312" /><h4 class="help_back_to_top"><a href="#top">Back to Top</a></h4></li>
      </ul></li>
  <li><a name="MyApplicationsInProgressApplications"><h3>"My Applications" changed to "In Progress Applications"<br /></h3></a>
    “My Applications” has been changed to “In  Progress Applications” in the applicant drop-down options menu.<h4 class="help_back_to_top"><a href="#top">Back to Top</a></h4></li>
  <li><a name="SubmittedApplications"><h3>Submitted applications<br /></h3></a>
    Submitted applications now automatically receive  a confirmation email from the system.<h4 class="help_back_to_top"><a href="#top">Back to Top</a></h4></li>
</ul>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFooter" Runat="Server">
</asp:Content>

