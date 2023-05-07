function SaveCancellation() {
    
    if (!isValidateSaveCancellation())
        return false;

    var myData = $('#Cancellation-form').serialize();

    $('#Cancellation-Details').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(saveCancellationUrl, myData, function (data) {
        $('#Cancellation-Details').html(data);
    });
}


function isValidateSaveCancellation() {

     $("#CancellationReason_validationMessage").hide(); 

    var isValid = true;
   
   
    if ($("#CancellationReason").data("kendoDropDownList").value() == "") {
        $("#CancellationReason_validationMessage").show();
        isValid = false;
    }
    

    return isValid;
}


function LoadContentCancellation() {
    $('#Cancellation-Details').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadContentCancellationUrl, {}, function (data) {
        $('#Cancellation-Details').html(data);
    });
}
