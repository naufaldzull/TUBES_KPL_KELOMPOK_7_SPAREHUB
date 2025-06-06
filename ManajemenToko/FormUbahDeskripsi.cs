using ManajemenToko.Controller;
using ManajemenToko.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManajemenToko
{
    public partial class FormUbahDeskripsi: Form
    {
        // Controls
        private DataGridView dgvBarang;
        private TextBox txtDeskripsi;
        private Label lblDeskripsi;
        private Label lblJudul;
        private Label lblPilihBarang;
        private Button btnUpdate;
        private Button btnBatal;
        private Button btnRefresh;

        // Controller dan data
        private readonly BarangController _controller;
        private int _selectedBarangId = 0;
        private Barang _selectedBarang = null;

        public FormUbahDeskripsi()
        {
            InitializeComponent();
            _controller = new BarangController();
            SetupCustomControls();
            LoadDataBarang();
            DisableInputControls();
        }

        private void SetupCustomControls()
        {
            // Initialize controls
            this.dgvBarang = new DataGridView();
            this.txtDeskripsi = new TextBox();
            this.lblDeskripsi = new Label();
            this.lblJudul = new Label();
            this.lblPilihBarang = new Label();
            this.btnUpdate = new Button();
            this.btnBatal = new Button();
            this.btnRefresh = new Button();

            this.SuspendLayout();

            // Form Properties
            this.Text = "Ubah Deskripsi Sparepart";
            this.Size = new Size(900, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.BackColor = Color.White;

            // Label Judul
            this.lblJudul.Text = "UBAH DESKRIPSI SPAREPART";
            this.lblJudul.Font = new Font("Arial", 14, FontStyle.Bold);
            this.lblJudul.ForeColor = Color.DarkBlue;
            this.lblJudul.Location = new Point(300, 20);
            this.lblJudul.Size = new Size(300, 25);
            this.lblJudul.TextAlign = ContentAlignment.MiddleCenter;

            // Label Pilih Barang
            this.lblPilihBarang.Text = "Pilih sparepart yang ingin diubah deskripsinya (klik pada baris):";
            this.lblPilihBarang.Location = new Point(30, 60);
            this.lblPilihBarang.Size = new Size(400, 20);
            this.lblPilihBarang.Font = new Font("Arial", 10);
            this.lblPilihBarang.ForeColor = Color.DarkGreen;

            // DataGridView
            this.dgvBarang.Location = new Point(30, 85);
            this.dgvBarang.Size = new Size(620, 200);
            this.dgvBarang.BackgroundColor = Color.White;
            this.dgvBarang.BorderStyle = BorderStyle.Fixed3D;
            this.dgvBarang.AllowUserToAddRows = false;
            this.dgvBarang.AllowUserToDeleteRows = false;
            this.dgvBarang.ReadOnly = true;
            this.dgvBarang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvBarang.MultiSelect = false;
            this.dgvBarang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBarang.RowHeadersVisible = false;
            this.dgvBarang.Font = new Font("Arial", 9);
            this.dgvBarang.CellClick += DgvBarang_CellClick;

            // Setup DataGridView columns
            SetupDataGridViewColumns();

            // Button Refresh
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Location = new Point(670, 85);
            this.btnRefresh.Size = new Size(80, 30);
            this.btnRefresh.BackColor = Color.LightBlue;
            this.btnRefresh.Font = new Font("Arial", 9);
            this.btnRefresh.Cursor = Cursors.Hand;
            this.btnRefresh.Click += BtnRefresh_Click;

            // === EDIT DESKRIPSI SECTION ===
            int yStart = 310;

            this.lblDeskripsi.Text = "Deskripsi Baru: *";
            this.lblDeskripsi.Location = new Point(50, yStart);
            this.lblDeskripsi.Size = new Size(120, 20);
            this.lblDeskripsi.Font = new Font("Arial", 10, FontStyle.Bold);
            this.lblDeskripsi.ForeColor = Color.DarkRed;

            this.txtDeskripsi.Location = new Point(50, yStart + 25);
            this.txtDeskripsi.Size = new Size(600, 80);
            this.txtDeskripsi.Font = new Font("Arial", 10);
            this.txtDeskripsi.Multiline = true;
            this.txtDeskripsi.ScrollBars = ScrollBars.Vertical;
            this.txtDeskripsi.BorderStyle = BorderStyle.FixedSingle;

            // Buttons
            this.btnUpdate.Text = "Update Deskripsi";
            this.btnUpdate.Location = new Point(450, yStart + 120);
            this.btnUpdate.Size = new Size(120, 35);
            this.btnUpdate.BackColor = Color.LightGreen;
            this.btnUpdate.Font = new Font("Arial", 10, FontStyle.Bold);
            this.btnUpdate.Cursor = Cursors.Hand;
            this.btnUpdate.Click += BtnUpdate_Click;

            this.btnBatal.Text = "Batal";
            this.btnBatal.Location = new Point(580, yStart + 120);
            this.btnBatal.Size = new Size(100, 35);
            this.btnBatal.BackColor = Color.LightCoral;
            this.btnBatal.Font = new Font("Arial", 10, FontStyle.Bold);
            this.btnBatal.Cursor = Cursors.Hand;
            this.btnBatal.Click += BtnBatal_Click;

            // Label info
            Label lblInfo = new Label();
            lblInfo.Text = "* Field wajib diisi";
            lblInfo.Location = new Point(50, yStart + 165);
            lblInfo.Size = new Size(150, 15);
            lblInfo.Font = new Font("Arial", 8, FontStyle.Italic);
            lblInfo.ForeColor = Color.Gray;

            // Add controls to form
            this.Controls.Add(this.lblJudul);
            this.Controls.Add(this.lblPilihBarang);
            this.Controls.Add(this.dgvBarang);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblDeskripsi);
            this.Controls.Add(this.txtDeskripsi);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnBatal);
            this.Controls.Add(lblInfo);

            this.ResumeLayout(false);
        }

        private void SetupDataGridViewColumns()
        {
            dgvBarang.Columns.Clear();

            dgvBarang.Columns.Add("Id", "ID");
            dgvBarang.Columns.Add("Nama", "Nama");
            dgvBarang.Columns.Add("Model", "Model");
            dgvBarang.Columns.Add("Merek", "Merek");
            dgvBarang.Columns.Add("Jenis", "Jenis");
            dgvBarang.Columns.Add("Deskripsi", "Deskripsi Saat Ini");

            dgvBarang.Columns["Id"].Width = 40;
            dgvBarang.Columns["Nama"].Width = 120;
            dgvBarang.Columns["Model"].Width = 80;
            dgvBarang.Columns["Merek"].Width = 80;
            dgvBarang.Columns["Jenis"].Width = 90;
            dgvBarang.Columns["Deskripsi"].Width = 200;

            // Header style
            dgvBarang.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgvBarang.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvBarang.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
            dgvBarang.ColumnHeadersHeight = 25;
        }

        private void LoadDataBarang()
        {
            try
            {
                var result = _controller.GetAllBarang();

                if (result.Success)
                {
                    LoadDataToGrid(result.Data);
                }
                else
                {
                    _controller.ShowErrorMessage(result.Message);
                }
            }
            catch (Exception ex)
            {
                _controller.ShowErrorMessage($"Error loading data: {ex.Message}");
            }
        }

        private void LoadDataToGrid(List<Barang> barangList)
        {
            try
            {
                dgvBarang.Rows.Clear();

                foreach (var barang in barangList)
                {
                    dgvBarang.Rows.Add(
                        barang.Id,
                        barang.Nama,
                        barang.Model ?? "-",
                        barang.Merek ?? "-",
                        barang.Jenis,
                        barang.Deskripsi
                    );
                }
            }
            catch (Exception ex)
            {
                _controller.ShowErrorMessage($"Error displaying data: {ex.Message}");
            }
        }

        private void DisableInputControls()
        {
            txtDeskripsi.Enabled = false;
            btnUpdate.Enabled = false;
            txtDeskripsi.Clear();
        }

        private void EnableInputControls()
        {
            txtDeskripsi.Enabled = true;
            btnUpdate.Enabled = true;
            txtDeskripsi.Focus();
        }

        private void DgvBarang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dgvBarang.Rows[e.RowIndex];
                    int id = Convert.ToInt32(row.Cells["Id"].Value);

                    var result = _controller.GetBarangById(id);

                    if (result.Success)
                    {
                        _selectedBarangId = id;
                        _selectedBarang = result.Data;

                        txtDeskripsi.Text = result.Data.Deskripsi;
                        EnableInputControls();

                        lblPilihBarang.Text = $"Mengubah deskripsi: {result.Data.Nama}";
                        lblPilihBarang.ForeColor = Color.DarkBlue;
                    }
                    else
                    {
                        _controller.ShowErrorMessage(result.Message);
                    }
                }
                catch (Exception ex)
                {
                    _controller.ShowErrorMessage($"Error selecting item: {ex.Message}");
                }
            }
        }



        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedBarangId == 0)
                {
                    _controller.ShowErrorMessage("Pilih sparepart yang ingin diubah deskripsinya!");
                    return;
                }

                string deskripsi = txtDeskripsi.Text.Trim();

                // Validasi deskripsi
                if (string.IsNullOrWhiteSpace(deskripsi))
                {
                    _controller.ShowErrorMessage("Deskripsi tidak boleh kosong!");
                    txtDeskripsi.Focus();
                    return;
                }

                if (deskripsi.Length > 500)
                {
                    _controller.ShowErrorMessage("Deskripsi maksimal 500 karakter!");
                    txtDeskripsi.Focus();
                    return;
                }

                // Cek apakah deskripsi berubah
                if (deskripsi == _selectedBarang.Deskripsi)
                {
                    _controller.ShowErrorMessage("Deskripsi tidak ada perubahan!");
                    return;
                }

                // Konfirmasi update
                bool confirm = _controller.ShowConfirmationDialog(
                    $"Yakin ingin mengubah deskripsi untuk '{_selectedBarang.Nama}'?\n\n" +
                    $"Deskripsi lama:\n{_selectedBarang.Deskripsi}\n\n" +
                    $"Deskripsi baru:\n{deskripsi}",
                    "Konfirmasi Update Deskripsi");

                if (!confirm) return;

                // Update deskripsi
                var result = _controller.UpdateDeskripsi(_selectedBarangId, deskripsi);

                if (result.Success)
                {
                    _controller.ShowSuccessMessage(result.Message);

                    // Refresh data dan reset form
                    LoadDataBarang();
                    DisableInputControls();
                    _selectedBarangId = 0;
                    _selectedBarang = null;

                    lblPilihBarang.Text = "Pilih sparepart yang ingin diubah deskripsinya (klik pada baris):";
                    lblPilihBarang.ForeColor = Color.DarkGreen;
                }
                else
                {
                    _controller.ShowErrorMessage(result.Message);
                }
            }
            catch (Exception ex)
            {
                _controller.ShowErrorMessage($"Error updating: {ex.Message}");
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataBarang();
            DisableInputControls();
            _selectedBarangId = 0;
            _selectedBarang = null;

            lblPilihBarang.Text = "Pilih sparepart yang ingin diubah deskripsinya (klik pada baris):";
            lblPilihBarang.ForeColor = Color.DarkGreen;
        }

        private void BtnBatal_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtDeskripsi.Text) && _selectedBarang != null)
            {
                if (txtDeskripsi.Text.Trim() != _selectedBarang.Deskripsi)
                {
                    bool confirm = _controller.ShowConfirmationDialog(
                        "Ada perubahan yang belum disimpan. Yakin ingin keluar?",
                        "Konfirmasi Keluar");

                    if (!confirm) return;
                }
            }

            this.Close();
        }
    }
}
