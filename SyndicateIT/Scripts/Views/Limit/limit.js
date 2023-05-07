function SearchLimit() {
    $('#LimitDefinitions-Details').html("");
    if (!isValidateSearch())
        return false;
   

    var searchFromDate = $("#SearchFromDate").val();
    var searchToDate = $("#SearchToDate").val();
    var pageId = $("#PageID").val();
  
    if (pageId == 1) {
        $('#LimitDefinitions-Details').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");   
     }

    $.post(loadLimitUrl, { searchFromDate: searchFromDate, searchToDate: searchToDate, pageId: pageId }, function (data) {
        if (pageId == 1) {
            $('#LimitDefinitions-Details').html(data);
        } 
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


