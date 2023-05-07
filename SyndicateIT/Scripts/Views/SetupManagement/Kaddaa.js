"use strict";
function FilterCountry() {
    return { CountryID: $("#COUNTRY_ID").data("kendoDropDownList").value() };
}
function AddKaddaa() {
    $('#KaddaaContent').html("");
    $('#KaddaaContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadKaddaaURL, {}, function (data) {
        $('#KaddaaContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditKaddaa(id) {
    var dataItem = id;
    $('#KaddaaContent').html("");
    $('#KaddaaContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadKaddaaURL, { KaddaaId: dataItem }, function (data) {
        $('#KaddaaContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveKaddaa() {
    if (!isValidateSaveKaddaa())
        return false;
    var myData = $('#Kaddaa-form').serialize();
    $.post(saveKaddaaUrl, myData, function (data) {
        $('#KaddaaContent').html(data);
        $("#Kaddaa_NAME").val("");
    });
}

function EditTranslateKaddaa(id) {
    var dataItem = id;
    $('#KaddaaList').html("");
    $('#KaddaaList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateKaddaaURL, { TableId: dataItem.STP_Kaddaa_ID, TableName: 'Kaddaa_BY_LANGUAGE' }, function (data) {
        $('#KaddaaList').html(data);
    });
}

function DeleteKaddaa(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(KaddaaDeleteURL, { KaddaaId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchKaddaa(false))
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

function SearchKaddaa(isRemoveAlert) {


    var Kaddaaname = $("#Kaddaa_NAME").val();
    var languageid = $("#LANGUAGE_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#Kaddaa-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(KaddaaSearchURL, { KaddaaName: Kaddaaname, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#Kaddaa-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveKaddaa() {

    var isValid = true;
    var lst = ["#Kaddaa_NAME", "#COUNTRY_ID", "#Country_ID", "#Region_ID"];
    var lstmsg = ["#Kaddaa_NAME_ValidationMsg", "#Country_ID_ValidationMsg",  "#Region_ID_ValidationMsg"];

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


//translate Kaddaa
function EditTranslateKaddaa(id) {

    $('#KaddaaTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#KaddaaTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationKaddaaURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#KaddaaTranslate-List').html(data)
    });
}



//end translate

