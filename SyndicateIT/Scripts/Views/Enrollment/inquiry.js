

function LoadContentInquiry() {
    $('#Inquiry-Details').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadContentInquiryUrl, {}, function (data) {
        $('#Inquiry-Details').html(data);
    });
}


function LoadInquiryContent() {
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadInquiryContentUrl, {}, function (data) {
        $('#MainContent').html(data);
    });
}

function LoadInquiryContentDetails() {
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadInquiryContentDetailsUrl, {}, function (data) {
        $('#MainContent').html(data);
    });
}


function LoadInquiryHistory() {
    $('#Main-inquiry').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadInquiryHistoryUrl, {}, function (data) {
        $('#Main-inquiry').html(data);
    });
}


function LoadInquiryProfileLimit() {
    $('#Main-inquiry').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadInquiryProfileLimitUrl, {}, function (data) {
        $('#Main-inquiry').html(data);
    });
}


function LoadInquiryTransaction() {
    $('#Main-inquiry').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadInquiryTransactionUrl, {}, function (data) {
        $('#Main-inquiry').html(data);
    });
}


function SearchDateInquiry() {
    var searchFromDate = $("#SearchFromDate").val();
    var searchToDate = $("#SearchToDate").val();
    $('#InquiryCustomerAccountsList').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadInquiryCustomerAccountsUrl, { searchFromDate: searchFromDate, searchToDate: searchToDate }, function (data) {
        $('#InquiryCustomerAccountsList').html(data);
    });
}
