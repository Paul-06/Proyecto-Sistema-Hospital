let dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {  
    dataTable = $("#tblCargos").DataTable({
        responsive: true,
        ordening: false,
        "ajax": {
            type: "GET",
            url: '/Cargo/GetAll',
            dataType: "json"
        },
        "columns": [
            { "data": "nombre" },
            { "data": "descripcion" },
            {
                "data": "idCargo",
                "render": function (data) {
                    return `
                                <div>
                                    <a href="/Cargo/Upsert/${data}" class="btn btn-primary text-white" style="cursor:pointer">
                                        <i class="fa-regular fa-pen-to-square"></i>
                                    </a>
                                    <a onclick=eliminar("/Cargo/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                        <i class="fa-regular fa-trash-can"></i>
                                    </a>
                                </div>
                            `;
                }, "width": "8%"
            }
        ],
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/2.0.0/i18n/es-ES.json"
        }
    });
}
