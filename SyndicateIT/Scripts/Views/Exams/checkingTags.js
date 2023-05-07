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
    $.post(printByCheckingTagstUrl, { Cycle_ID: Cycle_ID, Class_ID: Class_ID, Division_ID: Division_ID }, function (data) {
        debugger
       // console.log(data);
        mywindow.document.body.innerHTML = data;
        mywindow.print();
        mywindow.close();
    });
}
function ViewChekingTags(id) {
    $('#Template-CheckingTags').html(""); 
    var Cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
    var Class_ID = $("#Class_ID").data("kendoDropDownList").value();
    var Division_ID = $("#Division_ID").data("kendoDropDownList").value();

    $('#Template-CheckingTags').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadChekingTagsByTermtUrl, { Term_ID: id, Cycle_ID: Cycle_ID, Class_ID: Class_ID, Division_ID: Division_ID }, function (data) {
        $('#Template-CheckingTags').html(data);
    });
}


function SearchCheckingTags() {
    $('.CheckingTags-content').html("");
    var Cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
    var Class_ID = $("#Class_ID").data("kendoDropDownList").value();
    var Division_ID = $("#Division_ID").data("kendoDropDownList").value();
    var TermID = $("#TermID").val();

    if(Cycle_ID == "9")
    {
        $('.CheckingTags-content').html("<article class='col-sm-12'><div class='alert alert-warning fade in'><i class='fa-fw fa fa-check'></i><strong>Info</strong> No data available</div></article>");
        return false;

    }

    $('.CheckingTags-content').html("");
    if (!isValidateCheckingTagsSearch())
        return false;

    $('.CheckingTags-content').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadCheckingTagsContentUrl, { Term_ID: TermID, Cycle_ID: Cycle_ID, Class_ID: Class_ID, Division_ID: Division_ID }, function (data) {
        $('.CheckingTags-content').html(data);
    });
}

function isValidateCheckingTagsSearch() {

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
