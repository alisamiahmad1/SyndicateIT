function CloseChangeRequest(id)
{    

     var answer = BootstrapDialog.confirm({
        title: 'Pin Pay',
        message: "Are you sure you want to close this change request?",
        callback: function (result) {
            if (result) {
                $.post(closeChangeRequestURL, { id: id }, function (data) {
                    if (data.isSucces == true)
                    {
                        $('#close_' + id).remove();
                        $('#labelColor_' + id).html("Close");
                        $('#labelColor_' + id).removeClass("open").addClass("close");
                    }else
                    {
                        alert(data.message);
                    }
                });              
            } else {
                return false;
            }
        }
    })
}


function AddChangeRequest()
{

    if (!isValidateSaveChangeRequestDefinitions())
        return false;

    typeId = $("#ChangeRequestTypeID").data("kendoDropDownList").value();
    description = $("#ChangeRequestDescription").val();

    $('#ChangeRequest-List').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(saveChangeRequestDefinitionsURL, { typeId: typeId, description: description }, function (data) {
        $('#ChangeRequest-List').html(data);
         EmptyChangeRequest();
    });
}


function LoadViewAllChangeRequestList() {
    $('#ChangeRequest-List').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(loadViewAllChangeRequestListUrl, {}, function (data) {
        $('#ChangeRequest-List').html(data);
    });
}


function HideChangeRequestDefinitionsValidation() {   
    $("#ChangeRequestTypeID_validationMessage").hide();
}

function isValidateSaveChangeRequestDefinitions() {

    HideChangeRequestDefinitionsValidation();

    var isValid = true;

    if ($("#ChangeRequestTypeID").data("kendoDropDownList").value() == "") {
        $("#ChangeRequestTypeID_validationMessage").show();
        isValid = false;
    }  
    

    return isValid;
}

function EmptyChangeRequest()
{
    $("#ChangeRequestTypeID").data("kendoDropDownList").value("");
    $("#ChangeRequestDescription").val("");
}