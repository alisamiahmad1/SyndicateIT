function EditExamsProgram(e) {
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadExamProgramDetailsUrl, { id: dataItem.ID }, function (data) {
        $('#MainContent').html(data);
    });

}

function DeleteJoinCourse(id) {
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: 'Are You sure do you want to delete this Record',
        callback: function (result) {
            if (result) {
                $('#ExamProgramListID').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
                $.post(deleteJoinCourseUrl, { id, id }, function (data) {
                    $('#ExamProgramListID').html(data);
                    //$("#Alert-Type").hide();
                });
            } else {
                return false;
            }
        }
    })
}


function SaveExamProgram() {


    var cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
    var class_ID = $("#Class_ID").data("kendoDropDownList").value();
    var division_ID = $("#Division_ID").data("kendoDropDownList").value();
    var course_ID = $("#Course_ID").data("kendoDropDownList").value();
    var periodDate = $("#PeriodDate").val();
    var periodTime = $("#PeriodTime").val();


    if (!isValidateExamProgramSearch())
        return false;

    $('#ExamProgramListID').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(saveExamProgramUrl, { cycle_ID: cycle_ID, class_ID: class_ID, division_ID: division_ID, course_ID: course_ID, periodDate: periodDate, periodTime: periodTime }, function (data) {
        $('#ExamProgramListID').html(data);
    });
}

function isValidateExamProgramSearch() {

    $("#Course_ID_validationMessage").hide();
    var isValid = true;
    if ($("#Course_ID").data("kendoDropDownList").value() == null || $("#Course_ID").data("kendoDropDownList").value() == 0 || $("#Course_ID").data("kendoDropDownList").value() == "") {
        $("#Course_ID_validationMessage").show();
        isValid = false;
    }

    return isValid;
}


function SearchServices() {
    var searchCourse = "00000000-0000-0000-0000-000000000000";
    var searchFromDate = null;
    var searchToDate = null;
    var searchPeriodMonth = -1;
    var searchExamName = "";
    var searchCycle = -1;
    var searchClass = "00000000-0000-0000-0000-000000000000";
    var searchDivision = -1;

    $('#Service-List').html("");

    if ($("#UserTypeID").val() == "5") {
        var searchExamName = $("#SearchExamName").val();
    }
    else
    {
        if (!isValidateServicesSearch())
            return false;
        var searchCycle = $("#SearchCycle").data("kendoDropDownList").value();
        var searchClass = $("#SearchClass").data("kendoDropDownList").value();
        var searchDivision = $("#SearchDivision").data("kendoDropDownList").value();

    }


   
    if ($("#UserTypeID").val() == "1") {
        var searchCourse = $("#SearchCourse").data("kendoDropDownList").value();
        var searchFromDate = $("#SearchFromDate").val();
        var searchToDate = $("#SearchToDate").val();
    }else
        if ($("#UserTypeID").val() == "6" || $("#UserTypeID").val() == "7" || $("#UserTypeID").val() == "8" || $("#UserTypeID").val() == "9" || $("#UserTypeID").val() == "10" || $("#UserTypeID").val() == "11") {
            var searchPeriodMonth = $("#SearchPeriodMonth").data("kendoDropDownList").value();
    }
   
   
   
    var uerTypeID = $("#UserTypeID").val();  
    $('#Service-List').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadServicesUrl, { searchCycle: searchCycle, searchClass: searchClass, searchDivision: searchDivision, searchCourse: searchCourse,searchExamName:searchExamName, searchFromDate: searchFromDate, searchToDate: searchToDate, searchPeriodMonth: searchPeriodMonth, uerTypeID: uerTypeID }, function (data)
     {       
          $('#Service-List').html(data);        
     });
}

function EditServices(e)
{   
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    var uerTypeID = $("#UserTypeID").val();
    $.post(loadServicesContentDetailsUrl, { serviceId: dataItem.ServiceID, uerTypeID: uerTypeID }, function (data) {
        $('#MainContent').html(data);
        $("#Class_ID").data('kendoDropDownList').dataSource.read();
    });

}


function LoadExamProgramList() {
    $('#ExamProgramListID').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadExamProgramListUrl, {}, function (data) {
        $('#ExamProgramListID').html(data);
    });
}



/********************** Agenda  ********************************/

function SaveAgenda() {
    debugger;
    if (!isValidateSaveAgenda())
        return false;          
        
    var myData = $('#Agenda-form').serialize();
    //myData.AgendaID = $('#AgendaID').val();
    myData = myData + '&AgendaID='+ $('#AgendaID').val()


    $('#Main-Service-Container').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(saveAgendaUrl, myData  , function (data) {
        $('#Main-Service-Container').html(data);       
        $("#Class_ID").data('kendoDropDownList').dataSource.read();
    });

}

function SearchAgendaDaily()
{
    $('#Agenda-Daily-List').html("");
    if (!isValidateAgendaDailySearch())
        return false;
    var searchCycle = $("#SearchCycle").data("kendoDropDownList").value();
    var searchClass = $("#SearchClass").data("kendoDropDownList").value();
    var searchDivision = $("#SearchDivision").data("kendoDropDownList").value();   
    var searchDate = $("#SearchDate").val();

    $('#Agenda-Daily-List').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadAgendaDailyListUrl, { searchCycle: searchCycle, searchClass: searchClass, searchDivision: searchDivision, searchDate: searchDate }, function (data) {
        $('#Agenda-Daily-List').html(data);
    });
}

function PublishAgendaDaily(agendaId) {
    $('#Agenda-Daily-List').html("");    
    var searchCycle = $("#SearchCycle").data("kendoDropDownList").value();
    var searchClass = $("#SearchClass").data("kendoDropDownList").value();
    var searchDivision = $("#SearchDivision").data("kendoDropDownList").value();
    var searchDate = $("#SearchDate").val();

    $('#Agenda-Daily-List').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(publishAgendaDailyListUrl, { agendaId:agendaId,searchCycle: searchCycle, searchClass: searchClass, searchDivision: searchDivision, searchDate: searchDate }, function (data) {
        $('#Agenda-Daily-List').html(data);
    });
}
function EditAgendaDaily(serviceId) {
  
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    var uerTypeID = $("#UserTypeID").val();
    $.post(loadServicesContentDetailsUrl, { serviceId: serviceId, uerTypeID: uerTypeID }, function (data) {
        $('#MainContent').html(data);
        $("#Class_ID").data('kendoDropDownList').dataSource.read();
    });

}

function isValidateAgendaDailySearch() {

    $("#ErrorSearchCycle").hide();
    $("#ErrorSearchClass").hide();
    $("#ErrorSearchDivision").hide();
    $("#SearchDate").hide();
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


/********************** Attendance  ********************************/

function LoadAttendanceDetails() {
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    var uerTypeID = $("#UserTypeID").val();
    $.post(loadAttendanceDetailsUrl, { uerTypeID: uerTypeID }, function (data) {
        $('#MainContent').html(data);
    });
}

function LoadAttandanceTable() {
    $('#TableAttandanceId').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");  
    if (!isValidateServicesLevelGrade())
        return false;         
    var Cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
    var Class_ID = $("#Class_ID").data("kendoDropDownList").value();
    var Division_ID = $("#Division_ID").data("kendoDropDownList").value();
    var Perioddate = $("#PeriodDate").val();
    $.post(loadAttandanceTableUrl, { Cycle_ID: Cycle_ID, Class_ID: Class_ID, Division_ID: Division_ID, Perioddate: Perioddate }, function (data) {
        $('#TableAttandanceId').html(data);
    });
}

function LoadAttendanceMonthlyTable() {
    $('#TableAttandanceId').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");  
    $.post(loadAttendanceMonthlyTableUrl, {}, function (data) {
        $('#AttendanceMonthlyTableID').html(data);
    });
}

function SaveAttendance()
{
    var attendanceList = [];

    $(".ServiceTr").each(function (index) {
        var service = new Object();
       
        service.Attendances_ID = $(".hdAttendances", $(this)).val();
        service.Cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
        service.Class_ID = $("#Class_ID").data("kendoDropDownList").value();
        service.Division_ID = $("#Division_ID").data("kendoDropDownList").value();
        service.User_ID = $(".hdUser", $(this)).val();
        service.PeriodDate = $("#PeriodDate").val();       
        service.Reason_ID = $('#Reason_ID' + index).data("kendoDropDownList").value();
        var id = $(".radiobox", $(this)).attr('name');       
        service.Is_Present = $('input[name=' + id + ']:checked', $(this)).val();
        attendanceList.push(service);
    });


    var params = new Object();   
    params.Cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
    params.Class_ID = $("#Class_ID").data("kendoDropDownList").value();
    params.Division_ID = $("#Division_ID").data("kendoDropDownList").value();
    params.PeriodDate = $("#PeriodDate").val();
    params.attendanceList = attendanceList;


    $.post(loadSaveAttendanceUrl, params, function (data) {
        $('#TableAttandanceId').html(data);
    });
}

/********************** Absorption  ********************************/
function LoadAbsorptionGradesDetails() {
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadAbsorptionGradesDetailsUrl, {}, function (data) {
        $('#MainContent').html(data);
    });
}

function LoadAbsorptionGradesMonthlyTable() {
    $('#AbsorptionGradesMonthlyTableID').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadAbsorptionGradesMonthlyTableUrl, {}, function (data) {
        $('#AbsorptionGradesMonthlyTableID').html(data);
    });
}

function LoadAbsorptionGradesTable() {
    $('#AbsorptionGradesTableId').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    if (!isValidateServicesLevelGrade())
        return false;
    var Cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
    var Class_ID = $("#Class_ID").data("kendoDropDownList").value();
    var Division_ID = $("#Division_ID").data("kendoDropDownList").value();
    var Perioddate = $("#PeriodDate").val();
    $.post(loadAbsorptionGradesTableUrl, { Cycle_ID: Cycle_ID, Class_ID: Class_ID, Division_ID: Division_ID, Perioddate: Perioddate }, function (data) {    
        $('#AbsorptionGradesTableId').html(data);
    });
}

function SaveAbsorptionGrades() {
    var attendanceList = [];

    $(".ServiceTr").each(function (index) {
        var service = new Object();

        service.AbsorptionGrades_ID = $(".hdAttendances", $(this)).val();
        service.Cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
        service.Class_ID = $("#Class_ID").data("kendoDropDownList").value();
        service.Division_ID = $("#Division_ID").data("kendoDropDownList").value();
        service.User_ID = $(".hdUser", $(this)).val();
        service.PeriodDate = $("#PeriodDate").val();
        service.Reason_ID = $('#Reason_ID' + index).data("kendoDropDownList").value();
        var id = $(".radiobox", $(this)).attr('name');
        service.Percentage = $('input[name=' + id + ']', $(this)).val();
        attendanceList.push(service);
    });


    var params = new Object();
    params.Cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
    params.Class_ID = $("#Class_ID").data("kendoDropDownList").value();
    params.Division_ID = $("#Division_ID").data("kendoDropDownList").value();
    params.PeriodDate = $("#PeriodDate").val();
    params.absorptionGradesList = attendanceList;


    $.post(loadAbsorptionGradesUrl, params, function (data) {
        $('#AbsorptionGradesTableId').html(data);
    });
}

/********************** Degrees Participation Activites  ********************************/

function LoadDegreesParticipationActivitesDetails() {
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadDegreesParticipationActivitesDetailsUrl, {}, function (data) {
        $('#MainContent').html(data);
    });
}

function LoadDegreesParticipationActivitesMonthlyTable() {
    $('#DegreesParticipationActivitesMonthlyTableID').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadDegreesParticipationActivitesMonthlyTableUrl, {}, function (data) {
        $('#DegreesParticipationActivitesMonthlyTableID').html(data);
    });
}

function LoadDegreesParticipationActivitesTable() {
    $('#DegreesParticipationActivitesTableId').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    if (!isValidateServicesLevelGrade())
        return false;
    var Cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
    var Class_ID = $("#Class_ID").data("kendoDropDownList").value();
    var Division_ID = $("#Division_ID").data("kendoDropDownList").value();
    var Perioddate = $("#PeriodDate").val();
    $.post(loadDegreesParticipationActivitesTableUrl, { Cycle_ID: Cycle_ID, Class_ID: Class_ID, Division_ID: Division_ID, Perioddate: Perioddate }, function (data) {    
        $('#DegreesParticipationActivitesTableId').html(data);
    });
}

function SaveDegreesParticipationActivites() {
    var attendanceList = [];

    $(".ServiceTr").each(function (index) {
        var service = new Object();

        service.DegreesParticipations_ID = $(".hdAttendances", $(this)).val();
        service.Cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
        service.Class_ID = $("#Class_ID").data("kendoDropDownList").value();
        service.Division_ID = $("#Division_ID").data("kendoDropDownList").value();
        service.User_ID = $(".hdUser", $(this)).val();
        service.PeriodDate = $("#PeriodDate").val();
        service.Reason_ID = $('#Reason_ID' + index).data("kendoDropDownList").value();
        var id = $(".radiobox", $(this)).attr('name');
        service.Percentage = $('input[name=' + id + ']', $(this)).val();
        attendanceList.push(service);
    });


    var params = new Object();
    params.Cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
    params.Class_ID = $("#Class_ID").data("kendoDropDownList").value();
    params.Division_ID = $("#Division_ID").data("kendoDropDownList").value();
    params.PeriodDate = $("#PeriodDate").val();
    params.degreesParticipationActivitesList = attendanceList;


    $.post(loadDegreesParticipationActivitesUrl, params, function (data) {
        $('#DegreesParticipationActivitesTableId').html(data);
    });
}

/********************** Duties  ********************************/

function LoadDutiesDetails() {
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadDutiesDetailsUrl, {}, function (data) {
        $('#MainContent').html(data);
    });
}

function LoadDutiesMonthlyTable() {
    $('#DutiesMonthlyTableID').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadDutiesMonthlyTableUrl, {}, function (data) {
        $('#DutiesMonthlyTableID').html(data);
    });
}

function LoadDutiesTable() {
    $('#DutiesTableId').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    if (!isValidateServicesLevelGrade())
        return false;
    var Cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
    var Class_ID = $("#Class_ID").data("kendoDropDownList").value();
    var Division_ID = $("#Division_ID").data("kendoDropDownList").value();
    var Perioddate = $("#PeriodDate").val();
    $.post(loadDutiesTableUrl, { Cycle_ID: Cycle_ID, Class_ID: Class_ID, Division_ID: Division_ID, Perioddate: Perioddate }, function (data) {  
        $('#DutiesTableId').html(data);
    });
}

function SaveDuties() {
    var attendanceList = [];

    $(".ServiceTr").each(function (index) {
        var service = new Object();

        service.Duties_ID = $(".hdAttendances", $(this)).val();
        service.Cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
        service.Class_ID = $("#Class_ID").data("kendoDropDownList").value();
        service.Division_ID = $("#Division_ID").data("kendoDropDownList").value();
        service.User_ID = $(".hdUser", $(this)).val();
        service.PeriodDate = $("#PeriodDate").val();
        service.Reason_ID = $('#Reason_ID' + index).data("kendoDropDownList").value();
        var id = $(".radiobox", $(this)).attr('name');
        service.Percentage = $('input[name=' + id + ']', $(this)).val();
        attendanceList.push(service);
    });


    var params = new Object();
    params.Cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
    params.Class_ID = $("#Class_ID").data("kendoDropDownList").value();
    params.Division_ID = $("#Division_ID").data("kendoDropDownList").value();
    params.PeriodDate = $("#PeriodDate").val();
    params.dutiesList = attendanceList;


    $.post(loadDutiesUrl, params, function (data) {
        $('#DutiesTableId').html(data);
    });
}

/********************** Level Behavior  ********************************/

function LoadLevelBehaviorDetails() {
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadLevelBehaviorDetailsUrl, {}, function (data) {
        $('#MainContent').html(data);
    });
}

function LoadLevelBehaviorMonthlyTable() {
    $('#LevelBehaviorMonthlyTableID').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadLevelBehaviorMonthlyTableUrl, {}, function (data) {
        $('#LevelBehaviorMonthlyTableID').html(data);
    });
}

function LoadLevelBehaviorTable() {
    $('#LevelBehaviorTableId').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    if (!isValidateServicesLevelGrade())
        return false;
   
        var Cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
        var Class_ID = $("#Class_ID").data("kendoDropDownList").value();
        var Division_ID = $("#Division_ID").data("kendoDropDownList").value();
        var Perioddate = $("#PeriodDate").val();
        $.post(loadLevelBehaviorTableUrl, { Cycle_ID: Cycle_ID, Class_ID: Class_ID, Division_ID: Division_ID, Perioddate: Perioddate }, function (data) {
        $('#LevelBehaviorTableId').html(data);
    });
}

function SaveLevelBehavior() {
    var attendanceList = [];

    $(".ServiceTr").each(function (index) {
        var service = new Object();

        service.LevelBehavior_ID = $(".hdAttendances", $(this)).val();
        service.Cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
        service.Class_ID = $("#Class_ID").data("kendoDropDownList").value();
        service.Division_ID = $("#Division_ID").data("kendoDropDownList").value();
        service.User_ID = $(".hdUser", $(this)).val();
        service.PeriodDate = $("#PeriodDate").val();
        service.Reason_ID = $('#Reason_ID' + index).data("kendoDropDownList").value();
        var id = $(".radiobox", $(this)).attr('name');
        service.Percentage = $('input[name=' + id + ']', $(this)).val();
        attendanceList.push(service);
    });


    var params = new Object();
    params.Cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
    params.Class_ID = $("#Class_ID").data("kendoDropDownList").value();
    params.Division_ID = $("#Division_ID").data("kendoDropDownList").value();
    params.PeriodDate = $("#PeriodDate").val();
    params.levelBehaviorList = attendanceList;


    $.post(loadLevelBehaviorUrl, params, function (data) {
        $('#LevelBehaviorTableId').html(data);
    });
}

/********************** Level Intelligence ********************************/

function LoadLevelBehaviorDetails() {
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadLevelBehaviorDetailsUrl, {}, function (data) {
        $('#MainContent').html(data);
    });
}

function LoadLevelIntelligenceMonthlyTable() {
    $('#LevelIntelligenceMonthlyTableID').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadLevelIntelligenceMonthlyTableUrl, {}, function (data) {
        $('#LevelIntelligenceMonthlyTableID').html(data);
    });
}

function LoadLevelIntelligenceTable() {
    $('#LevelIntelligenceTableId').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    if (!isValidateServicesLevelGrade())
        return false;

     var Cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
     var Class_ID = $("#Class_ID").data("kendoDropDownList").value();
     var Division_ID = $("#Division_ID").data("kendoDropDownList").value();
     var Perioddate = $("#PeriodDate").val();
     $.post(loadLevelIntelligenceTableUrl, { Cycle_ID: Cycle_ID, Class_ID: Class_ID, Division_ID: Division_ID, Perioddate: Perioddate }, function (data) {
        $('#LevelIntelligenceTableId').html(data);
    });
}

function SaveLevelIntelligence() {
    var attendanceList = [];

    $(".ServiceTr").each(function (index) {
        var service = new Object();

        service.LevelIntelligence_ID = $(".hdAttendances", $(this)).val();
        service.Cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
        service.Class_ID = $("#Class_ID").data("kendoDropDownList").value();
        service.Division_ID = $("#Division_ID").data("kendoDropDownList").value();
        service.User_ID = $(".hdUser", $(this)).val();
        service.PeriodDate = $("#PeriodDate").val();
        service.Reason_ID = $('#Reason_ID' + index).data("kendoDropDownList").value();
        var id = $(".radiobox", $(this)).attr('name');
        service.Percentage = $('input[name=' + id + ']', $(this)).val();
        attendanceList.push(service);
    });


    var params = new Object();
    params.Cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
    params.Class_ID = $("#Class_ID").data("kendoDropDownList").value();
    params.Division_ID = $("#Division_ID").data("kendoDropDownList").value();
    params.PeriodDate = $("#PeriodDate").val();
    params.levelIntelligenceList = attendanceList;


    $.post(loadlevelIntelligenceUrl, params, function (data) {
        $('#LevelIntelligenceTableId').html(data);
    });
}

/********************** Schedule  ********************************/

function LoadSchedule()
{
    if (!isValidateLoadSchedule())
        return false;

    var pCycle = $("#Cycle_ID").data("kendoDropDownList").value();
    var pClass = $("#Class_ID").data("kendoDropDownList").value();
    var pDivision = $("#Division_ID").data("kendoDropDownList").value();

    $('#main_container').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadScheduleUrl, {cycleID :pCycle , classID: pClass, divisionID:pDivision}, function (data) {
        $('#main_container').html(data);
    });
}

function LoadPeriodSchedule()
{
    $('#main_container').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadPeriodScheduleUrl, {}, function (data) {
        $('#main_container').html(data);
    });
}

function LoadDistributionStudents()
{
    $('#main_container').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadDistributionStudentsUrl, {}, function (data) {
        $('#main_container').html(data);

    });
}

function LoadAgendaDetails() {
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    var uerTypeID = $("#UserTypeID").val();
    $.post(loadAgendaDetailsUrl, { uerTypeID: uerTypeID }, function (data) {
        $('#MainContent').html(data);
    });
}

function LoadExamProgramDetails()
{
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    var uerTypeID = $("#UserTypeID").val();
    $.post(loadExamProgramDetailsUrl, { uerTypeID: uerTypeID }, function (data) {
        $('#MainContent').html(data);
    });
}

function LoadServicesContentDetails() {
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    var uerTypeID = $("#UserTypeID").val();
    $.post(loadServicesContentDetailsUrl, { uerTypeID: uerTypeID }, function (data) {
        $('#MainContent').html(data);
    });
}

function LoadServicesContent() {
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadServicesContentUrl, {}, function (data) {
        $('#MainContent').html(data);
    });
}


/******************************************************/

function LoadLevelBehaviorDetails() {
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadLevelBehaviorDetailsUrl, {}, function (data) {
        $('#MainContent').html(data);
    });
}

function LoadLevelIntelligenceDetails() {
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadLevelIntelligenceDetailsUrl, {}, function (data) {
        $('#MainContent').html(data);
    });
}
/****************************************************************************/

function isValidateServicesLevelGrade() {

    $("#Cycle_id_validationMessage").hide();
    $("#Class_ID_validationMessage").hide();
    $("#Division_ID_validationMessage").hide();

    var isValid = true;  
   
    if ($("#Cycle_ID").data("kendoDropDownList").value() == 0) {
        $("#Cycle_id_validationMessage").show();
        isValid = false;
    }

    if ($("#Class_ID").data("kendoDropDownList").value() == 0) {
        $("#Class_ID_validationMessage").show();
        isValid = false;
    }

    if ($("#Division_ID").data("kendoDropDownList").value() == 0) {
        $("#Division_ID_validationMessage").show();
        isValid = false;
    }

    return isValid;
}



function isValidateServicesSearch() {

    $("#ErrorSearchCycle").hide();
    $("#ErrorSearchClass").hide();
    $("#ErrorSearchDivision").hide();
    $("#ErrorSearchCourse").hide();

    var isValid = true;
    var SearchStage = $("#SearchStage").val();
   
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

    if ($("#UserType").val() == "Agenda")
    {
        if ($("#SearchCourse").data("kendoDropDownList").value() == 0) {
            $("#ErrorSearchCourse").show();
            isValid = false;
        }
    }

    
 
    return isValid;
}



/****************************************** Ckeck *****************************/

function CheckNumericOnly() {
    $(".numericOnly").bind('keypress', function (e) {
        if (e.keyCode == '9' || e.keyCode == '16') {
            return;
        }
        var code;
        if (e.keyCode) code = e.keyCode;
        else if (e.which) code = e.which;
        if (e.which == 46)
            return false;
        if (code == 8 || code == 46)
            return true;
        if (code < 48 || code > 57)
            return false;
    });

    //Disable paste
    $(".numericOnly").bind("paste", function (e) {
        e.preventDefault();
    });

    $(".numericOnly").bind('mouseenter', function (e) {
        var val = $(this).val();
        if (val != '0') {
            val = val.replace(/[^0-9]+/g, "")
            $(this).val(val);
        }
    });
}

function isValidEmailAddress(emailAddress) {
    //var emailExp = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    //return emailExp.test(emailAddress);
    var pattern = new RegExp(/^[+a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i);    
    return pattern.test(emailAddress);
}

function isValidUsername(userName) {
    //var emailExp = /^([A-Za-z0-9])+\.([A-Za-z]{2,4})$/;
    //return emailExp.test(emailAddress);
    var pattern = new RegExp(/^[+a-zA-Z0-9]+\.[a-zA-Z]{2,4}$/i);
    return pattern.test(emailAddress);
}

function isValidateSaveAgenda() {

    var isValid = true;
    var lst = ["#Cycle_ID", "#Class_ID", "#Division_ID", "#Course_ID", "#PeriodDate", "#Title"];
    var lstmsg = ["#Cycle_id_validationMessage", "#Class_ID_validationMessage", "#Division_ID_validationMessage", "#Course_validationMessage", "#PeriodDate_validationMessage", "#Title_validationMessage"];

    for (var i = 0; i < lst.length ; i++) {
        var input = $(lst[i]);
        if (input.data("role") == "dropdownlist") {
            input = $(input).data("kendoDropDownList").value();
        }
        else {
            input = $(input).val();
        }

        if (input == "" || input == -1 || input == "1/1/0001") {
            $(lstmsg[i]).show();
            isValid = false;
            $(lstmsg[i]).parent().addClass("state-error");
        }
        else {
            $(lstmsg[i]).hide();
            if ($(lstmsg[i]).parent().hasClass("state-error")) {
                $(lstmsg[i]).parent().removeClass("state-error");

            }
        }

    }
    return isValid;
}

function FilterClass() {
    return { Cycle_id: $("#SearchCycle").data("kendoDropDownList").value() };
}

function FilterDivision() {
    return { Class_ID: $("#SearchClass").data("kendoDropDownList").value() };
}

function FilterCourse() {
    return { Division_ID: $("#SearchDivision").data("kendoDropDownList").value() };
}

function FilterServiceClass() {
    return { Cycle_id: $("#Cycle_ID").data("kendoDropDownList").value() };
}

function FilterServiceDivision() {
    return { Class_ID: $("#Class_ID").data("kendoDropDownList").value() };
}

function FilterServiceCourse() {
    return { Division_ID: $("#Division_ID").data("kendoDropDownList").value() };
}

function isValidateLoadSchedule() {

    var isValid = true;
    var lst = ["#Cycle_ID", "#Class_ID", "#Division_ID"];
    var lstmsg = ["#CycleID_validationMessage", "#ClassID_validationMessage", "#DivisionID_validationMessage"];

    for (var i = 0; i < lst.length ; i++) {
        var input = $(lst[i]);
        if (input.data("role") == "dropdownlist") {
            input = $(input).data("kendoDropDownList").value();
        }
        else {
            input = $(input).val();
        }

        if (input == "" || input == -1 || input == "1/1/0001") {
            $(lstmsg[i]).show();
            isValid = false;
            $(lstmsg[i]).parent().addClass("state-error");
        }
        else {
            $(lstmsg[i]).hide();
            if ($(lstmsg[i]).parent().hasClass("state-error")) {
                $(lstmsg[i]).parent().removeClass("state-error");

            }
        }

    }
    return isValid;
}
