function AddStatus() {
    $('#StatusContent').html("");
    $('#StatusContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadStatusURL, {}, function (data) {
        $('#StatusContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditStatus(id) {
    var dataItem = id;
    $('#StatusContent').html("");
    $('#StatusContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadStatusURL, { StatusId: dataItem }, function (data) {
        $('#StatusContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveStatus() {
    if (!isValidateSaveStatus())
        return false;
    var myData = $('#Status-form').serialize();
    $.post(saveStatusUrl, myData, function (data) {
        $('#StatusContent').html(data);
        $("#Status_Name").val("");
    });
}

function EditTranslateStatus(id) {
    var dataItem = id;
    $('#StatusList').html("");
    $('#StatusList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateStatusURL, { TableId: dataItem.Status_ID, TableName: 'STATUS_BY_LANGUAGE' }, function (data) {
        $('#StatusList').html(data);
    });
}

function DeleteStatus(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(StatusDeleteURL, { StatusId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchStatus(false))
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

function SearchStatus(isRemoveAlert) {


    var Statusname = $("#Status_Name").val();
    var languageid = $("#LANGUAGE_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#Status-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(StatusSearchURL, { StatusName: Statusname, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#Status-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveStatus() {

    var isValid = true;
    var lst = ["#Status_Name"];
    var lstmsg = ["#Status_Name_ValidationMsg"];

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


//translate Status
function EditTranslateStatus(id) {

    $('#StatusTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#StatusTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationStatusURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#StatusTranslate-List').html(data)
    });
}



//end translate

