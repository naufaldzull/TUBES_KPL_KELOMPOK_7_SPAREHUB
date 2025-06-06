using ManajemenToko.Controller;
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
    public partial class FormTambahBarang: Form
    {
        // Controls
        private TextBox txtNama;
        private TextBox txtDeskripsi;
        private TextBox txtHarga;
        private TextBox txtStok;
        private TextBox txtModel;
        private TextBox txtMerek;
        private ComboBox cmbJenis;
        private Label lblNama;
        private Label lblDeskripsi;
        private Label lblHarga;
        private Label lblStok;
        private Label lblModel;
        private Label lblMerek;
        private Label lblJenis;
        private Label lblJudul;
        private Button btnSimpan;
        private Button btnBatal;

        // Controller
        private readonly BarangController _controller;

        public FormTambahBarang()
        {
            InitializeComponent();
            _controller = new BarangController();
            SetupCustomControls();
            LoadJenisData();
        }

        private void SetupCustomControls()
        {
            // Initialize controls
            this.txtNama = new TextBox();
            this.txtDeskripsi = new TextBox();
            this.txtHarga = new TextBox();
            this.txtStok = new TextBox();
            this.txtModel = new TextBox();
            this.txtMerek = new TextBox();
            this.cmbJenis = new ComboBox();
            this.lblNama = new Label();
            this.lblDeskripsi = new Label();
            this.lblHarga = new Label();
            this.lblStok = new Label();
            this.lblModel = new Label();
            this.lblMerek = new Label();
            this.lblJenis = new Label();
            this.lblJudul = new Label();
            this.btnSimpan = new Button();
            this.btnBatal = new Button();

            this.SuspendLayout();

            // Form Properties
            this.Text = "Tambah Sparepart Motor";
            this.Size = new Size(500, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.White;

            // Label Judul
            this.lblJudul.Text = "TAMBAH SPAREPART MOTOR";
            this.lblJudul.Font = new Font("Arial", 14, FontStyle.Bold);
            this.lblJudul.ForeColor = Color.DarkBlue;
            this.lblJudul.Location = new Point(120, 20);
            this.lblJudul.Size = new Size(260, 25);
            this.lblJudul.TextAlign = ContentAlignment.MiddleCenter;

            // Label & TextBox Nama
            this.lblNama.Text = "Nama Sparepart: *";
            this.lblNama.Location = new Point(50, 70);
            this.lblNama.Size = new Size(120, 20);
            this.lblNama.Font = new Font("Arial", 10);
            this.lblNama.ForeColor = Color.Black;

            this.txtNama.Location = new Point(180, 68);
            this.txtNama.Size = new Size(250, 22);
            this.txtNama.Font = new Font("Arial", 10);

            // Label & TextBox Deskripsi
            this.lblDeskripsi.Text = "Deskripsi: *";
            this.lblDeskripsi.Location = new Point(50, 110);
            this.lblDeskripsi.Size = new Size(120, 20);
            this.lblDeskripsi.Font = new Font("Arial", 10);

            this.txtDeskripsi.Location = new Point(180, 108);
            this.txtDeskripsi.Size = new Size(250, 50);
            this.txtDeskripsi.Font = new Font("Arial", 10);
            this.txtDeskripsi.Multiline = true;
            this.txtDeskripsi.ScrollBars = ScrollBars.Vertical;

            // Label & TextBox Model
            this.lblModel.Text = "Model Barang:";
            this.lblModel.Location = new Point(50, 180);
            this.lblModel.Size = new Size(120, 20);
            this.lblModel.Font = new Font("Arial", 10);
            this.lblModel.ForeColor = Color.Gray;

            this.txtModel.Location = new Point(180, 178);
            this.txtModel.Size = new Size(150, 22);
            this.txtModel.Font = new Font("Arial", 10);
            this.txtModel.PlaceholderText = "Contoh: NGK G-Power";

            // Label & TextBox Merek
            this.lblMerek.Text = "Merek Barang:";
            this.lblMerek.Location = new Point(50, 220);
            this.lblMerek.Size = new Size(120, 20);
            this.lblMerek.Font = new Font("Arial", 10);
            this.lblMerek.ForeColor = Color.Gray;

            this.txtMerek.Location = new Point(180, 218);
            this.txtMerek.Size = new Size(150, 22);
            this.txtMerek.Font = new Font("Arial", 10);
            this.txtMerek.PlaceholderText = "Contoh: NGK, Aspira";

            // Label & ComboBox Jenis
            this.lblJenis.Text = "Jenis Sparepart: *";
            this.lblJenis.Location = new Point(50, 260);
            this.lblJenis.Size = new Size(120, 20);
            this.lblJenis.Font = new Font("Arial", 10);
            this.lblJenis.ForeColor = Color.Black;

            this.cmbJenis.Location = new Point(180, 258);
            this.cmbJenis.Size = new Size(200, 22);
            this.cmbJenis.Font = new Font("Arial", 10);
            this.cmbJenis.DropDownStyle = ComboBoxStyle.DropDownList;

            // Label & TextBox Harga
            this.lblHarga.Text = "Harga (Rp): *";
            this.lblHarga.Location = new Point(50, 300);
            this.lblHarga.Size = new Size(120, 20);
            this.lblHarga.Font = new Font("Arial", 10);

            this.txtHarga.Location = new Point(180, 298);
            this.txtHarga.Size = new Size(150, 22);
            this.txtHarga.Font = new Font("Arial", 10);
            this.txtHarga.KeyPress += TxtHarga_KeyPress;
            this.txtHarga.PlaceholderText = "Contoh: 150000";

            // Label & TextBox Stok
            this.lblStok.Text = "Stok: *";
            this.lblStok.Location = new Point(50, 340);
            this.lblStok.Size = new Size(120, 20);
            this.lblStok.Font = new Font("Arial", 10);

            this.txtStok.Location = new Point(180, 338);
            this.txtStok.Size = new Size(100, 22);
            this.txtStok.Font = new Font("Arial", 10);
            this.txtStok.KeyPress += TxtStok_KeyPress;
            this.txtStok.PlaceholderText = "Contoh: 10";

            // Button Simpan
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.Location = new Point(180, 390);
            this.btnSimpan.Size = new Size(100, 35);
            this.btnSimpan.BackColor = Color.LightGreen;
            this.btnSimpan.Font = new Font("Arial", 10, FontStyle.Bold);
            this.btnSimpan.Cursor = Cursors.Hand;
            this.btnSimpan.Click += BtnSimpan_Click;

            // Button Batal
            this.btnBatal.Text = "Batal";
            this.btnBatal.Location = new Point(290, 390);
            this.btnBatal.Size = new Size(100, 35);
            this.btnBatal.BackColor = Color.LightCoral;
            this.btnBatal.Font = new Font("Arial", 10, FontStyle.Bold);
            this.btnBatal.Cursor = Cursors.Hand;
            this.btnBatal.Click += BtnBatal_Click;

            // Label info
            Label lblInfo = new Label();
            lblInfo.Text = "* = Field wajib diisi";
            lblInfo.Location = new Point(50, 440);
            lblInfo.Size = new Size(150, 15);
            lblInfo.Font = new Font("Arial", 8, FontStyle.Italic);
            lblInfo.ForeColor = Color.Gray;

            // Add controls to form
            this.Controls.Add(this.lblJudul);
            this.Controls.Add(this.lblNama);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.lblDeskripsi);
            this.Controls.Add(this.txtDeskripsi);
            this.Controls.Add(this.lblModel);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.lblMerek);
            this.Controls.Add(this.txtMerek);
            this.Controls.Add(this.lblJenis);
            this.Controls.Add(this.cmbJenis);
            this.Controls.Add(this.lblHarga);
            this.Controls.Add(this.txtHarga);
            this.Controls.Add(this.lblStok);
            this.Controls.Add(this.txtStok);
            this.Controls.Add(this.btnSimpan);
            this.Controls.Add(this.btnBatal);
            this.Controls.Add(lblInfo);

            this.ResumeLayout(false);
        }

        private void LoadJenisData()
        {
            try
            {
                // Load jenis dari controller
                string[] jenisOptions = _controller.GetAvailableJenis();

                cmbJenis.Items.Clear();
                cmbJenis.Items.Add("-- Pilih Jenis --"); // Default option
                cmbJenis.Items.AddRange(jenisOptions);
                cmbJenis.SelectedIndex = 0; // Set default selection
            }
            catch (Exception ex)
            {
                _controller.ShowErrorMessage($"Error loading jenis data: {ex.Message}");
            }
        }

        // Validasi input harga (angka + decimal)
        private void TxtHarga_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow digits, decimal point, backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b')
            {
                e.Handled = true;
            }

            // Allow only one decimal point
            if (e.KeyChar == '.' && txtHarga.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        // Validasi input stok (angka only)
        private void TxtStok_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        // Event handler button Simpan
        private void BtnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                // Ambil input dari form
                string nama = txtNama.Text;
                string deskripsi = txtDeskripsi.Text;
                string harga = txtHarga.Text;
                string stok = txtStok.Text;
                string model = txtModel.Text;
                string merek = txtMerek.Text;
                string jenis = cmbJenis.SelectedIndex > 0 ? cmbJenis.SelectedItem.ToString() : "";

                // Validasi menggunakan controller
                var validationResult = _controller.ValidateBarangInput(nama, deskripsi, harga, stok, jenis);
                if (!validationResult.IsValid)
                {
                    _controller.ShowErrorMessage(validationResult.ErrorMessage, "Validasi Error");
                    return;
                }

                // Panggil controller untuk tambah barang
                var result = _controller.TambahBarang(nama, deskripsi, harga, stok, model, merek, jenis);

                if (result.Success)
                {
                    // Berhasil - show success message
                    _controller.ShowSuccessMessage(result.Message);

                    // Clear form setelah berhasil
                    ClearForm();
                }
                else
                {
                    // Gagal - show error message
                    _controller.ShowErrorMessage(result.Message);
                }
            }
            catch (Exception ex)
            {
                _controller.ShowErrorMessage($"Error tidak terduga: {ex.Message}");
            }
        }

        // Event handler button Batal
        private void BtnBatal_Click(object sender, EventArgs e)
        {
            // Cek apakah ada data yang belum disimpan
            if (HasUnsavedChanges())
            {
                bool konfirmasi = _controller.ShowConfirmationDialog(
                    "Ada data yang belum disimpan. Yakin mau keluar?",
                    "Konfirmasi Keluar");

                if (!konfirmasi)
                {
                    return; // User memilih tidak keluar
                }
            }

            this.Close();
        }

        // Check apakah ada perubahan yang belum disimpan
        private bool HasUnsavedChanges()
        {
            return !string.IsNullOrWhiteSpace(txtNama.Text) ||
                   !string.IsNullOrWhiteSpace(txtDeskripsi.Text) ||
                   !string.IsNullOrWhiteSpace(txtHarga.Text) ||
                   !string.IsNullOrWhiteSpace(txtStok.Text) ||
                   !string.IsNullOrWhiteSpace(txtModel.Text) ||
                   !string.IsNullOrWhiteSpace(txtMerek.Text) ||
                   cmbJenis.SelectedIndex > 0;
        }

        // Clear semua input
        private void ClearForm()
        {
            txtNama.Clear();
            txtDeskripsi.Clear();
            txtHarga.Clear();
            txtStok.Clear();
            txtModel.Clear();
            txtMerek.Clear();
            cmbJenis.SelectedIndex = 0; // Reset ke default
            txtNama.Focus(); // Set focus ke input pertama
        }

        // Method untuk set data (kalau dipanggil dari form lain untuk edit)
        public void SetBarangData(string nama, string deskripsi, decimal harga, int stok, string model, string merek, string jenis)
        {
            txtNama.Text = nama;
            txtDeskripsi.Text = deskripsi;
            txtHarga.Text = harga.ToString();
            txtStok.Text = stok.ToString();
            txtModel.Text = model;
            txtMerek.Text = merek;

            // Set jenis di combobox
            for (int i = 0; i < cmbJenis.Items.Count; i++)
            {
                if (cmbJenis.Items[i].ToString() == jenis)
                {
                    cmbJenis.SelectedIndex = i;
                    break;
                }
            }
        }

        // Method untuk mengecek apakah form dalam mode edit
        private bool _isEditMode = false;
        private int _editId = 0;

        public void SetEditMode(int id, string nama, string deskripsi, decimal harga, int stok, string model, string merek, string jenis)
        {
            _isEditMode = true;
            _editId = id;

            // Update UI untuk mode edit
            this.Text = "Edit Sparepart Motor";
            this.lblJudul.Text = "EDIT SPAREPART MOTOR";
            this.btnSimpan.Text = "Update";

            // Set data ke form
            SetBarangData(nama, deskripsi, harga, stok, model, merek, jenis);
        }

        // Quick fill methods untuk testing (bisa dihapus nanti)
        private void FillSampleData1()
        {
            txtNama.Text = "Kampas Rem Depan";
            txtDeskripsi.Text = "Kampas rem depan original untuk motor matic";
            txtModel.Text = "Genuine Part";
            txtMerek.Text = "Honda";
            cmbJenis.SelectedItem = "Kaki-kaki";
            txtHarga.Text = "150000";
            txtStok.Text = "10";
        }

        private void FillSampleData2()
        {
            txtNama.Text = "Busi Iridium";
            txtDeskripsi.Text = "Busi iridium premium tahan lama";
            txtModel.Text = "G-Power";
            txtMerek.Text = "NGK";
            cmbJenis.SelectedItem = "Kelistrikan";
            txtHarga.Text = "85000";
            txtStok.Text = "15";
        }
    }
}
