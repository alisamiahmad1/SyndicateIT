function AddDepartment() {
    $('#DepartmentContent').html("");
    $('#DepartmentContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadDepartmentURL, {}, function (data) {
        $('#DepartmentContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditDepartment(id) {
    var dataItem = id;
    $('#DepartmentContent').html("");
    $('#DepartmentContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadDepartmentURL, { DepartmentId: dataItem }, function (data) {
        $('#DepartmentContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveDepartment() {
    if (!isValidateSaveDepartment())
        return false;
    var myData = $('#Department-form').serialize();
    $.post(saveDepartmentUrl, myData, function (data) {
        $('#DepartmentContent').html(data);
        $("#DEPARTMENT_NAME").val("");
    });
}

function EditTranslateDepartment(id) {
    var dataItem = id;
    $('#DepartmentList').html("");
    $('#DepartmentList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateDepartmentURL, { TableId: dataItem.DEPARTMENT_ID, TableName: 'Department_BY_LANGUAGE' }, function (data) {
        $('#DepartmentList').html(data);
    });
}

function DeleteDepartment(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(DepartmentDeleteURL, { DepartmentId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchDepartment(false))
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

function SearchDepartment(isRemoveAlert) {


    var Departmentname = $("#DEPARTMENT_NAME").val();
    var languageid = $("#LANGUAGE_ID").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#Department-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(DepartmentSearchURL, { DepartmentName: Departmentname, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#Department-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveDepartment() {

    var isValid = true;
    var lst = ["#DEPARTMENT_NAME"];
    var lstmsg = ["#DEPARTMENT_NAME_ValidationMsg"];

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


//translate Department
function EditTranslateDepartment(id) {

    $('#DepartmentTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#DepartmentTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationDepartmentURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#DepartmentTranslate-List').html(data)
    });
}



//end translate

