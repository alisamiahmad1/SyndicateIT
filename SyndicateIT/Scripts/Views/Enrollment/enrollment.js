
function LoadContentRegistration() {
    $('#Registration-Details').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadContentRegistrationUrl, {}, function (data) {
        $('#Registration-Details').html(data);
    });
}

function SearchEnrollment() {
    $('#View-Inquiry-Details').hide();
    $('#Registration-Details').html("");
    if (!isValidateSearch())
        return false;
    $(".alert-dismissible").hide();
    var confirmSearchMobile = "";
    var searchClassificationID = "";

    var searchCIFID = $("#SearchCIFID").val();
    var searchMobile = $("#SearchMobile").val();
    var pageId = $("#PageID").val();
    if (IsShowConfirmSearchMobileDropDown == true) {
         confirmSearchMobile = $("#ConfirmSearchMobile").val();
    }
    if (IsShowConfirmSearchMobileDropDown == true) {
        searchClassificationID = $("#SearchClassificationID").data("kendoDropDownList").value();
    }     
    if (pageId == 1) {
        $('#Registration-Details').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    } else if (pageId == 2) {
        $('#Modification-Details').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    }
    else {
        $('#Inquiry-Details').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    }


    $.post(loadEnrollmentUrl, { searchCIFID: searchCIFID, searchMobile: searchMobile, confirmSearchMobile: confirmSearchMobile, searchClassificationID: searchClassificationID, pageId: pageId }, function (data) {
        if (pageId == 1) {
            $('#Registration-Details').html(data);
        }else  if (pageId == 2) {
            $('#Modification-Details').html(data);
        } else {
            $('#Inquiry-Details').html(data);
            if ($("#HasAlert").val() == false || $("#HasAlert").val()==undefined)
            {
                 $('#View-Inquiry-Details').show();
            }
           
        }
    });
}


function isValidateSearch() {

    $("#ErrorSearchCIFID").hide();
    $("#ErrorSearchMobile").hide();
    $("#ErrorConfirmSearchMobile").hide();
    $("#ErrorSearchClassificationID").hide();
    var pageId = $("#PageID").val();
    var isValid = true;
    confirmSearchMobile = "";
    searchClassificationID = "";
    var searchCIFID = $("#SearchCIFID").val();
    var searchMobile = $("#SearchMobile").val(); 
  

    if (IsShowConfirmSearchMobileDropDown == true) {
         confirmSearchMobile = $("#ConfirmSearchMobile").val();
    }
    if (IsShowClassificationDropDown == true) {
         searchClassificationID = $("#SearchClassificationID").data("kendoDropDownList").value();
    }


    if (searchCIFID == "" && searchMobile == "" && ((confirmSearchMobile == "" && IsShowConfirmSearchMobileDropDown.value == "True") || (IsShowConfirmSearchMobileDropDown.value == "False")) && ((searchClassificationID == "" && IsShowClassificationDropDown.value == "True") || (IsShowClassificationDropDown.value == "False")))
    {
        $("#ErrorSearchCIFID").html("Customer CIF ID is required").show();
        $("#ErrorSearchMobile").html("Customer Mobile is required").show();
        if (IsShowConfirmSearchMobileDropDown.value == "True") {
            $("#ErrorConfirmSearchMobile").html("Confirm Customer Mobile is required").show();
        }
        if (IsShowClassificationDropDown.value == "True") {
            $("#ErrorSearchClassificationID").html("Customer ClassificationID is required").show();
        }
       
        isValid = false;
    }
    if (searchCIFID == "" && pageId != 3) {
        $("#ErrorSearchCIFID").html("Customer CIF ID is required").show();
        isValid = false;

    } else if (searchMobile == "") {
        $("#ErrorSearchMobile").html("Customer Mobile is required").show();
        isValid = false;
    }
    else if (confirmSearchMobile == "" && IsShowConfirmSearchMobileDropDown == true) {
        $("#ErrorConfirmSearchMobile").html("Confirm Customer Mobile is required").show();
        isValid = false;
    } else if (searchClassificationID == "" && IsShowClassificationDropDown == true) {
        $("#ErrorSearchClassificationID").html("Customer ClassificationID is required").show();
        isValid = false;
    }

    return isValid;
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





