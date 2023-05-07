function AddCycle() {
    $('#CycleContent').html("");
    $('#CycleContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadCycleURL, {}, function (data) {
        $('#CycleContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditCycle(id) {
    var dataItem = id;
    $('#CycleContent').html("");
    $('#CycleContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadCycleURL, { CycleId: dataItem }, function (data) {
        $('#CycleContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveCycle() {
    if (!isValidateSaveCycle())
        return false;
    var myData = $('#Cycle-form').serialize();
    $.post(saveCycleUrl, myData, function (data) {
        $('#CycleContent').html(data);
        $("#Cycle_Title").val("");
    });
}

function EditTranslateCycle(id) {
    var dataItem = id;
    $('#CycleList').html("");
    $('#CycleList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateCycleURL, { TableId: dataItem.Cycle_ID, TableName: 'Cycle_BY_LANGUAGE' }, function (data) {
        $('#CycleList').html(data);
    });
}

function DeleteCycle(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(CycleDeleteURL, { CycleId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchCycle(false))
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

function SearchCycle(isRemoveAlert) {


    var Cyclename = $("#Cycle_Title").val();
    var languageid = $("#LANGUAGE_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#Cycle-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(CycleSearchURL, { CycleName: Cyclename, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#Cycle-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveCycle() {

    var isValid = true;
    var lst = ["#Cycle_Title", "#Company_ID"];
    var lstmsg = ["#Cycle_Title_ValidationMsg","#Company_ID_ValidationMsg"];

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


//translate Cycle
function EditTranslateCycle(id) {

    $('#CycleTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#CycleTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationCycleURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#CycleTranslate-List').html(data)
    });
}



//end translate

