
$(document).ready(function (e) {
    //initialize the javascript
    //App.init();
    //App.wizard();
    //$('form').parsley();
    // Edit Overlay
    //$('.md-trigger').modalEffects();
    

    $("#setupProjectli").addClass("complete");
    $("#scheduleProjectli").addClass("complete");
    $("#reviewProjectli").addClass("complete");
    $("#completeProjectli").addClass("active");

    $("#setupProjectli").click(function (e) {
        return false;
    });
    $("#scheduleProjectli").click(function (e) {
        return false;
    });
    $("#reviewProjectli").click(function (e) {
        return false;
    });
    //if ($('#DefaultEmailLanguageID').val() == 0)
    //{
    //    $('#rdbPrimaryforProject0').prop('disabled', true);
    //    $('#rdbPrimaryforProject0').prop('checked', false);
    //}
    //$('#SelectedLanguage').val($('#DefaultEmailLanguageID').val());
    //$('#emailtemplate').hide();
    //$('#next').prop('disabled', false); // disabled by default on page load

    //$("[id^=chkUseforProject]").each(function () {
    //    if ($(this).is(':checked')) {
    //        $('#UseCustomEmailTemplate').val("True");
    //        $('#customemailtemplate').show();
    //        var id = $(this).attr("id");
    //        var templateId = id.split('_');
    //        $("#rdbPrimaryforProject_" + templateId[1]).prop('disabled', false);
    //    }
    //});
    //ClearErrorDiv();
    //// for user roles
    //userRolesHandlerFunction();
});
//............end.................
