"use strict";
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
function FilterSearchClass() {
    return { Cycle_ID: $("#Cycle_id").data("kendoDropDownList").value() };
}
function FilterSearchDivision() {
    return { Class_ID: $("#Class_ID").data("kendoDropDownList").value() };
}

function AddSkills() {
    $('#SkillsContent').html("");
    $('#SkillsContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadSkillsURL, {}, function (data) {
        $('#SkillsContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
        $("#SecKjCoeficientGrade").hide();
    });
}

function EditSkills(id) {
    var dataItem = id;
    $('#SkillsContent').html("");
    $('#SkillsContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadSkillsURL, { SkillsId: dataItem }, function (data) {
        $('#SkillsContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveSkills() {
    if (!isValidateSaveSkills())
        return false;
    var myData = $('#Skills-form').serialize();
    $.post(saveSkillsUrl, myData, function (data) {
        $('#SkillsContent').html(data);
        $("#Skill_Name").val("");
    });
}

function EditTranslateSkills(id) {
    var dataItem = id;
    $('#SkillsList').html("");
    $('#SkillsList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateSkillsURL, { TableId: dataItem.Skill_ID, TableName: 'Skills_By_Language' }, function (data) {
        $('#SkillsList').html(data);
    });
}

function DeleteSkills(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(SkillsDeleteURL, { SkillsId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchSkills(false))
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

function SearchSkills(isRemoveAlert) {

    var Cycle_ID = $("#Cycle_id").data("kendoDropDownList").value();
    var Class_ID = $("#Class_ID").data("kendoDropDownList").value();
    var Division_ID = $("#Division_ID").data("kendoDropDownList").value();  
    var Course_ID = $("#Course_ID").data("kendoDropDownList").value();

    var Skillsname = $("#Skill_Name").val();
    var languageid = $("#LANGUAGE_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#Skills-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(SkillsSearchURL, { Cycle_ID: Cycle_ID, Class_ID: Class_ID, Division_ID: Division_ID,Course_ID:Course_ID, SkillsName: Skillsname, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#Skills-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

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


//translate Skills
function EditTranslateSkills(id) {

    $('#SkillsTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#SkillsTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationSkillsURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#SkillsTranslate-List').html(data)
    });
}



//end translate

