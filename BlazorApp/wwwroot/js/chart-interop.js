window.charts = (function () {
    const instances = {}; 

    function buildDatasets(datasets) {
        return datasets.map(ds => ({
            label: ds.label,
            data: ds.data,                 
            spanGaps: false,
            borderWidth: 2,
            pointRadius: 2,
            tension: 0.25
        }));
    }

    function createOrUpdateLineChart(canvasId, labelsIso, datasets) {
        const ctx = document.getElementById(canvasId);
        if (!ctx) return;

        const data = {
            labels: labelsIso,
            datasets: buildDatasets(datasets)
        };

        const options = {
            responsive: true,
            maintainAspectRatio: false,
            interaction: { mode: 'nearest', axis: 'x', intersect: false },
            plugins: {
                legend: { position: 'top' },
                tooltip: {
                    callbacks: {
                        title: items => {
                            if (!items?.length) return "";
                            const s = items[0].label;
                            const d = new Date(s + "T00:00:00");
                            return d.toLocaleDateString();
                        },
                        label: ctx => {
                            const v = ctx.parsed.y;
                            if (v == null) return "(sin dato)";
                            return ` $ ${v.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`;
                        }
                    }
                }
            },
            scales: {
                x: {
                    type: 'time',
                    time: {
                        parser: 'yyyy-MM-dd',
                        unit: 'day',
                        tooltipFormat: 'dd/MM/yyyy',
                        displayFormats: { day: 'dd/MM' }
                    },
                    ticks: { autoSkip: true, maxRotation: 0 }
                },
                y: {
                    ticks: {
                        callback: (value) => '$ ' + Number(value).toLocaleString()
                    }
                }
            }
        };


        if (instances[canvasId]) {
            instances[canvasId].data = data;
            instances[canvasId].options = options;
            instances[canvasId].update();
        } else {
            instances[canvasId] = new Chart(ctx, {
                type: 'line',
                data,
                options
            });
        }
    }

    return {
        createOrUpdateLineChart
    };
})();
