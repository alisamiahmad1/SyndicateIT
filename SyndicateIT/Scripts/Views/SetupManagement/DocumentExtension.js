function AddDocumentExtension() {
    $('#DocumentExtensionContent').html("");
    $('#DocumentExtensionContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadDocumentExtensionURL, {}, function (data) {
        $('#DocumentExtensionContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditDocumentExtension(id) {
    var dataItem = id;
    $('#DocumentExtensionContent').html("");
    $('#DocumentExtensionContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadDocumentExtensionURL, { DocumentExtensionId: dataItem }, function (data) {
        $('#DocumentExtensionContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveDocumentExtension() {
    if (!isValidateSaveDocumentExtension())
        return false;
    var myData = $('#DocumentExtension-form').serialize();
    $.post(saveDocumentExtensionUrl, myData, function (data) {
        $('#DocumentExtensionContent').html(data);
        $("#Extension_Name").val("");
    });
}

function EditTranslateDocumentExtension(id) {
    var dataItem = id;
    $('#DocumentExtensionList').html("");
    $('#DocumentExtensionList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateDocumentExtensionURL, { TableId: dataItem.Document_Ext_ID, TableName: 'Document_Extension_By_Language' }, function (data) {
        $('#DocumentExtensionList').html(data);
    });
}

function DeleteDocumentExtension(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(DocumentExtensionDeleteURL, { DocumentExtensionId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchDocumentExtension(false))
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

function SearchDocumentExtension(isRemoveAlert) {


    var DocumentExtensionname = $("#Extension_Name").val();
    var languageid = $("#LANGUAGE_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#DocumentExtension-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(DocumentExtensionSearchURL, { DocumentExtensionName: DocumentExtensionname, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#DocumentExtension-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveDocumentExtension() {

    var isValid = true;
    var lst = ["#Extension_Name", "#Extension_Type"];
    var lstmsg = ["#Extension_Name_ValidationMsg", "#Extension_Type_ValidationMsg"];

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


//translate DocumentExtension
function EditTranslateDocumentExtension(id) {

    $('#DocumentExtensionTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#DocumentExtensionTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationDocumentExtensionURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#DocumentExtensionTranslate-List').html(data)
    });
}



//end translate

