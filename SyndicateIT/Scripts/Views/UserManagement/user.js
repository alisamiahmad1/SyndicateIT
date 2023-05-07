
$(document).ready(function () {
    /// <summary>
    /// 
    /// </summary>
    $('#userList').html("<div style=' margin-left: 50%; margin-top:120px; width: 250px; height: 70px;'><img src=" + loader + " /></div>");
    setTimeout("OnLoadGridUser();", 1000);
});

function OnLoadGridUser() { 
    /// <summary>
    /// Called when [load grid user].
    /// </summary>
    $.post(loadGridUserURL, {}, function (data) {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">The data.</param>
        $('#userList').html(data);
       
    });
}
function EditUser(e) {
    /// <summary>
    /// Edits the user.
    /// </summary>
    /// <param name="e">The e.</param>
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    $.post(editUserURL, { id: dataItem.id }, function (data) {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">The data.</param>
        location = data.url;
    });
}

function AddUser() {
   
    location = loadAddUrl;
   
}

function onDataBound(e) {
    /// <summary>
    /// Ons the data bound.
    /// </summary>
    /// <param name="e">The e.</param>
    var grid = $("#Users").data("kendoGrid");
     var gridData = grid.dataSource.view();
    for (var i = 0; i < gridData.length; i++) {
        var currentUid = gridData[i].uid;
        var status = gridData[i].Status;
        var currentRow = grid.table.find("tr[data-uid='" + currentUid + "']");
        if (status == "Active") {
            var activeButton = $(currentRow).find(".k-grid-ActiveUser");
            activeButton.hide();
        }
        else {
            var activeButton = $(currentRow).find(".k-grid-DeactiveUser");
            activeButton.hide();
        }
    }
}

function DeactiveUser(e) {
    /// <summary>
    /// Deactives the user.
    /// </summary>
    /// <param name="e">The e.</param>
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    var answer = BootstrapDialog.confirm({
        title: 'Pin Pay',
        message:  deactivetedUser,
        callback: function (result) {
            // result will be true if button was click, while it will be false if users close the dialog directly.
            /// <summary>
            /// 
            /// </summary>
            /// <param name="result">The result.</param>
            if (result) {
                $.post(deactiveUserURL, { id: dataItem.ID }, function (data) {
                    /// <summary>
                    /// 
                    /// </summary>
                    /// <param name="data">The data.</param>
                    if (data.success) {
                        var grid = $("#Users").data("kendoGrid");
                        grid.dataSource.read();
                    } else { alert(data.message); }
                });
            } else {
                return false;
            }
        }
    })
}

function ActiveUser(e) {
    /// <summary>
    /// Actives the user.
    /// </summary>
    /// <param name="e">The e.</param>
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    var answer = BootstrapDialog.confirm({
        title: 'Pin Pay',
        message: activetedUser,
        callback: function(result) {
            // result will be true if button was click, while it will be false if users close the dialog directly.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result">The result.</param>
        if(result) {
            $.post(activeUserURL, { id: dataItem.ID }, function (data) {
                /// <summary>
                /// 
                /// </summary>
                /// <param name="data">The data.</param>
                if (data.success) {
                    var grid = $("#Users").data("kendoGrid");
                    grid.dataSource.read();
                } else {alert(data.message);}
            });
        } else{ return false;
        }
    }
    })   
}