function AddEducationalsystem() {
    $('#EducationalsystemContent').html("");
    $('#EducationalsystemContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadEducationalsystemURL, {}, function (data) {
        $('#EducationalsystemContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditEducationalsystem(id) {
    var dataItem = id;
    $('#EducationalsystemContent').html("");
    $('#EducationalsystemContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadEducationalsystemURL, { EducationalsystemId: dataItem }, function (data) {
        $('#EducationalsystemContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveEducationalsystem() {
    if (!isValidateSaveEducationalsystem())
        return false;
    var myData = $('#Educationalsystem-form').serialize();
    $.post(saveEducationalsystemUrl, myData, function (data) {
        $('#EducationalsystemContent').html(data);
        $("#Educationalsystem_Name").val("");
    });
}

function EditTranslateEducationalsystem(id) {
    var dataItem = id;
    $('#EducationalsystemList').html("");
    $('#EducationalsystemList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateEducationalsystemURL, { TableId: dataItem.Educationalsystem_ID, TableName: 'Educationalsystem_By_Language' }, function (data) {
        $('#EducationalsystemList').html(data);
    });
}

function DeleteEducationalsystem(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(EducationalsystemDeleteURL, { EducationalsystemId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchEducationalsystem(false))
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

function SearchEducationalsystem(isRemoveAlert) {


    var Educationalsystemname = $("#Educationalsystem_Name").val();
    var languageid = $("#LANGUAGE_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#Educationalsystem-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(EducationalsystemSearchURL, { EducationalsystemName: Educationalsystemname, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#Educationalsystem-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveEducationalsystem() {

    var isValid = true;
    var lst = ["#Educationalsystem_Name"];
    var lstmsg = ["#Educationalsystem_Name_ValidationMsg"];

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


//translate Educationalsystem
function EditTranslateEducationalsystem(id) {

    $('#EducationalsystemTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#EducationalsystemTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationEducationalsystemURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#EducationalsystemTranslate-List').html(data)
    });
}



//end translate

