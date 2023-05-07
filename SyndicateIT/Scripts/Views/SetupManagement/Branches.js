function AddBranches() {
    $('#BranchesContent').html("");
    $('#BranchesContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadBranchesURL, {}, function (data) {
        $('#BranchesContent').html(data);
        $('.form-horizontal').find('input[type=text], textarea').val("");
        $('.form-horizontal #IS_ACTIVE').prop('checked', true);
    });
}

function EditBranches(id) {
    var dataItem = id;
    $('#BranchesContent').html("");
    $('#BranchesContent').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadBranchesURL, { BranchesId: dataItem }, function (data) {
        $('#BranchesContent').html(data);
        $('#checkout-form').find('input[type=text], textarea').val("");
    });
}

function SaveBranches() {
    if (!isValidateSaveBranches())
        return false;
    var myData = $('#Branches-form').serialize();
    $.post(saveBranchesUrl, myData, function (data) {
        $('#BranchesContent').html(data);
        $("#Name").val("");
    });
}

function EditTranslateBranches(id) {
    var dataItem = id;
    $('#BranchesList').html("");
    $('#BranchesList').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslateBranchesURL, { TableId: dataItem.BranchId, TableName: 'Branches_By_Language' }, function (data) {
        $('#BranchesList').html(data);
    });
}

function DeleteBranches(id) {
    //e.preventDefault();
    var dataItem = id;
    var answer = BootstrapDialog.confirm({
        title: 'SyndicateIT',
        message: messageDelete,
        callback: function (result) {
            if (result) {
                $.post(BranchesDeleteURL, { BranchesId: dataItem }, function (data) {
                    if (data.Success) {
                        $.when(SearchBranches(false))
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

function SearchBranches(isRemoveAlert) {


    var Branchesname = $("#Name").val();
    var languageid = $("#language").data("kendoDropDownList").value();
    var isActive = $("#IS_ACTIVE").is(":checked");
    //$('#loader-img').show();
    $('#Branches-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(BranchesSearchURL, { BranchesName: Branchesname, languageId: languageid, Is_Active: isActive }, function (data) {
        $("#widget-grid-Transalte").hide();
        $('#Branches-List').html(data);
        if (isRemoveAlert == true) {
            $("#Alert-Type").remove();
        }

    });

}



function isValidateSaveBranches() {

    var isValid = true;
    var lst = ["#Name","#Email","#Address","#MobileNumbe","#Country","#City","#User_Id","#RecordDate"];
    var lstmsg = ["#Name_ValidationMsg","#Email_ValidationMsg","#Address_ValidationMsg","#MobileNumber_ValidationMsg","#Country_ValidationMsg","#City_ValidationMsg","#User_Id_ValidationMsg","#RecordDate_validationMessage"];

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


//translate Branches
function EditTranslateBranches(id) {

    $('#BranchesTranslate-List').html("");
    $("#widget-grid-Transalte").show();
    $('#BranchesTranslate-List').html("<div class='loader'><img src=" + loader + " /></div>");
    $.post(loadTranslationBranchesURL, { TableId: id, TableName: '' }, function (data) {
        $("#widget-grid-Transalte").show();
        $('#BranchesTranslate-List').html(data)
    });
}



//end translate

