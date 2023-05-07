

function LoadContentResetPin() {
    $('#ResetPin-Details').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadContentResetPinUrl, {}, function (data) {
        $('#ResetPin-Details').html(data);
    });
}
