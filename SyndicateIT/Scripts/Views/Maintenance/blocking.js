function SaveBlocking() {
    
    if (!isValidateSaveBlocking())
        return false;

    var myData = $('#Blocking-form').serialize();

    $('#Blocking-Details').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(saveBlockingUrl, myData, function (data) {
        $('#Blocking-Details').html(data);
    });
}


function isValidateSaveBlocking() {

     $("#BlockingReason_validationMessage").hide(); 

    var isValid = true;
   
   
    if ($("#BlockingReason").data("kendoDropDownList").value() == "") {
        $("#BlockingReason_validationMessage").show();
        isValid = false;
    }
    

    return isValid;
}


function LoadContentBlocking() {
    $('#Blocking-Details').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadContentBlockingUrl, {}, function (data) {
        $('#Blocking-Details').html(data);
    });
}
