function AddSubjects() {
    $('#SubjectsContent').html("");
    $('#SubjectsContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadSubjectsURL, {}, function (data) {
        $('#SubjectsContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditSubjects(id) {
    var dataItem = id;
    $('#SubjectsContent').html("");
    $('#SubjectsContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadSubjectsURL, { SubjectsId: dataItem }, function (data) {
        $('#SubjectsContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveSubjects() {
    if (!isValidateSaveSubjects())
        return false;
    var myData = $('#Subjects-form').serialize();
    $.post(saveSubjectsUrl, myData, function (data) {
        $('#SubjectsContent').html(data);
        $("#Subject_Name").val("");
    });
}

function EditTranslateSubjects(id) {
    var dataItem = id;
    $('#SubjectsList').html("");
    $('#SubjectsList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateSubjectsURL, { TableId: dataItem.Subject_ID, TableName: 'Subjects_By_Language' }, function (data) {
        $('#SubjectsList').html(data);
    });
}

function DeleteSubjects(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(SubjectsDeleteURL, { SubjectsId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchSubjects(false))
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

function SearchSubjects(isRemoveAlert) {


    var Subjectsname = $("#Subject_Name").val();
    var languageid = $("#LANGUAGE_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#Subjects-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(SubjectsSearchURL, { SubjectsName: Subjectsname, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#Subjects-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveSubjects() {

    var isValid = true;
    var lst = ["#Subject_Name"];
    var lstmsg = ["#Subject_Name_ValidationMsg"];

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


//translate Subjects
function EditTranslateSubjects(id) {

    $('#SubjectsTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#SubjectsTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationSubjectsURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#SubjectsTranslate-List').html(data)
    });
}



//end translate

