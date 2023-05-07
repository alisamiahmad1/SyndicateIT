function AddTerm() {
    $('#TermContent').html("");
    $('#TermContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTermURL, {}, function (data) {
        $('#TermContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditTerm(id) {
    var dataItem = id;
    $('#TermContent').html("");
    $('#TermContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTermURL, { TermId: dataItem }, function (data) {
        $('#TermContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveTerm() {
    if (!isValidateSaveTerm())
        return false;
    var myData = $('#Term-form').serialize();
    $.post(saveTermUrl, myData, function (data) {
        $('#TermContent').html(data);
        $("#Term_Name").val("");
    });
}

function EditTranslateTerm(id) {
    var dataItem = id;
    $('#TermList').html("");
    $('#TermList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateTermURL, { TableId: dataItem.Relation_Type_ID, TableName: 'TBL_Term_By_Language' }, function (data) {
        $('#TermList').html(data);
    });
}

function DeleteTerm(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(TermDeleteURL, { TermID: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchTerm(false))
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

function SearchTerm(isRemoveAlert) {


    var Termname = $("#Term_Name").val();
    var languageid = $("#LANGUAGE_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#Term-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(TermSearchURL, { TermName: Termname, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#Term-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveTerm() {

    var isValid = true;
    var lst = ["#Term_Name"];
    var lstmsg = ["#Term_Name_ValidationMsg"];

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


//translate Term
function EditTranslateTerm(id) {

    $('#TermTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#TermTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationTermURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#TermTranslate-List').html(data)
    });
}



//end translate

