UploadFiles = [];
var UrlUploaddocument = "";
function uploadCopyID() {

    var fileUpload = $("#fileUploadCopyID").get(0);
    var files = fileUpload.files;

    UploadFiles.push(files);

    //var FD = new FormData()
    //var fileInput = document.getElementById('fileUploadCopyID')
    //FD.append("files", fileInput.files[0])
    //const data = FD.entries().next().value
    //UploadFiles.push(data);
}

function uploadJudicialRecord() {

    var fileUpload = $("#fileUploadJudicialRecord").get(0);
    var files = fileUpload.files;

    UploadFiles.push(files);

    //var FD = new FormData()
    //var fileInput = document.getElementById('fileUploadJudicialRecord')
    //FD.append("files", fileInput.files[0])
    //const data = FD.entries().next().value
    //UploadFiles.push(data);
}


$("#file_CopyID").change(function () {
    alert();
    const file = this.files[0];
    if (file) {
        let reader = new FileReader();
        reader.onload = function (event) {
            $("#imgPreview")
                .attr("src", event.target.result);
        };
        reader.readAsDataURL(file);
    }
});


function UplaodDocument(e1, imgID)
{
    alert("ss");
    var files = $(e1)[0].files;
    UploadFiles.push(files);
}

$('.upload_image').on('change', function () {
    var files = $(this)[0].files;
    UploadFiles.push(files);
});

function documentPostRequset() {
    $("#Alert-Type").hide();
    var data = new FormData();
    for (i = 0; i < UploadFiles.length; i++) {
        data.append(UploadFiles[i][0], UploadFiles[i][1]);
    }
    data.append("User_ID", '@SessionContent.Container.Profiles.CurrentProfilesID');
    $.ajax({
        url: UrlPhoto,
        type: 'POST',
        contentType: "multipart/form-data",
        data: data,
        cache: false,
        processData: false,
        success: function (data) {
            $("#listFile").html(data.File);
            UploadFiles = [];
            if (data.Success == true) {
                $("#Alert-Type").show();
                $("#Alert-type-message").html("Success");
                $("#Alert-type-alert").addClass("alert-success");
                $("#Alert-type-alert").removeClass("alert-warning");
            }
            else {
                $("#Alert-Type").show();
                $("#Alert-type-message").html("Error");
                $("#Alert-type-alert").addClass("alert-warning");
                $("#Alert-type-alert").removeClass("alert-success");
            }
        },
        error: function () {
        }
    });
}