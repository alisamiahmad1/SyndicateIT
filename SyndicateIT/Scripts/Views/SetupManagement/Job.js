function AddJob() {
    $('#JobContent').html("");
    $('#JobContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadJobURL, {}, function (data) {
        $('#JobContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditJob(id) {
    var dataItem = id;
    $('#JobContent').html("");
    $('#JobContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadJobURL, { JobId: dataItem }, function (data) {
        $('#JobContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveJob() {
    if (!isValidateSaveJob())
        return false;
    var myData = $('#Job-form').serialize();
    $.post(saveJobUrl, myData, function (data) {
        $('#JobContent').html(data);
        $("#JOB_NAME").val("");
    });
}

function EditTranslateJob(id) {
    var dataItem = id;
    $('#JobList').html("");
    $('#JobList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateJobURL, { TableId: dataItem.JOB_ID, TableName: 'JOB_BY_LANGUAGE' }, function (data) {
        $('#JobList').html(data);
    });
}

function DeleteJob(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(JobDeleteURL, { JobId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchJob(false))
                         .then(function () { $("#AlertTypeId").html(data.Alert) });

                    } else {
                        $("#AlertTypeId").html(data.Alert);
                    }
                });
            } else {
                return false;
            }
        }
    })
}

function SearchJob(isRemoveAlert) {


    var Jobname = $("#JOB_NAME").val();
    var languageid = $("#LANGUAGE_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#Job-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(JobSearchURL, { JobName: Jobname, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#Job-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveJob() {

    var isValid = true;
    var lst = ["#JOB_NAME"];
    var lstmsg = ["#JOB_NAME_ValidationMsg"];

    for (var i = 0; i < lst.length ; i++) {
        var input = $(lst[i]);
        if (input.data("role") == "dropdownlist") {
            input = $(input).data("kendoDropDownList").value();
        }
        else {
            input = $(input).val();
        }

        if (input == "" || input == -1) {
            $(lstmsg[i]).show();
            isValid = false;
            $(lstmsg[i]).parent().addClass("state-error");
        }
        else {
            $(lstmsg[i]).hide();
            if ($(lstmsg[i]).parent().hasClass("state-error")) {
                $(lstmsg[i]).parent().removeClass("state-error");

            }
        }

    }
    return isValid;
}


//translate Job
function EditTranslateJob(id) {

    $('#JobTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#JobTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationJobURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#JobTranslate-List').html(data)
    });
}



//end translate

