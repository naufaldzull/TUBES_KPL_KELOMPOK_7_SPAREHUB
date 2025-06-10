using ManajemenToko.Controller;
using ManajemenToko.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ManajemenToko
{
    public partial class FormUbahDeskripsi : Form
    {
        private readonly BarangController _controller;
        private DataGridView dgvBarang;
        private TextBox txtDeskripsi;
        private Button btnUpdate;
        private int _selectedId = 0;

        public FormUbahDeskripsi()
        {
            InitializeComponent();
            _controller = new BarangController(); // Sudah Singleton di controller
            SetupForm();
            LoadData();
        }

        private void SetupForm()
        {
            Text = "Ubah Deskripsi";
            Size = new Size(700, 450);
            StartPosition = FormStartPosition.CenterScreen;

            var lblTitle = new Label
            {
                Text = "UBAH DESKRIPSI SPAREPART",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(200, 20),
                Size = new Size(300, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };
            Controls.Add(lblTitle);

            dgvBarang = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(640, 200),
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgvBarang.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    _selectedId = Convert.ToInt32(dgvBarang.Rows[e.RowIndex].Cells["Id"].Value);
                    var result = _controller.GetBarangById(_selectedId);
                    if (result.Success)
                    {
                        txtDeskripsi.Text = result.Data.Deskripsi;
                        txtDeskripsi.Enabled = true;
                        btnUpdate.Enabled = true;
                    }
                }
            };
            Controls.Add(dgvBarang);

            var lblDesc = new Label
            {
                Text = "Deskripsi Baru:",
                Location = new Point(20, 280),
                Size = new Size(100, 20)
            };
            Controls.Add(lblDesc);

            txtDeskripsi = new TextBox
            {
                Location = new Point(20, 305),
                Size = new Size(400, 60),
                Multiline = true,
                Enabled = false
            };
            Controls.Add(txtDeskripsi);

            btnUpdate = new Button
            {
                Text = "Update",
                Location = new Point(340, 380),
                Size = new Size(80, 30),
                BackColor = Color.LightGreen,
                Enabled = false
            };
            btnUpdate.Click += BtnUpdate_Click;
            Controls.Add(btnUpdate);

            var btnBatal = new Button
            {
                Text = "Batal",
                Location = new Point(430, 380),
                Size = new Size(80, 30),
                BackColor = Color.LightCoral
            };
            btnBatal.Click += (s, e) => Close();
            Controls.Add(btnBatal);
        }

        private void LoadData()
        {
            dgvBarang.Columns.Clear();
            dgvBarang.Columns.Add("Id", "ID");
            dgvBarang.Columns.Add("Nama", "Nama");
            dgvBarang.Columns.Add("Deskripsi", "Deskripsi");

            dgvBarang.Columns["Id"].Width = 50;
            dgvBarang.Columns["Deskripsi"].Width = 300;

            var result = _controller.GetAllBarang();
            if (result.Success)
            {
                dgvBarang.Rows.Clear();
                foreach (var barang in result.Data)
                    dgvBarang.Rows.Add(barang.Id, barang.Nama, barang.Deskripsi);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedId == 0)
            {
                MessageBox.Show("Pilih barang dulu!", "Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDeskripsi.Text))
            {
                MessageBox.Show("Deskripsi tidak boleh kosong!", "Error");
                return;
            }

            var result = _controller.UpdateDeskripsi(_selectedId, txtDeskripsi.Text.Trim());
            MessageBox.Show(result.Message, result.Success ? "Success" : "Error");

            if (result.Success)
            {
                LoadData();
                _selectedId = 0;
                txtDeskripsi.Clear();
                txtDeskripsi.Enabled = false;
                btnUpdate.Enabled = false;
            }
        }
    }
}
