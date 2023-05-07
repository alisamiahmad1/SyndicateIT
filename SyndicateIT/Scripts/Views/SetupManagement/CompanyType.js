function AddCompanyType() {
    $('#CompanyTypeContent').html("");
    $('#CompanyTypeContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadCompanyTypeURL, {}, function (data) {
        $('#CompanyTypeContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditCompanyType(id) {
    var dataItem = id;
    $('#CompanyTypeContent').html("");
    $('#CompanyTypeContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadCompanyTypeURL, { CompanyTypeId: dataItem }, function (data) {
        $('#CompanyTypeContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveCompanyType() {
    if (!isValidateSaveCompanyType())
        return false;
    var myData = $('#CompanyType-form').serialize();
    $.post(saveCompanyTypeUrl, myData, function (data) {
        $('#CompanyTypeContent').html(data);
        $("#Company_Type_Title").val("");
    });
}

function EditTranslateCompanyType(id) {
    var dataItem = id;
    $('#CompanyTypeList').html("");
    $('#CompanyTypeList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateCompanyTypeURL, { TableId: dataItem.Company_Type_ID, TableName: 'CompanyType_BY_LANGUAGE' }, function (data) {
        $('#CompanyTypeList').html(data);
    });
}

function DeleteCompanyType(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(CompanyTypeDeleteURL, { CompanyTypeId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchCompanyType(false))
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

function SearchCompanyType(isRemoveAlert) {


    var CompanyTypename = $("#Company_Type_Title").val();
    var languageid = $("#LANGUAGE_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#CompanyType-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(CompanyTypeSearchURL, { CompanyTypeName: CompanyTypename, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#CompanyType-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveCompanyType() {

    var isValid = true;
    var lst = ["#Company_Type_Title"];
    var lstmsg = ["#Company_Type_Title_ValidationMsg"];

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


//translate CompanyType
function EditTranslateCompanyType(id) {

    $('#CompanyTypeTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#CompanyTypeTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationCompanyTypeURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#CompanyTypeTranslate-List').html(data)
    });
}



//end translate

