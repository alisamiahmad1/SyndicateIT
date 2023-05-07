
function SearchFiles() {
    $('#File-List').html("");
  
    var searchFileType = $("#FileType").val();
   
    $('#File-List').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadFilesUrl, { searchFileType: searchFileType}, function (data) {
        $('#File-List').html(data);
    });
}

function FilterClass() {
    return { Cycle_id: $("#SearchCycle").data("kendoDropDownList").value() };
}

function FilterDivision() {
    return { Class_ID: $("#SearchClass").data("kendoDropDownList").value() };
}
function isValidateFileSearch() {

    $("#ErrorSearchCycle").hide();
    $("#ErrorSearchClass").hide();
    $("#ErrorSearchDivision").hide();
    var isValid = true;
    if ($("#SearchCycle").data("kendoDropDownList").value() == 0) {
        $("#ErrorSearchCycle").show();
        isValid = false;
    }

    if ($("#SearchClass").data("kendoDropDownList").value() == 0) {
        $("#ErrorSearchClass").show();
        isValid = false;
    }

    if ($("#SearchDivision").data("kendoDropDownList").value() == 0) {
        $("#ErrorSearchDivision").show();
        isValid = false;
    }

    return isValid;
}


function ViewFileDetail(fileId, fileTypeId) {
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    var fileType = $("#FileTypeID").val();
    $.post(loadFilesContentDetailsUrl, { fileId: fileId, fileType: fileTypeId }, function (data) {
        $('#MainContent').html(data);
    });
}
function ViewPeronalDetail(fileId, sourceId) {

    window.location.href = '/ProfileManagement/ViewProfilesContentDetails?userId=' + fileId + '&sourceId=' + sourceId;

}
function ViewFileExam(fileId, sourceId) {

    window.location.href = '/ExamManagement/FileStudent?userId=' + fileId ;

}

function LoadFileExamProgram() {
    $('#Main-FilesesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadFileExamProgramUrl, {}, function (data) {
        $('#Main-FilesesDetails').html(data);
    });
}


function LoadFilesContentDetails() {
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    var uerTypeID = $("#UserTypeID").val();
    $.post(loadFilesContentDetailsUrl, { uerTypeID: uerTypeID }, function (data) {
        $('#MainContent').html(data);
    });
}

function LoadFilesContent() {
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadFilesContentUrl, {}, function (data) {
        $('#MainContent').html(data);
    });
}


function LoadFileDocument() {
    $('#Main-FilesesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadFileDocumentUrl, {}, function (data) {
        $('#Main-FilesesDetails').html(data);
    });
}

function LoadFileDistibutionStudents() {
    $('#Main-FilesesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadFileDistibutionStudentsUrl, {}, function (data) {
        $('#Main-FilesesDetails').html(data);
    });
}

function LoadFileAttendance() {
    $('#Main-FilesesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadFileAttendanceUrl, {}, function (data) {
        $('#Main-FilesesDetails').html(data);
    });
}

function LoadFileSchedule() {
    $('#Main-FilesesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadFileScheduleUrl, {}, function (data) {
        $('#Main-FilesesDetails').html(data);
    });
}

function LoadFileAgenda() {
    $('#Main-FilesesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadFileAgendaUrl, {}, function (data) {
        $('#Main-FilesesDetails').html(data);
    });
}


function SearchAgenda() {
    $('#Agenda-Daily-List').html("");

    var searchDate = $("#SearchDate").val();
    $('#Agenda-Daily-List').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadAgendaListUrl, { searchDate: searchDate }, function (data) {
        $('#Agenda-Daily-List').html(data);
    });
}
