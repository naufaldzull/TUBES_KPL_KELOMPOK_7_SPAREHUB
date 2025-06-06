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
    public partial class FormHapusBarang: Form
    {
        // Controls
        private DataGridView dgvBarang;
        private Label lblJudul;
        private Label lblPilihBarang;
        private Label lblWarning;
        private Label lblDetailBarang;
        private Button btnHapus;
        private Button btnBatal;
        private Button btnRefresh;

        // Controller dan data
        private readonly BarangController _controller;
        private int _selectedBarangId = 0;
        private Barang _selectedBarang = null;

        public FormHapusBarang()
        {
            InitializeComponent();
            _controller = new BarangController();
            SetupCustomControls();
            LoadDataBarang();
            
        }

        private void SetupCustomControls()
        {
            // Initialize controls
            this.dgvBarang = new DataGridView();
            this.lblJudul = new Label();
            this.lblPilihBarang = new Label();
            this.lblWarning = new Label();
            this.lblDetailBarang = new Label();
            this.btnHapus = new Button();
            this.btnBatal = new Button();
            this.btnRefresh = new Button();

            this.SuspendLayout();

            // Form Properties
            this.Text = "Hapus Sparepart";
            this.Size = new Size(900, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.BackColor = Color.White;

            // Label Judul
            this.lblJudul.Text = "HAPUS SPAREPART MOTOR";
            this.lblJudul.Font = new Font("Arial", 14, FontStyle.Bold);
            this.lblJudul.ForeColor = Color.DarkRed;
            this.lblJudul.Location = new Point(300, 20);
            this.lblJudul.Size = new Size(300, 25);
            this.lblJudul.TextAlign = ContentAlignment.MiddleCenter;

            // Label Warning
            this.lblWarning.Text = "PERINGATAN: Data yang dihapus tidak dapat dikembalikan!";
            this.lblWarning.Font = new Font("Arial", 10, FontStyle.Bold);
            this.lblWarning.ForeColor = Color.Red;
            this.lblWarning.Location = new Point(250, 50);
            this.lblWarning.Size = new Size(400, 20);
            this.lblWarning.TextAlign = ContentAlignment.MiddleCenter;

            // Label Pilih Barang
            this.lblPilihBarang.Text = "Pilih sparepart yang ingin dihapus (klik pada baris):";
            this.lblPilihBarang.Location = new Point(30, 85);
            this.lblPilihBarang.Size = new Size(350, 20);
            this.lblPilihBarang.Font = new Font("Arial", 10);
            this.lblPilihBarang.ForeColor = Color.DarkGreen;

            // DataGridView
            this.dgvBarang.Location = new Point(30, 110);
            this.dgvBarang.Size = new Size(680, 280);
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
            this.btnRefresh.Location = new Point(730, 110);
            this.btnRefresh.Size = new Size(80, 30);
            this.btnRefresh.BackColor = Color.LightBlue;
            this.btnRefresh.Font = new Font("Arial", 9);
            this.btnRefresh.Cursor = Cursors.Hand;
            this.btnRefresh.Click += BtnRefresh_Click;


            // Button Hapus
            this.btnHapus.Text = "HAPUS SPAREPART";
            this.btnHapus.Location = new Point(530, 470);
            this.btnHapus.Size = new Size(150, 40);
            this.btnHapus.BackColor = Color.Red;
            this.btnHapus.ForeColor = Color.White;
            this.btnHapus.Font = new Font("Arial", 10, FontStyle.Bold);
            this.btnHapus.Cursor = Cursors.Hand;
            this.btnHapus.Click += BtnHapus_Click;

            // Button Batal
            this.btnBatal.Text = "Batal";
            this.btnBatal.Location = new Point(690, 470);
            this.btnBatal.Size = new Size(100, 40);
            this.btnBatal.BackColor = Color.LightGray;
            this.btnBatal.Font = new Font("Arial", 10, FontStyle.Bold);
            this.btnBatal.Cursor = Cursors.Hand;
            this.btnBatal.Click += BtnBatal_Click;

            // Add controls to form
            this.Controls.Add(this.lblJudul);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.lblPilihBarang);
            this.Controls.Add(this.dgvBarang);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblDetailBarang);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnBatal);

            this.ResumeLayout(false);
        }

        private void SetupDataGridViewColumns()
        {
            dgvBarang.Columns.Clear();

            dgvBarang.Columns.Add("Id", "ID");
            dgvBarang.Columns.Add("Nama", "Nama Sparepart");
            dgvBarang.Columns.Add("Model", "Model");
            dgvBarang.Columns.Add("Merek", "Merek");
            dgvBarang.Columns.Add("Jenis", "Jenis");
            dgvBarang.Columns.Add("Harga", "Harga");
            dgvBarang.Columns.Add("Stok", "Stok");

            // Set column widths
            dgvBarang.Columns["Id"].Width = 40;
            dgvBarang.Columns["Nama"].Width = 140;
            dgvBarang.Columns["Model"].Width = 90;
            dgvBarang.Columns["Merek"].Width = 80;
            dgvBarang.Columns["Jenis"].Width = 90;
            dgvBarang.Columns["Harga"].Width = 100;
            dgvBarang.Columns["Stok"].Width = 60;

            // Format columns
            dgvBarang.Columns["Harga"].DefaultCellStyle.Format = "C0";
            dgvBarang.Columns["Harga"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvBarang.Columns["Stok"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Header style
            dgvBarang.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkRed;
            dgvBarang.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvBarang.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
            dgvBarang.ColumnHeadersHeight = 25;

            // Selected row style
            dgvBarang.DefaultCellStyle.SelectionBackColor = Color.LightCoral;
            dgvBarang.DefaultCellStyle.SelectionForeColor = Color.Black;
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
                        barang.Harga,
                        barang.Stok
                    );
                }

                // Update status
                lblPilihBarang.Text = $"Total {barangList.Count} sparepart tersedia. Pilih yang ingin dihapus:";
            }
            catch (Exception ex)
            {
                _controller.ShowErrorMessage($"Error displaying data: {ex.Message}");
            }
        }

   

        private void EnableDeleteButton()
        {
            btnHapus.Enabled = true;
            btnHapus.BackColor = Color.Red;
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

                        
                        EnableDeleteButton();

                        lblPilihBarang.Text = $"DIPILIH UNTUK DIHAPUS: {result.Data.Nama}";
                        lblPilihBarang.ForeColor = Color.DarkRed;
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

      

        private void BtnHapus_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedBarangId == 0 || _selectedBarang == null)
                {
                    _controller.ShowErrorMessage("Pilih sparepart yang ingin dihapus terlebih dahulu!");
                    return;
                }

                // Konfirmasi ganda untuk safety
                string konfirmasiPesan = $"KONFIRMASI PENGHAPUSAN\n\n" +
                                       $"Anda akan menghapus sparepart:\n" +
                                       $"Nama: {_selectedBarang.Nama}\n" +
                                       $"Model: {(_selectedBarang.Model ?? "Tidak ada")}\n" +
                                       $"Merek: {(_selectedBarang.Merek ?? "Tidak ada")}\n" +
                                       $"Jenis: {_selectedBarang.Jenis}\n" +
                                       $"Harga: Rp{_selectedBarang.Harga:N0}\n" +
                                       $"Stok: {_selectedBarang.Stok}\n\n" +
                                       $"DATA YANG DIHAPUS TIDAK DAPAT DIKEMBALIKAN!\n\n" +
                                       $"Yakin ingin melanjutkan?";

                bool confirm = _controller.ShowConfirmationDialog(konfirmasiPesan, "KONFIRMASI HAPUS");

                if (!confirm) return;


                // Hapus barang
                var result = _controller.HapusBarang(_selectedBarangId);

                if (result.Success)
                {
                    _controller.ShowSuccessMessage($"{result.Message}\n\nSparepart '{_selectedBarang.Nama}' telah dihapus dari sistem.");

                    // Refresh data dan reset
                    LoadDataBarang();
                    
                    _selectedBarangId = 0;
                    _selectedBarang = null;

                    lblPilihBarang.Text = "Pilih sparepart yang ingin dihapus (klik pada baris):";
                    lblPilihBarang.ForeColor = Color.DarkGreen;
                }
                else
                {
                    _controller.ShowErrorMessage($"Gagal menghapus!\n\n{result.Message}");
                }
            }
            catch (Exception ex)
            {
                _controller.ShowErrorMessage($"Error saat menghapus: {ex.Message}");
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataBarang();
            _selectedBarangId = 0;
            _selectedBarang = null;

            lblPilihBarang.Text = "Pilih sparepart yang ingin dihapus (klik pada baris):";
            lblPilihBarang.ForeColor = Color.DarkGreen;
        }

        private void BtnBatal_Click(object sender, EventArgs e)
        {
            // Konfirmasi jika ada item yang dipilih
            if (_selectedBarangId != 0)
            {
                bool confirm = _controller.ShowConfirmationDialog(
                    "Ada sparepart yang dipilih untuk dihapus. Yakin ingin keluar?",
                    "Konfirmasi Keluar");

                if (!confirm) return;
            }

            this.Close();
        }

        // Method untuk get total sparepart (untuk statistik)
        private int GetTotalSparepart()
        {
            return dgvBarang.Rows.Count;
        }

        // Override form closing untuk safety
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_selectedBarangId != 0)
            {
                bool confirm = _controller.ShowConfirmationDialog(
                    "Ada sparepart yang dipilih. Yakin ingin menutup form?",
                    "Konfirmasi Tutup");

                if (!confirm)
                {
                    e.Cancel = true;
                    return;
                }
            }

            base.OnFormClosing(e);
        }
    }
}
