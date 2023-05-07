function AddGender() {
    $('#GenderContent').html("");
    $('#GenderContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadGenderURL, {}, function (data) {
        $('#GenderContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditGender(id) {
    var dataItem = id;
    $('#GenderContent').html("");
    $('#GenderContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadGenderURL, { GenderId: dataItem }, function (data) {
        $('#GenderContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveGender() {
    if (!isValidateSaveGender())
        return false;
    var myData = $('#Gender-form').serialize();
    $.post(saveGenderUrl, myData, function (data) {
        $('#GenderContent').html(data);
        $("#Gender_NAME").val("");
     });
}

function EditTranslateGender(id) {
    var dataItem = id;
    $('#GenderList').html("");
    $('#GenderList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateGenderURL, { TableId: dataItem.Gender_ID, TableName: 'Gender_BY_LANGUAGE' }, function (data) {
        $('#GenderList').html(data);
      });
}

function DeleteGender(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(GenderDeleteURL, { GenderId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchGender(false))
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

function SearchGender(isRemoveAlert) {

       
    var Gendername = $("#Gender_NAME").val();
    var languageid = $("#LANGUAGE_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#Gender-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(GenderSearchURL, { GenderName: Gendername, languageId: languageid, Is_Active: isActive }, function (data)
    {
        $("#widget-grid-Transalte").hide();
        $('#Gender-List').html(data);
        if (isRemoveAlert == true)
        {
            $("#Alert-Type").remove();
        }
        
    });

}



function isValidateSaveGender() {

    var isValid = true;
    var lst = ["#GENDER_NAME"];
    var lstmsg = ["#GENDER_NAME_ValidationMsg"];

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
            if ($(lstmsg[i]).parent().hasClass("state-error") ) {
                $(lstmsg[i]).parent().removeClass("state-error");
                
            }
        }

    }
    return isValid;
}


//translate Gender
function EditTranslateGender(id) {
    
    $('#GenderTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#GenderTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationGenderURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#GenderTranslate-List').html(data)
    });
}



//end translate
