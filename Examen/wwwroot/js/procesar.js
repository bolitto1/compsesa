$(document).ready(function () {
    $('#filterButton').on('click', async function () {
        const modo = $('#modo').val();
        const fechaDesde = $('#fechaDesde').val();
        const fechaHasta = $('#fechaHasta').val();

        const response = await $.ajax({
            type: 'GET',
            url: '/Dashboard/GetData',
            data: { modo, fechaDesde, fechaHasta }
        });

        drawChart('radiacionChart', response.device_data.find(d => d.codigo_parametro == "18054"));
        drawChart('temperaturaChart', response.device_data.find(d => d.codigo_parametro == "18001"));
        drawChart('humedadChart', response.device_data.find(d => d.codigo_parametro == "18036"));
    });
});

function drawChart(canvasId, data) {
    const ctx = document.getElementById(canvasId).getContext('2d');
    new Chart(ctx, {
        type: 'line',
        data: {
            labels: data.values.avg_data.map((_, i) => data.values.avg_data[i].toString()),
            datasets: [
                {
                    label: 'Promedio',
                    data: data.values.avg_data,
                    borderColor: 'rgba(75, 192, 192, 1)',
                    fill: false
                },
                {
                    label: 'Mínimo',
                    data: data.values.min_data,
                    borderColor: 'rgba(255, 99, 132, 1)',
                    fill: false
                },
                {
                    label: 'Máximo',
                    data: data.values.max_data,
                    borderColor: 'rgba(255, 206, 86, 1)',
                    fill: false
                }
            ]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}