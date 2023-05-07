function AddRelationTypes() {
    $('#RelationTypesContent').html("");
    $('#RelationTypesContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadRelationTypesURL, {}, function (data) {
        $('#RelationTypesContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditRelationTypes(id) {
    var dataItem = id;
    $('#RelationTypesContent').html("");
    $('#RelationTypesContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadRelationTypesURL, { RelationTypesId: dataItem }, function (data) {
        $('#RelationTypesContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveRelationTypes() {
    if (!isValidateSaveRelationTypes())
        return false;
    var myData = $('#RelationTypes-form').serialize();
    $.post(saveRelationTypesUrl, myData, function (data) {
        $('#RelationTypesContent').html(data);
        $("#Relation_Type_Title").val("");
    });
}

function EditTranslateRelationTypes(id) {
    var dataItem = id;
    $('#RelationTypesList').html("");
    $('#RelationTypesList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateRelationTypesURL, { TableId: dataItem.Relation_Type_ID, TableName: 'Relation_Types_By_Language' }, function (data) {
        $('#RelationTypesList').html(data);
    });
}

function DeleteRelationTypes(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(RelationTypesDeleteURL, { RelationTypesId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchRelationTypes(false))
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

function SearchRelationTypes(isRemoveAlert) {


    var RelationTypesname = $("#Relation_Type_Title").val();
    var languageid = $("#LANGUAGE_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#RelationTypes-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(RelationTypesSearchURL, { RelationTypesName: RelationTypesname, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#RelationTypes-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveRelationTypes() {

    var isValid = true;
    var lst = ["#Relation_Type_Title"];
    var lstmsg = ["#Relation_Type_Title_ValidationMsg"];

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


//translate RelationTypes
function EditTranslateRelationTypes(id) {

    $('#RelationTypesTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#RelationTypesTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationRelationTypesURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#RelationTypesTranslate-List').html(data)
    });
}



//end translate

