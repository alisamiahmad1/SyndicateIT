function AddClasses() {
    $('#ClassesContent').html("");
    $('#ClassesContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadClassesURL, {}, function (data) {
        $('#ClassesContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditClasses(id) {
    var dataItem = id;
    $('#ClassesContent').html("");
    $('#ClassesContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadClassesURL, { ClassesId: dataItem }, function (data) {
        $('#ClassesContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveClasses() {
    if (!isValidateSaveClasses())
        return false;
    var myData = $('#Classes-form').serialize();
    $.post(saveClassesUrl, myData, function (data) {
        $('#ClassesContent').html(data);
        $("#Class_Title").val("");
    });
}

function EditTranslateClasses(id) {
    var dataItem = id;
    $('#ClassesList').html("");
    $('#ClassesList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateClassesURL, { TableId: dataItem.Class_ID, TableName: 'Classes_By_Language' }, function (data) {
        $('#ClassesList').html(data);
    });
}

function DeleteClasses(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(ClassesDeleteURL, { ClassesId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchClasses(false))
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

function SearchClasses(isRemoveAlert) {

    var Cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
    var Classesname = $("#Class_Title").val();
    var languageid = $("#language").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#Classes-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(ClassesSearchURL, { Cycle_ID: Cycle_ID, ClassesName: Classesname, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#Classes-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveClasses() {

    var isValid = true;
    var lst = ["#Class_Title", "#Cycle_ID", "#Company_ID", "#Class_Row", "#Class_Column"];
    var lstmsg = ["#Class_Title_ValidationMsg", "#Cycle_ID_ValidationMsg", "#Company_ID_ValidationMsg", "#Class_Row_ValidationMsg", "#Class_Column_ValidationMsg"];

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


//translate Classes
function EditTranslateClasses(id) {

    $('#ClassesTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#ClassesTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationClassesURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#ClassesTranslate-List').html(data)
    });
}



//end translate

