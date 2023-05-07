function AddSkillCoefficient() {
    $('#SkillCoefficientContent').html("");
    $('#SkillCoefficientContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadSkillCoefficientURL, {}, function (data) {
        $('#SkillCoefficientContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditSkillCoefficient(id) {
    var dataItem = id;
    $('#SkillCoefficientContent').html("");
    $('#SkillCoefficientContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadSkillCoefficientURL, { SkillCoefficientId: dataItem }, function (data) {
        $('#SkillCoefficientContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveSkillCoefficient() {
    if (!isValidateSaveSkillCoefficient())
        return false;
    var myData = $('#SkillCoefficient-form').serialize();
    $.post(saveSkillCoefficientUrl, myData, function (data) {
        $('#SkillCoefficientContent').html(data);
        $("#SkillCoefficient_NAME").val("");
    });
}

function EditTranslateSkillCoefficient(id) {
    var dataItem = id;
    $('#SkillCoefficientList').html("");
    $('#SkillCoefficientList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateSkillCoefficientURL, { TableId: dataItem.SkillCoefficient_ID, TableName: 'SkillCoefficient_BY_LANGUAGE' }, function (data) {
        $('#SkillCoefficientList').html(data);
    });
}

function DeleteSkillCoefficient(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(SkillCoefficientDeleteURL, { SkillCoefficientId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchSkillCoefficient(false))
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

function SearchSkillCoefficient(isRemoveAlert) {


    var SkillCoefficientname = $("#SkillCoefficient_NAME").val();
    var languageid = $("#LANGUAGE_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#SkillCoefficient-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(SkillCoefficientSearchURL, { SkillCoefficientName: SkillCoefficientname, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#SkillCoefficient-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveSkillCoefficient() {

    var isValid = true;
    var lst = ["#SkillCoefficient_NAME"];
    var lstmsg = ["#SkillCoefficient_NAME_ValidationMsg"];

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


//translate SkillCoefficient
function EditTranslateSkillCoefficient(id) {

    $('#SkillCoefficientTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#SkillCoefficientTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationSkillCoefficientURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#SkillCoefficientTranslate-List').html(data)
    });
}



//end translate
