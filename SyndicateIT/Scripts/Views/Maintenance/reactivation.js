function SaveReactivation() {
    
   
    var myData = $('#Reactivation-form').serialize();

    $('#Reactivation-Details').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(saveReactivationUrl, myData, function (data) {
        $('#Reactivation-Details').html(data);
    });
}


function LoadContentReactivation() {
    $('#Reactivation-Details').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadContentReactivationUrl, {}, function (data) {
        $('#Reactivation-Details').html(data);
    });
}
