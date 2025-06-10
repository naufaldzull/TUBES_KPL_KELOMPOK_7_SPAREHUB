using ManajemenToko.Controller;
using ManajemenToko.Services;
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
        private readonly BarangController _controller;
        private readonly BarangService _barangService;

        // Basic controls only
        private TextBox txtNama, txtDeskripsi, txtHarga, txtStok, txtModel, txtMerek;
        private ComboBox cmbJenis;
        private Button btnSimpan, btnBatal;

        public FormTambahBarang()
        {
            InitializeComponent();
            _controller = new BarangController();
            _barangService = BarangService.Instance;
            SetupForm();
            LoadJenisData();
        }

        private void SetupForm()
        {
            // Form setup
            Text = "Tambah Sparepart Motor";
            Size = new Size(450, 400);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            // Create and position controls
            int y = 20;

            CreateLabel("TAMBAH SPAREPART", 20, y, 400, true);
            y += 40;

            txtNama = CreateTextBox("Nama Sparepart:", 20, y);
            y += 50;

            txtDeskripsi = CreateTextBox("Deskripsi:", 20, y, 60);
            txtDeskripsi.Multiline = true;
            y += 70;

            // Optional fields - simplified
            txtModel = CreateTextBox("Model:", 20, y);
            txtMerek = CreateTextBox("Merek:", 230, y);
            y += 50;

            cmbJenis = CreateComboBox("Jenis:", 20, y);
            y += 50;

            txtHarga = CreateTextBox("Harga (Rp):", 20, y);
            txtStok = CreateTextBox("Stok:", 230, y);
            y += 60;

            // Buttons
            btnSimpan = CreateButton("Simpan", 150, y, Color.LightGreen);
            btnSimpan.Click += BtnSimpan_Click;

            btnBatal = CreateButton("Batal", 260, y, Color.LightCoral);
            btnBatal.Click += (s, e) => Close();
        }

        // Helper methods untuk kurangi duplicate code
        private Label CreateLabel(string text, int x, int y, int width = 100, bool isTitle = false)
        {
            var lbl = new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, 20),
                Font = isTitle ? new Font("Arial", 12, FontStyle.Bold) : new Font("Arial", 9)
            };
            Controls.Add(lbl);
            return lbl;
        }

        private TextBox CreateTextBox(string labelText, int x, int y, int height = 25)
        {
            CreateLabel(labelText, x, y);
            var txt = new TextBox
            {
                Location = new Point(x + 100, y - 2),
                Size = new Size(100, height),
                Font = new Font("Arial", 9)
            };
            Controls.Add(txt);
            return txt;
        }

        private ComboBox CreateComboBox(string labelText, int x, int y)
        {
            CreateLabel(labelText, x, y);
            var cmb = new ComboBox
            {
                Location = new Point(x + 100, y - 2),
                Size = new Size(150, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Arial", 9)
            };
            Controls.Add(cmb);
            return cmb;
        }

        private Button CreateButton(string text, int x, int y, Color color)
        {
            var btn = new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(90, 30),
                BackColor = color,
                Font = new Font("Arial", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            Controls.Add(btn);
            return btn;
        }

        private void LoadJenisData()
        {
            try
            {
                string[] jenisOptions = _controller.GetAvailableJenis();
                cmbJenis.Items.Add("-- Pilih --");
                cmbJenis.Items.AddRange(jenisOptions);
                cmbJenis.SelectedIndex = 0;
            }
            catch
            {
                MessageBox.Show("Error loading jenis data", "Error");
            }
        }

        private void BtnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                // Basic validation
                if (string.IsNullOrWhiteSpace(txtNama.Text) ||
                    string.IsNullOrWhiteSpace(txtDeskripsi.Text) ||
                    string.IsNullOrWhiteSpace(txtHarga.Text) ||
                    string.IsNullOrWhiteSpace(txtStok.Text) ||
                    cmbJenis.SelectedIndex <= 0)
                {
                    MessageBox.Show("Lengkapi semua field yang wajib!", "Error");
                    return;
                }

                // Tambah barang
                var result = _controller.TambahBarang(
                    txtNama.Text,
                    txtDeskripsi.Text,
                    txtHarga.Text,
                    txtStok.Text,
                    txtModel.Text,
                    txtMerek.Text,
                    cmbJenis.SelectedItem.ToString()
                );

                if (result.Success)
                {
                    MessageBox.Show(result.Message, "Success");
                    ClearForm();

                    // Sync to API in background (simplified)
                    _ = _barangService.SyncToApiAsync();
                }
                else
                {
                    MessageBox.Show(result.Message, "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error");
            }
        }

        private void ClearForm()
        {
            txtNama.Clear();
            txtDeskripsi.Clear();
            txtHarga.Clear();
            txtStok.Clear();
            txtModel.Clear();
            txtMerek.Clear();
            cmbJenis.SelectedIndex = 0;
        }

    }
}
