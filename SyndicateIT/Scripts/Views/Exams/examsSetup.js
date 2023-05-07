function InitializeExamTabWizard() {
     if (currentExamsID > 0) {        
        InitializeWizardCoefficients();
        InitializeWizardCheckers();
        InitializeWizardMaterials();
        InitializeWizardStudents();
    } else {
        $("#coefficients-a").attr("data-toggle", "");
        $("#coefficients-a").attr("data-toggle", "");
        $("#Materials-a").attr("data-toggle", "");
        $("#Student-a").attr("data-toggle", "");
    }

}

function InitializeWizardCoefficients() {
    $("#coefficients-a").click(function () {
        LoadExamscoefficients();
        $("#coefficients-a").attr("data-toggle", "tab");
    });
}
function InitializeWizardCheckers() {
    $("#Checkers-a").click(function () {
        LoadExamsCheckers();
        $("#Checkers-a").attr("data-toggle", "tab");
    });
}


function InitializeWizardMaterials() {
    $("#Materials-a").click(function () {
        LoadExamsMaterials();
        $("#Materials-a").attr("data-toggle", "tab");
    });
}

function InitializeWizardStudents() {
    $("#Students-a").click(function () {
        LoadExamsStudents();
        $("#Students-a").attr("data-toggle", "tab");
    });
}

function LoadExamscoefficients() {
    $('#Main-exams').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadExamscoefficientsUrl, {}, function (data) {
        $('#Main-exams').html(data);
    });
}

function LoadExamsCheckers() {
    $('#Main-exams').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadExamsCheckersUrl, {}, function (data) {
        $('#Main-exams').html(data);
    });
}


function LoadExamsStudents() {
    $('#Main-exams').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadExamsStudentsUrl, {}, function (data) {
        $('#Main-exams').html(data);
    });
}

function LoadExamsMaterials() {
    $('#Main-exams').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadExamsMaterialsUrl, {}, function (data) {
        $('#Main-exams').html(data);
    });
}

function LoadExamsSetupContent() {
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadExamsSetupContentUrl, {}, function (data) {
        $('#MainContent').html(data);
    });
}

function LoadExamsDefinitions() {
    $('#Main-exams').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadExamsDefinitionsUrl, {}, function (data) {
        $('#Main-exams').html(data);
        InitializeExamTabWizard();
    });
}
/********************** Begin Published Tags By Students***************************/
function ViewPublishedTagsStudents() {
    var cycleID = $("#Cycle_ID").data("kendoDropDownList").value();
    var classId = $("#Class_ID").data("kendoDropDownList").value();
    var divisionId = $("#Division_ID").data("kendoDropDownList").value();
    $('#ExamStudentTemplates-container').html("").hide();
    $('#PrintExamMaterials-container').html("").hide();
    $('#ExamStudentMaterials-Content').html("");
    if (!IsValidateExamStudents())
        return false;

    $.post(publishedTagsStudentTemplatesUrl, { cycleID: cycleID, classId: classId, divisionId: divisionId }, function (data) {
        $('#PublishedTags-Content').html(data).show();
        $("#CycleID").val(cycleID);
        $("#ClassID").val(classId);
        $("#DivisionID").val(divisionId);     
        
    });
    
}

function IsValidateExamStudents() {

    $("#CycleID_validationMessage").hide();
    $("#ClassID_validationMessage").hide();
    $("#DivisionID_validationMessage").hide();
     

    var isValid = true;

    if ($("#Cycle_ID").data("kendoDropDownList").value() != null && $("#Cycle_ID").data("kendoDropDownList").value() == 0) {
        $("#CycleID_validationMessage").show();
        isValid = false;
    }
    if ($("#Class_ID").data("kendoDropDownList").value() != null && $("#Class_ID").data("kendoDropDownList").value() == 0) {
        $("#ClassID_validationMessage").show();
        isValid = false;
    }
    if ($("#Division_ID").data("kendoDropDownList").value() != null && $("#Division_ID").data("kendoDropDownList").value() == 0) {
        $("#DivisionID_validationMessage").show();
        isValid = false;
    }

    return isValid;

}

function PublishedStudents() {
    var studentList = [];
     var params = new Object();
    $(".li-studentsExsiting").each(function (index) {
        var student = new Object();

        student.StudentID = $(".txt-studentsExsiting", $(this)).attr('studientId');
        if ($('#studentsExsitingcheckbox_' + index).is(":checked")) {
            student.HasPublished = true;
        }
        else {
            student.HasPublished = false;
        }
        studentList.push(student);
    });
    $(".li-studentsNoExsiting").each(function (index) {
        var student = new Object();
        student.StudentID = $(".txt-studentsNoExsiting", $(this)).attr('studientId');
        if ($('#studentsNotExsitingcheckbox_' + index).is(":checked")) {
            student.HasPublished = true;
        }
        else {
            student.HasPublished = false;
        }
        studentList.push(student);
    });

   
  
    params.CycleId = $("#CycleID").val();
    params.Class_ID = $("#ClassID").val();
    params.Division_ID = $("#DivisionID").val();
    params.Term_ID = $("#TermID").val();
    params.ListStudents = studentList;



    $.post(publishedStudentsUrl, params, function (data) {
        $('#PublishedTags-Content').html(data).show();

    });


}

/********************** Begin Exam By Students***************************/

function ViewStudents() {
    var stageId = $("#Cycle_ID").data("kendoDropDownList").value();
    var classId = $("#Class_ID").data("kendoDropDownList").value();
    var divisionId = $("#Division_ID").data("kendoDropDownList").value();
   
    $('#ExamStudentTemplates-container').html("").hide();
    $('#ExamStudentMaterials-Content').html("");
    $('#Student-Materials').hide();

    if (!IsValidateExamStudents())
        return false;
    $('#ExamStudentTemplates-container').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>").show();
    $.post(loadExamStudentTemplatesUrl, { stageId: stageId, classId: classId, divisionId: divisionId }, function (data) {
        $('#ExamStudentTemplates-container').html(data).show();
        $("#CycleID").val(stageId);
        $("#ClassID").val(classId);
        $("#DivisionID").val(divisionId);     
        
    });

    //$.post(loadExamStudentsPrintUrl, {}, function (data) {
    //    $('#PrintExamStudents-container').html(data).show();
    //    $("#PrintCycleID").val(stageId);
    //    $("#PrintClassID").val(classId);
    //    $("#PrintDivisionID").val(divisionId);
    //});
}

function IsValidateExamStudents() {

    $("#CycleID_validationMessage").hide();
    $("#ClassID_validationMessage").hide();
    $("#DivisionID_validationMessage").hide();
     

    var isValid = true;

    if ($("#Cycle_ID").data("kendoDropDownList").value() != null && $("#Cycle_ID").data("kendoDropDownList").value() == 0) {
        $("#CycleID_validationMessage").show();
        isValid = false;
    }
    if ($("#Class_ID").data("kendoDropDownList").value() != null && $("#Class_ID").data("kendoDropDownList").value() == 0) {
        $("#ClassID_validationMessage").show();
        isValid = false;
    }
    if ($("#Division_ID").data("kendoDropDownList").value() != null && $("#Division_ID").data("kendoDropDownList").value() == 0) {
        $("#DivisionID_validationMessage").show();
        isValid = false;
    }

    return isValid;

    }

function PrintAllStudent()
{
    var cycleId = $("#PrintCycleID").val();
    var classId = $("#PrintClassID").val();
    var divisionId = $("#PrintDivisionID").val();

    $.post(printAllStudentsUrl, { cycleId: cycleId, classId: classId, divisionId: divisionId }, function (data) {
              
    });
}

function LoadPlansSubscriberAccounts() {
    $('#Main-exams').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadPlansSubscriberAccountsUrl, {}, function (data) {
        $('#Main-exams').html(data);
    });
}

function LoadPlansSubscriberLimit() {
    $('#Main-exams').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadPlansSubscriberLimitUrl, {}, function (data) {
        $('#Main-exams').html(data);
    });
}

function LoadLimitDefinition() {
    $('#LimitDefinitions-Content').html("");
    var accountTypeCode = $("#AccountTypeCode").data("kendoDropDownList").value();
    var templateID = $("#TemplateID").data("kendoDropDownList").value();
    $('#LimitDefinitions-Content').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadLimitDefinitionUrl, { IsPlanCodeSelected: true, limitCode: "", accountTypeCode: accountTypeCode, templateID: templateID }, function (data) {
        $('#LimitDefinitions-Content').html(data);
        $('#Limit-Action').show();
    });
}

function LoadStudentById(id) {
    var cycleID = $("#CycleID").val();
    var classId = $("#ClassID").val();
    var divisionId = $("#DivisionID").val();
    $('#ExamStudentMaterials-Content').html("");   
    $('#ExamStudentMaterials-Content').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadExamStudentMaterialsUrl, {cycleID:cycleID,classId:classId,divisionId:divisionId, id: id }, function (data) {
        $('#ExamStudentMaterials-Content').html(data);
        $("#StudentID").val(id);
        $("#Student-Materials").show();
   });

}
function LoadFileStudentById(id) { 
    $('#ExamFileStudent-Content').html("");
    $('#ExamFileStudent-Content').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadExamFileStudentMaterialsUrl, { id: id }, function (data) {
        $('#ExamFileStudent-Content').html(data);       
    });

}

/********************** Begin Exam By Materials***************************/

function LoadMaterialsById(id) {
    var cycleID = $("#CycleID").val();
    var classId = $("#ClassID").val();
    var divisionId = $("#DivisionID").val();
    $('#ExamMaterialStudents-Content').html("");
    $('#ExamMaterialStudents-Content').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadExamMaterialStudentsUrl, { cycleID: cycleID, classId: classId, divisionId: divisionId, id: id }, function (data) {
        $('#ExamMaterialStudents-Content').html(data);
        $("#MaterialId").val(id);
        $("#Material-Students").show();
    });

}

function ViewMaterials() {
    var stageId = $("#Cycle_ID").data("kendoDropDownList").value();
    var classId = $("#Class_ID").data("kendoDropDownList").value();
    var divisionId = $("#Division_ID").data("kendoDropDownList").value();
    $('#ExamMaterialsTemplates-container').html("").hide();
    $('#ExamMaterialStudents-Content').html("");
    $('#Material-Students').hide();
    
    if (!IsValidateExamStudents())
        return false;
    $('#ExamMaterialsTemplates-container').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>").show();
    $.post(loadExamMaterialTemplatesUrl, { stageId: stageId, classId: classId, divisionId: divisionId }, function (data) {
        $('#ExamMaterialsTemplates-container').html(data).show();
        $("#CycleID").val(stageId);
        $("#ClassID").val(classId);
        $("#DivisionID").val(divisionId);     
        
    });

    $.post(loadExamStudentsPrintUrl, {}, function (data) {
        $('#PrintExamStudents-container').html(data).show();
        $("#PrintCycleID").val(stageId);
        $("#PrintClassID").val(classId);
        $("#PrintDivisionID").val(divisionId);
    });
}
