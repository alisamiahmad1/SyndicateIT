function AddEvent() {
    $('#EventContent').html("");
    $('#EventContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadEventURL, {}, function (data) {
        $('#EventContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditEvent(id) {
    var dataItem = id;
    $('#EventContent').html("");
    $('#EventContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadEventURL, { EventId: dataItem }, function (data) {
        $('#EventContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveEvent() {
    if (!isValidateSaveEvent())
        return false;
    var myData = $('#Event-form').serialize();
    $.post(saveEventUrl, myData, function (data) {
        $('#EventContent').html(data);
        $("#Event_Title").val("");
    });
}

function EditTranslateEvent(id) {
    var dataItem = id;
    $('#EventList').html("");
    $('#EventList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateEventURL, { TableId: dataItem.Event_Id, TableName: 'EventByLanguage' }, function (data) {
        $('#EventList').html(data);
    });
}

function DeleteEvent(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(EventDeleteURL, { EventId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchEvent(false))
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

function SearchEvent(isRemoveAlert) {


    var Eventname = $("#Event_Title").val();
    var languageid = $("#LANGUAGE_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#Event-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(EventSearchURL, { EventName: Eventname, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#Event-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveEvent() {

    var isValid = true;
    var lst = ["#Event_Title"];
    var lstmsg = ["#Event_Title_ValidationMsg"];

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


//translate Event
function EditTranslateEvent(id) {

    $('#EventTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#EventTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationEventURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#EventTranslate-List').html(data)
    });
}



//end translate

