$(document).ready(function () {
    LoadStudentTable("");
});

function LoadStudentTable(searchString) {
    FetchData("/Student/Table", { searchString: searchString })
        .done(function (result) {
            $("#TableForm").html(result);
        });
}

function ViewAddress(addressID) {
    FetchData("/Student/AddressModal", { addressID: addressID })
        .done(function (modalContent) {
            $("#ViewAddressModalContent").html(modalContent);
        }); 
}

function ViewDetails(studentID) {
    FetchData("/Student/UpdateDetails", { studentID: studentID })
        .done(function (modalContent) {
            $("#UpdateModalContent").html(modalContent);
        });
}

function Remove(studentID) {
    if (confirm("Sure to delete?") == true) {
        FetchData("/Student/Delete", { studentID: studentID })
            .done(function () {
                LoadStudentTable("");
            });
    }
}

/** EVENTS **/
$(document).on("click", "#btnToggleAddCustomer", function () {
    FetchData("/Student/NewStudentModal", "")
        .done(function (modalContent) {
            $("#NewCustomerModalContent").html(modalContent);
        });
});

$(document).on("click", "#BtnSaveNewStudent", function () {
    if (confirm("Save new Student?") == true) {
        $("#BtnConfirmSaveNewStudent").click();
    }
});

$(document).on("click", "#BtnSaveUpdate", function () {
    if (confirm("Edit Student?") == true) {
        $("#BtnConfirmSaveUpdate").click();
    }
});

$(document).on("keyup", "#txtSearch", function () {
    LoadStudentTable($(this).val());
});