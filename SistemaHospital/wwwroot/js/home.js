$(function () {
    obtenerDatosGraficoEspecialidad();
    console.log("Hola :D");
});

function obtenerDatosGraficoEspecialidad() {
    $.ajax({
        url: '/Especialidad/ObtenerNroEmpleadosxEspecialidad',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            if (data.data != null) {
                console.log(data.data);
                crearGraficoEspecialidad(data.data);
            }
        }
    })
}

function crearGraficoEspecialidad(json) {
    const especialidades = json.map(e => e.especialidad);
    const nroEmpleados = json.map(e => e.nroEmpleados);
    const backgroundColors = [];
    const borderColors = [];
    
    // Generar colores para el fondo y los bordes
    for (let i = 0; i < json.length; i++) {
        const colorBase = `${getRandomColor(255, 0)}, ${getRandomColor(255, 0)}, ${getRandomColor(255, 0)}`;
        backgroundColors.push(`rgba(${colorBase}, 0.5)`);
        borderColors.push(`rgb(${colorBase})`);
    }
    
    const ctx = document.getElementById('graficoEspecialidad');

    new Chart(ctx, {
        type: 'bar',
        data: {
          labels: especialidades,
          datasets: [{
            label: '# de Empleados',
            data: nroEmpleados,
            backgroundColor: backgroundColors,
            borderColor: borderColors,
            borderWidth: 1
          }]
        },
        options: {
          scales: {
            y: {
              beginAtZero: true
            }
          }
        }
      });
}

function getRandomColor(max, min) {
    return Math.random() * (max - min) + min;
}
