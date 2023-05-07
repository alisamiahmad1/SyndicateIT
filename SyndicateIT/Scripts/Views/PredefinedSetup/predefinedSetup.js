function LoadSchoolContent() {
    EmptyContent();
    $('#Company-Content').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadCompanyURL, {}, function (data) {
        $('#Company-Content').html(data);
    });
}

function SaveCompany() {
    if (!isValidateSaveCompany())
        return false;
    var myData = $('#Company-form').serialize();
    $.post(saveCompanyUrl, myData, function (data) {
        LoadSchoolContent();
    });
}

function isValidateSaveCompany() {

    var isValid = true;
    var lst = ["#Company_Name"];
    var lstmsg = ["#Company_Name_ValidationMsg"];

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

/////////////////////////////////Couse //////

function FilterCourse() {
    return { Division_ID: $("#Division_ID").data("kendoDropDownList").value() };
}

function LoadTermMonthContent() {
    EmptyContent();
    $('#TermMonth-Content').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTermMonthContentURL, { TermMonthId: "" }, function (data) {
        $('#TermMonth-Content').html(data);
        InitializeTermMonth();
    });
}

function InitializeTermMonth() {
    $('#TermMonth-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTermMonthURL, {}, function (data) {
        $('#TermMonth-List').html(data);
    });
}

function EditTermMonth(id) {
    var dataItem = id;
    $('#TermMonth-Content').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTermMonthContentURL, { TermMonthId: dataItem }, function (data) {
        $('#TermMonth-Content').html(data);

    });
}

function SaveTermMonth() {
    if (!isValidateSaveTermMonth())
        return false;
    var myData = $('#TermMonth-form').serialize();
    $.post(saveTermMonthUrl, myData, function (data) {
        InitializeTermMonth();
        LoadTermMonthContent();
    });
}

function isValidateSaveTermMonth() {

    var isValid = true;
    var lst = ["#TermMonth_Name"];
    var lstmsg = ["#TermMonth_Name_ValidationMsg"];

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

function DeleteTermMonth(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'IT Syndicate',
        message: 'Are You sure to delete this record?',
        callback: function (result) {
            if (result) {
                $.post(termMonthURL, { TermMonthID: dataItem }, function (data) {
                    if (data.Success) {
                        InitializeTermMonth();
                    } else {
                        alert("error");
                    }
                });
            } else {
                return false;
            }
        }
    })
}

/////////////////////////////Term//////////////////////////////////



function LoadTermContent() {
    EmptyContent();
    $('#Term-Content').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTermContentURL, { TermId: "" }, function (data) {
        $('#Term-Content').html(data);
        InitializeTerm();
    });
}

function InitializeTerm() {
    $('#Term-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTermURL, {}, function (data) {
        $('#Term-List').html(data);
    });
}

function EditTerm(id) {
    var dataItem = id;
    $('#Term-Content').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTermContentURL, { TermId: dataItem }, function (data) {
        $('#Term-Content').html(data);

    });
}

function SaveTerm() {
    if (!isValidateSaveTerm())
        return false;
    var myData = $('#Term-form').serialize();
    $.post(saveTermUrl, myData, function (data) {
        InitializeTerm();
        LoadTermContent();
    });
}

function isValidateSaveTerm() {

    var isValid = true;
    var lst = ["#Term_Name"];
    var lstmsg = ["#Term_Name_ValidationMsg"];

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

function DeleteTerm(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'IT Syndicate',
        message: 'Are You sure to delete this record?',
        callback: function (result) {
            if (result) {
                $.post(termDeleteURL, { TermId: dataItem }, function (data) {
                    if (data.Success) {
                        InitializeTerm();
                    } else {
                        alert("error");
                    }
                });
            } else {
                return false;
            }
        }
    })
}

/////////////////////////////Skill Coefficient//////////////////////////////////


function LoadSkillCoefficientContent() {
    EmptyContent();
    $('#SkillCoefficient-Content').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadSkillCoefficientContentURL, { SkillCoefficientId: "" }, function (data) {
        $('#SkillCoefficient-Content').html(data);
        InitializeSkillCoefficient();
    });
}

function InitializeSkillCoefficient() {
    $('#SkillCoefficient-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadSkillCoefficientURL, {}, function (data) {
        $('#SkillCoefficient-List').html(data);
    });
}

function EditSkillCoefficient(id) {
    var dataItem = id;
    $('#SkillCoefficient-Content').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadSkillCoefficientContentURL, { SkillCoefficientId: dataItem }, function (data) {
        $('#SkillCoefficient-Content').html(data);

    });
}

function SaveSkillCoefficient() {
    if (!isValidateSaveSkillCoefficient())
        return false;
    var myData = $('#SkillsCoefficient-form').serialize();
    $.post(saveSkillCoefficientUrl, myData, function (data) {
        InitializeSkillCoefficient();
        LoadSkillCoefficientContent();
    });
}

function isValidateSaveSkillCoefficient() {

    var isValid = true;
    var lst = ["#SkillCoefficient_NAME"];
    var lstmsg = ["#SkillCoefficient_NAME_ValidationMsg"];

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

function DeleteSkillCoefficient(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'IT Syndicate',
        message: 'Are You sure to delete this record?',
        callback: function (result) {
            if (result) {
                $.post(skillCoefficientDeleteURL, { SkillCoefficientId: dataItem }, function (data) {
                    if (data.Success) {
                        InitializeSkillCoefficient();
                    } else {
                        alert("error");
                    }
                });
            } else {
                return false;
            }
        }
    })
}

/////////////////////////////Course//////////////////////////////////



function LoadCourseContent() {
    EmptyContent();
    $('#Course-Content').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadCourseContentURL, { CourseId: "" }, function (data) {
        $('#Course-Content').html(data);
        InitializeCourse();
    });
}

function InitializeCourse() {
    $('#Course-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadCourseURL, {}, function (data) {
        $('#Course-List').html(data);
        
    });
}

function EditCourses(id) {
    var dataItem = id;
    $('#Course-Content').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadCourseContentURL, { CourseId: dataItem }, function (data) {
        $('#Course-Content').html(data);

    });
}

function SaveCourses() {
    if (!isValidateSaveCourses())
        return false;
    var myData = $('#Courses-form').serialize();
    $.post(saveCoursesUrl, myData, function (data) {
        InitializeCourse();
        LoadCourseContent();
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

function DeleteCourses(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'Sky School Book',
        message: 'Are You sure to delete this record?',
        callback: function (result) {
            if (result) {
                $.post(courseDeleteURL, { CourseId: dataItem }, function (data) {
                    if (data.Success) {
                        InitializeCourse();
                    } else {
                        alert("error");
                    }
                });
            } else {
                return false;
            }
        }
    })
}

/////////////////////////////Subject//////////////////////////////////


function LoadSubjectContent() {
    EmptyContent();
    $('#Subject-Content').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadSubjectContentURL, { SubjectId: "" }, function (data) {
        $('#Subject-Content').html(data);
        InitializeSubject();
    });
}

function InitializeSubject() {
    $('#Subject-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadSubjectURL, {}, function (data) {
        $('#Subject-List').html(data);
    });
}

function EditSubjects(id) {
    var dataItem = id;
    $('#Subject-Content').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadSubjectContentURL, { SubjectId: dataItem }, function (data) {
        $('#Subject-Content').html(data);

    });
}

function SaveSubjects() {
    if (!isValidateSaveSubjects())
        return false;
    var myData = $('#Subject-form').serialize();
    $.post(saveSubjectUrl, myData, function (data) {
        InitializeSubject();
        LoadSubjectContent();
    });
}

function isValidateSaveSubjects() {

    var isValid = true;
    var lst = ["#Subject_Name"];
    var lstmsg = ["#Subject_Name_ValidationMsg"];

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

function DeleteSubjects(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'Sky School Book',
        message: 'Are You sure to delete this record?',
        callback: function (result) {
            if (result) {
                $.post(subjectDeleteURL, { SubjectsId: dataItem }, function (data) {
                    if (data.Success) {
                        InitializeSubject();
                    } else {
                        alert("error");
                    }
                });
            } else {
                return false;
            }
        }
    })
}

/////////////////////////////Skills//////////////////////////////////

function FilterStage() {
    if ($("#Cycle_id").data("kendoDropDownList").value() == 9) {
        
       $("#SecCoeficientGrade").hide();
        $("#SecKjCoeficientGrade").show();
        
    }
    else {
      $("#SecCoeficientGrade").show();
        $("#SecKjCoeficientGrade").hide();
       
      }
      return { Cycle_id: $("#Cycle_id").data("kendoDropDownList").value() };
}


function FilterClass() {
    return { Class_ID: $("#Class_ID").data("kendoDropDownList").value() };

}

function FilterDivision() {
        return { Division_ID: $("#Division_ID").data("kendoDropDownList").value() };
}


function InitializeSkills() {
    $('#Skills-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadSkillsURL, {}, function (data) {
        $('#Skills-List').html(data);
    });
}

function LoadSkillsContent() {
    EmptyContent();
    $('#Skills-Content').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadSkillsContentURL, { SkillsId: "" }, function (data) {
        $('#Skills-Content').html(data);
        InitializeSkills();
    });
}


function EditSkills(id) {
    var dataItem = id;
    $('#Skills-Content').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadSkillsContentURL, { SkillsId: dataItem }, function (data) {
        $('#Skills-Content').html(data);
    });
}

function SaveSkills() {
    if (!isValidateSaveSkills())
        return false;
    var myData = $('#Skills-form').serialize();
    $.post(saveSkillsUrl, myData, function (data) {
        InitializeSkills();
        LoadSkillsContent();
    });
}

function isValidateSaveSkills() {

    var isValid = true;
    var lst = ["#Skill_Name", "#Coeficient_Grade", "#Cycle_id", "#Class_ID", "#Division_ID", "#Subject_ID", "#Course_ID"];
    var lstmsg = ["#Skill_Name_ValidationMsg", "#Coeficient_Grade_ValidationMsg", "#Cycle_id_ValidationMsg", "#Class_ID_ValidationMsg", "#Division_ID_ValidationMsg", "#Subject_ID_ValidationMsg", "#Course_ID_ValidationMsg"];

    if ($("#Cycle_id").data("kendoDropDownList").value() == 9) {

         lst = ["#Skill_Name", "#Cycle_id", "#Class_ID", "#Division_ID", "#Subject_ID", "#Course_ID", "#Coeficient_KJGrade"];
         lstmsg = ["#Skill_Name_ValidationMsg",  "#Cycle_id_ValidationMsg", "#Class_ID_ValidationMsg", "#Division_ID_ValidationMsg", "#Subject_ID_ValidationMsg", "#Course_ID_ValidationMsg", "#Coeficient_KJGrade_ValidationMsg"];
    }
    
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

function DeleteSkills(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: 'Are You sure to delete this record?',
        callback: function (result) {
            if (result) {
                $.post(skillsDeleteURL, { SkillsId: dataItem }, function (data) {
                    if (data.Success) {
                        InitializeSkills();
                    } else {
                        alert("error");
                    }
                });
            } else {
                return false;
            }
        }
    })
}

/////////////////////////////Division//////////////////////////////////



function InitializeDivision() {
    $('#Division-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadDivisionURL, {}, function (data) {
        $('#Division-List').html(data);
    });
}

function LoadDivisionContent() {
    EmptyContent();
    $('#Division-Content').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadDivisionContentURL, { DivisionId: "" }, function (data) {
        $('#Division-Content').html(data);
        InitializeDivision();
    });
}


function EditDivision(id) {
    var dataItem = id;
    $('#Division-Content').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadDivisionContentURL, { DivisionId: dataItem }, function (data) {
        $('#Division-Content').html(data);
    });
}

function SaveDivision() {
    if (!isValidateSaveDivision())
        return false;
    var myData = $('#Division-form').serialize();
    $.post(saveDivisionUrl, myData, function (data) {
        InitializeDivision();
        LoadDivisionContent();
    });
}

function isValidateSaveDivision() {

    var isValid = true;
    var lst = ["#Division_Name", "#Class_ID"];
    var lstmsg = ["#Division_Name_ValidationMsg", "#Class_ID_ValidationMsg"];

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

function DeleteDivision(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: 'Are You sure to delete this record?',
        callback: function (result) {
            if (result) {
                $.post(divisionDeleteURL, { DivisionId: dataItem }, function (data) {
                    if (data.Success) {
                        InitializeDivision();
                    } else {
                        alert("error");
                    }
                });
            } else {
                return false;
            }
        }
    })
}

/////////////////////////////Class//////////////////////////////////




function InitializeClass() {
    $('#Class-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadClassURL, {}, function (data) {
        $('#Class-List').html(data);
    });
}

function LoadClassContent() {
    EmptyContent();
    $('#Class-Content').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadClassContentURL, { ClassId: "" }, function (data) {
        $('#Class-Content').html(data);
        InitializeClass();
    });
}


function EditClasses(id) {
    var dataItem = id;
    $('#Class-Content').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadClassContentURL, { ClassId: dataItem }, function (data) {
        $('#Class-Content').html(data);
    });
}

function SaveClasses() {
    if (!isValidateSaveClasses())
        return false;
    var myData = $('#Classes-form').serialize();
    $.post(saveClassesUrl, myData, function (data) {
        InitializeClass();
        LoadClassContent();
    });
}

function isValidateSaveClasses() {

    var isValid = true;
    var lst = ["#Class_Title", "#Cycle_ID", "#Company_ID", "#Class_Row", "#Class_Column"];
    var lstmsg = ["#Class_Title_ValidationMsg", "#Cycle_ID_ValidationMsg", "#Company_ID_ValidationMsg", "#Class_Row_ValidationMsg", "#Class_Column_ValidationMsg"];

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

function DeleteClasses(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: 'Are You sure to delete this record?',
        callback: function (result) {
            if (result) {
                $.post(classDeleteURL, { ClassesId: dataItem }, function (data) {
                    if (data.Success) {
                        InitializeClass();
                    } else {
                        alert("error");
                    }
                });
            } else {
                return false;
            }
        }
    })
}

/////////////////////////////Cycle//////////////////////////////////

function LoadCyleContent()
{
    EmptyContent();
    $('#Cycle-Content').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadCycleContentURL, { CycleId: "" }, function (data) {
        $('#Cycle-Content').html(data);
        InitializeCycle();
    });
}

function InitializeCycle() {
    $('#Cycle-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadCycleURL, {}, function (data) {
        $('#Cycle-List').html(data);
        
    });
}

function EditCycle(id) {
    var dataItem = id;
    $('#Cycle-Content').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadCycleContentURL, { CycleId: dataItem }, function (data) {
        $('#Cycle-Content').html(data);
       
    });
}

function SaveCycle() {
    if (!isValidateSaveCycle())
        return false;
    var myData = $('#Cycle-form').serialize();
    $.post(saveCycleUrl, myData, function (data) {
        InitializeCycle();
        LoadCyleContent();
    });
}

function isValidateSaveCycle() {

    var isValid = true;
    var lst = ["#Cycle_Title", "#Company_ID"];
    var lstmsg = ["#Cycle_Title_ValidationMsg", "#Company_ID_ValidationMsg"];

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

function DeleteCycle(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'Sky School Book',
        message: 'Are You sure to delete this record?',
        callback: function (result) {
            if (result) {
                $.post(cycleDeleteURL, { CycleId: dataItem }, function (data) {
                    if (data.Success) {
                        InitializeCycle();

                    } else {
                       alert("error");
                    }
                });
            } else {
                return false;
            }
        }
    })
}

function EmptyContent()
{
    $('#Company-Content').html("");
    $('#TermMonth-Content').html("");
    $('#Term-Content').html("");
    $('#SkillCoefficient-Content').html("");
    $('#Course-Content').html("");
    $('#Subject-Content').html("");
    $('#Skills-Content').html("");
    $('#Division-Content').html("");
    $('#Class-Content').html("");
    $('#Cycle-Content').html("");
}