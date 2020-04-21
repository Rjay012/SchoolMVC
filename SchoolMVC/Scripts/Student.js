$(document).ready(function () {
    LoadStudentTable("");
});

function LoadStudentTable(data) {
    alert(JSON.stringify(data));
    var columns = [{ 'data': 'studentID' }, { 'data': 'studentName' }, { 'data': 'standardName' }, { 'data': 'rowVersion' },
        {
            'data': 'studentID', render: function (studentID, type, row) {
                return "<button class='btn btn-success' type='button' data-toggle='modal' data-target='#myModalViewAddress' onclick='ViewAddress(" + studentID + ")'>VIEW ADDRESS</button>";
            }
        },
        {
            'data': 'studentID', render: function (studentID, type, row) {
                return "<button class='btn btn-primary' type='button' data-toggle='modal' data-target='#myModalEditCustomer' onclick='ViewDetails(" + studentID + ")'>EDIT</button>";
            }
        },
        {
            'data': 'studentID', render: function (studentID, type, row) {
                return "<button class='btn btn-danger' type='button' onclick='Remove(" + studentID + ")'>REMOVE</button>";
            }
        }];
    LoadTableViaServerSideProcess("StudentTable", "/Student/Table", columns);
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