function AddShift() {
    $('#ShiftContent').html("");
    $('#ShiftContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadShiftURL, {}, function (data) {
        $('#ShiftContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditShift(id) {
    var dataItem = id;
    $('#ShiftContent').html("");
    $('#ShiftContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadShiftURL, { ShiftId: dataItem }, function (data) {
        $('#ShiftContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveShift() {
    if (!isValidateSaveShift())
        return false;
    var myData = $('#Shift-form').serialize();
    $.post(saveShiftUrl, myData, function (data) {
        $('#ShiftContent').html(data);
        $("#SHIFT_NAME").val("");
    });
}

function EditTranslateShift(id) {
    var dataItem = id;
    $('#ShiftList').html("");
    $('#ShiftList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateShiftURL, { TableId: dataItem.SHIFT_ID, TableName: 'SHIFT_BY_LANGUAGE' }, function (data) {
        $('#ShiftList').html(data);
    });
}

function DeleteShift(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(ShiftDeleteURL, { ShiftId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchShift(false))
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

function SearchShift(isRemoveAlert) {


    var Shiftname = $("#SHIFT_NAME").val();
    var languageid = $("#LANGUAGE_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#Shift-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(ShiftSearchURL, { ShiftName: Shiftname, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#Shift-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveShift() {

    var isValid = true;
    var lst = ["#SHIFT_NAME"];
    var lstmsg = ["#SHIFT_NAME_ValidationMsg"];

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


//translate Shift
function EditTranslateShift(id) {

    $('#ShiftTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#ShiftTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationShiftURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#ShiftTranslate-List').html(data)
    });
}



//end translate

