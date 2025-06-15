using System;
using System.Drawing; // pastikan ini ada untuk Size, Point, Font, Color
using System.Windows.Forms;

namespace ManajemenToko
{
    /// <summary>
    /// Main Menu Form untuk aplikasi Manajemen Toko.
    /// Mengikuti .NET naming convention dan UI best practices.
    /// </summary>
    public partial class ManajemenToko : Form // PascalCase 
    {
        public ManajemenToko()
        {
            InitializeComponent();
            SetupMenuStyle(); // PascalCase 
        }

        /// <summary>
        /// Setup tampilan awal dan elemen menu.
        /// </summary>
        private void SetupMenuStyle() // PascalCase 
        {
            this.Text = "Manajemen Toko - Menu Utama";

            if (this.Controls.Contains(dataGridViewBarang1)) // camelCase 
            {
                dataGridViewBarang1.Visible = false;
            }

            CreateMenuButtons();
        }

        /// <summary>
        /// Buat tombol-tombol menu utama.
        /// </summary>
        private void CreateMenuButtons() // PascalCase 
        {
            // Button Tambah Barang
            Button btnTambahBarang = new()
            {
                Text = "1. Tambah Barang",
                Size = new Size(200, 50),
                Location = new Point(150, 100),
                BackColor = Color.LightBlue,
                Font = new Font("Arial", 10, FontStyle.Regular)
            };
            btnTambahBarang.Click += BtnTambahBarang_Click;
            this.Controls.Add(btnTambahBarang);

            // Button Lihat Barang
            Button btnLihatBarang = new()
            {
                Text = "2. Lihat Semua Barang",
                Size = new Size(200, 50),
                Location = new Point(150, 160),
                BackColor = Color.LightGreen,
                Font = new Font("Arial", 10, FontStyle.Regular)
            };
            btnLihatBarang.Click += BtnLihatBarang_Click;
            this.Controls.Add(btnLihatBarang);

            // Button Ubah Deskripsi Barang
            Button btnUbahDeskripsi = new()
            {
                Text = "3. Ubah Deskripsi Barang",
                Size = new Size(200, 50),
                Location = new Point(150, 220),
                BackColor = Color.LightYellow,
                Font = new Font("Arial", 10, FontStyle.Regular)
            };
            btnUbahDeskripsi.Click += BtnUbahDeskripsi_Click;
            this.Controls.Add(btnUbahDeskripsi);

            // Button Hapus Barang
            Button btnHapusBarang = new()
            {
                Text = "4. Hapus Barang",
                Size = new Size(200, 50),
                Location = new Point(150, 280),
                BackColor = Color.LightCoral,
                Font = new Font("Arial", 10, FontStyle.Regular)
            };
            btnHapusBarang.Click += BtnHapusBarang_Click;
            this.Controls.Add(btnHapusBarang);

            // Label Judul
            Label lblJudul = new()
            {
                Text = "MANAJEMEN BARANG TOKO",
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Size = new Size(400, 40),
                Location = new Point(100, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblJudul);
        }

        // Event handler: Tambah Barang
        private void BtnTambahBarang_Click(object sender, EventArgs e) // PascalCase 
        {
            using FormTambahBarang formTambah = new(); // using untuk modal form 
            formTambah.ShowDialog();
        }

        // Event handler: Lihat Semua Barang
        private void BtnLihatBarang_Click(object sender, EventArgs e)
        {
            using FormLihatBarang formLihat = new();
            formLihat.ShowDialog();
        }

        // Event handler: Ubah Deskripsi Barang
        private void BtnUbahDeskripsi_Click(object sender, EventArgs e)
        {
            using FormUbahDeskripsi formUbah = new();
            formUbah.ShowDialog();
        }

        // Event handler: Hapus Barang
        private void BtnHapusBarang_Click(object sender, EventArgs e)
        {
            using FormHapusBarang formHapus = new();
            formHapus.ShowDialog();
        }

        // Handler tambahan dari designer (jangan dihapus)
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { } // camelCase 
        private void txtNama_TextChanged(object sender, EventArgs e) { }
        private void ManajemenToko_Load(object sender, EventArgs e) { }
    }
}
