function AddEventType() {
    $('#EventTypeContent').html("");
    $('#EventTypeContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadEventTypeURL, {}, function (data) {
        $('#EventTypeContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditEventType(id) {
    var dataItem = id;
    $('#EventTypeContent').html("");
    $('#EventTypeContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadEventTypeURL, { EventTypeId: dataItem }, function (data) {
        $('#EventTypeContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveEventType() {
    if (!isValidateSaveEventType())
        return false;
    var myData = $('#EventType-form').serialize();
    $.post(saveEventTypeUrl, myData, function (data) {
        $('#EventTypeContent').html(data);
        $("#EventType_Title").val("");
    });
}

function EditTranslateEventType(id) {
    var dataItem = id;
    $('#EventTypeList').html("");
    $('#EventTypeList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateEventTypeURL, { TableId: dataItem.EventType_Id, TableName: 'EventTypeByLanguage' }, function (data) {
        $('#EventTypeList').html(data);
    });
}

function DeleteEventType(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(EventTypeDeleteURL, { EventTypeId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchEventType(false))
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

function SearchEventType(isRemoveAlert) {


    var EventTypename = $("#EventType_Title").val();
    var languageid = $("#LANGUAGE_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#EventType-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(EventTypeSearchURL, { EventTypeName: EventTypename, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#EventType-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveEventType() {

    var isValid = true;
    var lst = ["#EventType_Title"];
    var lstmsg = ["#EventType_Title_ValidationMsg"];

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


//translate EventType
function EditTranslateEventType(id) {

    $('#EventTypeTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#EventTypeTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationEventTypeURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#EventTypeTranslate-List').html(data)
    });
}



//end translate

