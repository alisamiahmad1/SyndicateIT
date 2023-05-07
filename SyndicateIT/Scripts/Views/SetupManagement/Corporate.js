function AddCorporate() {
    $('#CorporateContent').html("");
    $('#CorporateContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadCorporateURL, {}, function (data) {
        $('#CorporateContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditCorporate(id) {
    var dataItem = id;
    $('#CorporateContent').html("");
    $('#CorporateContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadCorporateURL, { CorporateId: dataItem }, function (data) {
        $('#CorporateContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveCorporate() {
    if (!isValidateSaveCorporate())
        return false;
    var myData = $('#Corporate-form').serialize();
    $.post(saveCorporateUrl, myData, function (data) {
        $('#CorporateContent').html(data);
        $("#Name").val("");
    });
}

function EditTranslateCorporate(id) {
    var dataItem = id;
    $('#CorporateList').html("");
    $('#CorporateList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateCorporateURL, { TableId: dataItem.ID, TableName: 'Corporate_BY_LANGUAGE' }, function (data) {
        $('#CorporateList').html(data);
    });
}

function DeleteCorporate(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(CorporateDeleteURL, { CorporateId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchCorporate(false))
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

function SearchCorporate(isRemoveAlert) {


    var Corporatename = $("#Name").val();
    var languageid = $("#LANGUAGE_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#Corporate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(CorporateSearchURL, { CorporateName: Corporatename, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#Corporate-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveCorporate() {

    var isValid = true;
    var lst = ["#Name"];
    var lstmsg = ["#Name_ValidationMsg"];

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


//translate Corporate
function EditTranslateCorporate(id) {

    $('#CorporateTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#CorporateTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationCorporateURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#CorporateTranslate-List').html(data)
    });
}



//end translate

