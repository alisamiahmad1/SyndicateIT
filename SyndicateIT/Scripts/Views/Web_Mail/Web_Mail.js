function LaunchInbox_Sent(page_Name) {
    $('#Web_MailContent').html("");
    $('#Web_MailContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadInbox_SentURL, { pageName: page_Name }, function (data) {
        $('#Web_MailContent').html(data);
    });
}

function SaveCompose() {
    var myData = $('#Compose-form').serialize();
    var files=$("#lstAttachements").prop("files");
    $.post(saveComposeUrl, myData,  function (data) {
        $('#Web_MailContent').html(data);
    });

}

function EditMessage_Content(id) {
    var dataItem = id;
    $('#Web_MailContent').html("");
    $('#Web_MailContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadMessageContentURL, { Message_ID: dataItem }, function (data) {
        $('#Web_MailContent').html(data);
       
    });
}
