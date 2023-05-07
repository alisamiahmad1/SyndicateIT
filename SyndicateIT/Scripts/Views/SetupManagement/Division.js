function FilterSearchClass() {
    return { Cycle_id: $("#Cycle_ID").data("kendoDropDownList").value() };
}

function AddDivision() {
    $('#DivisionContent').html("");
    $('#DivisionContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadDivisionURL, {}, function (data) {
        $('#DivisionContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditDivision(id) {
    var dataItem = id;
    $('#DivisionContent').html("");
    $('#DivisionContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadDivisionURL, { DivisionId: dataItem }, function (data) {
        $('#DivisionContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveDivision() {
    if (!isValidateSaveDivision())
        return false;
    var myData = $('#Division-form').serialize();
    $.post(saveDivisionUrl, myData, function (data) {
        $('#DivisionContent').html(data);
        $("#Division_Name").val("");
    });
}

function EditTranslateDivision(id) {
    var dataItem = id;
    $('#DivisionList').html("");
    $('#DivisionList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateDivisionURL, { TableId: dataItem.Division_ID, TableName: 'Division_By_Language_ID' }, function (data) {
        $('#DivisionList').html(data);
    });
}

function DeleteDivision(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(DivisionDeleteURL, { DivisionId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchDivision(false))
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

function SearchDivision(isRemoveAlert) {

    var Cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
    var Class_ID = $("#Class_ID").data("kendoDropDownList").value();
    var Division_Name = $("#Division_Name").val();
    var Language_ID = $("#Language_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#Division-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(DivisionSearchURL, { Cycle_ID: Cycle_ID, Class_ID: Class_ID, DivisionName: Division_Name, Language_ID: Language_ID, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#Division-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveDivision() {

    var isValid = true;
    var lst = ["#Division_Name", "#Class_ID"];
    var lstmsg = ["#Division_Name_ValidationMsg", "#Class_ID_ValidationMsg"];

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


//translate Division
function EditTranslateDivision(id) {

    $('#DivisionTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#DivisionTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationDivisionURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#DivisionTranslate-List').html(data)
    });
}



//end translate

