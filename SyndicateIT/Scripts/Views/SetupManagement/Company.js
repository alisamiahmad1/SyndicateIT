function AddCompany() {
    $('#CompanyContent').html("");
    $('#CompanyContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadCompanyURL, {}, function (data) {
        $('#CompanyContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditCompany(id) {
    var dataItem = id;
    $('#CompanyContent').html("");
    $('#CompanyContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadCompanyURL, { CompanyId: dataItem }, function (data) {
        $('#CompanyContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveCompany() {
    if (!isValidateSaveCompany())
        return false;
    var myData = $('#Company-form').serialize();
    $.post(saveCompanyUrl, myData, function (data) {
        $('#CompanyContent').html(data);
        $("#Company_Name").val("");
    });
}

function EditTranslateCompany(id) {
    var dataItem = id;
    $('#CompanyList').html("");
    $('#CompanyList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateCompanyURL, { TableId: dataItem.Company_ID, TableName: 'Company_By_Language' }, function (data) {
        $('#CompanyList').html(data);
    });
}

function DeleteCompany(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(CompanyDeleteURL, { CompanyId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchCompany(false))
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

function SearchCompany(isRemoveAlert) {


    var Companyname = $("#Company_Name").val();
    var languageid = $("#language").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#Company-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(CompanySearchURL, { CompanyName: Companyname, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#Company-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveCompany() {

    var isValid = true;
    var lst = ["#Company_Name"];
    var lstmsg = ["#Company_Name_ValidationMsg"];

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


//translate Company
function EditTranslateCompany(id) {

    $('#CompanyTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#CompanyTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationCompanyURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#CompanyTranslate-List').html(data)
    });
}



//end translate

