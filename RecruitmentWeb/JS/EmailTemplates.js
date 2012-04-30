var EmailTemplateEditor = null;

tinyMCE.init({
    mode: "specific_textareas",
    editor_selector: "richTextEditor", //Just use textareas with the richTextEditor class applied
    theme: "advanced",
    skin: "o2k7",
    plugins: "paste",

    theme_advanced_buttons1: "preview,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,formatselect,fontsizeselect",
    theme_advanced_buttons2: "cut,copy,pastetext,pasteword,|,bullist,numlist,|,undo,redo,|,link,unlink,anchor,image,|,forecolor,backcolor",
    theme_advanced_buttons3: "",
    theme_advanced_toolbar_location: "top",

    setup: function(ed) {
        EmailTemplateEditor = ed;

        ed.addButton('preview', {
            title: 'Preview', image: '../Images/previewIcon.gif',
            onclick: function() {

                var content = ed.getContent(); //Get the current content inside the editor

                RecruitmentService.GetTemplatePreview(content, callback);
                
                function callback(result) {
                    newWin = window.open('', 'Preview', '');

                    if (newWin != null) {
                        var doc = newWin.document;
                        doc.write(result);
                        doc.close();
                    }
                }
            }
        });
    }
});

function InsertTemplateText(text) {
    EmailTemplateEditor.focus();
    EmailTemplateEditor.selection.setContent(text);
}

//Setup the events around template creation
$(document).ready(function() {
    $("select[id$=dlistEmailTemplates]").change(TemplateSectionChanged);

    $('#ReferenceTemplateHelp').bt('When creating a form letter you can click the fields bellow and the information will auto populate the reference template', {
        trigger: 'click',
        positions: 'top'
    });

    $('#BccAddressHelp').bt('Optionally include an email address to be blind carbon copied on all emails sent from this page', {
        trigger: 'click',
        positions: 'top'
    });
});

function TemplateSectionChanged() {
    EmailTemplateEditor.setProgressState(1); //Set the progress image

    //Now get the template text
    RecruitmentService.GetTemplateText($(this).val(), TemplateSectionChangedSuccess, TemplateSectionChangedFailure);
}

function TemplateSectionChangedSuccess(result) {
    EmailTemplateEditor.setProgressState(0);
    EmailTemplateEditor.setContent(result);
}

function TemplateSectionChangedFailure() {
    EmailTemplateEditor.setProgressState(0);
    EmailTemplateEditor.setContent("An Error Has Occurred While Retrieving The Template");
}
        