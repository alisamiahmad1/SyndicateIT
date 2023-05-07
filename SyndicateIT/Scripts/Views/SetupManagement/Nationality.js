function AddNationality() {
    $('#NationalityContent').html("");
    $('#NationalityContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadNationalityURL, {}, function (data) {
        $('#NationalityContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditNationality(id) {
    var dataItem = id;
    $('#NationalityContent').html("");
    $('#NationalityContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadNationalityURL, { NationalityID: dataItem }, function (data) {
        $('#NationalityContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveNationality() {
    if (!isValidateSaveNationality())
        return false;
    var myData = $('#Nationality-form').serialize();
    $.post(saveNationalityUrl, myData, function (data) {
        $('#NationalityContent').html(data);
        $("#Nationality_Title").val("");
    });
}

function EditTranslateNationality(id) {
    var dataItem = id;
    $('#NationalityList').html("");
    $('#NationalityList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateNationalityURL, { TableId: dataItem.Nationality_ID, TableName: 'TBL_Nationality_By_Language' }, function (data) {
        $('#NationalityList').html(data);
    });
}

function DeleteNationality(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(NationalityDeleteURL, { Nationality_ID: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchNationality(false))
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

function SearchNationality(isRemoveAlert) {


    var Nationality_Title = $("#Nationality_Title").val();
    var languageid = $("#LANGUAGE_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#Nationality-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(NationalitySearchURL, { Nationality_Title: Nationality_Title, languageId: languageid, IS_ACTIVE: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#Nationality-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveNationality() {

    var isValid = true;
    var lst = ["#Nationality_Title"];
    var lstmsg = ["#NATIONALITY_NAME_ValidationMsg"];

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


//translate Nationality
function EditTranslateNationality(id) {

    $('#NationalityTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#NationalityTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationNationalityURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#NationalityTranslate-List').html(data)
    });
}



//end translate

