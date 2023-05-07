function SearchReport() {
    $('#Report-Details').html("");
    if (!isValidateSearch())
        return false;   

    var searchFromDate = $("#SearchFromDate").val();
    var searchToDate = $("#SearchToDate").val();
    var pageId = $("#PageID").val();
    var aTypeCode = $("#AccountTypeCode").data("kendoDropDownList").value();
    var pTypeId = $("#PaymentTypeID").val();
    var isActive = $(Status).prop("checked")

    $('#Report-Details').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");     

    $.post(loadReportUrl, { searchFromDate: searchFromDate, searchToDate: searchToDate, aTypeCode: aTypeCode, pTypeId: pTypeId, isActive:isActive , pageId: pageId }, function (data) {
         $('#Report-Details').html(data);        
    });
}


function isValidateSearch() {

    $("#ErrorSearchFromDate").hide();
    $("#ErrorSearchToDate").hide();  

    var isValid = true;  
    var searchFromDate = $("#SearchFromDate").val();
    var searchToDate = $("#SearchToDate").val();  

    if (searchFromDate == "" && searchToDate == "" ) {
        $("#ErrorSearchFromDate").html("From Date is required").show();
        $("#ErrorSearchToDate").html("To Date is required").show();
        isValid = false;
    }
    if (searchFromDate == "") {
        $("#ErrorSearchFromDate").html("From Date is required").show();
        isValid = false;

    } else if (searchToDate = "") {
        $("#ErrorSearchToDate").html("To Date is required").show();
        isValid = false;
    }  

    return isValid;
}


