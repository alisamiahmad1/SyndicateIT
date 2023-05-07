"use strict";
function FilterStage() {
    
    return { Cycle_id: $("#Cycle_id").data("kendoDropDownList").value() };
}
function FilterClass() {
    return { Class_ID: $("#Class_ID").data("kendoDropDownList").value() };
}
function FilterSearchClass() {
    return { Cycle_ID: $("#Cycle_ID").data("kendoDropDownList").value() };
}
function FilterSearchDivision() {
    return { Class_ID: $("#Class_ID").data("kendoDropDownList").value() };
}
function AddCourses() {
    $('#CoursesContent').html("");
    $('#CoursesContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadCoursesURL, {}, function (data) {
        $('#CoursesContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditCourses(id) {
    var dataItem = id;
    $('#CoursesContent').html("");
    $('#CoursesContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadCoursesURL, { CoursesId: dataItem }, function (data) {
        $('#CoursesContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveCourses() {
    if (!isValidateSaveCourses())
        return false;
    var myData = $('#Courses-form').serialize();
    $.post(saveCoursesUrl, myData, function (data) {
        $('#CoursesContent').html(data);
        $("#Course_Name").val("");
    });
}

function EditTranslateCourses(id) {
    var dataItem = id;
    $('#CoursesList').html("");
    $('#CoursesList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateCoursesURL, { TableId: dataItem.Course_ID, TableName: 'Courses_By_Language' }, function (data) {
        $('#CoursesList').html(data);
    });
}

function DeleteCourses(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(CoursesDeleteURL, { CoursesId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchCourses(false))
                         .then(function () { $("#AlertTypeId").html(data.Alert) });

                    } else {
                        $("#AlertTypeId").html(data.Alert);
                    }
                });
            } else {
                return false;
            }
        }
    })
}

function SearchCourses(isRemoveAlert) {

    var Cycle_ID = $("#Cycle_ID").data("kendoDropDownList").value();
    var Class_ID = $("#Class_ID").data("kendoDropDownList").value();
    var Division_ID = $("#Division_ID").data("kendoDropDownList").value();
    var Coursesname = $("#Course_Name").val();
    var languageid = $("#LANGUAGE_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#Courses-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(CoursesSearchURL, {Cycle_ID: Cycle_ID, Class_ID: Class_ID, Division_ID: Division_ID, CoursesName: Coursesname, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#Courses-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveCourses() {

    var isValid = true;
    var lst = ["#Course_Name", "#Coeficient_Grade", "#Cycle_id", "#Class_ID", "#Division_ID", "#Subject_ID", "#Company_ID", "#Educationalsystem_ID"];
    var lstmsg = ["#Course_Name_ValidationMsg", "#Coeficient_Grade_ValidationMsg", "#Cycle_id_ValidationMsg", "#Class_ID_ValidationMsg", "#Division_ID_ValidationMsg", "#Subject_ID_ValidationMsg", "#Company_ID_ValidationMsg", "#Educationalsystem_ID_ValidationMsg"];

    for (var i = 0; i < lst.length ; i++) {
        var input = $(lst[i]);
        if (input.data("role") == "dropdownlist") {
            input = $(input).data("kendoDropDownList").value();
        }
        else {
            input = $(input).val();
        }

        if (input == "" || input == -1) {
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


//translate Courses
function EditTranslateCourses(id) {

    $('#CoursesTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#CoursesTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationCoursesURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#CoursesTranslate-List').html(data)
    });
}



//end translate

