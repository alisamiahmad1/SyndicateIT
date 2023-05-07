"use strict";
function FilterClass() {
    $('#ExamMaterialsTemplates-container').html("").hide();
    $('#ExamMaterialStudents-Content').html("");
    $('#Material-Students').hide();

    $('#ExamStudentTemplates-container').html("").hide();
    $('#ExamStudentMaterials-Content').html(""); 
    $('#Student-Materials').hide();

    return { Cycle_ID: $("#Cycle_ID").data("kendoDropDownList").value() };
}

function FilterDivision() {
    $('#ExamMaterialsTemplates-container').html("").hide();
    $('#ExamMaterialStudents-Content').html("");
    $('#Material-Students').hide();

    $('#ExamStudentTemplates-container').html("").hide();
    $('#ExamStudentMaterials-Content').html("");
    $('#Student-Materials').hide();

    return { Class_ID: $("#Class_ID").data("kendoDropDownList").value() };
}

function EditExamsSetup(e) {
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadExamsSetupContentDetailsUrl, { id: dataItem.ID }, function (data) {
        $('#MainContent').html(data);
    });
}

function SaveExamMaterilaStudents() {
   
    var courseList = [];

    $(".parent_li.student-content").each(function (index) {
        var course = new Object();
        course.MaterialId = $(".txt-Material", $(this)).attr('materialid');
        course.MaterialName = $(".txt-Material", $(this)).attr('materialname');
        course.TotalValue = $(".txt-Material", $(this)).val() ;        
        var termMonthList = [];
        $(".smart-li-form", $(this)).each(function (index) {
            var termMonth = new Object();
            termMonth.TermMonthId = $(".txt-Material-TermMonth", $(this)).attr('termMonthId');
            termMonth.TermMonthname = $(".txt-Material-TermMonth", $(this)).attr('termMonthName');
            termMonth.Value = $(".txt-Material-TermMonth", $(this)).val();
            termMonth.Coefficient = $(".txt-Coefficient", $(this)).val();
             var skilsList = [];
                $(".smart-form", $(this)).each(function (index) {
                    var skils = new Object();           
                    skils.SkilsId = $(".txt-Skill", $(this)).attr('skillid');
                    skils.SkilsName = $(".txt-Skill", $(this)).attr('name');
                    if ($('#CycleID').val() == 9) {
                        var id = $(".txt-Skill", $(this)).attr('name')
                        skils.Value = $('input[name=' + id + ']:checked', $(this)).val()
                    }
                    else {
                        skils.Value = $(".txt-Skill", $(this)).val();
                    }
                    skils.Percentage = $(".txt-Percentage", $(this)).val();
                    skilsList.push(skils);
                });
                termMonth.ExamStudentSkils = skilsList;
            termMonthList.push(termMonth);
        });
        course.ExamMaterilasTermMonths = termMonthList;
        courseList.push(course);
    });


    var params = new Object();
    params.StudentID = $('#StudentID').val();
    params.EnglishNotes = $('#EnglishNotes').val();
    params.ArabicNotes = $('#ArabicNotes').val();
    params.FrenchNotes = $('#FrenchNotes').val();
    params.CycleId = $('#CycleID').val();
    params.Class_ID = $('#ClassID').val();
    params.Division_ID = $('#DivisionID').val();
    params.Term_ID = $('#TermID').val();
    params.ExamMaterials = courseList;


    $('#ExamStudentMaterials-Content').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(saveExamMaterialStudentsUrl, params, function (data) {
        $('#ExamStudentMaterials-Content').html(data);
       
    });
}

function SaveExamStudentMaterials() {

    var StudentsList = [];

    $(".parent_li.student-content").each(function (index) {
        var Students = new Object();

        Students.StudentId = $(".txt-Student ", $(this)).attr('studentid');
        Students.StudentName = $(".txt-Student", $(this)).attr('studentname');
        Students.TotalValue = $(".txt-Student", $(this)).val();
        var termMonthList = [];
        $(".smart-li-form", $(this)).each(function (index) {
            var termMonth = new Object();
            termMonth.TermMonthId = $(".txt-Material-TermMonth", $(this)).attr('termMonthId');
            termMonth.TermMonthname = $(".txt-Material-TermMonth", $(this)).attr('termMonthName');
            termMonth.Value = $(".txt-Material-TermMonth", $(this)).val();
            termMonth.Coefficient = $(".txt-Coefficient", $(this)).val();
            var skilsList = [];
            $(".smart-form", $(this)).each(function (index) {
                var skils = new Object();
                skils.SkilsId = $(".txt-Skill", $(this)).attr('skillid');
                skils.SkilsName = $(".txt-Skill", $(this)).attr('name');
                if ($('#CycleID').val() == 9) {
                    var id = $(".txt-Skill", $(this)).attr('name')
                    skils.Value = $('input[name=' + id + ']:checked', $(this)).val()
                }
                else {
                    skils.Value = $(".txt-Skill", $(this)).val();
                }
                skils.Percentage = $(".txt-Percentage", $(this)).val();
                skilsList.push(skils);
            });
            termMonth.ExamMaterialSkils = skilsList;
            termMonthList.push(termMonth);
        });
        Students.ExamStudentsTermMonths = termMonthList;
        StudentsList.push(Students);
       
    });


    var params = new Object();
    params.MaterialId = $('#MaterialId').val();
    params.Cycle_ID = $('#CycleID').val();
    params.Class_ID = $('#ClassID').val();
    params.Division_ID = $('#DivisionID').val();
    params.Term_ID = $('#TermID').val();
    params.ExamStudents = StudentsList;

    $('#ExamMaterialStudents-Content').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(saveExamStudentMaterialsUrl, params, function (data) {
        $('#ExamMaterialStudents-Content').html(data);

    });
}

function SearchExams() {
    $('#ExamsSetup-Details').html("");
    //if (!isValidatePlansSearch())
    //    return false;   
    var searchExamName = $("#SearchExamName").val();
    var pageId = $("#PageID").val();

    if (pageId == 1) {
        $('#ExamsSetup-Details').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    }

    $.post(loadExamsUrl, { searchExamName: searchExamName, pageId: pageId }, function (data) {
        if (pageId == 1) {
            $('#ExamsSetup-Details').html(data);
        }
    });
}
