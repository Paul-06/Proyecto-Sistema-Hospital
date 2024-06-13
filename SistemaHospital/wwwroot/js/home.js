$(function () {
  fetchDataAndCreateChart("/Especialidad/ObtenerNroEmpleadosxEspecialidad", crearGraficoEspecialidad);
  fetchDataAndCreateChart("/Paciente/ObtenerDistribucionxGrupoEtario", crearGraficoPaciente);
  console.log("Hola :D");
});

function fetchDataAndCreateChart(url, createChartCallback) {
  $.ajax({
    url: url,
    type: "GET",
    dataType: "json",
    success: function (data) {
      if (data.data != null) {
        createChartCallback(data.data);
      }
    },
    error: function (jqXHR, textStatus, errorThrown) {
      console.error(`Error fetching data from ${url}:`, textStatus, errorThrown);
    }
  });
}

function crearGraficoEspecialidad(json) {
  const labels = json.map((e) => e.especialidad);
  const data = json.map((e) => e.nroEmpleados);
  const { backgroundColors, borderColors } = generateColors(json.length);

  const ctx1 = document.getElementById("graficoEspecialidad");

  new Chart(ctx1, {
    type: "bar",
    data: {
      labels: labels,
      datasets: [
        {
          label: "# de Empleados",
          data: data,
          backgroundColor: backgroundColors,
          borderColor: borderColors,
          borderWidth: 1,
        },
      ],
    },
    options: {
      scales: {
        y: {
          beginAtZero: true,
        },
      },
    },
  });
}

function crearGraficoPaciente(json) {
  const labels = json.map((e) => e.grupoEdad);
  const data = json.map((e) => e.nroPacientes);
  const { backgroundColors, borderColors } = generateColors(json.length);

  const ctx2 = document.getElementById("graficoPaciente");

  new Chart(ctx2, {
    type: "pie",
    data: {
      labels: labels,
      datasets: [
        {
          label: "# de Pacientes",
          data: data,
          backgroundColor: backgroundColors,
          borderColor: borderColors,
          borderWidth: 1,
        },
      ],
    },
  });
}

function generateColors(count) {
  const backgroundColors = [];
  const borderColors = [];
  for (let i = 0; i < count; i++) {
    const colorBase = `${getRandomColor(255, 0)}, ${getRandomColor(255, 0)}, ${getRandomColor(255, 0)}`;
    backgroundColors.push(`rgba(${colorBase}, 0.5)`);
    borderColors.push(`rgb(${colorBase})`);
  }
  return { backgroundColors, borderColors };
}

function getRandomColor(max, min) {
  return Math.floor(Math.random() * (max - min) + min);
}
