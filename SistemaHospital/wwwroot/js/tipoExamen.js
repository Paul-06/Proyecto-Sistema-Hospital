let dataTable;

// Cargar la tabla cuando el
// document esté listo
$(function () {
    loadDataTable();
});

function loadDataTable() {  
    dataTable = $("#tblTiposExamen").DataTable({
        responsive: true,
        ordening: false,
        "ajax": {
            type: "GET",
            url: '/TipoExamen/ListarTiposExamen',
            dataType: "json"
        },
        "columns": [
            { "data": "nombre" },
            { "data": "descripcion" },
            {
                "data": "idTipoExamen",
                "render": function (data) {
                    return `
                                <div>
                                    <a href="/TipoExamen/Upsert/${data}" class="btn btn-primary text-white" style="cursor:pointer">
                                        <i class="fa-regular fa-pen-to-square"></i>
                                    </a>
                                    <a onclick=eliminar("/TipoExamen/Eliminar/${data}") class="btn btn-danger text-white" style="cursor:pointer">
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

function eliminar(url) {
    // SweetAlert
    swal({
        title: "¿Está seguro de eliminar el tipo de examen?",
        text: "Este registro no se podrá recuperar.",
        icon: "warning",
        buttons: ["Cancelar", "Aceptar"],
        dangerMode: true
    }).then((borrar) => {
        if (borrar) {
            // Mediante AJAX
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        // Toastr JS
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
