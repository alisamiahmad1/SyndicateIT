
var currentTemplatePricing = [];
var currentSelectTemplate = [];

function InitializeTabWizard() {
    if (currentPlansID > 0) {

        InitializeWizardSubscriberPricing();
        InitializeWizardSubscriberLimit();
        InitializeWizardSubscriberFees();
        InitializeWizardSubscriberAccounts();
    } else {
        $("#SubscriberPricing-a").attr("data-toggle", "");
        $("#SubscriberFee-a").attr("data-toggle", "");
        $("#SubscriberLimit-a").attr("data-toggle", "");
    }

}

function InitializeWizardSubscriberAccounts() {
    $("#SubscriberAccounts-a").click(function () {
        LoadPlansSubscriberAccounts();
        $("#SubscriberAccounts-a").attr("data-toggle", "tab");
    });
}
function InitializeWizardSubscriberLimit() {
    $("#SubscriberLimit-a").click(function () {
        LoadPlansSubscriberLimit();
        $("#SubscriberLimit-a").attr("data-toggle", "tab");
    });
}


function InitializeWizardSubscriberPricing() {
    $("#SubscriberPricing-a").click(function () {
        LoadPlansSubscriberPricing();
        $("#SubscriberPricing-a").attr("data-toggle", "tab");
    });
}

function InitializeWizardSubscriberFees() {
    $("#SubscriberFee-a").click(function () {
        LoadPlansSubscriberFees();
        $("#SubscriberFee-a").attr("data-toggle", "tab");
    });
}
/********************** Begin Template  Pricing ***************************/

function HideError()
{
    $("#TempalteAlert-Type").hide();
    $("#TempalteAlert-Warning").html("");
    $(".smart-form", $("#TreePricing").html()).each(function () {
     var index = $("#BracketsIndex", $(this)).val();        
     $("#PricingValue_" + index).removeClass("input-error");
     $("#MinFeeValue_" + index).removeClass("input-error");
     $("#MaxFeeValue_" + index).removeClass("input-error");
     $("#MinBracketValue_" + index).removeClass("input-error");
     $("#MaxBracketValue_" + index).removeClass("input-error");
  });
}

function isValidateSavePaymentTypePricing() {
   
    var isValid = true;
   
    $(".smart-form", $("#TreePricing").html()).not('.remove').not('.diseabled').each(function () {
        var index = $("#BracketsIndex", $(this)).val();    
       
        if ($("#PricingValue_" + index).val() == undefined || $("#PricingValue_" + index).val() == "" || parseFloat($("#PricingValue_" + index).val()) < 0) {
            $("#PricingValue_" + index).addClass("input-error");
            isValid = false;
        }
        if (!$("#MinFeeValue_" + index).parents(".col-2").hasClass("hide"))
        {
            if ($("#MinFeeValue_" + index).val() == undefined || $("#MinFeeValue_" + index).val() == "" || parseFloat($("#MinFeeValue_" + index).val()) < 0) {
                $("#MinFeeValue_" + index).addClass("input-error");
                isValid = false;
            }
        }
        if (!$("#MaxFeeValue_" + index).parents(".col-2").hasClass("hide")) {
            if ($("#MaxFeeValue_" + index).val() == undefined || $("#MaxFeeValue_" + index).val() == "" || parseFloat($("#MaxFeeValue_" + index).val()) <= 0) {
                $("#MaxFeeValue_" + index).addClass("input-error");
                isValid = false;
            }
            else if (parseFloat($("#MaxFeeValue_" + index).val()) <= parseFloat($("#MinFeeValue_" + index).val())) {
                $("#MaxFeeValue_" + index).addClass("input-error");
                isValid = false;
            }
        }
        if (!$("#MinBracketValue_" + index).parents(".col-2").hasClass("hide")) {
            if ($("#MinBracketValue_" + index).val() == undefined || $("#MinBracketValue_" + index).val() == "" || parseFloat($("#MinBracketValue_" + index).val()) < 0) {
                $("#MinBracketValue_" + index).addClass("input-error");
                isValid = false;
            }
        }
        if (!$("#MaxBracketValue_" + index).parents(".col-2").hasClass("hide")) {
            if ($("#MaxBracketValue_" + index).val() == undefined || $("#MaxBracketValue_" + index).val() == "" || parseFloat($("#MaxBracketValue_" + index).val()) <= 0) {
                $("#MaxBracketValue_" + index).addClass("input-error");
                isValid = false;
            } else if (parseFloat($("#MaxBracketValue_" + index).val()) <= $("#MinBracketValue_" + index).val()) {
                $("#MaxBracketValue_" + index).addClass("input-error");
                isValid = false;
            }
        }
    });
    return isValid
}


    
function LoadPaymentType() {
    $('#PaymentType-Content').html("");

    if (!isValidatePaymentType())
        return false;

    var billingType = $("#SelectedBillingType").val();

    $('#PaymentType-Content').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadPaymentTypeUrl, { billingType: billingType }, function (data) {
        $('#PaymentType-Content').html(data);
        $('#paymentTypeSelected-container').html("").hide();
    });
}


/********************** End Template Pricing ***************************/

/********************** Begin Plan Subscriber Limit ***************************/

function LoadPlansSubscriberAccounts() {
    $('#Main-plans').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadPlansSubscriberAccountsUrl, {}, function (data) {
        $('#Main-plans').html(data);
    });
}

function LoadPlansSubscriberLimit() {
    $('#Main-plans').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadPlansSubscriberLimitUrl, {}, function (data) {
        $('#Main-plans').html(data);
    });
}

function filterTemplateLimit() {
    return {
        accountTypeCode: $("#AccountTypeCode").data("kendoDropDownList").value()
    };
}

function LoadLimitDefinition() {   
    $('#LimitDefinitions-Content').html("");
    var accountTypeCode = $("#AccountTypeCode").data("kendoDropDownList").value();
    var templateID = $("#TemplateID").data("kendoDropDownList").value();
    $('#LimitDefinitions-Content').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadLimitDefinitionUrl, {IsPlanCodeSelected: true, limitCode: "", accountTypeCode: accountTypeCode, templateID: templateID }, function (data) {
        $('#LimitDefinitions-Content').html(data);
        $('#Limit-Action').show();
    });
}

function LoadLimitDefinitionByCode(limitCode) {
    $('#LimitDefinitions-Content').html("");
    var  accountTypeCode = $("#AccountTypeCode").data("kendoDropDownList").value();
    var templateID = $("#TemplateID").data("kendoDropDownList").value();    
    $('#LimitDefinitions-Content').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadLimitDefinitionUrl, { IsPlanCodeSelected :false, limitCode: limitCode, accountTypeCode: accountTypeCode, templateID: templateID }, function (data) {
        $('#LimitDefinitions-Content').html(data);
        $('#Limit-Action').show();
       
    });
   
}

function SaveLimitDefinitions() {
   
    HideTemplateLimitValidation();
    if (!isValidateSaveTemplateLimit()) {
        $("#TempalteAlert-Type").show();
        return false;
    }
    var accountTypeCode = $("#AccountTypeCode").data("kendoDropDownList").value();
    var myData = $('#LimitDefinitions-form').serialize();
    $('#LimitDefinitions-Content').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(saveLimitDefinitionUrl, myData, function (data) {
        $('#LimitDefinitions-Content').html(data);
        $('#Limit-Action').hide();
        $.post(loadLimitCodeTemplateUrl, { accountTypeCode: accountTypeCode }, function (data) {
            $('#LimitCodeTemplate-container').html(data).show();
        });
    });
}

function isValidateSaveTemplateLimit() {

    HideTemplateLimitValidation();

    var isValid = true;

    //if ($("#IsPinPayLimitCode").val() == "False" && $("#LimitDefinitionId").val() == 0) {
    //    if ($("#Code").data("kendoDropDownList").value() == "") {
    //        $("#Code_validationMessage").show();
    //        isValid = false;
    //    }
    //}

    if ($("#Description").val() == "") {
        $("#Description_validationMessage").show();
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

function CheckLimitNumericOnly() {
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

function CheckLimitDecimalOnly() {
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

function onChangeLimitAccountType(e) {
    $("#add-Plan-LimitCode").hide();
    $('#LimitCodeTemplate-container').html("").hide();
    $('#LimitDefinitions-Content').html("no data available");
    $('#Limit-Action').hide();
    var dataItem = this.dataItem(e.item);
    if (dataItem.Value != "") {

        if (dataItem.Value == "_GLOBAL_" || dataItem.Value == "P2PINTER" || dataItem.Value == "P2PINTRA")
        {
             $("#add-Plan-LimitCode").show();
        }
       else
        {
            $.post(loadLimitCodeTemplateUrl, { accountTypeCode: dataItem.Value }, function (data) {
                $('#LimitCodeTemplate-container').html(data).show();
            });
        }       
    }
}

function onResetLimitAccountType() {
    $('#LimitDefinitions-Content').html("no data available")
    $('#Limit-Action').hide();
}
/********************** End Plan Subscriber Limit ***************************/


/********************** Begin Plan Subscriber Accounts ***************************/

function onChangeIssuerAccountsType(e) {
    $("#AddAccounts-container").hide();  
    $('#Accounts-Content').html("no data available");
    var dataItem = this.dataItem(e.item);
    if (dataItem.Value != "") {      
        $('#Accounts-Content').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
        $.post(loadIssuerAccountsTypeUrl, { accountTypeCode: dataItem.Value }, function (data) {
            $("#AddAccounts-container").show();
            $('#Accounts-Content').html(data).show();
        });        
    }
}

function SaveIssuerAccounts() {

    $("#CollectionAccountNumber_validationMessage").hide();
    if ($("#CollectionAccountNumber").val() == "") {
        $("#CollectionAccountNumber_validationMessage").show();
        return false;
    }
    var accountTypeCode = $("#AccountTypeCode").data("kendoDropDownList").value();
    $("#AccountsTypeCode").val(accountTypeCode);
    var myData = $('#PlansIssuerAccounts-form').serialize();
    $('#Accounts-Content').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(savePlansIssuerAccountsUrl, myData, function (data) {
        $('#Accounts-Content').html(data);
        EmptyIssuerAccounts();
    });
}


function EmptyIssuerAccounts() {
    $("#CollectionAccountNumber").val("");
    $("#OnUsCollectionAccountNumber").val("");
    $("#OffUsCollectionAccountNumber").val("");
    $("#FeesOnSenderAccountNumber").val("");
}



/********************** End Plan Subscriber Accounts ***************************/


/********************** Begin Plan Subscriber Pricing ***************************/

function selectPaymentType() {

    /// <summary>
    /// Selects the bill batch.
    /// </summary>
    if ($("#chkPaymentType").is(':checked') == true) {
        $('#PlansPaymentTypeGrid').data('kendoGrid').tbody.find("tr").find("td").find('input').filter(':checkbox').not(":disabled").prop("checked", true);
    } else {
        $('#PlansPaymentTypeGrid').data('kendoGrid').tbody.find("tr").find("td").find('input').filter(':checkbox').not(":disabled").prop("checked", false);
    }

}

function LoadPlansSubscriberPricing() {
    $('#Main-plans').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadPlansSubscriberPricingUrl, {}, function (data) {
        $('#Main-plans').html(data);
    });
}



function isValidatePaymentType() {
    $("#ErrorSelectedbillingType").hide();
    var isValid = true;  
    if ($("#SelectedBillingType").val() == null) {
        $("#SelectedBillingType_validationMessage").show();
        isValid = false;
    }
    return isValid;
}


/********************** End Plan Subscriber Pricing ***************************/

/********************** Begin Plan Subscriber Fee ***************************/

function LoadPlansSubscriberFees() {
    $('#Main-plans').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadPlansSubscriberFeesUrl, {}, function (data) {     
        $('#Main-plans').html(data);
    });
}

function SavePlansSubscriberFees() { 
   var myData = $('#PlansSubscriberFees-form').serialize();
    $('#Main-plans').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(savePlansSubscriberFeesUrl, myData, function (data) {
        $('#Main-plans').html(data);
    });
}

function HidePlansSubscriberFees() {
    $("#SubscriptionFee_validationMessage").hide();
    $("#MonthlyFee_validationMessage").hide();
    $("#YearlyFee_validationMessage").hide();
    $("#MonthlyFeewaiver_validationMessage").hide();
}

function InitializePlansSubscriberFeesInput() {
    $("#SubscriptionFee").val("");
    $("#MonthlyFee").val("");
    $("#YearlyFee").val("");
    $("#MonthlyFeewaiver").val("");  
}

/********************** Begin Plan Subscriber Fee ***************************/


/********************** Begin Plan Definition ***************************/


function LoadPlansDefinitions() {
    $('#Main-plans').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadPlansDefinitionsUrl, {}, function (data) {
        $('#Main-plans').html(data);
        InitializeTabWizard();
    });
}


function SavePlansDefinitions() {
    if (!isValidateSavePlansDefinitions())
        return false;
   

    var myData = $('#PlansDefinitions-form').serialize();
    $('#Main-plans').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(savePlansDefinitionsUrl, myData, function (data) {
        $('#Main-plans').html(data);
        InitializeTabWizard();
    });
}

function isValidateSavePlansDefinitions() {

    HidePlansDefinitions();

    var isValid = true;


    if ($("#PlanName").val() == "") {
        $("#PlanName_validationMessage").show();
        isValid = false;
    }
    //if ($("#LimitDefinitionsID").data("kendoDropDownList").value() == "") {
    //    $("#LimitDefinitionsID_validationMessage").show();
    //    isValid = false;
    //}

    if ($("#SelectedAccountTypes").val() == null) {
        $("#SelectedAccountTypes_validationMessage").show();
        isValid = false;
    }
    if ($("#SelectedCustomerSegmentations").val() == null) {
        $("#SelectedCustomerSegmentations_validationMessage").show();
        isValid = false;
    }
    return isValid;
}

function HidePlansDefinitions() {
    $("#PlanName_validationMessage").hide();
    //$("#LimitDefinitionsID_validationMessage").hide();
    $("#SelectedAccountTypes_validationMessage").hide();
    $("#SelectedCustomerSegmentations_validationMessage").hide();

}
/********************** Begin Plan Definition ***************************/


function AddPlansSetup() {
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadPlansSetupContentDetailsUrl, {}, function (data) {
        $('#MainContent').html(data);
    });
}

function LoadPlansSetupContent() {
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadPlansSetupContentUrl, {}, function (data) {
        $('#MainContent').html(data);
    });
}



function CheckPlansSubscriberFeesNumericOnly() {

    $(".numberSubscriptionFee").keydown(function (event) {


        if (event.shiftKey == true) {
            event.preventDefault();
        }

        if ((event.keyCode >= 48 && event.keyCode <= 57) || 
            (event.keyCode >= 96 && event.keyCode <= 105) || 
            event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 37 ||
            event.keyCode == 39 || event.keyCode == 46 || event.keyCode == 190) {

        } else {
            event.preventDefault();
        }

        if($(this).val().indexOf('.') !== -1 && event.keyCode == 190)
            event.preventDefault(); 
        //if a decimal has been added, disable the "."-button

    });


}