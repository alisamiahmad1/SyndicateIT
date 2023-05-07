function AddLimitDefinition() {
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadLimitContentDefinitionsDetailsUrl, {}, function (data) {
        $('#MainContent').html(data);
    });
}



function LoadLimitDefinitionsContent() {
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadLimitContentDefinitionsUrl, {}, function (data) {
        $('#MainContent').html(data);
    });
}

function SaveLimitDefinitions() {

    if (!isValidateSaveLimitDefinitions())
        return false;

    var myData = $('#LimitDefinitions-form').serialize();

    $('#LimitDefinitions-Div').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(saveLimitDefinitionsUrl, myData, function (data) {
        $('#LimitDefinitions-Div').html(data);
    });
}


function isValidateSaveLimitDefinitions() {

    HideLimitDefinitionsValidation();

    var isValid = true;

    if ($("#Code").data("kendoDropDownList").value() == "") {
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

    if ($("#MonthlyTransactionCount").val() != "" && $("#DailyTransactionCount").val() != "" && $("#MonthlyTransactionCount").val() > $("#DailyTransactionCount").val() )
    {
        $("#DailyTransactionCount_validationMessage").show();
        isValid = false;
    }

   

    return isValid;
}

function HideLimitDefinitionsValidation() {
    $("#Code_validationMessage").hide();
    $("#Description_validationMessage").hide();
    $("#Qualifier_validationMessage").hide();
    $("#DailyLimit_validationMessage").hide();
    $("#MonthlyLimit_validationMessage").hide();
    $("#DailyTransactionCount_validationMessage").hide();
    $("#MonthlyTransactionCount_validationMessage").hide();
    $("#MaxTransactionValue_validationMessage").hide();
    $("#CurrencyId_validationMessage").hide();
    $("#IsGeneral_validationMessage").hide();


   
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