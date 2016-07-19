
// To Hide ApproveOEQ popup and Enable Next button.
function OnSuccessApproveOEQ(data) {

    if (data.success) {
        $("#form-primary-approve").modal("hide");
        $('#next').prop('disabled', false);
    }
}

// To Hide ApproveOEQ popup, when Custom OEQ is not selected.
function OnFailedApproveOEQ() {

    $("#form-primary-approve").modal("hide");
    $('#next').prop('disabled', true);
    alert('Select any Custom OEQ Template.');
}

// To Hide AddProjectContact popup.
function OnSuccessAddProjectContact(data) {
    if (data.success) {
        //$('#ProjectContactIDs').append('<option value="'+data.user.UserID+'">'+data.user.UserID+'</option>');

        $('#ProjectContactIDs').prepend('<option value="' + data.userID + '">' + data.userName + '</option>');
        $("#form-primary-addpc").modal("hide");

        alert(data.successMsg);
    }
}

// To Update Project Contact List values.
function SetProjectContactListValues(companyId) {
    var url = '/Project/GetSelectList/';

    $.ajax({
        async: false,
        cache: false,
        type: 'POST',
        url: url,
        dataType: 'json',
        data: { companyID: companyId, optionType: 2 },
        success: function (result) {

            $('#ProjectContactIDs').empty();
            $.each(result, function (i, result) {
                $('#ProjectContactIDs').append('<option value="' + result.Value + '">' + result.Text + '</option>');
            });
        },

        error: function (ex) {
            alert('An error occurred.' + ex);
        }
    });
}

// To Update OEQ List values.
function SetOEQListValues(companyId) {
    var url = '/Project/GetSelectList/';

    $.ajax({
        async: false,
        cache: false,
        type: 'POST',
        url: url,
        dataType: 'json',
        data: { companyID: companyId, optionType: 4 },
        success: function (result) {

            $('#customoeq').empty();
            $('#customoeq').prepend('<option value=""> - Select Custom OEQ Template - </option>');
            $.each(result, function (i, result) {
                $('#customoeq').append('<option value="' + result.Value + '">' + result.Text + '</option>');
            });
        },

        error: function (ex) {
            alert('An error occurred during the process. Please try again later.' + ex);
        }
    });
}

// To Update Client List values.
function SetClientListValues(companyId) {
    var url = '/Project/GetSelectList/';

    $.ajax({
        async: false,
        cache: false,
        type: 'POST',
        url: url,
        dataType: 'json',
        data: { companyID: companyId, optionType: 6 },
        success: function (result) {

            $('#clientList').empty();
            $('#clientList').prepend('<option value=""> - Select - </option>');
            $.each(result, function (i, result) {
                $('#clientList').append('<option value="' + result.Value + '">' + result.Text + '</option>');
            });
        },

        error: function (ex) {
            alert('An error occurred during the process. Please try again later.' + ex);
        }
    });
}

// To Enable/Disable Form Controls.
function DisablingFormControls(isReadOnly, isDisableAddProjectContact) {

    if (isReadOnly == 'True') {
        $('.form-control').prop('disabled', true);
        $("#ProjectContactIDs").attr("disabled", "disabled");
        $("#addProjectContact a").attr("disabled", "disabled");
    }
    else {
        $('.form-control').prop('disabled', false);
        $("#ProjectContactIDs").removeAttr("disabled");

        if (isDisableAddProjectContact == 'True') {
            $("#addProjectContact a").attr("disabled", "disabled");
        }
        else {
            $("#addProjectContact a").removeAttr("disabled");
        }
    }
}