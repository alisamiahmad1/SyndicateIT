function AddReligion() {
    $('#ReligionContent').html("");
    $('#ReligionContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadReligionURL, {}, function (data) {
        $('#ReligionContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditReligion(id) {
    var dataItem = id;
    $('#ReligionContent').html("");
    $('#ReligionContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadReligionURL, { ReligionId: dataItem }, function (data) {
        $('#ReligionContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveReligion() {
    if (!isValidateSaveReligion())
        return false;
    var myData = $('#Religion-form').serialize();
    $.post(saveReligionUrl, myData, function (data) {
        $('#ReligionContent').html(data);
        $("#Religion_Name").val("");
    });
}

function EditTranslateReligion(id) {
    var dataItem = id;
    $('#ReligionList').html("");
    $('#ReligionList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateReligionURL, { TableId: dataItem.Religion_ID, TableName: 'RELIGION_BY_LANGUAGE' }, function (data) {
        $('#ReligionList').html(data);
    });
}

function DeleteReligion(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(ReligionDeleteURL, { ReligionId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchReligion(false))
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

function SearchReligion(isRemoveAlert) {


    var Religionname = $("#Religion_Name").val();
    var languageid = $("#LANGUAGE_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#Religion-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(ReligionSearchURL, { ReligionName: Religionname, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#Religion-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveReligion() {

    var isValid = true;
    var lst = ["#Religion_Name"];
    var lstmsg = ["#Religion_Name_ValidationMsg"];

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


//translate Religion
function EditTranslateReligion(id) {

    $('#ReligionTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#ReligionTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationReligionURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#ReligionTranslate-List').html(data)
    });
}



//end translate

