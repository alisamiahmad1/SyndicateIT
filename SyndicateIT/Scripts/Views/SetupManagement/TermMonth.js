"use strict";
function FilterClass() {
    return { Cycle_ID: $("#Cycle_ID").data("kendoDropDownList").value() };
}
function FilterDivision() {
    return { Class_ID: $("#Class_ID").data("kendoDropDownList").value() };
}
function FilterCourse() {
    return { Division_ID: $("#Division_ID").data("kendoDropDownList").value() };
}
function AddTermMonth() {
    $('#TermMonthContent').html("");
    $('#TermMonthContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTermMonthURL, {}, function (data) {
        $('#TermMonthContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditTermMonth(id) {
    var dataItem = id;
    $('#TermMonthContent').html("");
    $('#TermMonthContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTermMonthURL, { TermMonthId: dataItem }, function (data) {
        $('#TermMonthContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveTermMonth() {
    if (!isValidateSaveTermMonth())
        return false;
    var myData = $('#TermMonth-form').serialize();
    $.post(saveTermMonthUrl, myData, function (data) {
        $('#TermMonthContent').html(data);
        $("#TermMonth_Name").val("");
    });
}

function EditTranslateTermMonth(id) {
    var dataItem = id;
    $('#TermMonthList').html("");
    $('#TermMonthList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateTermMonthURL, { TableId: dataItem.Relation_Type_ID, TableName: 'TBL_TermMonth_By_Language' }, function (data) {
        $('#TermMonthList').html(data);
    });
}

function DeleteTermMonth(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(TermMonthDeleteURL, { TermMonthID: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchTermMonth(false))
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

function SearchTermMonth(isRemoveAlert) {


   
    var termId = $("#Term_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#TermMonth-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(TermMonthSearchURL, { Term_ID: termId, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#TermMonth-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveTermMonth() {

    var isValid = true;
    var lst = ["#TermMonth_Name"];
    var lstmsg = ["#TermMonth_Name_ValidationMsg"];

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


//translate TermMonth
function EditTranslateTermMonth(id) {

    $('#TermMonthTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#TermMonthTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationTermMonthURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#TermMonthTranslate-List').html(data)
    });
}



//end translate

