function LoadTableViaServerSideProcess(id, url, projectColumn) {
    $('#' + id).DataTable({
        lengthMenu: [[5, 10, 15], [5, 10, 15]],
        processing: true,
        bPaginate: true,
        bServerSide: true,
        sAjaxSource: url,
        fnServerData: function (sSource, aoData, fnCallBack) {
            $.ajax({
                type: "post",
                dataType: "json",
                data: aoData,
                url: sSource,
                success: fnCallBack
            });
        },
        columns: projectColumn
    });
}