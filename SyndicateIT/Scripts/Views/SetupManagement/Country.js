function AddCountry() {
    $('#CountryContent').html("");
    $('#CountryContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadCountryURL, {}, function (data) {
        $('#CountryContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditCountry(id) {
    var dataItem = id;
    $('#CountryContent').html("");
    $('#CountryContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadCountryURL, { CountryId: dataItem }, function (data) {
        $('#CountryContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveCountry() {
    if (!isValidateSaveCountry())
        return false;
    var myData = $('#Country-form').serialize();
    $.post(saveCountryUrl, myData, function (data) {
        $('#CountryContent').html(data);
        $("#Country_NAME").val("");
    });
}

function EditTranslateCountry(id) {
    var dataItem = id;
    $('#CountryList').html("");
    $('#CountryList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateCountryURL, { TableId: dataItem.Country_ID, TableName: 'COUNTRY_BY_LANGUAGE' }, function (data) {
        $('#CountryList').html(data);
    });
}

function DeleteCountry(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(CountryDeleteURL, { CountryId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchCountry(false))
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

function SearchCountry(isRemoveAlert) {


    var Countryname = $("#Country_NAME").val();
    var languageid = $("#LANGUAGE_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#Country-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(CountrySearchURL, { CountryName: Countryname, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#Country-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveCountry() {

    var isValid = true;
    var lst = ["#Country_NAME"];
    var lstmsg = ["#Country_NAME_ValidationMsg"];

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


//translate Country
function EditTranslateCountry(id) {

    $('#CountryTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#CountryTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationCountryURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#CountryTranslate-List').html(data)
    });
}



//end translate
