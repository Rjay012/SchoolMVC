$(document).ready(function () {
    LoadStandardTable("");
});

function LoadStandardTable(searchString) {
    FetchData("/Standard/Table", { searchString: searchString })
        .done(function (table) {
            $("#StandardTableForm").html(table);
        });
}

function EditDetails(standardID) {
    FetchData("/Standard/Details", { standardID: standardID })
        .done(function (modalContent) {
            $("#EditModalContent").html(modalContent);
        });
}

function Remove(standardID) {
    if (confirm("Sure you want to remove this standard? Related data could be deleted if you continue") == true) {
        FetchData("/Standard/Delete", { standardID: standardID })
            .done(function (result) {
                LoadStandardTable("");
            });
    }
}

/** EVENTS **/
$(document).on("keyup", "#txtSearch", function () {
    LoadStandardTable($(this).val());
});

$(document).on("click", "#BtnToggleAddStandard", function () {
    FetchData("/Standard/NewStandardModal", "")
        .done(function (modalContent) {
            $("#NewStandardModalContent").html(modalContent);
        });
});

$(document).on("click", "#BtnSaveNewStandard", function () {
    if (confirm("Sure you want to add this new standard?") == true) {
        $("#BtnConfirmSaveNewStandard").submit();
    }
});

$(document).on("click", "#BtnEditStandard", function () {
    if (confirm("Sure you wan to edit this standard?") == true) {
        $("#BtnConfirmEditStandard").submit();
    }
});