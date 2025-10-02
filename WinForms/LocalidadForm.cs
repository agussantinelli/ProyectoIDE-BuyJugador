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
    public partial class LocalidadForm : BaseForm
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

            // Aplicar estilos
            StyleManager.ApplyDataGridViewStyle(dgvLocalidades);
            StyleManager.ApplyButtonStyle(btnOrdenarAZ);
            StyleManager.ApplyButtonStyle(btnOrdenarZA);
            StyleManager.ApplyButtonStyle(btnVolver);
        }

        private async void LocalidadForm_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Cargar datos de Provincias y Localidades
                _todasLasProvincias = await _provinciaApiClient.GetAllAsync() ?? new List<ProvinciaDTO>();
                _todasLasLocalidades = await _localidadApiClient.GetAllAsync() ?? new List<LocalidadDTO>();

                // 2. Configurar el ComboBox de Provincias
                var provinciasConOpcionTodas = new List<ProvinciaDTO> { new ProvinciaDTO { IdProvincia = 0, Nombre = "Todas las provincias" } };
                provinciasConOpcionTodas.AddRange(_todasLasProvincias.OrderBy(p => p.Nombre));
                cboProvincias.DataSource = provinciasConOpcionTodas;
                cboProvincias.DisplayMember = "Nombre";
                cboProvincias.ValueMember = "IdProvincia";

                // 3. Cargar la grilla con todas las localidades al inicio
                CargarGrilla(_todasLasLocalidades.OrderBy(l => l.Nombre));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos iniciales: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarGrilla(IEnumerable<LocalidadDTO> localidades)
        {
            var vistaLocalidades = localidades.Select(l => new
            {
                l.IdLocalidad,
                l.Nombre,
                NombreProvincia = _todasLasProvincias.FirstOrDefault(p => p.IdProvincia == l.IdProvincia)?.Nombre ?? "N/A"
            }).ToList();

            dgvLocalidades.DataSource = vistaLocalidades;
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            dgvLocalidades.Columns["IdLocalidad"].HeaderText = "ID";
            dgvLocalidades.Columns["IdLocalidad"].Width = 60;
            dgvLocalidades.Columns["Nombre"].HeaderText = "Localidad";
            dgvLocalidades.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLocalidades.Columns["NombreProvincia"].HeaderText = "Provincia";
            dgvLocalidades.Columns["NombreProvincia"].Width = 200;
        }

        private void cboProvincias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboProvincias.SelectedValue is int provinciaId)
            {
                IEnumerable<LocalidadDTO> localidadesFiltradas;
                if (provinciaId == 0) // "Todas las provincias"
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

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

