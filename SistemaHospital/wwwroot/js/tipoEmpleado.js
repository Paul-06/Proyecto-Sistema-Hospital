let dataTable;

// Cargar la tabla cuando el
// document esté listo
$(function () {
    loadDataTable();
});

function loadDataTable() {  
    dataTable = $("#tblTiposEmpleado").DataTable({
        responsive: true,
        ordening: false,
        "ajax": {
            type: "GET",
            url: '/TipoEmpleado/ListarTiposEmpleado',
            dataType: "json"
        },
        "columns": [
            { "data": "nombre" },
            { "data": "descripcion" },
            {
                "data": "idTipoEmpleado",
                "render": function (data) {
                    return `
                                <div>
                                    <a href="/TipoEmpleado/Upsert/${data}" class="btn btn-primary text-white" style="cursor:pointer">
                                        <i class="fa-regular fa-pen-to-square"></i>
                                    </a>
                                    <a onclick=eliminar("/TipoEmpleado/Eliminar/${data}") class="btn btn-danger text-white" style="cursor:pointer">
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
        title: "¿Está seguro de eliminar el tipo de empleado?",
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