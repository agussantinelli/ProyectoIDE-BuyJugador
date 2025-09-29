using ApiClient;
using DTOs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WinForms
{
    public partial class LocalidadForm : Form
    {
        private readonly LocalidadApiClient _localidadApiClient;
        private readonly ProvinciaApiClient _provinciaApiClient;

        // Listas para almacenar los datos cargados desde la API
        private List<LocalidadDTO> _todasLasLocalidades;
        private List<ProvinciaDTO> _todasLasProvincias;

        public LocalidadForm(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            // Obtenemos las instancias de los ApiClients a través del proveedor de servicios
            _localidadApiClient = serviceProvider.GetRequiredService<LocalidadApiClient>();
            _provinciaApiClient = serviceProvider.GetRequiredService<ProvinciaApiClient>();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private async void LocalidadForm_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Cargar datos de Provincias y Localidades
                _todasLasProvincias = await _provinciaApiClient.GetAllAsync() ?? new List<ProvinciaDTO>();
                _todasLasLocalidades = await _localidadApiClient.GetAllAsync() ?? new List<LocalidadDTO>();

                // 2. Configurar el ComboBox de Provincias para el filtro
                CargarComboProvincias();

                // 3. Configurar y cargar el DataGridView con todas las localidades
                ConfigurarGrilla();
                CargarGrilla(_todasLasLocalidades);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos iniciales: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarComboProvincias()
        {
            // Creamos una lista temporal que incluya la opción "Todas" al principio
            var provinciasParaFiltro = new List<ProvinciaDTO>
            {
                new ProvinciaDTO { IdProvincia = 0, Nombre = "Todas" }
            };
            provinciasParaFiltro.AddRange(_todasLasProvincias.OrderBy(p => p.Nombre));

            cboProvincias.DataSource = provinciasParaFiltro;
            cboProvincias.DisplayMember = "Nombre";
            cboProvincias.ValueMember = "IdProvincia";
        }

        private void ConfigurarGrilla()
        {
            dgvLocalidades.AutoGenerateColumns = false;
            dgvLocalidades.Columns.Clear();

            // Añadimos las columnas manualmente como en tu ProvinciaForm
            dgvLocalidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdLocalidad",
                DataPropertyName = "IdLocalidad",
                HeaderText = "Código",
                Width = 100
            });
            dgvLocalidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                DataPropertyName = "Nombre",
                HeaderText = "Localidad",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgvLocalidades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Provincia",
                HeaderText = "Provincia",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }

        private void CargarGrilla(IEnumerable<LocalidadDTO> localidades)
        {
            dgvLocalidades.Rows.Clear(); // Limpiamos la grilla antes de cargar nuevos datos

            foreach (var localidad in localidades)
            {
                // Buscamos el nombre de la provincia usando el ProvinciaId de la localidad
                string nombreProvincia = _todasLasProvincias
                                            .FirstOrDefault(p => p.IdProvincia == localidad.IdProvincia)?
                                            .Nombre ?? "N/D";

                // Agregamos la fila a la grilla
                dgvLocalidades.Rows.Add(localidad.IdLocalidad, localidad.Nombre, nombreProvincia);
            }
        }

        private void cboProvincias_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Se ejecuta cada vez que el usuario cambia la selección del ComboBox
            if (cboProvincias.SelectedValue is int provinciaId)
            {
                IEnumerable<LocalidadDTO> localidadesFiltradas;

                if (provinciaId == 0) // Si se selecciona "Todas"
                {
                    localidadesFiltradas = _todasLasLocalidades;
                }
                else
                {
                    localidadesFiltradas = _todasLasLocalidades.Where(l => l.IdProvincia == provinciaId);
                }

                CargarGrilla(localidadesFiltradas.OrderBy(l => l.Nombre)); // Cargamos y ordenamos por defecto A-Z
            }
        }

        private void btnOrdenar_Click(object sender, EventArgs e)
        {
            // Este método único maneja ambos ordenamientos
            if (!(cboProvincias.SelectedValue is int provinciaId)) return;

            // 1. Obtenemos la lista de localidades actualmente filtrada
            var localidadesParaOrdenar = (provinciaId == 0)
                ? _todasLasLocalidades
                : _todasLasLocalidades.Where(l => l.IdProvincia == provinciaId);

            // 2. Determinamos el orden basado en qué botón se presionó
            IEnumerable<LocalidadDTO> localidadesOrdenadas;
            if (sender == btnOrdenarAZ)
            {
                localidadesOrdenadas = localidadesParaOrdenar.OrderBy(l => l.Nombre);
            }
            else // (sender == btnOrdenarZA)
            {
                localidadesOrdenadas = localidadesParaOrdenar.OrderByDescending(l => l.Nombre);
            }

            // 3. Recargamos la grilla con los datos ordenados
            CargarGrilla(localidadesOrdenadas);
        }

        private void btnVolver_Click(object sender, EventArgs e) => this.Close();
    }
}

