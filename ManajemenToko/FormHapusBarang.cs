using ManajemenToko.Controller;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ManajemenToko
{
    /// <summary>
    /// Form untuk menghapus barang dari daftar sparepart.
    /// </summary>
    public partial class FormHapusBarang : Form // PascalCase 
    {
        private readonly BarangController _controller; // camelCase readonly 
        private DataGridView dgvBarang;
        private Button btnHapus;
        private Label lblSelected;
        private int _selectedId = 0;

        public FormHapusBarang()
        {
            InitializeComponent();
            _controller = new BarangController(); // Singleton 
            SetupForm();
            LoadData();
        }

        private void SetupForm()
        {
            Text = "Hapus Sparepart";
            Size = new Size(700, 450);
            StartPosition = FormStartPosition.CenterScreen;

            var lblTitle = new Label
            {
                Text = "HAPUS SPAREPART",
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.DarkRed,
                Location = new Point(250, 20),
                Size = new Size(200, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };
            Controls.Add(lblTitle);

            dgvBarang = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(640, 250),
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill // Tambahan 
            };
            dgvBarang.CellClick += DgvBarang_CellClick;
            Controls.Add(dgvBarang);

            lblSelected = new Label
            {
                Text = "Pilih barang yang akan dihapus",
                Location = new Point(20, 340),
                Size = new Size(400, 20),
                ForeColor = Color.Blue
            };
            Controls.Add(lblSelected);

            btnHapus = new Button
            {
                Text = "HAPUS",
                Location = new Point(480, 370),
                Size = new Size(80, 30),
                BackColor = Color.Red,
                ForeColor = Color.White,
                Enabled = false,
                Cursor = Cursors.Hand
            };
            btnHapus.Click += BtnHapus_Click;
            Controls.Add(btnHapus);

            var btnBatal = new Button
            {
                Text = "Batal",
                Location = new Point(570, 370),
                Size = new Size(80, 30),
                BackColor = Color.LightGray,
                Cursor = Cursors.Hand
            };
            btnBatal.Click += (s, e) => Close();
            Controls.Add(btnBatal);
        }

        private void DgvBarang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _selectedId = Convert.ToInt32(dgvBarang.Rows[e.RowIndex].Cells["Id"].Value);
                var nama = dgvBarang.Rows[e.RowIndex].Cells["Nama"].Value?.ToString() ?? "-";
                lblSelected.Text = $"Dipilih: {nama}";
                btnHapus.Enabled = true;
            }
        }

        private void LoadData()
        {
            dgvBarang.Columns.Clear();
            dgvBarang.Columns.Add("Id", "ID");
            dgvBarang.Columns.Add("Nama", "Nama");
            dgvBarang.Columns.Add("Jenis", "Jenis");
            dgvBarang.Columns.Add("Stok", "Stok");

            dgvBarang.Columns["Id"].Width = 50;
            dgvBarang.Columns["Stok"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            var result = _controller.GetAllBarang();
            if (result.Success)
            {
                dgvBarang.Rows.Clear();
                foreach (var barang in result.Data)
                {
                    dgvBarang.Rows.Add(barang.Id, barang.Nama, barang.Jenis, barang.Stok);
                }
            }
        }

        private void BtnHapus_Click(object sender, EventArgs e)
        {
            if (_selectedId == 0) return;

            var confirm = MessageBox.Show(
                "Yakin hapus barang ini?\nData tidak bisa dikembalikan!",
                "Konfirmasi Hapus",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm == DialogResult.Yes)
            {
                var result = _controller.HapusBarang(_selectedId);
                MessageBox.Show(result.Message, result.Success ? "Success" : "Error");

                LoadData();
                ResetFormState();
            }
        }

        private void ResetFormState()
        {
            _selectedId = 0;
            lblSelected.Text = "Pilih barang yang akan dihapus";
            btnHapus.Enabled = false;
        }
    }
}
