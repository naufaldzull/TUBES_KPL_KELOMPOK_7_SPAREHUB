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
    public partial class FormLihatBarang : Form
    {
        // Controls
        private DataGridView dgvBarang;
        private Button btnRefresh;
        private Button btnTutup;
        private Label lblJudul;
        private Label lblTotal;
        private TextBox txtCari;
        private Button btnCari;
        private Label lblCari;

        // Controller
        private readonly BarangController _controller;

        public FormLihatBarang()
        {
            InitializeComponent();
            _controller = new BarangController();
            SetupCustomControls();
            LoadData(); // Load data pas form dibuka
        }

        private void SetupCustomControls()
        {
            // Initialize controls
            this.dgvBarang = new DataGridView();
            this.btnRefresh = new Button();
            this.btnTutup = new Button();
            this.lblJudul = new Label();
            this.lblTotal = new Label();
            this.txtCari = new TextBox();
            this.btnCari = new Button();
            this.lblCari = new Label();

            this.SuspendLayout();

            // Form Properties
            this.Text = "Lihat Semua Sparepart";
            this.Size = new Size(900, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.BackColor = Color.White;

            // Label Judul
            this.lblJudul.Text = "DAFTAR SEMUA SPAREPART MOTOR";
            this.lblJudul.Font = new Font("Arial", 14, FontStyle.Bold);
            this.lblJudul.ForeColor = Color.DarkBlue;
            this.lblJudul.Location = new Point(280, 20);
            this.lblJudul.Size = new Size(340, 25);
            this.lblJudul.TextAlign = ContentAlignment.MiddleCenter;

            // Label Cari
            this.lblCari.Text = "Cari Sparepart:";
            this.lblCari.Location = new Point(30, 60);
            this.lblCari.Size = new Size(90, 20);
            this.lblCari.Font = new Font("Arial", 10);

            // TextBox Cari
            this.txtCari.Location = new Point(130, 58);
            this.txtCari.Size = new Size(200, 22);
            this.txtCari.Font = new Font("Arial", 10);
            this.txtCari.KeyPress += TxtCari_KeyPress; // Enter untuk search

            // Button Cari
            this.btnCari.Text = "Cari";
            this.btnCari.Location = new Point(340, 57);
            this.btnCari.Size = new Size(60, 25);
            this.btnCari.BackColor = Color.LightBlue;
            this.btnCari.Font = new Font("Arial", 9);
            this.btnCari.Cursor = Cursors.Hand;
            this.btnCari.Click += BtnCari_Click;

            // DataGridView
            this.dgvBarang.Location = new Point(30, 90);
            this.dgvBarang.Size = new Size(830, 350);
            this.dgvBarang.BackgroundColor = Color.White;
            this.dgvBarang.BorderStyle = BorderStyle.Fixed3D;
            this.dgvBarang.AllowUserToAddRows = false;
            this.dgvBarang.AllowUserToDeleteRows = false;
            this.dgvBarang.ReadOnly = true;
            this.dgvBarang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvBarang.MultiSelect = false;
            this.dgvBarang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBarang.RowHeadersVisible = false;
            this.dgvBarang.Font = new Font("Arial", 10);
            this.dgvBarang.CellClick += DgvBarang_CellClick;
            this.dgvBarang.CellDoubleClick += DgvBarang_CellDoubleClick;

            // Setup DataGridView columns
            SetupDataGridViewColumns();

            // Label Total
            this.lblTotal.Text = "Total Barang: 0";
            this.lblTotal.Location = new Point(30, 450);
            this.lblTotal.Size = new Size(200, 20);
            this.lblTotal.Font = new Font("Arial", 10, FontStyle.Bold);
            this.lblTotal.ForeColor = Color.DarkGreen;

            // Button Refresh
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Location = new Point(650, 470);
            this.btnRefresh.Size = new Size(100, 35);
            this.btnRefresh.BackColor = Color.LightBlue;
            this.btnRefresh.Font = new Font("Arial", 10, FontStyle.Bold);
            this.btnRefresh.Cursor = Cursors.Hand;
            this.btnRefresh.Click += BtnRefresh_Click;

            // Button Tutup
            this.btnTutup.Text = "Tutup";
            this.btnTutup.Location = new Point(760, 470);
            this.btnTutup.Size = new Size(100, 35);
            this.btnTutup.BackColor = Color.LightCoral;
            this.btnTutup.Font = new Font("Arial", 10, FontStyle.Bold);
            this.btnTutup.Cursor = Cursors.Hand;
            this.btnTutup.Click += BtnTutup_Click;

            // Add controls to form
            this.Controls.Add(this.lblJudul);
            this.Controls.Add(this.lblCari);
            this.Controls.Add(this.txtCari);
            this.Controls.Add(this.btnCari);
            this.Controls.Add(this.dgvBarang);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnTutup);

            this.ResumeLayout(false);
        }

        private void SetupDataGridViewColumns()
        {
            // Clear existing columns
            dgvBarang.Columns.Clear();

            // Add columns
            dgvBarang.Columns.Add("Id", "ID");
            dgvBarang.Columns.Add("Nama", "Nama Barang");
            dgvBarang.Columns.Add("Model", "Model");
            dgvBarang.Columns.Add("Merek", "Merek");
            dgvBarang.Columns.Add("Jenis", "Jenis");
            dgvBarang.Columns.Add("Deskripsi", "Deskripsi");
            dgvBarang.Columns.Add("Harga", "Harga");
            dgvBarang.Columns.Add("Stok", "Stok");

            // Set column widths
            dgvBarang.Columns["Id"].Width = 40;
            dgvBarang.Columns["Nama"].Width = 140;
            dgvBarang.Columns["Model"].Width = 100;
            dgvBarang.Columns["Merek"].Width = 80;
            dgvBarang.Columns["Jenis"].Width = 100;
            dgvBarang.Columns["Deskripsi"].Width = 160;
            dgvBarang.Columns["Harga"].Width = 100;
            dgvBarang.Columns["Stok"].Width = 60;

            // Format harga column
            dgvBarang.Columns["Harga"].DefaultCellStyle.Format = "C0"; // Currency format
            dgvBarang.Columns["Harga"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Format stok column
            dgvBarang.Columns["Stok"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Format jenis column
            dgvBarang.Columns["Jenis"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBarang.Columns["Jenis"].DefaultCellStyle.BackColor = Color.LightBlue;

            // Header style
            dgvBarang.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgvBarang.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvBarang.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
            dgvBarang.ColumnHeadersHeight = 30;
        }

        private void LoadData()
        {
            try
            {
                // Ambil data dari controller
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
                // Clear existing rows
                dgvBarang.Rows.Clear();

                // Add data to DataGridView
                foreach (var barang in barangList)
                {
                    dgvBarang.Rows.Add(
                        barang.Id,
                        barang.Nama,
                        barang.Model ?? "-",
                        barang.Merek ?? "-",
                        barang.Jenis,
                        barang.Deskripsi,
                        barang.Harga,
                        barang.Stok
                    );
                }

                // Update total count
                lblTotal.Text = $"Total Barang: {barangList.Count}";

                // Show statistics
                ShowStatistics();
            }
            catch (Exception ex)
            {
                _controller.ShowErrorMessage($"Error displaying data: {ex.Message}");
            }
        }

        private void ShowStatistics()
        {
            try
            {
                var stats = _controller.GetStatistik();
                if (stats.Success)
                {
                    lblTotal.Text = $"Total: {stats.TotalBarang} barang | " +
                                   $"Nilai: Rp{stats.TotalNilai:N0} | " +
                                   $"Stok: {stats.TotalStok} unit";
                }
            }
            catch (Exception ex)
            {
                // Just use simple count if statistics fail
                lblTotal.Text = $"Total Barang: {dgvBarang.Rows.Count}";
            }
        }

        // Event handlers
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                txtCari.Clear(); // Clear search box
                _controller.ShowSuccessMessage("Data berhasil di-refresh!", "Info");
            }
            catch (Exception ex)
            {
                _controller.ShowErrorMessage($"Error refresh: {ex.Message}");
            }
        }

        private void BtnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnCari_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void TxtCari_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Search when Enter is pressed
            if (e.KeyChar == (char)Keys.Enter)
            {
                PerformSearch();
                e.Handled = true;
            }
        }

        private void PerformSearch()
        {
            try
            {
                string keyword = txtCari.Text.Trim();

                var result = _controller.CariBarang(keyword);

                if (result.Success)
                {
                    LoadDataToGrid(result.Data);

                    if (string.IsNullOrWhiteSpace(keyword))
                    {
                        this.Text = "Lihat Semua Barang";
                    }
                    else
                    {
                        this.Text = $"Lihat Semua Barang - Hasil Pencarian: {keyword}";
                    }
                }
                else
                {
                    _controller.ShowErrorMessage(result.Message);
                }
            }
            catch (Exception ex)
            {
                _controller.ShowErrorMessage($"Error searching: {ex.Message}");
            }
        }

        private void DgvBarang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle when user clicks on a row
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvBarang.Rows[e.RowIndex];

                string nama = row.Cells["Nama"].Value?.ToString() ?? "";
                string harga = row.Cells["Harga"].Value?.ToString() ?? "";

                // Update title with selected item
                this.Text = $"Lihat Semua Sparepart - Dipilih: {nama}";
            }
        }

        private void DgvBarang_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle double click untuk show detail
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvBarang.Rows[e.RowIndex];

                string nama = row.Cells["Nama"].Value?.ToString() ?? "";
                string model = row.Cells["Model"].Value?.ToString() ?? "";
                string merek = row.Cells["Merek"].Value?.ToString() ?? "";
                string jenis = row.Cells["Jenis"].Value?.ToString() ?? "";
                string deskripsi = row.Cells["Deskripsi"].Value?.ToString() ?? "";
                string harga = row.Cells["Harga"].Value?.ToString() ?? "";
                string stok = row.Cells["Stok"].Value?.ToString() ?? "";

                string detail = $"Nama: {nama}\n" +
                               $"Model: {(model == "-" ? "Tidak ada" : model)}\n" +
                               $"Merek: {(merek == "-" ? "Tidak ada" : merek)}\n" +
                               $"Jenis: {jenis}\n" +
                               $"Deskripsi: {deskripsi}\n" +
                               $"Harga: {harga}\n" +
                               $"Stok: {stok}";

                MessageBox.Show(detail, "Detail Sparepart", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Method untuk mendapatkan barang yang dipilih
        public Barang GetSelectedBarang()
        {
            if (dgvBarang.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvBarang.SelectedRows[0];

                int id = Convert.ToInt32(row.Cells["Id"].Value);
                var result = _controller.GetBarangById(id);

                if (result.Success)
                {
                    return result.Data;
                }
            }
            return null;
        }

        // Method untuk refresh setelah ada perubahan data
        public void RefreshData()
        {
            LoadData();
        }


        private void LihatBarang_Load(object sender, EventArgs e)
        {

        }
    }
}
