var currentTemplatePricing = [];


function onChangeBillingType(e)
{   
    EmptyTemplate();    
    var dataItem = this.dataItem(e.item);
    if (dataItem.Value != "") {
        $("#AddTemplate-Content").show();
        $("#AddTemplate-Action").show();       
        $.post(loadPricingTemplateExistUrl, { billingTypeId: dataItem.Value }, function (data) {
            $('#ExistTemplate-container').html(data).show();
        });
    }
}

function EmptyTemplate() {
    $("#PricingName").val("");
    $("#PricingDescription").val("");
    $('#Pricing-Content').html("");
    $('#ExistTemplate-container').html("").hide();
    $('#Pricing-Action').hide();
    $("#AddTemplate-Action").hide();
    $("#AddTemplate-Content").hide();
    $("#ExistTemplate-container").hide();
}

function AddPricingTemplates() {
    $("#PricingName_validationMessage").hide();
    $("#PricingName_validationMessage").hide();
    var name = $("#PricingName").val();
    var description = $("#PricingDescription").val();
    var billingTypeId = $("#BillingTypeID").data("kendoDropDownList").value();

    if (name == "" || description == "")
    {
        if (name == "") {
            $("#PricingName_validationMessage").show();

        }

        if (description == "") {
            $("#PricingDescription_validationMessage").show();
        }
            
            return false;
    }

    $('#Pricing-Content').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(addPricingTemplatesUrl, { name: name, description: description, billingTypeId: billingTypeId }, function (data) {
        $('#Pricing-Content').html(data.pricingTemplate);
        $('#ExistTemplate-container').html(data.ExistTemplate).show();
      
        if (data.IsHideSave == true) {
            $('#Pricing-Action').hide();
        } else {
            $('#Pricing-Action').show();
        }

    });
}

function EditPricingTemplates(id) {
    var billingTypeId = $("#BillingTypeID").data("kendoDropDownList").value();
    $('#Pricing-Content').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(editPricingTemplatesUrl, { templateId: id, billingTypeId: billingTypeId }, function (data) {
        $('#Pricing-Content').html(data);
        $('#Pricing-Action').show();
    });
}


/********************** Begin save  Pricing ***************************/
function isValidateIntersectionSaveTemplatePricing() {

    var isValid = true;

    $(".billingType-content", $("#TreePricing").html()).each(function () {
        var counter = 0;
        if ($(".checkbox-Bracket", $(this)).is(':checked') == true) {
            var countDiv = $(".smart-form", $(this)).not('.remove').length;
            $(".smart-form", $(this)).not('.remove').each(function () {
                var index = $("#BracketsIndex", $(this)).val();

                for (var i = counter; i < countDiv; i++) {
                    var secondIndex = $("#BracketsIndex", $(this).next()).val();
                    var firstArray = [$("#MinBracketValue_" + index).val(), $("#MaxBracketValue_" + index).val()];
                    var secondArray = [$("#MinBracketValue_" + secondIndex).val(), $("#MaxBracketValue_" + secondIndex).val()];
                    var arrayIntersect = AarrayIntersect(firstArray, secondArray);
                    if (arrayIntersect == true) {
                        $("#MaxBracketValue_" + secondIndex).addClass("input-error");
                        $("#MinBracketValue_" + secondIndex).addClass("input-error");
                        isValid = false;
                    }
                    ;
                }
            })
        }
    });

    return isValid
}
function AarrayIntersect(a, b) {

    if (parseInt(a[0]) < parseInt(b[0]) && parseInt(b[0]) < parseInt(a[1]))
        return true;
    else if (parseInt(a[0]) < parseInt(b[1]) && parseInt(b[1]) <= parseInt(a[1]))
        return true
    else if (parseInt(b[0]) < parseInt(a[0]) && parseInt(b[1]) > parseInt(a[1]))
        return true;
    else if (parseInt(a[0]) > parseInt(b[0]) && parseInt(a[1]) < parseInt(b[1]))
        return true;
    else if (parseInt(a[0]) < parseInt(b[0]) && parseInt(a[1]) > parseInt(b[1]))
        return true;
    else if (parseInt(b[0]) == parseInt(a[0]) && parseInt(b[1]) == parseInt(a[1]))
        return true;
    else
        return false;
}
function SavePricingTemplates() {
    currentTemplatePricing = [];
    HideError();    

    if (!isValidateSaveTemplatePricing()) {
        $("#TempalteAlert-Type").show();
        $("#TempalteAlert-Warning").html("fill in required fields");
        return false;
    }
    if (!isValidateIntersectionSaveTemplatePricing()) {
        $("#TempalteAlert-Type").show();
        $("#TempalteAlert-Warning").html("Brackets data are intersection");
        return false;
    }

    var name = $("#TemplatePricingName").val();
    var description = $("#TemplatePricingDescription").val();

    $(".smart-form", $("#TreePricing").html()).each(function () {
        var index = $("#BracketsIndex", $(this)).val();

        var $accountTypeObject = $(this).parents(".accountType-content");
        var $billingTypeObject = $accountTypeObject.parents(".billingType-content");

        currentTemplatePricing.push({
            TemplateID: $("input[id^='TemplateID_']", $billingTypeObject).val(),
            BillingTypeID: $("input[id^='BillingType_']", $billingTypeObject).val(),
            AccountTypeID: $("input[id^='AccountType_']", $accountTypeObject).val(),
            AccountTypeBracketID: $("#AccountTypeBracketID_" + index).val(),
            TypeAmount: $("#TypeAmount_" + index).val(),
            Value: $("#PricingValue_" + index).val(),
            Rounding: $("#PricingRounding_" + index).val(),
            MinFee: $("#MinFeeValue_" + index).val(),
            MaxFee: $("#MaxFeeValue_" + index).val(),
            MinBracket: $("#MinBracketValue_" + index).val(),
            MaxBracket: $("#MaxBracketValue_" + index).val(),
            Status: $("#BracketsStatus", $(this)).val(),
            HasBracket: $("#HasBracket_" + index).val(),

        });
    });
    $('#Pricing-Content').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(savePricingTemplatesUrl, { currentTemplatePricing: currentTemplatePricing, name: name, description: description }, function (data) {       
        $('#Pricing-Content').html(data.pricingTemplate);
        $('#ExistTemplate-container').html(data.ExistTemplate).show();
        $("#TemplatePricingName").val("");
        $("#TemplatePricingDescription").val("");
        $('#Pricing-Action').hide();
    });
}



function HideError() {
    $("#TempalteAlert-Type").hide();
    $(".smart-form", $("#TreePricing").html()).each(function () {
        var index = $("#BracketsIndex", $(this)).val();
        $("#PricingValue_" + index).removeClass("input-error");
        $("#MinFeeValue_" + index).removeClass("input-error");
        $("#MaxFeeValue_" + index).removeClass("input-error");
        $("#MinBracketValue_" + index).removeClass("input-error");
        $("#MaxBracketValue_" + index).removeClass("input-error");
        $("#TemplatePricingName").removeClass("input-error");
        $("#TemplatePricingDescription").removeClass("input-error");
    });
}


function isValidateSaveTemplatePricing() {

    var isValid = true;
    var name = $("#TemplatePricingName").val();
    var description = $("#TemplatePricingDescription").val();
    if(name ==  undefined ||(name == ""))
    {
        $("#TemplatePricingName").addClass("input-error");
        isValid = false;
    }
    if (description == undefined || (description == "")) {
        $("#TemplatePricingDescription").addClass("input-error");
        isValid = false;
    }

    $(".smart-form", $("#TreePricing").html()).not('.remove').not('.diseabled').each(function () {
        var index = $("#BracketsIndex", $(this)).val();

        if ($("#PricingValue_" + index).val() == undefined || $("#PricingValue_" + index).val() == "" || parseFloat($("#PricingValue_" + index).val()) < 0) {
            $("#PricingValue_" + index).addClass("input-error");
            isValid = false;
        }
        if (!$("#MinFeeValue_" + index).parents(".col-2").hasClass("hide")) {
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

/********************** end saves  Pricing ***************************/



