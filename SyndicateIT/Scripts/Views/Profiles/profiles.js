function FilterSearchClass() {
    return { Cycle_id: $("#SearchCycle").data("kendoDropDownList").value() };
}

function FilterSearchDivision() {
    return { Class_ID: $("#SearchClass").data("kendoDropDownList").value() };
}

function SearchProfiles() {
    $('#Profile-List').html("");
    //if (!isValidateProfilesSearch())
    //    return false;   
    var searchFirstName = $("#SearchFirstName").val();
    var searchLastName = $("#SearchLastName").val();
    var userType = $("#UserType").val();
    var searchCycle = "";
    var searchClass = "";
    var searchDivision = "";

    if (userType == "Students")
    {
         searchCycle = $("#SearchCycle").data("kendoDropDownList").value();
         searchClass = $("#SearchClass").data("kendoDropDownList").value();
         searchDivision = $("#SearchDivision").data("kendoDropDownList").value();
    }  


    $('#Profile-List').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadPofilesUrl, { searchFirstName: searchFirstName, searchLastName: searchLastName,searchCycle: searchCycle, searchClass: searchClass, searchDivision: searchDivision, userType: userType }, function (data)
     {       
          $('#Profile-List').html(data);        
     });
}

function DeleteJoinCourse(id)
{
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: 'Are You sure do you want to delete this Record',
        callback: function (result) {
            if (result) {
                $('#Main-AssignmentJoinCources').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
                $.post(deleteJoinCourseUrl, { id, id }, function (data) {
                    $('#Main-AssignmentJoinCources').html(data);
                    $("#Alert-Type").hide();
                });
            } else {
                return false;
            }
        }
    })
}

function EditProfiles(e)
{
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    //var uerTypeID = $("#UserTypeID").val();
    $.post(loadProfilesContentDetailsUrl, { userId: dataItem.ProfileID }, function (data) {
        $('#MainContent').html(data);
        $("#ContactDetailsTabId a").removeClass("pace");
        $("#EmploymentTabId a").removeClass("pace");
        $("#DependentTabId a").removeClass("pace");
        $("#InsurranceTabId a").removeClass("pace");
        $("#RegistrationTabId a").removeClass("pace");
        $("#AssignmentTabId a").removeClass("pace");
        $("#DocumentTabId a").removeClass("pace");
        $("#SystemPrivilegesTabId a").removeClass("pace");
    });
}

function LoadProfilesContentDetails() {
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    var uerTypeID = $("#UserTypeID").val();
    $.post(loadProfilesContentDetailsUrl, { uerTypeID: uerTypeID }, function (data) {
        $('#MainContent').html(data);
    });
}

function LoadProfilesContent() {
    $('#MainContent').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadProfilesContentUrl, {}, function (data) {
        $('#MainContent').html(data);
    });
}

function LoadPersonalInformations() {
    $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadPersonalInformationUrl, {}, function (data) {
        $('#Main-ProfilesDetails').html(data);
    });
}

function LoadEmployment() {
    $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadEmploymentUrl, {}, function (data) {
        $('#Main-ProfilesDetails').html(data);
    });
}

function LoadAssignment() {
    $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadAssignmentUrl, {}, function (data) {
        $('#Main-ProfilesDetails').html(data);
    });
}

function LoadDependent() {
    $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadDependentUrl, {}, function (data) {
        $('#Main-ProfilesDetails').html(data);
    });
}

function LoadEducation() {
    $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadEducationUrl, {}, function (data) {
        $('#Main-ProfilesDetails').html(data);
    });
}

function LoadExperience() {
    $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadExperienceUrl, {}, function (data) {
        $('#Main-ProfilesDetails').html(data);
    });
}
function LoadAccount() {
    $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadAccountUrl, {}, function (data) {
        $('#Main-ProfilesDetails').html(data);
    });
}

function LoadTransaction() {
    $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadTransactionUrl, {}, function (data) {
        $('#Main-ProfilesDetails').html(data);
    });
}


function LoadContactDetails() {
 $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
 $.post(loadContactDetailsUrl, {}, function (data) {
        $('#Main-ProfilesDetails').html(data);
    });
}

function LoadParentsInformation() {
 $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadParentsInformationUrl, {}, function (data) {
        $('#Main-ProfilesDetails').html(data);
    });
}

function LoadInsurrance() {
 $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadInsurranceUrl, {}, function (data) {
        $('#Main-ProfilesDetails').html(data);
    });
}

function LoadRegistration() {
 $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadRegistrationUrl, {}, function (data) {
        $('#Main-ProfilesDetails').html(data);
    });
}

function LoadDocument() {
 $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
 $.post(loadDocumentUrl, { }, function (data) {
        $('#Main-ProfilesDetails').html(data);
    });
}

function LoadApplicationStatus() {
    $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadApplicationStatusUrl, {}, function (data) {
        $('#Main-ProfilesDetails').html(data);
    });
}

function LoadSystemPrivileges() {
 $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
 $.post(loadSystemPrivilegesUrl, {}, function (data) {
        $('#Main-ProfilesDetails').html(data);
    });
}

function isValidateProfilesSearch() {

    $("#ErrorSearchFirstName").hide();
    $("#ErrorSearchLastName").hide();

    var isValid = true;
    var searchFirstName = $("#SearchFirstName").val();
    var searchLastName = $("#SearchLastName").val();

    if (searchFirstName == "") {
        $("#ErrorSearchFirstName").html("First Name is required").show();
        isValid = false;
    }

    if (searchLastName == "") {
        $("#ErrorSearchLastName").html("Last Name is required").show();
        isValid = false;
    }
    return isValid;
}

/***********************Profile Information *****************************************/

function SavePersonalInformation() {
    
    if (!IsValidatePersonalInformation())
        return false;

    var myData = $('#PersonalInformations-form').serialize();

    myData.SpeakingProficiencyArabic = $("#SpeakingProficiencyArabic").val();
    myData.ReadingProficiencyArabic = $("#ReadingProficiencyArabic").val();
    myData.WritingProficiencyArabic = $("#WritingProficiencyArabic").val();


    $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(savePersonalInformationsUrl, myData, function (data) {
        $('#Main-ProfilesDetails').html(data);
        if ($("#User_ID").val() != null && $("#User_ID").val() != "") {
            $("#ContactDetailsTabId a").removeClass("pace");
            $("#EmploymentTabId a").removeClass("pace");
            $("#DependentTabId a").removeClass("pace");
            $("#InsurranceTabId a").removeClass("pace");
            $("#RegistrationTabId a").removeClass("pace");
            $("#AssignmentTabId a").removeClass("pace");
            $("#DocumentTabId a").removeClass("pace");
            $("#SystemPrivilegesTabId a").removeClass("pace");
        }        
    });
}


function IsValidatePersonalInformation() {

    HidePersonalInformation();

    var isValid = true;


    if ($("#FirstName").val() == "") {
        $("#FirstName_validationMessage").show();
        isValid = false;
    }

    if ($("#MIddleName").val() == "") {
        $("#MiddleName_validationMessage").show();
        isValid = false;
    }

    if ($("#LastName").val() == "") {
        $("#LastName_validationMessage").show();
        isValid = false;
    }

    if ($("#MotherName").val() == "") {
        $("#MotherName_validationMessage").show();
        isValid = false;
    }


    if ($("#Date_Of_Birth").val() == "") {
        $("#Date_Of_Birth_validationMessage").show();
        isValid = false;
    }

    if ($("#FirstNationality").data("kendoDropDownList").value() != null && $("#FirstNationality").data("kendoDropDownList").value() == 0) {
        $("#FirstNationality_validationMessage").show();
        isValid = false;
    }
    
    if ($("#Place_Of_Birth").data("kendoDropDownList").value() != null && $("#Place_Of_Birth").data("kendoDropDownList").value() == 0) {
        $("#Place_Of_Birth_validationMessage").show();
        isValid = false;
    }
    
    
    if ($("#Email").val() == "") {
        $("#Email_validationMessage").show();
        isValid = false;
    }

    if ($("#UserName").val() == "") {
        $("#UserName_validationMessage").show();
        isValid = false;
    }
    
    if ($("#FirstNameName_Ar").val() == "") {
        $("#FirstNameName_Ar_validationMessage").show();
        isValid = false;
    }

    if ($("#MiddleName_Ar").val() == "") {
        $("#MiddleName_Ar_validationMessage").show();
        isValid = false;
    }

    if ($("#LastName_Ar").val() == "") {
        $("#LastName_Ar_validationMessage").show();
        isValid = false;
    }

    if ($("#MotherName_Ar").val() == "") {
        $("#MotherName_Ar_validationMessage").show();
        isValid = false;
    }
    if (sourceId != 9) {
        if ($("#UserReference_ID").data("kendoDropDownList").value() != null && $("#UserReference_ID").data("kendoDropDownList").value() == 0) {
            $("#UserReference_ID_validationMessage").show();
            isValid = false;
        }
    }
    if ($("#Roles_ID").data("kendoDropDownList").value() != null && $("#Roles_ID").data("kendoDropDownList").value() == 0) {
        $("#Roles_ID_validationMessage").show();
        isValid = false;
    }
   

    if (sourceId != 9) {

        if ($("#ApplicationNumber").val() == "") {
            $("#ApplicationNumber_validationMessage").show();
            isValid = false;
        }

        if ($("#ApplicationDate").val() == "") {
            $("#ApplicationDate_validationMessage").show();
            isValid = false;
        }

        if ($("#RegisteryNumber").val() == "") {
            $("#RegisteryNumber_validationMessage").show();
            isValid = false;
        }

        if ($("#RegisteryPlace").val() == "") {
            $("#RegisteryPlace_validationMessage").show();
            isValid = false;
        }
    }


    




    return isValid;
}


function HidePersonalInformation()
{
    $("#FirstName_validationMessage").hide();
    $("#MiddleName_validationMessage").hide();
    $("#LastName_validationMessage").hide();
    $("#MotherName_validationMessage").hide();
    $("#Place_Of_Birth_validationMessage").hide();
    $("#Date_Of_Birth_validationMessage").hide();
    $("#FirstNameName_Ar_validationMessage").hide();
    $("#MiddleName_Ar_validationMessage").hide();
    $("#LastName_Ar_validationMessage").hide();
    $("#MotherName_Ar_validationMessage").hide();
    $("#Email_validationMessage").hide();
    $("#FirstNationality_validationMessage").hide();

}

/***********************Contact Information *****************************************/

function SaveContactInformation() {

    if (!IsValidateContactInformation())
        return false;

    var myData = $('#PersonalContacts-form').serialize();
    
    $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(saveContactInformationsUrl, myData, function (data) {
        $('#Main-ProfilesDetails').html(data);
    });
}

// to add all the element to the verification

function IsValidateContactInformation() {

    HideContactInformationValidation();

    var isValid = true;
    if ($("#Country_ID").data("kendoDropDownList").value() != null && $("#Country_ID").data("kendoDropDownList").value() == 0) {
        $("#Country_validationMessage").show();
        isValid = false;
    }
    if ($("#City_ID").data("kendoDropDownList").value() != null && $("#City_ID").data("kendoDropDownList").value() == 0) {
        $("#City_validationMessage").show();
        isValid = false;
    }
    if ($("#Region_ID").data("kendoDropDownList").value() != null && $("#Region_ID").data("kendoDropDownList").value() == 0) {
        $("#Region_validationMessage").show();
        isValid = false;
    }
    if ($("#State_ID").data("kendoDropDownList").value() != null && $("#State_ID").data("kendoDropDownList").value() == 0) {
        $("#State_validationMessage").show();
        isValid = false;
    }

     if ($("#Floor").val() == "") {
        $("#Floor_validationMessage").show();
       isValid = false;
     }
     if ($("#TownShip").val() == "") {
         $("#TownShip_validationMessage").show();
         isValid = false;
     }
     if ($("#Building").val() == "") {
         $("#Building_validationMessage").show();
         isValid = false;
     }
     if ($("#Street").val() == "") {
         $("#Street_validationMessage").show();
         isValid = false;
     }
    //Modified so the validition work properly
     return isValid;


}


function HideContactInformationValidation() {
    $("#Country_validationMessage").hide();
    $("#State_validationMessage").hide();
    $("#Region_validationMessage").hide();
    $("#City_validationMessage").hide();
    $("#TownShip_validationMessage").hide();
    $("#Street_validationMessage").hide();
    $("#Building_validationMessage").hide();
    $("#Floor_validationMessage").hide();
   }


/***********************Insurance *****************************************/

function SaveInsurance() {

    if (!IsValidateInsurance())
        return false;

    var myData = $('#Insurance-form').serialize();

    $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(saveInsuranceUrl, myData, function (data) {
        $('#Main-ProfilesDetails').html(data);
    });
}


function IsValidateInsurance() {

    HideInsuranceValidation();
    var isValid = true
    if ($("#HasInsurance_True").is(':checked') == true) {
        if ($("#TypeInsurance_Id").data("kendoDropDownList").value() == 0) {
            $("#TypeInsurance_Id_validationMessage").show();
            isValid = false;
        }

        if ($("#DegreeInsurance_Id").data("kendoDropDownList").value() == 0) {
            $("#DegreeInsurance_validationMessage").show();
            isValid = false;
        }

    }

    if ($("#HasGuarantee_True").is(':checked') == true) {
        if ($("#TypeGuarantee_Id").data("kendoDropDownList").value() == 0) {
            $("#TypeGuarantee_validationMessage").show();
            isValid = false;
        }
        if ($("#DegreeGuarantee_Id").data("kendoDropDownList").value() == 0) {
            $("#DegreeGuarantee_validationMessage").show();
            isValid = false;
        }
    }
    return isValid;

}


function HideInsuranceValidation() {
    $("#TypeInsurance_Id_validationMessage").hide();
    $("#DegreeInsurance_validationMessage").hide();
    $("#TypeGuarantee_validationMessage").hide();
    $("#DegreeGuarantee_validationMessage").hide();
}

/***********************Employment *****************************************/

function SaveEmployment() {

    if (!IsValidateEmployment())
        return false;

    var myData = $('#Employment-form').serialize();

    $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(saveEmploymentUrl, myData, function (data) {
        $('#Main-ProfilesDetails').html(data);
    });
}


function IsValidateEmployment() {

    HideEmploymentValidation();

    var isValid = true;


    if ($("#JOB_ID").data("kendoDropDownList").value() != null && $("#JOB_ID").data("kendoDropDownList").value() == 0) {
        $("#JobTitle_validationMessage").show();
        isValid = false;
    }
    if ($("#DEPARTMENT_ID").data("kendoDropDownList").value() != null && $("#DEPARTMENT_ID").data("kendoDropDownList").value() == 0) {
        $("#Department_validationMessage").show();
        isValid = false;
    }



    if ($("#Grade").val() == "") {
        $("#Grade_validationMessage").show();
        isValid = false;
    }

    if ($("#Division").val() == "") {
        $("#Division_validationMessage").show();
        isValid = false;
    }

    if ($("#EmployeeNumber").val() == "") {
        $("#EmployeeNumber_validationMessage").show();
        isValid = false;
    }

    if ($("#ContractDates").val() == "") {
        $("#ContractDates_validationMessage").show();
        isValid = false;
    }

    if ($("#HoursPerDay").val() == "") {
        $("#HoursPerDay_validationMessage").show();
        isValid = false;
    }

    if ($("#WorkingDays").val() == "") {
        $("#WorkingDays_validationMessage").show();
        isValid = false;
    }
    return isValid;

}


function HideEmploymentValidation() {
    $("#JobTitle_validationMessage").hide();
    $("#Department_validationMessage").hide();
    $("#Grade_validationMessage").hide();
    $("#Division_validationMessage").hide();
    $("#EmployeeNumber_validationMessage").hide();
    $("#ContractDates_validationMessage").hide();
    $("#HoursPerDay_validationMessage").hide();
    $("#WorkingDays_validationMessage").hide();
}

/***********************Experience *****************************************/


function DeleteExperience(i) {


    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: 'Are You sure do you want to delete this Record',
        callback: function (result) {
            if (result) {
                var myData = $('#ExperienceList-form').serialize();
                $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
                $.post(DeleteExperienceUrl, { myData, i }, function (data) {
                    $('#Main-ProfilesDetails').html(data);
                });
            } else {
                return false;
            }
        }
    })
}

function SaveExperience() {

    if (!IsValidateExperience())
        return false;

    var myData = $('#Experience-form').serialize();

    $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(saveExperienceUrl, myData, function (data) {
        $('#Main-ProfilesDetails').html(data);        
    });
}

function IsValidateExperience() {

    HideExperiencetValidation();

    var isValid = true;

    
    if ($("#JobTitle").val() == "") {
        $("#JobTitle_validationMessage").show();
        isValid = false;
    }
    if ($("#Department").val() == "") {
        $("#Department_validationMessage").show();
        isValid = false;
    }
    if ($("#Company").val() == "") {
        $("#Company_validationMessage").show();
        isValid = false;

    }
    if ($("#Location").val() == "") {
        $("#Location_validationMessage").show();
        isValid = false;
    }
    if ($("#FromDate").val() == "") {
        $("#FromDate_validationMessage").show();
        isValid = false;
    }
    if ($("#ToDate").val() == "") {
        $("#ToDate_validationMessage").show();
        isValid = false;
    }


    return isValid;
}

function HideExperiencetValidation() {  
    $("#JobTitle_validationMessage").hide();
    $("#Department_validationMessage").hide();
    $("#Company_validationMessage").hide();
    $("#Email_validationMessage").hide();
    $("#Location_validationMessage").hide();
    $("#FromDate_validationMessage").hide();
    $("#ToDate_validationMessage").hide();
}

/*********************** Education *****************************************/


function DeleteEducation(i) {


    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: 'Are You sure do you want to delete this Record',
        callback: function (result) {
            if (result) {
                var myData = $('#EducationList-form').serialize();
                $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
                $.post(DeleteEducationUrl, { myData, i }, function (data) {
                    $('#Main-ProfilesDetails').html(data);
                });
            } else {
                return false;
            }
        }
    })
}

function SaveEducation() {

    if (!IsValidateEducation())
        return false;

    var myData = $('#Education-form').serialize();

    $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(saveEducationUrl, myData, function (data) {
        $('#Main-ProfilesDetails').html(data);
    });
}

function IsValidateEducation() {

    HideEducationtValidation ();

    var isValid = true;


    if ($("#School").val() == "") {
        $("#School_validationMessage").show();
        isValid = false;
    }
    if ($("#Degree").val() == "") {
        $("#Degree_validationMessage").show();
        isValid = false;
    }
    if ($("#Grade").val() == "") {
        $("#Grade_validationMessage").show();
        isValid = false;

    }
  
    if ($("#FromDate").val() == "") {
        $("#FromDate_validationMessage").show();
        isValid = false;
    }
    if ($("#ToDate").val() == "") {
        $("#ToDate_validationMessage").show();
        isValid = false;
    }


    return isValid;
}

function HideEducationtValidation () {
    $("#School_validationMessage").hide();
    $("#Degree_validationMessage").hide();
    $("#Grade_validationMessage").hide();
    $("#FromDate_validationMessage").hide();
    $("#ToDate_validationMessage").hide();
}

/***********************Dependent *****************************************/

function DeleteDependent(i) {


    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: 'Are You sure do you want to delete this Record',
        callback: function (result) {
            if (result) {
                var myData = $('#DependentList-form').serialize();
                $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
                $.post(DeleteDependentUrl, { myData, i }, function (data) {
                    $('#Main-ProfilesDetails').html(data);
                });
            } else {
                return false;
            }
        }
    })
}

function SaveDependent() {

    if (!IsValidateDependent())
        return false;

    var myData = $('#Dependent-form').serialize();

    $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(saveDependentUrl, myData, function (data) {
        $('#Main-ProfilesDetails').html(data);
        if ($("IsMemberOfSchool").is(':checked')) {
            $("#PersonalInformationIsMember").show();
            $("#PersonalInformationNotMember").hide();

        } else {
            $("#PersonalInformationIsMember").hide();
            $("#PersonalInformationNotMember").show();
        }
    });
}

function IsValidateDependent() {

    HideDependentValidation();

    var isValid = true;

    if ($("#Relation_Type_ID").data("kendoDropDownList").value() != null && $("#Relation_Type_ID").data("kendoDropDownList").value() == 0) {
        $("#Relation_Type_ID_validationMessage").show();
        isValid = false;
    }

    if ($(IsMemberOfSchool).is(':checked') == false) {
        if ($("#Title").val() == "") {
            $("#Title_validationMessage").show();
            isValid = false;
        }
        if ($("#DateOfBirth").val() == "") {
            $("#DateOfBirth_validationMessage").show();
            isValid = false;
        }
        if ($("#FirstName").val() == "") {
            $("#FirstName_validationMessage").show();
            isValid = false;

        }
        if ($("#LastName").val() == "") {
            $("#LastName_validationMessage").show();
            isValid = false;
        }
        if ($("#Email").val() == "") {
            $("#Email_validationMessage").show();
            isValid = false;
        }
        if ($("#Mobile").val() == "") {
            $("#Mobile_validationMessage").show();
            isValid = false;
        }

    }
    else {
        if ($("#Relative_ID").data("kendoDropDownList").value() != null && $("#Relative_ID").data("kendoDropDownList").value() == 0) {
            $("#Relative_ID_validationMessage").show();
            isValid = false;
        }

    }

    //Relation_Type_ID_validationMessage


    return isValid;
}

function HideDependentValidation() {
    $("#Title_validationMessage").hide();
    $("#DateOfBirth_validationMessage").hide();
    $("#FirstName_validationMessage").hide();
    $("#LastName_validationMessage").hide();
    $("#Email_validationMessage").hide();
    $("#Mobile_validationMessage").hide();
    $("#Relation_Type_ID_validationMessage_validationMessage").hide();
    $("#Relative_ID_validationMessage_validationMessage").hide();


}

/***********************System Privileges *****************************************/

function SaveSystemPrivileges() {

    if (!IsValidateSystemPrivileges())
        return false;

    var myData = $('#SystemPrivileges-form').serialize();

    $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(saveSystemPrivilegesUrl, myData, function (data) {
        $('#Main-ProfilesDetails').html(data);
    });
}

function IsValidateSystemPrivileges() {

    HideSystemPrivilegesValidation();

    var isValid = true;


      if ($("#FirstName").val() == "") {
        $("#FirstName_validationMessage").show();
        isValid = false;
    }

    if ($("#LastName").val() == "") {
        $("#LastName_validationMessage").show();
        isValid = false;
    }

    if ($("#AccountNumberId").val() == "") {
         $("#AccountNumberId_validationMessage").show();
        isValid = false;
     }

     if ($("#Username").val() == "") {
         $("#Username_validationMessage").show();
         isValid = false;
     }     
     if ($("#RoleID").val() == "" || $("#RoleID").val() == 0) {
         $("#RoleID_validationMessage").show();
         isValid = false;
     }
     if ($("#HasEmail").prop("checked") == false && ($("#Password").val() == "" || $("#Password").val() == 0)) {
         $("#Password_validationMessage").show();
         isValid = false;
     }
     

     if ($("#HasEmail").prop("checked") == false && ($("#ConfirmPassword").val() == "" || $("#ConfirmPassword").val() == 0)) {
         $("#ConfirmPassword_validationMessage").show();
         isValid = false;
     }

     if ($("#HasEmail").prop("checked") == false && ($("#ConfirmPassword").val() != $("#Password").val())) {
         $("#ConfirmPasswordValid_validationMessage").show();
         isValid = false;
     }

   
     return isValid;

}


function HideSystemPrivilegesValidation() {

    $("#AccountNumberId_validationMessage").hide();
    $("#Username_validationMessage").hide();
    $("#RoleID_validationMessage").hide();
    $("#Password_validationMessage").hide();
    $("#ConfirmPassword_validationMessage").hide();
    $("#ConfirmPasswordValid_validationMessage").hide();
}


/***********************Document *****************************************/

function SaveDocument() {

    if (!IsValidateDocument())
        return false;

    var myData = $('#Document-form').serialize();

    $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(saveDocumentUrl, myData, function (data) {
        $('#Main-ProfilesDetails').html(data);
    });
}

function IsValidateDocument() {

    HideDocumentValidation();

    var isValid = true;



    return isValid;

}


function HideDocumentValidation() {

}



/*********************** Application Status *****************************************/

function SaveApplicationStatus() {

    if (!IsValidateRegistration())
        return false;

    var myData = $('#ApplicationStatus-form').serialize();

    $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(saveApplicationStatusUrl, myData, function (data) {
        $('#Main-ProfilesDetails').html(data);
    });
}

/*********************** Registration *****************************************/

function SaveRegistration() {

    if (!IsValidateRegistration())
        return false;

    var myData = $('#Registration-form').serialize();

    $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(saveRegistrationUrl, myData, function (data) {
        $('#Main-ProfilesDetails').html(data);
    });
}

function SaveHistoryRegistration() {

    if (!IsValidateHistoryRegistration())
        return false;

    var myData = $('#RegistartionHistory-form').serialize();

    $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(SaveHistoryRegistrationUrl, myData, function (data) {
        $('#Main-ProfilesDetails').html(data);
    });
}

function DeleteHistory(i) {


    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: 'Are You sure do you want to delete this Record',
        callback: function (result) {
            if (result) {
                var myData = $('#HistoryList-form').serialize();
                $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
                $.post(DeleteHistoryUrl, { myData, i }, function (data) {
                    $('#Main-ProfilesDetails').html(data);
                });
            } else {
                return false;
            }
        }
    })
}

function IsValidateRegistration() {

    HideRegistrationValidation();

    var isValid = true;

    //if ($("#CardNumber").val() == "") {
    //    $("#CardNumber_validationMessage").show();
    //    isValid = false;
    //}
  


    if ($("#TypeSpecialty").val() == "") {
        $("#TypeSpecialty_validationMessage").show();
        isValid = false;
    }

    if ($("#From").val() == "") {
        $("#From_validationMessage").show();
        isValid = false;
    }
    if ($("#To").val() == "") {
        $("#To_validationMessage").show();
        isValid = false;
    }


    return isValid;

}

function HideRegistrationValidation() {
    $("#TypeSpecialty_validationMessage").hide();
    $("#From_validationMessage").hide();
    $("#To_validationMessage").hide();
}

function IsValidateHistoryRegistration() {

    HideHistoryRegistrationValidation();

    var isValid = true;

    if ($("#SchoolName").val() == "") {
        $("#SchoolName_validationMessage").show();
        isValid = false;
    }

    if ($("#Location").val() == "") {
        $("#Location_validationMessage").show();
        isValid = false;
    }
    if ($("#Description").val() == "") {
        $("#Description_validationMessage").show();
        isValid = false;
    }

    if ($("#FromHistory").val() == "") {
        $("#FromHistory_validationMessage").show();
        isValid = false;
    }
    if ($("#ToHistory").val() == "") {
        $("#ToHistory_validationMessage").show();
        isValid = false;
    }


    return isValid;

}

function HideHistoryRegistrationValidation() {

    $("#SchoolName_validationMessage").hide();
    $("#Location_validationMessage").hide();
    $("#Description_validationMessage").hide();
    $("#FromHistory_validationMessage").hide();
    $("#ToHistory_validationMessage").hide();
}

/*********************** Assignment *****************************************/

function SaveAssignment() {

    if (!IsValidateAssignment())
        return false;

    var myData = $('#Assignment-form').serialize();

    $('#Main-ProfilesDetails').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(saveAssignmentUrl, myData, function (data) {
        $('#Main-ProfilesDetails').html(data);

    });
}

function IsValidateAssignment() {

    HideAssignmentValidation();

    var isValid = true;
    if ($("#Course_ID").data("kendoDropDownList").value() == null || $("#Course_ID").data("kendoDropDownList").value() == 0 || $("#Course_ID").data("kendoDropDownList").value() == "") {
        $("#Course_ID_validationMessage").show();
        isValid = false;
    }


    return isValid;

}

function HideAssignmentValidation() {
    $("#Course_ID_validationMessage").hide();
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

function FilterRegion() {
    return { Country_ID: $("#Country_ID").data("kendoDropDownList").value() };
}

function FilterKaddaa() {
    return { Region_ID: $("#Region_ID").data("kendoDropDownList").value() };
}

function FilterPlace() {
    return { Kaddaa_ID: $("#State_ID").data("kendoDropDownList").value() };
}

function FilterPlaceOfBirth() {
    return { Country_ID: $("#Country_ID").data("kendoDropDownList").value() };
}

function sFilterRegion() {
    return { Country_ID: $("#SecondCOUNTRY_ID").data("kendoDropDownList").value() };
}

function sFilterKaddaa() {
    return { Region_ID: $("#SecondREGION_ID").data("kendoDropDownList").value() };
}

function sFilterPlace() {
    return { Kaddaa_ID: $("#SecondSTATE_ID").data("kendoDropDownList").value() };
}


$("#IsMemberOfSchool").change(function () {
        if ($(this).is(':checked')) {
            $("#Relation_Type_ID").data("kendoDropDownList").enable(true);
            $("#Relative_ID").data("kendoDropDownList").enable(true);
        } else {
            $("#Relation_Type_ID").data("kendoDropDownList").enable(false);
            $("#Relative_ID").data("kendoDropDownList").enable(false);
        }
    });

function FilterClass() {
    return { Cycle_ID: $("#Cycle_ID").data("kendoDropDownList").value() };
}
function FilterDivision() {
    return { Class_ID: $("#Class_ID").data("kendoDropDownList").value() };
}
function FilterCourse() {
    return { Division_ID: $("#Division_ID").data("kendoDropDownList").value() };
}
function FilterClassName() {
    return { Cycle_ID: $("#CycleName").data("kendoDropDownList").value() };
}

function IntiliaizeStar()
{
    $("#SpeakingProficiencyArabic-1").click(function () {
        $("#SpeakingProficiencyArabic").val(1);
    });

    $("#SpeakingProficiencyArabic-2").click(function () {
        $("#SpeakingProficiencyArabic").val(2);
    });

    $("#SpeakingProficiencyArabic-3").click(function () {
        $("#SpeakingProficiencyArabic").val(3);
    });

    $("#SpeakingProficiencyArabic-4").click(function () {
        $("#SpeakingProficiencyArabic").val(4);
    });

    $("#SpeakingProficiencyArabic-5").click(function () {
        $("#SpeakingProficiencyArabic").val(5);
    });
    /********************************************************************************************/
    $("#ReadingProficiencyArabic-1").click(function () {
        $("#ReadingProficiencyArabic").val(1);
    });

    $("#ReadingProficiencyArabic-2").click(function () {
        $("#ReadingProficiencyArabic").val(2);
    });

    $("#ReadingProficiencyArabic-3").click(function () {
        $("#ReadingProficiencyArabic").val(3);
    });

    $("#ReadingProficiencyArabic-4").click(function () {
        $("#ReadingProficiencyArabic").val(4);
    });

    $("#ReadingProficiencyArabic-5").click(function () {
        $("#ReadingProficiencyArabic").val(5);
    });
    /********************************************************************************************/

    $("#WritingProficiencyArabic-1").click(function () {
        $("#WritingProficiencyArabic").val(1);
    });

    $("#WritingProficiencyArabic-2").click(function () {
        $("#WritingProficiencyArabic").val(2);
    });

    $("#WritingProficiencyArabic-3").click(function () {
        $("#WritingProficiencyArabic").val(3);
    });

    $("#WritingProficiencyArabic-4").click(function () {
        $("#WritingProficiencyArabic").val(4);
    });

    $("#WritingProficiencyArabic-5").click(function () {
        $("#WritingProficiencyArabic").val(5);
    });
    /******************************************************Anglais*******************************************/

    $("#SpeakingProficiencyAnglais-1").click(function () {
        $("#SpeakingProficiencyAnglais").val(1);
    });

    $("#SpeakingProficiencyAnglais-2").click(function () {
        $("#SpeakingProficiencyAnglais").val(2);
    });

    $("#SpeakingProficiencyAnglais-3").click(function () {
        $("#SpeakingProficiencyAnglais").val(3);
    });

    $("#SpeakingProficiencyAnglais-4").click(function () {
        $("#SpeakingProficiencyAnglais").val(4);
    });

    $("#SpeakingProficiencyAnglais-5").click(function () {
        $("#SpeakingProficiencyAnglais").val(5);
    });
    /********************************************************************************************/
    $("#ReadingProficiencyAnglais-1").click(function () {
        $("#ReadingProficiencyAnglais").val(1);
    });

    $("#ReadingProficiencyAnglais-2").click(function () {
        $("#ReadingProficiencyAnglais").val(2);
    });

    $("#ReadingProficiencyAnglais-3").click(function () {
        $("#ReadingProficiencyAnglais").val(3);
    });

    $("#ReadingProficiencyAnglais-4").click(function () {
        $("#ReadingProficiencyAnglais").val(4);
    });

    $("#ReadingProficiencyAnglais-5").click(function () {
        $("#ReadingProficiencyAnglais").val(5);
    });
    /********************************************************************************************/

    $("#WritingProficiencyAnglais-1").click(function () {
        $("#WritingProficiencyAnglais").val(1);
    });

    $("#WritingProficiencyAnglais-2").click(function () {
        $("#WritingProficiencyAnglais").val(2);
    });

    $("#WritingProficiencyAnglais-3").click(function () {
        $("#WritingProficiencyAnglais").val(3);
    });

    $("#WritingProficiencyAnglais-4").click(function () {
        $("#WritingProficiencyAnglais").val(4);
    });

    $("#WritingProficiencyAnglais-5").click(function () {
        $("#WritingProficiencyAnglais").val(5);
    });

    /******************************************************French*******************************************/
    $("#SpeakingProficiencyFrench-1").click(function () {
        $("#SpeakingProficiencyFrench").val(1);
    });

    $("#SpeakingProficiencyFrench-2").click(function () {
        $("#SpeakingProficiencyFrench").val(2);
    });

    $("#SpeakingProficiencyFrench-3").click(function () {
        $("#SpeakingProficiencyFrench").val(3);
    });

    $("#SpeakingProficiencyFrench-4").click(function () {
        $("#SpeakingProficiencyFrench").val(4);
    });

    $("#SpeakingProficiencyFrench-5").click(function () {
        $("#SpeakingProficiencyFrench").val(5);
    });
    /********************************************************************************************/
    $("#ReadingProficiencyFrench--1").click(function () {
        $("#ReadingProficiencyFrench").val(1);
    });

    $("#ReadingProficiencyFrench-2").click(function () {
        $("#ReadingProficiencyFrench").val(2);
    });

    $("#ReadingProficiencyFrench-3").click(function () {
        $("#ReadingProficiencyFrench").val(3);
    });

    $("#ReadingProficiencyFrench-4").click(function () {
        $("#ReadingProficiencyFrench").val(4);
    });

    $("#ReadingProficiencyFrench-5").click(function () {
        $("#ReadingProficiencyFrench").val(5);
    });
    /********************************************************************************************/

    $("#WritingProficiencyFrench-1").click(function () {
        $("#WritingProficiencyFrench").val(1);
    });

    $("#WritingProficiencyFrench-2").click(function () {
        $("#WritingProficiencyFrench").val(2);
    });

    $("#WritingProficiencyFrench-3").click(function () {
        $("#WritingProficiencyFrench").val(3);
    });

    $("#WritingProficiencyFrench-4").click(function () {
        $("#WritingProficiencyFrench").val(4);
    });

    $("#WritingProficiencyFrench-5").click(function () {
        $("#WritingProficiencyFrench").val(5);
    });

}
