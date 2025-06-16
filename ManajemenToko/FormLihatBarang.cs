using ManajemenToko.Controller;
using ManajemenToko.Models;
using ManajemenToko.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ManajemenToko
{
    /// <summary>
    /// Form untuk menampilkan daftar barang dari data lokal/API.
    /// </summary>
    public partial class FormLihatBarang : Form // PascalCase 
    {
        private readonly BarangController _controller;
        private DataGridView dgvBarang;
        private TextBox txtCari;
        private Button btnRefresh, btnRefreshApi, btnTutup;
        private Label lblTotal;

        public FormLihatBarang()
        {
            InitializeComponent();
            _controller = new BarangController();
            SetupForm(); // PascalCase 
            LoadData();  // PascalCase 
        }

        private void SetupForm()
        {
            Text = "Daftar Sparepart";
            Size = new Size(800, 500);
            StartPosition = FormStartPosition.CenterScreen;

            var lblTitle = new Label
            {
                Text = "DAFTAR SPAREPART MOTOR",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(250, 20),
                Size = new Size(300, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };
            Controls.Add(lblTitle);

            var lblCari = new Label
            {
                Text = "Cari:",
                Location = new Point(20, 60),
                Size = new Size(40, 20)
            };
            Controls.Add(lblCari);

            txtCari = new TextBox
            {
                Location = new Point(65, 58),
                Size = new Size(200, 22)
            };
            txtCari.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    SearchData();
                    e.Handled = true;
                }
            };
            Controls.Add(txtCari);

            dgvBarang = new DataGridView
            {
                Location = new Point(20, 90),
                Size = new Size(740, 300),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            SetupColumns();
            Controls.Add(dgvBarang);

            lblTotal = new Label
            {
                Text = "Total: 0 sparepart",
                Location = new Point(20, 400),
                Size = new Size(200, 20),
                Font = new Font("Arial", 9, FontStyle.Bold)
            };
            Controls.Add(lblTotal);

            btnRefresh = CreateButton("Refresh Lokal", new Point(480, 420), Color.LightBlue, (s, e) => LoadData());
            btnRefreshApi = CreateButton("Refresh API", new Point(580, 420), Color.LightYellow, async (s, e) =>
            {
                await BarangService.Instance.LoadFromApiAsync();
                LoadData();
            });
            btnTutup = CreateButton("Tutup", new Point(680, 420), Color.LightCoral, (s, e) => Close());
        }

        private Button CreateButton(string text, Point location, Color color, EventHandler onClick)
        {
            var button = new Button
            {
                Text = text,
                Location = location,
                Size = new Size(90, 30),
                BackColor = color,
                Font = new Font("Arial", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            button.Click += onClick;
            Controls.Add(button);
            return button;
        }

        private void SetupColumns()
        {
            dgvBarang.Columns.Add("Id", "ID");
            dgvBarang.Columns.Add("Nama", "Nama");
            dgvBarang.Columns.Add("Deskripsi", "Deskripsi");
            dgvBarang.Columns.Add("Jenis", "Jenis");
            dgvBarang.Columns.Add("Harga", "Harga");
            dgvBarang.Columns.Add("Stok", "Stok");

            dgvBarang.Columns["Id"].Width = 50;
            dgvBarang.Columns["Deskripsi"].Width = 300;
            dgvBarang.Columns["Harga"].DefaultCellStyle.Format = "C0"; // currency IDR
            dgvBarang.Columns["Stok"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void LoadData()
        {
            var result = _controller.GetAllBarang();
            if (result.Success)
                DisplayData(result.Data);
        }

        private void DisplayData(List<Barang> barangList)
        {
            dgvBarang.Rows.Clear();

            foreach (var barang in barangList)
            {
                dgvBarang.Rows.Add(
                    barang.Id,
                    barang.Nama,
                    barang.Deskripsi,
                    barang.Jenis,
                    barang.Harga,
                    barang.Stok
                );
            }

            lblTotal.Text = $"Total: {barangList.Count} sparepart";
        }

        private void SearchData()
        {
            var result = _controller.CariBarang(txtCari.Text.Trim());
            if (result.Success)
                DisplayData(result.Data);
        }

        private void LihatBarang_Load(object sender, EventArgs e) { } // Event handler dari Designer, jangan dihapus
    }
}
