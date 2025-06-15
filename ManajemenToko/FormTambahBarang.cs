using ManajemenToko.Controller;
using ManajemenToko.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ManajemenToko
{
    /// <summary>
    /// Form untuk menambahkan data barang ke sistem.
    /// Mengikuti .NET naming convention dan WinForms best practice.
    /// </summary>
    public partial class FormTambahBarang : Form // PascalCase 
    {
        private readonly BarangController _controller; // camelCase private readonly 
        private readonly BarangService _barangService;

        // Controls
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

        private void SetupForm() // PascalCase 
        {
            Text = "Tambah Sparepart Motor";
            Size = new Size(450, 400);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            int y = 20;

            CreateLabel("TAMBAH SPAREPART", 20, y, 400, true);
            y += 40;

            txtNama = CreateTextBox("Nama Sparepart:", 20, y);
            y += 50;

            txtDeskripsi = CreateTextBox("Deskripsi:", 20, y, 60);
            txtDeskripsi.Multiline = true;
            y += 70;

            txtModel = CreateTextBox("Model:", 20, y);
            txtMerek = CreateTextBox("Merek:", 230, y);
            y += 50;

            cmbJenis = CreateComboBox("Jenis:", 20, y);
            y += 50;

            txtHarga = CreateTextBox("Harga (Rp):", 20, y);
            txtStok = CreateTextBox("Stok:", 230, y);
            y += 40;

            btnSimpan = CreateButton("Simpan", 150, y, Color.LightGreen);
            btnSimpan.Click += BtnSimpan_Click;

            btnBatal = CreateButton("Batal", 260, y, Color.LightCoral);
            btnBatal.Click += (s, e) => Close();
        }

        private Label CreateLabel(string text, int x, int y, int width = 100, bool isTitle = false) // camelCase parameter 
        {
            var label = new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, 20),
                Font = isTitle ? new Font("Arial", 12, FontStyle.Bold) : new Font("Arial", 9)
            };
            Controls.Add(label);
            return label;
        }

        private TextBox CreateTextBox(string labelText, int x, int y, int height = 25)
        {
            CreateLabel(labelText, x, y);
            var textBox = new TextBox
            {
                Location = new Point(x + 100, y - 2),
                Size = new Size(100, height),
                Font = new Font("Arial", 9)
            };
            Controls.Add(textBox);
            return textBox;
        }

        private ComboBox CreateComboBox(string labelText, int x, int y)
        {
            CreateLabel(labelText, x, y);
            var comboBox = new ComboBox
            {
                Location = new Point(x + 100, y - 2),
                Size = new Size(150, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Arial", 9)
            };
            Controls.Add(comboBox);
            return comboBox;
        }

        private Button CreateButton(string text, int x, int y, Color color)
        {
            var button = new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(90, 30),
                BackColor = color,
                Font = new Font("Arial", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            Controls.Add(button);
            return button;
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
                if (string.IsNullOrWhiteSpace(txtNama.Text) ||
                    string.IsNullOrWhiteSpace(txtDeskripsi.Text) ||
                    string.IsNullOrWhiteSpace(txtHarga.Text) ||
                    string.IsNullOrWhiteSpace(txtStok.Text) ||
                    cmbJenis.SelectedIndex <= 0)
                {
                    MessageBox.Show("Lengkapi semua field yang wajib!", "Error");
                    return;
                }

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

                    _ = _barangService.SyncToApiAsync(); // fire-and-forget 
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
