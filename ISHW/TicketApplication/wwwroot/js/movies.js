$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/Movie/GetAll' },
        "columns":
            [
                { data: "name" },
                { data: "releaseYear" },
                { data: "ticketPrice" },
                { data: "category.name" },
                {
                    data: "id",
                    "render": function (data) {
                        return `<div class="btn-group">
                            <a href="/movie/edit/id=${data}" class="btn btn-outline-primary mx-1"> <i class="bi bi-pencil"></i> Edit</a>    
                            <a href="/movie/delete/${data}" class="btn btn-outline-danger mx-1"> <i class="bi bi-trash-fill"></i> Delete</a>    

                       </div > `
                    }
                }

            ]
    });
}

