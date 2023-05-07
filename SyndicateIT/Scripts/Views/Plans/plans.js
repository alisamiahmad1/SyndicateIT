

function isValidateIntersectionSavePaymentTypePricing() {

    var isValid = true;

    $(".accountType-content", $("#TreePricing").html()).each(function () {
        var counter = 0;
         if ($(".checkbox-Bracket", $(this)).is(':checked') == true) {
            var countDiv = $(".smart-form", $(this)).not('.remove').length;
           
            $(".smart-form", $(this)).not('.remove').each(function () {
                var index = $("#BracketsIndex", $(this)).val();

                var bracketsStatus = $("#BracketsStatus", $(this)).val();

                if (bracketsStatus != "D")
                {
                    for (var i = counter; i < countDiv; i++) {
                        var secondIndex = $("#BracketsIndex", $(this).next().not('.remove')).val();
                        var firstArray = [$("#MinBracketValue_" + index).val(), $("#MaxBracketValue_" + index).val()];
                        var secondArray = [$("#MinBracketValue_" + secondIndex).val(), $("#MaxBracketValue_" + secondIndex).val()];
                        var arrayIntersect = AarrayIntersect(firstArray, secondArray);
                        if (arrayIntersect == true) {
                            $("#MaxBracketValue_" + secondIndex).addClass("input-error");
                            $("#MinBracketValue_" + secondIndex).addClass("input-error");
                            isValid = false;
                        }
                    }
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

function SavePaymentTypePricing() {
    currentTemplatePricing = [];
    HideError();
    if (!isValidateSavePaymentTypePricing()) {
        $("#TempalteAlert-Type").show();
        $("#TempalteAlert-Warning").html("fill in required fields");
        return false;
    }
    if (!isValidateIntersectionSavePaymentTypePricing()) {
        $("#TempalteAlert-Type").show();
        $("#TempalteAlert-Warning").html("Brackets data are intersection");
        return false;
    }
    $(".smart-form", $("#TreePricing").html()).each(function () {
        var index = $("#BracketsIndex", $(this)).val();

        var $accountTypeObject = $(this).parents(".accountType-content");
        var $paymentTypeObject = $accountTypeObject.parents(".paymentType-content");

        currentTemplatePricing.push({
            PlanID: $("input[id^='PlanID_']", $paymentTypeObject).val(),
            PaymentTypeID: $("input[id^='PaymentType_']", $paymentTypeObject).val(),
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
    $('#PlansSubscriberPricing-content').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(savePaymentTypePricingUrl, { currentTemplatePricing: currentTemplatePricing }, function (data) {
        $('#PlansSubscriberPricing-content').html(data);
    });
}




function InitializeSelectedAccountTypes(SelectedAccountTypesArray)
{

    var arrValue = SelectedAccountTypesArray.split(',');

    $('#SelectedAccountTypes option').each(function (i, item) {
        var itemValue = $(item).val();
        var found = $.inArray(itemValue, arrValue) > -1;
        if (found)
        {
            item.disabled = true;
            $(item).attr("style", "background-color: #ccc;");
        }
            
    });
}


function onDataBound(e) {
    var grid = $("#PlansPaymentTypeGrid").data("kendoGrid");
    var gridData = grid.dataSource.view();
    for (var i = 0; i < gridData.length; i++) {
        var currentUid = gridData[i].uid;
        var showTemplate = gridData[i].ShowTemplate;
        var billingTypeID = gridData[i].BillingTypeID;
        var paymentTypeID = gridData[i].PaymentTypeID;
        var currentRow = grid.table.find("tr[data-uid='" + currentUid + "']");
        if (showTemplate == true) {
            FillDropDownDataSource(paymentTypeID,billingTypeID);
        }        
    }
}


function FillDropDownDataSource(paymentTypeID,billingTypeID)
{
    var ddl = $('#inputSelect_' + paymentTypeID);
    ddl.empty();
    $.post(loadTemplatePricingByBillingTypeUrl, { billingTypeID: billingTypeID }, function (result) {
        $(result).each(function () {
            $(document.createElement('option'))
                .attr('value', this.Value)
                .text(this.Text)
                .appendTo(ddl);
        });
    });
}

function ApplySubscriberPricing() {
    currentSelectTemplate= [];
    var grid = $("#PlansPaymentTypeGrid").data("kendoGrid");
    var sel = $("input:checked:not(:disabled)", grid.tbody).closest("tr");
    var items = [];
    $.each(sel, function (idx, row) {
        var item = grid.dataItem(row);
        items.push(item.PaymentTypeID);
   
        var dpDownTemplateID = $("#inputSelect_" + item.PaymentTypeID).val();

        if (dpDownTemplateID > 0)
        {
         currentSelectTemplate.push({
            PaymentTypeID: item.PaymentTypeID,
            BillingTypeID: item.BillingTypeID,
            TemplateID: dpDownTemplateID,
          });
        }
       
    });

    if (items != "") {
        var answer = BootstrapDialog.confirm({
            title: 'Pin Pay',
            message: "Are you sure you want to apply this Payment Type?",
            callback: function (result) {
                if (result) {
                    $('#PaymentType-Content').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
                    $.post(loadApplySubscriberPricingUrl, { plansTypeIds: items, templatePricingSelected: currentSelectTemplate }, function (data) {
                        $('#PaymentType-Content').html(data.pricingTemplate);
                        $('#paymentTypeSelected-container').html(data.paymentTemplate).show();
                    });
                } else {
                    return false;
                }
            }
        })
    } else {
        BootstrapDialog.alert("Please select at least one Payment Type ")
    }
}



    //function DeletePlansSetup(e) {

    //    e.preventDefault();
    //    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    //    var answer = BootstrapDialog.confirm({
    //        title: 'Pin Pay',
    //        message: "Are yu sure to delete this plans",
    //        callback: function (result) {
    //            if (result) {
    //                $.post(deletePlansSetupURL, { id: dataItem.ID }, function (data) {
    //                    /// <summary>
    //                    /// 
    //                    /// </summary>
    //                    /// <param name="data">The data.</param>
    //                    if (data.success) {
    //                        var grid = $("#PlansSetupGrid").data("kendoGrid");
    //                        grid.dataSource.read();
    //                    } else { alert(data.message); }
    //                });
    //            } else {
    //                return false;
    //            }
    //        }
    //    })
    //}






    function EditPlansSetup(e) {
        e.preventDefault();
        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
        $.post(loadPlansSetupContentDetailsUrl, { id: dataItem.ID }, function (data) {
            $('#MainContent').html(data);
        });
    }

    function SearchPlans() {
        $('#PlansSetup-Details').html("");
        //if (!isValidatePlansSearch())
        //    return false;   
        var searchPlanName = $("#SearchPlanName").val();  
        var pageId = $("#PageID").val();
  
        if (pageId == 1) {
            $('#PlansSetup-Details').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
        }

        $.post(loadPlansUrl, { searchPlanName: searchPlanName,pageId:pageId }, function (data) {
            if (pageId == 1) {
                $('#PlansSetup-Details').html(data);
            } 
        });
    }


    function isValidatePlansSearch() {

        $("#ErrorSearchPlanName").hide();

        var isValid = true;  
        var searchPlanName= $("#SearchPlanName").val(); 
    
        if (searchPlanName == "") {
            $("#ErrorSearchPlanName").html("Plan Name is required").show();
            isValid = false;
        }
        return isValid;
    }

   
