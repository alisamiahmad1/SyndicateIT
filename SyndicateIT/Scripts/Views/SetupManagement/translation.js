
function SaveTranslation() {

    if (!isValidateSaveTranslation())
        return false;
    var myData = $('#Translation-form').serialize();
    $.post(saveTranslationUrl, myData, function (data) {
        $('#TranslationContent').html(data);
        $('#Translation_NAME').val("");
        $('#IS_ACTIVE').is(":checked");
        $('#widget-grid-Transalte').hide();
    });
}
 function AddTranslation() {
    $('#TranslationContent').html("");
    $('#TranslationContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationURL, {}, function (data) {
        $('#TranslationContent').html(data);
       
    });
}

function EditTranslation(id) {
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    $('#TranslationContent').html("");
    $('#TranslationContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationURL, { TranslationId: dataItem.STP_Translation_ID }, function (data) {
        $('#TranslationContent').html(data)
    });
}

function SearchTranslation() {

    $('#TranslationList').html("");
    //if (!isValidateSearch())
    //    return false;
    $(".alert-dismissible").hide();
    var tablename = $("#TableID").data("kendoDropDownList").text()
    $('#TranslationList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(TranslationSearchURL, { TableId: TableId, tablename: tablename }, function (data) {
        if (data.success == undefined) {
            $('#TranslationList').html(data);
        } else if (data.success == false) {

        }
    });

}



function isValidateSaveTranslation() {

    var isValid = true;

    var TranslationName = $("#Translation_NAME").val();
    $("#Translation_NAME_validationMessage").hide();
    if (TranslationName == "") {


        $("#Translation_NAME_validationMessage").show();
        isValid = false;
    }
    return isValid;
}