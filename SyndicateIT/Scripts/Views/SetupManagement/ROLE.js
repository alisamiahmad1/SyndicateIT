function AddROLE() {
    $('#ROLEContent').html("");
    $('#ROLEContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadROLEURL, {}, function (data) {
        $('#ROLEContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditROLE(id) {
    var dataItem = id;
    $('#ROLEContent').html("");
    $('#ROLEContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadROLEURL, { ROLEId: dataItem }, function (data) {
        $('#ROLEContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveROLE() {
    if (!isValidateSaveROLE())
        return false;
    var myData = $('#ROLE-form').serialize();
    $.post(saveROLEUrl, myData, function (data) {
        $('#ROLEContent').html(data);
        $("#ROLE_NAME").val("");
    });
}

function EditTranslateROLE(id) {
    var dataItem = id;
    $('#ROLEList').html("");
    $('#ROLEList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateROLEURL, { TableId: dataItem.Role_ID, TableName: 'ROLE_BY_LANGUAGE' }, function (data) {
        $('#ROLEList').html(data);
    });
}

function DeleteROLE(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(ROLEDeleteURL, { ROLEId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchROLE(false))
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

function SearchROLE(isRemoveAlert) {


    var ROLEname = $("#ROLE_NAME").val();
    var languageid = $("#LANGUAGE_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#ROLE-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(ROLESearchURL, { ROLEName: ROLEname, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#ROLE-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveROLE() {

    var isValid = true;
    var lst = ["#ROLE_NAME","#Company_ID"];
    var lstmsg = ["#ROLE_NAME_ValidationMsg", "#Company_ID_ValidationMsg"];

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


//translate ROLE
function EditTranslateROLE(id) {

    $('#ROLETranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#ROLETranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationROLEURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#ROLETranslate-List').html(data)
    });
}



//end translate

