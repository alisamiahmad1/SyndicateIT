"use strict";
function FilterCountry() {
    return { CountryID: $("#Country_ID").data("kendoDropDownList").value() };
}
function FilterRegion() {
    return { RegionID: $("#Region_ID").data("kendoDropDownList").value() };
}

function AddPlace() {
    $('#PlaceContent').html("");
    $('#PlaceContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadPlaceURL, {}, function (data) {
        $('#PlaceContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditPlace(id) {
    var dataItem = id;
    $('#PlaceContent').html("");
    $('#PlaceContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadPlaceURL, { PlaceId: dataItem }, function (data) {
        $('#PlaceContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SavePlace() {
    if (!isValidateSavePlace())
        return false;
    var myData = $('#Place-form').serialize();
    $.post(savePlaceUrl, myData, function (data) {
        $('#PlaceContent').html(data);
        $("#Place_Name").val("");
    });
}

function EditTranslatePlace(id) {
    var dataItem = id;
    $('#PlaceList').html("");
    $('#PlaceList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslatePlaceURL, { TableId: dataItem.Place_ID, TableName: 'Place_BY_LANGUAGE' }, function (data) {
        $('#PlaceList').html(data);
    });
}

function DeletePlace(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(PlaceDeleteURL, { PlaceId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchPlace(false))
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

function SearchPlace(isRemoveAlert) {


    var Placename = $("#Place_Name").val();
    var languageid = $("#LANGUAGE_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#Place-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(PlaceSearchURL, { PlaceName: Placename, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#Place-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSavePlace() {

    var isValid = true;
    var lst = ["#Place_Name", "#COUNTRY_ID", "#Country_ID", "#Region_ID", "#Kaddaa_ID"];
    var lstmsg = ["#Place_Name_ValidationMsg", "#Country_ID_ValidationMsg", , "#Region_ID_ValidationMsg", "#Kaddaa_ID_ValidationMsg"];

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


//translate Place
function EditTranslatePlace(id) {

    $('#PlaceTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#PlaceTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationPlaceURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#PlaceTranslate-List').html(data)
    });
}



//end translate

