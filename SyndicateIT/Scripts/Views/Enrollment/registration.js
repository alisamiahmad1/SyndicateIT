function SaveRegistration() {
    
    if (!isValidateSaveRegistration())
        return false;

    var myData = $('#Registration-form').serialize();

    $('#Registration-Details').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(saveRegistrationUrl, myData, function (data) {
        $('#Registration-Details').html(data);
    });
}


function isValidateSaveRegistration() {

    HideRegistrationValidation();

    var isValid = true;


    if ($("#CustomerCIFID").val() == "") {
      $("#CustomerCIFID_validationMessage").show();
        isValid = false;
    }  
    if ($("#CustomerMobile").val() == "") {
     $("#CustomerMobile_validationMessage").show();
        isValid = false;
    }  
    if ($("#CustomerName").val() == "") {
        $("#CustomerName_validationMessage").show();
        isValid = false;
    }
    if ($("#CustomerEmail").val() == "") {
        $("#CustomerEmail_validationMessage").show();
        isValid = false;
    }
    else if (!isValidEmailAddress($("#CustomerEmail").val()))
    {
        $("#CustomerEmailRegx_validationMessage").show();
        isValid = false;
    }
    if ($("#SalesPersonID").val() == "") {
        $("#SalesPersonID_validationMessage").show();
        isValid = false;
    }
    if ($("#PrimaryAccountNo").val() == "") {
        $("#PrimaryAccountNo_validationMessage").show();
        isValid = false;
    }
    if ($("#Address").val() == "") {
        $("#Address_validationMessage").show();
        isValid = false;
    }
    if ($("#Country").val() == "") {
        $("#Country_validationMessage").show();
        isValid = false;
    }
    if ($("#City").val() == "") {
        $("#City_validationMessage").show();
        isValid = false;
    }
    if ($("#PinPaySecurityNickName").val() == "") {
        $("#PinPaySecurityNickName_validationMessage").show();
        isValid = false;
    }
    if ($("#CustomerClassification").data("kendoDropDownList").value() == "") {
        $("#CustomerClassification_validationMessage").show();
        isValid = false;
    }
    if ($("#CustomerTypeID").data("kendoDropDownList").value() == "") {
        $("#CustomerTypeID_validationMessage").show();
        isValid = false;
    }

    if ($("#ServicePlan").data("kendoDropDownList").value() == "") {
        $("#ServicePlan_validationMessage").show();
        isValid = false;
    }
    if ($("#AssignedBranch").data("kendoDropDownList").value() == "") {
        $("#AssignedBranch_validationMessage").show();
        isValid = false;
    }
    if ($("#Channel").data("kendoDropDownList").value() == "") {
        $("#Channel_validationMessage").show();
        isValid = false;
    }

    return isValid;
}

function HideRegistrationValidation() {
    $("#CustomerEmailRegx_validationMessage").hide();
    $("#CustomerCIFID_validationMessage").hide();
    $("#CustomerMobile_validationMessage").hide();
    $("#CustomerName_validationMessage").hide();
    $("#CustomerEmail_validationMessage").hide();
    $("#PinPaySecurityNickName_validationMessage").hide();
    $("#SalesPersonID_validationMessage").hide();
    $("#PrimaryAccountNo_validationMessage").hide();
    $("#Address_validationMessage").hide();
    $("#Country_validationMessage").hide();
    $("#City_validationMessage").hide();
    $("#CustomerClassification_validationMessage").hide();
    $("#CustomerTypeID_validationMessage").hide();
    $("#ServicePlan_validationMessage").hide();
    $("#AssignedBranch_validationMessage").hide();
    $("#Channel_validationMessage").hide();
}

function LoadContentRegistration() {
    $('#Registration-Details').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadContentRegistrationUrl, {}, function (data) {
        $('#Registration-Details').html(data);
    });
}

function isValidEmailAddress(emailAddress) {
    //var emailExp = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    //return emailExp.test(emailAddress);
    var pattern = new RegExp(/^[+a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i);    
    return pattern.test(emailAddress);
}

function CheckNumericOnly() {
    $("#PrimaryAccountNo").bind('keypress', function (e) {
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
    $("#PrimaryAccountNo").bind("paste", function (e) {
        e.preventDefault();
    });

    $("#PrimaryAccountNo").bind('mouseenter', function (e) {
        var val = $(this).val();
        if (val != '0') {
            val = val.replace(/[^0-9]+/g, "")
            $(this).val(val);
        }
    });
    //////////////////
    $("#SalesPersonID").bind('keypress', function (e) {
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
    $("#SalesPersonID").bind("paste", function (e) {
        e.preventDefault();
    });

    $("#SalesPersonID").bind('mouseenter', function (e) {
        var val = $(this).val();
        if (val != '0') {
            val = val.replace(/[^0-9]+/g, "")
            $(this).val(val);
        }
    });
    //////////////////
    $("#CustomerCIFID").bind('keypress', function (e) {
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
    $("#CustomerCIFID").bind("paste", function (e) {
        e.preventDefault();
    });

    $("#CustomerCIFID").bind('mouseenter', function (e) {
        var val = $(this).val();
        if (val != '0') {
            val = val.replace(/[^0-9]+/g, "")
            $(this).val(val);
        }
    });
    //////////////////
    $("#CustomerMobile").bind('keypress', function (e) {
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
    $("#CustomerMobile").bind("paste", function (e) {
        e.preventDefault();
    });

    $("#CustomerMobile").bind('mouseenter', function (e) {
        var val = $(this).val();
        if (val != '0') {
            val = val.replace(/[^0-9]+/g, "")
            $(this).val(val);
        }
    });
}