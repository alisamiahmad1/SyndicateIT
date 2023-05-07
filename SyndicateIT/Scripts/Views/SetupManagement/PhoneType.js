function AddPhoneType() {
    $('#PhoneTypeContent').html("");
    $('#PhoneTypeContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadPhoneTypeURL, {}, function (data) {
        $('#PhoneTypeContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditPhoneType(id) {
    var dataItem = id;
    $('#PhoneTypeContent').html("");
    $('#PhoneTypeContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadPhoneTypeURL, { PhoneTypeId: dataItem }, function (data) {
        $('#PhoneTypeContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SavePhoneType() {
    if (!isValidateSavePhoneType())
        return false;
    var myData = $('#PhoneType-form').serialize();
    $.post(savePhoneTypeUrl, myData, function (data) {
        $('#PhoneTypeContent').html(data);
        $("#Phone_Type_Title").val("");
    });
}

function EditTranslatePhoneType(id) {
    var dataItem = id;
    $('#PhoneTypeList').html("");
    $('#PhoneTypeList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslatePhoneTypeURL, { TableId: dataItem.Phone_Type_ID, TableName: 'Phone_Type_BY_LANGUAGE' }, function (data) {
        $('#PhoneTypeList').html(data);
    });
}

function DeletePhoneType(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(PhoneTypeDeleteURL, { PhoneTypeId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchPhoneType(false))
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

function SearchPhoneType(isRemoveAlert) {


    var PhoneTypename = $("#Phone_Type_Title").val();
    var languageid = $("#LANGUAGE_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#PhoneType-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(PhoneTypeSearchURL, { PhoneTypeName: PhoneTypename, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#PhoneType-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSavePhoneType() {

    var isValid = true;
    var lst = ["#Phone_Type_Title"];
    var lstmsg = ["#Phone_Type_Title_ValidationMsg"];

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


//translate PhoneType
function EditTranslatePhoneType(id) {

    $('#PhoneTypeTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#PhoneTypeTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationPhoneTypeURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#PhoneTypeTranslate-List').html(data)
    });
}



//end translate

