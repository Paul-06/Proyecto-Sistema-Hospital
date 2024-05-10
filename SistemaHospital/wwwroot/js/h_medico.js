let dataTable;

// Cargar la tabla cuando el
// document esté listo
$(function () {
    loadDataTable();
});

function loadDataTable() {  
    dataTable = $("#tblHistoriales").DataTable({
        responsive: true,
        ordening: false,
        "ajax": {
            type: "GET",
            url: '/HMedico/ListarHistorialesMedicos',
            dataType: "json"
        },
        "columns": [
            { "data": "idHistorialMedico" },
            { "data": "paciente" },
            { "data": "fechaNacimiento" },
            { "data": "cantidadExamen" },
            { "data": "cantidadTratamiento" },
            {
                "data": "idPaciente",
                "render": function (data) {
                    return `
                                <div>
                                    <a href="/HMedico/DetalleHMedico?pacienteId=${data}" class="btn btn-warning text-black" style="cursor:pointer">
                                        <i class="fa-regular fa-eye"></i>
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
