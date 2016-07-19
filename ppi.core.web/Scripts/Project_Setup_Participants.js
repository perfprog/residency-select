
$(document).ready(function () {
    $("#setupProjectLi").addClass("complete");
    $("#customizationsProjectLi").addClass("complete");
    $("#scheduleProjectLi").addClass("complete");
    $("#emailProjectLi").addClass("complete");
    $("#participantProjectLi").addClass("active");

    $('#datatable-icons').dataTable({
        "aoColumns": [
        { "bSortable": false },
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        { "bSortable": false }
        ],
        "sDom": 'tir'
    });

    var functions = $('<div class="btn-group"><button class="btn btn-default btn-xs" type="button">Actions</button><button data-toggle="dropdown" class="btn btn-xs btn-primary dropdown-toggle" type="button"><span class="caret"></span><span class="sr-only">Toggle Dropdown</span></button><ul role="menu" class="dropdown-menu pull-right"><li><a href="#">Edit</a></li><li><a href="#">Copy</a></li><li><a href="#">Details</a></li><li class="divider"></li><li><a href="#">Remove</a></li></ul></div>');
    $("#datatable tbody tr td:last-child").each(function () {
        $(this).html("");
        functions.clone().appendTo(this);
    });

    //Add dataTable Icons
    var functions = $('<a class="btn btn-primary btn-xs md-trigger" href="#" data-original-title="Edit" data-modal="form-primary"><i class="fa fa-pencil"></i></a> <a class="btn btn-info btn-xs md-trigger" href="#" data-original-title="Comments" data-original-title="Comments" data-modal="form-primary-3"><i class="fa fa-comment"></i></a><span class="bubbleaction">2</span>');
    $("#datatable-icons tbody tr td:last-child").each(function () {
        $(this).html("");
        functions.clone().appendTo(this);
    });

    //Initialize particiapnts table   
    $('#participantDataTable').dataTable({
        "bPaginate": false,
        ordering: false,
        bFilter: false,
        "bProcessing": true,
        bInfo: false,
        "oLanguage": {
            "sEmptyTable": "No participants found."
        }
    });
    
    // Edit Overlay
    $('.md-trigger').modalEffects();
    
    $('#OnlineRNFEmailTemplateID').hide();
    $('#StanderedOnlineRNFTemplateID').hide();
    if ($('#UseDefaultEmailwithOnlineRNF').val() == "False") {
        $('#OnlineRNFEmailTemplateID').show();
    }
    if ($('#UseStanderedOnlineRNF').val() == "False") {
        $('#StanderedOnlineRNFTemplateID').show();
    }
    $('#successMessage').hide();
    $('#successMessageCSV').hide();
    // for user roles
    userRolesHandlerFunction();

});

$('#UseDefaultEmailwithOnlineRNF').change(function () {
    if ($('#UseDefaultEmailwithOnlineRNF').val() == "False") {
        $('#OnlineRNFEmailTemplateID').show();
    }
    else {
        $('#OnlineRNFEmailTemplateID').hide();
    }
});

$('#UseStanderedOnlineRNF').change(function () {
    if ($('#UseStanderedOnlineRNF').val() == "False") {
        $('#StanderedOnlineRNFTemplateID').show();
    } else {
        $('#StanderedOnlineRNFTemplateID').hide();
    }
});

function ReloadParticipantsList(projectId) {
    var url = '/Project/Participants/';
    if (projectId == null || projectId == 0) {
        projectId = $("#ProjectID").val();
    }    
    if ($("#isFromRNFStatus").val() == "True" )
    {
        url = "/RNF/Index?SortColumn=" + $("#SortColumn").val() + "&SortOrder=" + $("#SortOrder").val() + "&Page=" + $("#hdnPageNumber").val() + "&PageSize=" + $("#hdnPageSize").val();
    }
    $.ajax({
        async: false,
        cache: false,
        type: 'GET',
        url: url,
        data: {
            projectId: projectId, isFromRNFStatus: $("#isFromRNFStatus").val()
        },
        success: function (data, event) {
            $('#ParticipantList').html(data);
            //if ($("#isFromRNFStatus").val() == "True") {
            //    $("#participantLoadedText").text($("#participantsCount").val());
            //}
            event.preventDefault();
            event.stopPropagation();            
        }
    });
}

function SaveProjectParticipantEmailDetails(projectId) {

    var UseDefaultEmailwithOnlineRNF = true;
    var UseStanderedOnlineRNF = true;
    var OnlineRNFEmailTemplateID = 0;
    var StanderedOnlineRNFTemplateID = 0;
    if ($('#UseDefaultEmailwithOnlineRNF').val() == "False") {
        UseDefaultEmailwithOnlineRNF = false;
        OnlineRNFEmailTemplateID = $('#OnlineRNFEmailTemplateID').val();
    }
    if ($('#UseStanderedOnlineRNF').val() == "False") {
        UseStanderedOnlineRNF = false;
        StanderedOnlineRNFTemplateID = $('#StanderedOnlineRNFTemplateID').val();
    }

    var url = '/Project/SaveProjectParticipantEmailDetails/';
    $.ajax({
        async: false,
        cache: false,
        type: 'POST',
        url: url,
        data: {
            UseDefaultEmailwithOnlineRNF: UseDefaultEmailwithOnlineRNF, OnlineRNFEmailTemplateID: OnlineRNFEmailTemplateID,
            UseStanderedOnlineRNF: UseStanderedOnlineRNF, StanderedOnlineRNFTemplateID: StanderedOnlineRNFTemplateID, projectId: projectId
        },
        success: function (data) {
            window.location = "/Project/Complete?projectId=" + projectId;

        }
    });
}

function ClearAddParticipantPopUp()
{
    $('#successMessage').hide();
    $("#FirstName").val("");
    $("#LastName").val("");
    $("#Email").val("");
    $("#FirstNameMessage").empty();
    $("#LastNameMessage").empty();
    $("#EmailMessage").empty();
    
}


//---------------------------- CSV file Upload logic starts here ----------------------------

function ClearCSVTextBox() {
    $("#txtFileNameText").val("");
}

$("#txtFile").change(function () {
    var file = document.getElementById('txtFile');
    var doc = file.value;
    document.getElementById("txtFakeText").value = doc;
});

$("#txtBrowse").change(function () {
    var file = document.getElementById('txtBrowse');
    var doc = file.value;

    var extension = doc.split('.');
    if (extension[1] != "csv") {
        $("#txtFileNameText").focus();
        file.value = '';
        alert("Please choose a .csv file");
        return;
    }
    $("#txtFileNameText").val(doc);
});

function HandleFileButtonClick() {

    var file = document.getElementById('txtFile');
    file.click();
    var doc = file.value;

    document.getElementById("txtFakeText").value = doc;
    document.getElementById('txtFile').value = doc;
}

function HandleBrowseButtonClick() {
    var file = document.getElementById('txtBrowse');
    file.click();
    var doc = file.value;
    if (doc != '') {
        var extension = doc.split('.');
        if (extension[1] != "csv") {
            $("#txtFileNameText").focus();
            file.value = "";
            alert("Please choose a .csv file");
            return;
        }
        $("#txtFileNameText").val(doc);
        $('#txtBrowse').val(doc);
    }
}

$("#btnUploadCSV").on('click', function () {
    $('#successMessageCSV').hide();
    var fileUpload = document.getElementById("txtBrowse");
    if (fileUpload.value.trim() != null) {
        var uploadFile = new FormData();
        var files = $("#txtBrowse").get(0).files;
        var projectId = $("#ProjectId").val();
        $("#errUploadCSV").html("");
        // Add the uploaded file content to the form data collection
        if (files.length > 0) {
            uploadFile.append("CsvDoc", files[0]);
            uploadFile.append('projectId', projectId);
            $.ajax({
                url: "/Project/UploadParticipantsCSV",
                contentType: false,
                processData: false,
                data: uploadFile,
                type: 'POST',
                success: function (data) {
                    if (data == "") {
                        alert("Participants saved successfully");
                       // $('#successMessageCSV').show();
                        ReloadParticipantsList(projectId);
                    }
                    else {

                        alert(data);
                        $("#errUploadCSV").html(data);
                    }
                },
                error: function (data) {

                    alert(data);
                    $("#errUploadCSV").html(data);
                }
            });
        }
        else {
            $("#modalCreateParticipantCSV").show;
            // alert("Please choose a .csv file");
        }
    }
    else {
        $("#modalCreateParticipantCSV").show;
        // alert("Please choose a .csv file");
    }
});

function OnSuccessSave() {
    $('#successMessage').hide();
    if (!$("#hdError").val() == "") {
        var form = $("#formParticipantCreate");
        jQuery.validator.unobtrusive.parse(form);
        //  $('#myModal').modal('show');
    }
    else {
        ClearAddParticipantPopUp();
        $('#successMessage').show();
    }
}

//---------------------------- CSV file Upload logic ends here ----------------------------
//for user loads
function userRolesHandlerFunction() {
    if ($('#isReadOnly').val() == 'True') {
        $('.form-control').prop('disabled', true);
        $(".btn-primary").hide();
    }
    else {
        $('.form-control').prop('disabled', false);
        $(".btn-primary").show();
    }
}
//............end.................
