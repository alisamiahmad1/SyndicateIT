function AddBus() {
    $('#BusContent').html("");
    $('#BusContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadBusURL, {}, function (data) {
        $('#BusContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}



function EditBus(id) {
    var dataItem = id;
    $('#BusContent').html("");
    $('#BusContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadBusURL, { BusId: dataItem }, function (data) {
        $('#BusContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveBus() {
    if (!isValidateSaveBus())
        return false;
    var myData = $('#Bus-form').serialize();
    $.post(saveBusUrl, myData, function (data) {
        $('#BusContent').html(data);
        $("#Bus_Name").val("");
    });
}

function EditTranslateBus(id) {
    var dataItem = id;
    $('#BusList').html("");
    $('#BusList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateBusURL, { TableId: dataItem.Bus_ID, TableName: 'Bus_By_Language' }, function (data) {
        $('#BusList').html(data);
    });
}

function DeleteBus(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(BusDeleteURL, { BusId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchBus(false))
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

function SearchBus(isRemoveAlert) {


    var Busname = $("#Bus_Name").val();
    var languageid = $("#language").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#Bus-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(BusSearchURL, { BusName: Busname, languageid: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#Bus-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveBus() {

    var isValid = true;
    var lst = ["#Bus_Name", "#Bus_Number", "#Bus_Area", "#Company_ID", "#Bus_Platenumber", "#Bus_Name", "#Driver_Name", "#Phone_Number"];
    var lstmsg = ["#Bus_Name_ValidationMsg", "#Bus_Number_ValidationMsg", "#Bus_Area_ValidationMsg", "#Company_ID_ValidationMsg", "#Bus_Platenumber_ValidationMsg", "#Driver_Name_ValidationMsg", "#Phone_Number_ValidationMsg"];

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


//translate Bus
function EditTranslateBus(id) {

    $('#BusTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#BusTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationBusURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#BusTranslate-List').html(data)
    });
}



//end translate

