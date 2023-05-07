

function AddAccountTypeBrackets(obj) {
    var templateIndex = $("#TemplateIndex").val();
    var $template = $("#TemplatePricingEmpty").clone();
    $(".add", $template).each(function (index) {
        var id = $(this).attr("id");
        var name = $(this).attr("name");
        $(this).attr("id", id.replace("_t", "_" + templateIndex));
        if ($(this).attr("name") != "" && $(this).attr("name") != undefined) {
            $(this).attr("name", name.replace("_t", "_" + templateIndex));
        }
    });
    if ($(".checkbox-Bracket", $(obj).parents(".smart-li-form")).is(':checked') == false) {
        $("#SectionMinBracketValue_" + templateIndex, $template).addClass("hide");
        $("#SectionMaxBracketValue_" + templateIndex, $template).addClass("hide");
        $("#HasBracket_" + templateIndex, $template).val(false);
    }
    $("#BracketsIndex", $template).val(templateIndex);
    $("#BracketsStatus", $template).val("C");
    $(".smart-li-form", $(obj).parents(".accountType-content")).append($template.html());
    var n1 = parseInt(templateIndex);
    var n2 = parseInt("1");
    var index = n1 + n2;
    $("#TemplateIndex").val(index);
    CheckDecimalOnly();
}


function onDeletePaymentType(obj) {
    var answer = BootstrapDialog.confirm({
        title: 'Pin Pay',
        message: "Are you sure you want to delete this Payment Type?",
        callback: function (result) {
            if (result) {
               $(".accountType-content", $(obj).parents(".paymentType-content")).each(function () {
                    var $PaymentTypeObject = $(this);
                    $(".checkbox-Bracket", $(this)).prop('checked', false);
                    $(".btn-primary", $(this)).addClass("hide");
                    $(".smart-form", $(this)).each(function (counter) {
                        if (counter == 0)
                        {
                            AddAccountTypeBrackets($(this));
                        }
                        if ($("input[id^='AccountTypeBracketID_']", $(this)).val() > 0) {
                            var index = $("#BracketsIndex", $(this)).val();
                            HideBracketPricing(index);
                            $(this).addClass("hide remove");
                            $("#BracketsStatus", $(this)).val("D");
                        } else { $(this).remove(); }
                    });
                });
            } else {
                return false;
            }
        }
    })

}


function DeleteAccountTypeBrackets(obj) {

    var answer = BootstrapDialog.confirm({
        title: 'Pin Pay',
        message: "Are you sure you want to apply this Pricing?",
        callback: function (result) {
            if (result) {
                var $div = $(obj).parents(".smart-li-form");
                if ($("input[id^='AccountTypeBracketID_']", $(obj).parents(".smart-form")).val() > 0) {
                    $(obj).parents(".smart-form").addClass("hide remove");
                    $("#BracketsStatus", $(obj).parents(".smart-form")).val("D");
                } else { $(obj).parents(".smart-form").remove(); }
                var tcount = $(".smart-form", $div).not('.remove').length;
                if (tcount == 0) { AddNewAccountTypeBrackets($div); }
            } else {
                return false;
            }
        }
    })
   
}


function AddNewAccountTypeBrackets($div) {
    var templateIndex = $("#TemplateIndex").val();
    var $template = $("#TemplatePricingEmpty").clone();
    $(".add", $template).each(function (index) {
        var id = $(this).attr("id");
        var name = $(this).attr("name");
        $(this).attr("id", id.replace("_t", "_" + templateIndex));
        if ($(this).attr("name") != "" && $(this).attr("name") != undefined) {
            $(this).attr("name", name.replace("_t", "_" + templateIndex));
        }
    });

    if ($(".checkbox-Bracket", $div).is(':checked') == false) {
        $("#SectionMinBracketValue_" + templateIndex, $template).addClass("hide");
        $("#SectionMaxBracketValue_" + templateIndex, $template).addClass("hide");
        $("#HasBracket_" + templateIndex, $template).val(false);
    }

    $("#BracketsIndex", $template).val(templateIndex);
    $("#BracketsStatus", $template).val("C");

    $(".smart-li-form", $div.parents(".accountType-content")).append($template.html());
    var n1 = parseInt(templateIndex);
    var n2 = parseInt("1");
    var index = n1 + n2;
    $("#TemplateIndex").val(index);
    CheckDecimalOnly();
}


function onDeleteAccountType(obj) {

    var answer = BootstrapDialog.confirm({
        title: 'Pin Pay',
        message: "Are you sure you want to apply this Account Type?",
        callback: function (result) {
            if (result) {
               $(".checkbox-Bracket", $(obj).parents(".accountType-content")).prop('checked', false);
                $(".btn-primary", $(obj).parents(".accountType-content")).addClass("hide");
                $(".smart-form", $(obj).parents(".accountType-content")).each(function (counter) {
                    var index = $("#BracketsIndex", $(this)).val();
                    if ($("input[id^='AccountTypeBracketID_']", $(this)).val() > 0)
                    {
                        HideBracketPricing(index);
                        $(this).addClass("hide remove");
                    } else{ $(this).remove();}        
                });
                AddAccountTypeBrackets(obj);
            } else {
                return false;
            }
        }
    })

    
}


function onChangeBracket(obj) {
    var message = "";
    if ($(obj).is(':checked') == true) { message = "Are you sure you want to checked this Account Type?"; $(obj).attr("checked", "checked") }
    else {  message ="Are you sure you want to unchecked  this Account Type?"; }

  var answer = BootstrapDialog.confirm({
        title: 'Pin Pay',
        message: message,
        callback: function (result) {
            if (result) {
                $(".btn-primary", $(obj).parents(".accountType-content")).addClass("hide");
                $(".smart-form", $(obj).parents(".accountType-content")).each(function (counter) {
                    var index = $("#BracketsIndex", $(this)).val();
                    if ($("#AccountTypeBracketID_" + index).val() > 0) {
                        HideBracketPricing(index);
                        $(this).addClass("hide remove");
                        $("#BracketsStatus", $(this)).val("D");
                    } else {  $(this).remove();}
                });
                AddAccountTypeBrackets(obj);
                $(".smart-form", $(obj).parents(".accountType-content")).each(function (counter) {
                    var index = $("#BracketsIndex", $(this)).val();
                    if ($(this).not('.remove')) {
                        if ($(obj).is(':checked') == true) {
                            $(".btn-primary", $(this).parents(".accountType-content")).removeClass("hide");
                            showBracketPricing(index);
                            $("#HasBracket_" + index).val(true);
                        }
                    }
                });

            } else {
                if ($(obj).is(':checked') == true) { $(obj).prop("checked", false); }    
              else {$(obj).prop("checked", true);}
                return false;
            }
        }
    })

}


function onChangeTypePricing(obj) {
    var index = $("#BracketsIndex", $(obj).parents(".smart-form")).val();
    if ($(obj).val() == 1) { HidePricing(index); } else { showPricing(index) }
    CheckDecimalOnly();
}


function HidePricing(index) {
    $("#SectionPricingRounding_" + index).addClass("hide");
    $("#SectionMinFeeValue_" + index).addClass("hide");
    $("#SectionMaxFeeValue_" + index).addClass("hide");
    $("#PricingRounding_" + index).val("0");
    $("#MinFeeValue_" + index).val("0");
    $("#MaxFeeValue_" + index).val("0");
}


function showPricing(index) {
    $("#SectionPricingRounding_" + index).removeClass("hide");
    $("#SectionMinFeeValue_" + index).removeClass("hide");
    $("#SectionMaxFeeValue_" + index).removeClass("hide");
    $("#PricingRounding_" + index).val("0");
    $("#MinFeeValue_" + index).val("0");
    $("#MaxFeeValue_" + index).val("0");
}


function HideBracketPricing(index) {
    $("#SectionMinBracketValue_" + index).addClass("hide");
    $("#SectionMaxBracketValue_" + index).addClass("hide");
    $("#MinBracketValue_" + index).val("0");
    $("#MaxBracketValue_" + index).val("0");
    $("#HasBracket_" + index).val(false);
}


function showBracketPricing(index) {
    $("#SectionMinBracketValue_" + index).removeClass("hide");
    $("#SectionMaxBracketValue_" + index).removeClass("hide");
    $("#MinBracketValue_" + index).val("0");
    $("#MaxBracketValue_" + index).val("0");
    $("#HasBracket_" + index).val(true);
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