(function () {
    const instances = {}; 

    function hasDateAdapter() {
        try {
            return !!(Chart && Chart._adapters && Chart._adapters._date && Chart._adapters._date.formats);
        } catch { return false; }
    }

    function buildDatasets(datasets) {
        return (datasets || []).map(ds => ({
            label: ds.label,
            data: ds.data,      
            spanGaps: true,
            borderWidth: 2,
            pointRadius: 2,
            tension: 0.25
        }));
    }

    function createOrUpdateLineChart(canvasId, labelsIso, datasets) {
        try {
            const ctx = document.getElementById(canvasId);
            if (!ctx) {
                console.warn(`[charts] canvas #${canvasId} no encontrado`);
                return;
            }

            const useTime = hasDateAdapter(); 
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
                    x: useTime
                        ? {
                            type: 'time', 
                            time: {
                                parser: 'yyyy-MM-dd',
                                unit: 'day',
                                tooltipFormat: 'dd/MM/yyyy',
                                displayFormats: { day: 'dd/MM' }
                            },
                            ticks: { autoSkip: true, maxRotation: 0 }
                        }
                        : {
                            type: 'category',    
                            ticks: { autoSkip: true, maxRotation: 0 }
                        },
                    y: {
                        ticks: { callback: v => '$ ' + Number(v).toLocaleString() }
                    }
                }
            };

            if (instances[canvasId]) {
                const chart = instances[canvasId];
                chart.data = data;
                chart.options = options;
                chart.update();
            } else {
                instances[canvasId] = new Chart(ctx, { type: 'line', data, options });
            }

            console.info(`[charts] Render OK (${useTime ? 'time' : 'category'})`);
        } catch (err) {
            console.error('[charts] Error al crear/actualizar el gráfico:', err);
        }
    }

    // expone global
    window.charts = { createOrUpdateLineChart };
})();
