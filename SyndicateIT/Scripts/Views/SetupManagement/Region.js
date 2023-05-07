function AddRegion() {
    $('#RegionContent').html("");
    $('#RegionContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadRegionURL, {}, function (data) {
        $('#RegionContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditRegion(id) {
    var dataItem = id;
    $('#RegionContent').html("");
    $('#RegionContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadRegionURL, { RegionId: dataItem }, function (data) {
        $('#RegionContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveRegion() {
    if (!isValidateSaveRegion())
        return false;
    var myData = $('#Region-form').serialize();
    $.post(saveRegionUrl, myData, function (data) {
        $('#RegionContent').html(data);
        $("#Region_Name").val("");
    });
}

function EditTranslateRegion(id) {
    var dataItem = id;
    $('#RegionList').html("");
    $('#RegionList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateRegionURL, { TableId: dataItem.Region_ID, TableName: 'Region_BY_LANGUAGE' }, function (data) {
        $('#RegionList').html(data);
    });
}

function DeleteRegion(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(RegionDeleteURL, { RegionId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchRegion(false))
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

function SearchRegion(isRemoveAlert) {


    var Regionname = $("#Region_Name").val();
    var languageid = $("#LANGUAGE_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#Region-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(RegionSearchURL, { RegionName: Regionname, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#Region-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveRegion() {

    var isValid = true;
    var lst = ["#Region_Name", "#Country_ID"];
    var lstmsg = ["#Region_Name_ValidationMsg", "#Country_ID_ValidationMsg"];

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


//translate Region
function EditTranslateRegion(id) {

    $('#RegionTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#RegionTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationRegionURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#RegionTranslate-List').html(data)
    });
}



//end translate

