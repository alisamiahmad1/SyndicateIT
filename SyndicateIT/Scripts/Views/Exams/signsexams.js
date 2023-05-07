"use strict";
function FilterClass() {
    return { Cycle_ID: $("#Cycle_ID").data("kendoDropDownList").value() };
}
function FilterDivision() {
    return { Class_ID: $("#Class_ID").data("kendoDropDownList").value() };
}


function PrintAllStudent() {

    var Cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
    var Class_ID = $("#Class_ID").data("kendoDropDownList").value();
    var Division_ID = $("#Division_ID").data("kendoDropDownList").value();
    var mywindow = window.open("", "_blank", 'height=400,width=600');
    $.post(printByAllStudentUrl, { Cycle_ID: Cycle_ID, Class_ID: Class_ID, Division_ID: Division_ID }, function (data) {
        debugger
       // console.log(data);
        mywindow.document.body.innerHTML = data;
        mywindow.print();
        mywindow.close();
    });
}

function PrintByStudent(id) {
   
    var Cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
    var Class_ID = $("#Class_ID").data("kendoDropDownList").value();
    var Division_ID = $("#Division_ID").data("kendoDropDownList").value();
    var mywindow = window.open("", "_blank", 'height=400,width=600');
    $.post(printByStudentUrl, { Cycle_ID: Cycle_ID, Class_ID: Class_ID, Division_ID: Division_ID, Student_ID: id }, function (data) {
        debugger
        console.log(data);
        mywindow.document.body.innerHTML = data;
        mywindow.print();
        mywindow.close();
    });
}

function ViewSignExam(id)
{
    $('#Template-PS').html("");
    if (!isValidateSignExamSearch())
        return false;
    var Cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
    var Class_ID = $("#Class_ID").data("kendoDropDownList").value();
    var Division_ID = $("#Division_ID").data("kendoDropDownList").value();

    $('#Template-PS').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadSignExamsByTermtUrl, { Term_ID: id, Cycle_ID: Cycle_ID, Class_ID: Class_ID, Division_ID: Division_ID }, function (data) {
        $('#Template-PS').html(data);
    });
}



function SearchSignExams() {
    $('.Sign-Exam-content').html("");
    if (!isValidateSignExamSearch())
        return false;
    var Cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
    var Class_ID = $("#Class_ID").data("kendoDropDownList").value();   
    var Division_ID = $("#Division_ID").data("kendoDropDownList").value();
    var TermID = $("#TermID").val();

    $('.Sign-Exam-content').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadSignExamsContentUrl, { Term_ID: TermID, Cycle_ID: Cycle_ID, Class_ID: Class_ID, Division_ID: Division_ID }, function (data) {
        $('.Sign-Exam-content').html(data);
    });
}

function isValidateSignExamSearch() {

    $("#ErrorCycle_ID").hide();
    $("#ErrorClass_ID").hide();
    $("#ErrorDivision_ID").hide();

    var isValid = true;  

    if ($("#Cycle_ID").data("kendoDropDownList").value() == 0) {
        $("#ErrorCycle_ID").show();
        isValid = false;
    }

    if ($("#Class_ID").data("kendoDropDownList").value() == 0) {
        $("#ErrorClass_ID").show();
        isValid = false;
    }

    if ($("#Division_ID").data("kendoDropDownList").value() == 0) {
        $("#ErrorDivision_ID").show();
        isValid = false;
    }

    return isValid;
}
