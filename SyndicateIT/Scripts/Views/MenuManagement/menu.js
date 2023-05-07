var currentTemplateRole= [];



function EditMenu(id)
{

    $('#Menu-Content').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(editMenuTemplatesUrl, { roleId: id }, function (data) {
        $('#Menu-Content').html(data);
        $('#Menu-Action').show();
        $("#add-template").show();
    });
}


function SaveMenuManagement()
{
    currentTemplateRole = [];
    var i =0;
    
    $(".level2-content", $("#TreeRole").html()).each(function () {
        var j = 0;
         var level1 = $(this);
         $(".smart-li-form", level1.html()).each(function () {

             if ($("#MenuCheckbox_"+i+"_"+j).is(':checked'))
            {
                currentTemplateRole.push({
                    ParentItemId: $("#MenuLevel1_" + i).val(),
                    ChildItemId: $("#MenuLevel2_" + i + "_" + j).val(),
                    RoleId: $("#RoleID_" + i).val(),
                });
            }

           
            j = j + 1;
        });

        i = i + 1;
    });

    $('#Menu-Content').html("<div style=' margin-left: 50%; margin-top:10px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    $.post(saveMenuManagementUrl, { currentTemplateRole: currentTemplateRole}, function (data) {
        $('#Menu-Content').html(data);
        $("#add-template").hide();
    });

}
