function ViewPeronalDetail(fileId, sourceId) {

    window.location.href = '/ProfileManagement/ViewProfilesContentDetails?userId=' + fileId + '&sourceId=' + sourceId;

}

function ViewFileDetail(fileId, sourceId) {
    window.location.href = '/FileManagement/Student?userid=' + fileId + '&sourceId=' + sourceId;
}