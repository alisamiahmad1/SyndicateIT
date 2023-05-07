var currentTemplateLimit = [];


function onChangeAccountType(e)
{   
    EmptyTemplate();    
    var dataItem = this.dataItem(e.item);
    if (dataItem.Value != "") {
        $("#AddTemplate-Content").show();
        $("#AddTemplate-Action").show();       
        $.post(loadLimitTemplateExistUrl, { accountTypeCode: dataItem.Value }, function (data) {
            $('#ExistTemplate-container').html(data).show();
        });
    }
}

function EmptyTemplate() {
    $("#LimitName").val("");
    $("#LimitDescription").val("");
    $('#Limit-Content').html("");
    $('#ExistTemplate-container').html("").hide();
    $('#Limit-Action').hide();
    $("#AddTemplate-Action").hide();
    $("#AddTemplate-Content").hide();
    $("#ExistTemplate-container").hide();
}

function AddLimitTemplates() {
    $("#LimitName_validationMessage").hide();
    $("#LimitName_validationMessage").hide();
    var name = $("#LimitName").val();
    var description = $("#LimitDescription").val();
    var accountTypeCode = $("#AccountTypeID").data("kendoDropDownList").value();

    if (name == "" || description == "")
    {
        if (name == "") {
            $("#LimitName_validationMessage").show();

        }

        if (description == "") {
            $("#LimitDescription_validationMessage").show();
        }
            
            return false;
    }

    $('#Limit-Content').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(addLimitTemplatesUrl, { name: name, description: description, accountTypeCode: accountTypeCode }, function (data) {
        $('#Limit-Content').html(data.LimitTemplate);
        $('#ExistTemplate-container').html(data.ExistTemplate).show();
        if (data.IsHideSave == true)
        {
            $('#Limit-Action').hide();
        } else
        {
            $('#Limit-Action').show();
        }
       
        $('#LimitName').val("");
        $('#LimitDescription').val("");
    });
}

function EditLimitTemplates(id) {
    var accountTypeCode = $("#AccountTypeID").data("kendoDropDownList").value();
    $('#Limit-Content').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(editLimitTemplatesUrl, { templateId: id, accountTypeCode: accountTypeCode }, function (data) {
        $('#Limit-Content').html(data);
        $('#Limit-Action').show();
    });
}


/********************** Begin save  Limit ***************************/

function SaveLimitTemplates() {
    currentTemplateLimit = [];
     HideTemplateLimitValidation();
    if (!isValidateSaveTemplateLimit()) {
        $("#TempalteAlert-Type").show();
        return false;
    }

    var name = $("#TemplateLimitName").val();
    var description = $("#TemplateLimitDescription").val();

    currentTemplateLimit.push({
        TemplateID: $("#TemplateID").val(),
        Code: $("#Code").val(),
        Description: $("#Description").val(),      
        DailyLimit: $("#DailyLimit").val(),
        MonthlyLimit: $("#MonthlyLimit").val(),
        DailyTransactionCount: $("#DailyTransactionCount").val(),
        MonthlyTransactionCount: $("#MonthlyTransactionCount").val(),
        MaxTransactionValue: $("#MaxTransactionValue").val(),
        CurrencyId: $("#CurrencyId").data("kendoDropDownList").value(),
        IsSender: $(IsSender).prop("checked") ,
        IsGeneral: $(IsGeneral).prop("checked") ,
        AffectGlobal: $(AffectGlobal).prop("checked") ,

    });
    $('#Limit-Content').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(saveLimitTemplatesUrl, { currentLimitTemplate: currentTemplateLimit, name: name, description: description }, function (data) {
        $('#Limit-Content').html(data);
        $("#TemplateLimitName").val("");
        $("#TemplateLimitDescription").val("");
        $('#Limit-Action').hide();
    });
}



/********************** end saves  Limit ***************************/

function isValidateSaveTemplateLimit() {

    HideTemplateLimitValidation();

    var isValid = true;

    if ($("#Code").val() == "") {
        $("#Code_validationMessage").show();
        isValid = false;
    }
    if ($("#Description").val() == "") {
        $("#Description_validationMessage").show();
        isValid = false;
    }
    if ($("#Qualifier").val() == "") {
        $("#Qualifier_validationMessage").show();
        isValid = false;
    }
    if ($("#DailyLimit").val() == "") {
        $("#DailyLimit_validationMessage").show();
        isValid = false;
    }
    if ($("#MonthlyLimit").val() == "") {
        $("#MonthlyLimit_validationMessage").show();
        isValid = false;
    }
    if ($("#MonthlyLimit").val() == "") {
        $("#MonthlyLimit_validationMessage").show();
        isValid = false;
    }
    if ($("#DailyTransactionCount").val() == "") {
        $("#DailyTransactionCount_validationMessage").show();
        isValid = false;
    }
    if ($("#MonthlyTransactionCount").val() == "") {
        $("#MonthlyTransactionCount_validationMessage").show();
        isValid = false;
    }
    if ($("#MaxTransactionValue").val() == "") {
        $("#MaxTransactionValue_validationMessage").show();
        isValid = false;
    }
    if ($("#CurrencyId").data("kendoDropDownList").value() == "") {
        $("#CurrencyId_validationMessage").show();
        isValid = false;
    }

    //if ($("#MonthlyLimit").val() != "" && $("#DailyLimit").val() != "" && $("#MonthlyLimit").val() < $("#DailyLimit").val()) {
    //    $("#MonthlyDailyLimit_validationMessage").show();
    //    isValid = false;
    //}

    //if ($("#MonthlyTransactionCount").val() != "" && $("#DailyTransactionCount").val() != "" && $("#MonthlyTransactionCount").val() < $("#DailyTransactionCount").val()) {
    //    $("#MonthlyDailyTransactionCount_validationMessage").show();
    //    isValid = false;
    //}

    //if ($("#MaxTransactionValue").val() != "" && $("#DailyLimit").val() != "" && $("#MaxTransactionValue").val() > $("#DailyLimit").val()) {
    //    $("#MaxDailyTransactionAmount_validationMessage").show();
    //    isValid = false;
    //}


    return isValid;
}

function HideTemplateLimitValidation() {
    $("#Code_validationMessage").hide();
    $("#Description_validationMessage").hide();   
    $("#DailyLimit_validationMessage").hide();
    $("#MonthlyLimit_validationMessage").hide();
    $("#DailyTransactionCount_validationMessage").hide();
    $("#MonthlyTransactionCount_validationMessage").hide();
    $("#MaxTransactionValue_validationMessage").hide();
    $("#CurrencyId_validationMessage").hide();
    $("#IsGeneral_validationMessage").hide();
    $("#MonthlyDailyLimit_validationMessage").hide();
    $("#MonthlyDailyTransactionCount_validationMessage").hide();
    $("#MaxDailyTransactionAmount_validationMessage").hide();
}

function InitializeInput() {
    $("#Code").val("");
    $("#Description").val("");
    $("#Qualifier").val("");
    $("#DailyLimit").val("");
    $("#MonthlyLimit").val("");
    $("#DailyTransactionCount").val("");
    $("#MonthlyTransactionCount").val("");
    $("#MaxTransactionValue").val("");

}
function CheckNumericOnly() {
    $(".numericOnly").bind('keypress', function (e) {
        if (e.keyCode == '9' || e.keyCode == '16') {
            return;
        }
        var code;
        if (e.keyCode) code = e.keyCode;
        else if (e.which) code = e.which;
        if (e.which == 46)
            return false;
        if (code == 8 || code == 46)
            return true;
        if (code < 48 || code > 57)
            return false;
    });

    
    //Disable paste
    $(".numericOnly").bind("paste", function (e) {
        e.preventDefault();
    });

    $(".numericOnly").bind('mouseenter', function (e) {
        var val = $(this).val();
        if (val != '0') {
            val = val.replace(/[^0-9]+/g, "")
            $(this).val(val);
        }
    });
}

function CheckDecimalOnly() {
$('.decimalOnly').keypress(function (event) {
        var $this = $(this);
        if ((event.which != 46 || $this.val().indexOf('.') != -1) &&
           ((event.which < 48 || event.which > 57) &&
           (event.which != 0 && event.which != 8))) {
            event.preventDefault();
        }

        var text = $(this).val();
        if ((event.which == 46) && (text.indexOf('.') == -1)) {
            setTimeout(function () {
                if ($this.val().substring($this.val().indexOf('.')).length > 3) {
                    $this.val($this.val().substring(0, $this.val().indexOf('.') + 3));
                }
            }, 1);
        }

        if ((text.indexOf('.') != -1) &&
            (text.substring(text.indexOf('.')).length > 2) &&
            (event.which != 0 && event.which != 8) &&
            ($(this)[0].selectionStart >= text.length - 2)) {
            event.preventDefault();
        }
    });

$(".decimalOnly").bind("paste", function (e) {
    e.preventDefault();
});

}

