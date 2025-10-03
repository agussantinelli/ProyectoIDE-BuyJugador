using System.Drawing;
using System.Windows.Forms;

namespace WinForms
{
    public static class StyleManager
    {
        public static void ApplyDataGridViewStyle(DataGridView dgv)
        {
            var backgroundColor = Color.FromArgb(45, 55, 70);
            var headerColor = Color.FromArgb(26, 32, 40);
            var rowColor = Color.FromArgb(49, 66, 82);
            var alternateRowColor = Color.FromArgb(45, 55, 70);
            var textColor = Color.White;
            var gridColor = Color.FromArgb(60, 70, 85);
            var selectionColor = Color.FromArgb(0, 80, 200);

            dgv.BackgroundColor = backgroundColor;
            dgv.BorderStyle = BorderStyle.None;
            dgv.GridColor = gridColor;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = headerColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = textColor;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(4);
            dgv.EnableHeadersVisualStyles = false;
            dgv.DefaultCellStyle.BackColor = rowColor;
            dgv.DefaultCellStyle.ForeColor = textColor;
            dgv.DefaultCellStyle.Font = new Font("Century Gothic", 9.5F);
            dgv.DefaultCellStyle.SelectionBackColor = selectionColor;
            dgv.DefaultCellStyle.SelectionForeColor = textColor;
            dgv.DefaultCellStyle.Padding = new Padding(4);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = alternateRowColor;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.RowTemplate.Height = 32;
            dgv.ColumnHeadersHeight = 40;
        }

        public static void ApplyButtonStyle(Button btn)
        {
            btn.BackColor = Color.FromArgb(26, 32, 40);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.FromArgb(0, 80, 200);
        }
    }
}
