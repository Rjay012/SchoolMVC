$(document).ready(function () {
    LoadTeacherTable("");
});

function LoadTeacherTable(searchString) {
    FetchData("/Teacher/Table", { searchString: searchString }).done(function (table) {
        $("#TeacherTableForm").html(table);
    });
}