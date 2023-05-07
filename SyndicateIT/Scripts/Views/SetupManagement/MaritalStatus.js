function AddMaritalStatus() {
    $('#MaritalStatusContent').html("");
    $('#MaritalStatusContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadMaritalStatusURL, {}, function (data) {
        $('#MaritalStatusContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditMaritalStatus(id) {
    var dataItem = id;
    $('#MaritalStatusContent').html("");
    $('#MaritalStatusContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadMaritalStatusURL, { MaritalStatusId: dataItem }, function (data) {
        $('#MaritalStatusContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveMaritalStatus() {
    if (!isValidateSaveMaritalStatus())
        return false;
    var myData = $('#MaritalStatus-form').serialize();
    $.post(saveMaritalStatusUrl, myData, function (data) {
        $('#MaritalStatusContent').html(data);
        $("#Marital_Status_Title").val("");
    });
}

function EditTranslateMaritalStatus(id) {
    var dataItem = id;
    $('#MaritalStatusList').html("");
    $('#MaritalStatusList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateMaritalStatusURL, { TableId: dataItem.Marital_Status_ID, TableName: 'Marital_Status_By_Language' }, function (data) {
        $('#MaritalStatusList').html(data);
    });
}

function DeleteMaritalStatus(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(MaritalStatusDeleteURL, { MaritalStatusId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchMaritalStatus(false))
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

function SearchMaritalStatus(isRemoveAlert) {


    var MaritalStatusname = $("#Marital_Status_Title").val();
    var languageid = $("#LANGUAGE_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#MaritalStatus-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(MaritalStatusSearchURL, { MaritalStatusName: MaritalStatusname, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#MaritalStatus-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveMaritalStatus() {

    var isValid = true;
    var lst = ["#Marital_Status_Title"];
    var lstmsg = ["#Marital_Status_Title_ValidationMsg"];

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


//translate MaritalStatus
function EditTranslateMaritalStatus(id) {

    $('#MaritalStatusTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#MaritalStatusTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationMaritalStatusURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#MaritalStatusTranslate-List').html(data)
    });
}



//end translate

