using System.Windows.Forms;

namespace ManajemenToko
{
    public partial class ManajemenToko : Form
    {
        public ManajemenToko()
        {
            InitializeComponent();
            SetupMenuStyle(); // Panggil method buat setup menu style
        }

        private void SetupMenuStyle()
        {
            // Ubah form jadi menu utama
            this.Text = "Manajemen Toko - Menu Utama";

            // Sembunyiin DataGridView dulu (nanti dipake di form lain)
            if (this.Controls.Contains(dataGridViewBarang1))
            {
                dataGridViewBarang1.Visible = false;
            }

            // Bikin button menu baru (tambahin lewat designer atau code)
            CreateMenuButtons();
        }

        private void CreateMenuButtons()
        {
            // Button Tambah Barang
            Button btnTambahBarang = new Button();
            btnTambahBarang.Text = "1. Tambah Barang";
            btnTambahBarang.Size = new Size(200, 50);
            btnTambahBarang.Location = new Point(150, 100);
            btnTambahBarang.BackColor = Color.LightBlue;
            btnTambahBarang.Font = new Font("Arial", 10, FontStyle.Regular);
            btnTambahBarang.Click += BtnTambahBarang_Click;
            this.Controls.Add(btnTambahBarang);

            // Button Lihat Barang
            Button btnLihatBarang = new Button();
            btnLihatBarang.Text = "2. Lihat Semua Barang";
            btnLihatBarang.Size = new Size(200, 50);
            btnLihatBarang.Location = new Point(150, 160);
            btnLihatBarang.BackColor = Color.LightGreen;
            btnLihatBarang.Font = new Font("Arial", 10, FontStyle.Regular);
            btnLihatBarang.Click += BtnLihatBarang_Click;
            this.Controls.Add(btnLihatBarang);

            // Button Ubah Barang
            Button btnUbahDeskripsi = new Button();
            btnUbahDeskripsi.Text = "3. Ubah Deskripsi Barang";
            btnUbahDeskripsi.Size = new Size(200, 50);
            btnUbahDeskripsi.Location = new Point(150, 220);
            btnUbahDeskripsi.BackColor = Color.LightYellow;
            btnUbahDeskripsi.Font = new Font("Arial", 10, FontStyle.Regular);
            btnUbahDeskripsi.Click += btnUbahDeskripsi_Click;
            this.Controls.Add(btnUbahDeskripsi);

            // Button Hapus Barang
            Button btnHapusBarang = new Button();
            btnHapusBarang.Text = "4. Hapus Barang";
            btnHapusBarang.Size = new Size(200, 50);
            btnHapusBarang.Location = new Point(150, 280);
            btnHapusBarang.BackColor = Color.LightCoral;
            btnHapusBarang.Font = new Font("Arial", 10, FontStyle.Regular);
            btnHapusBarang.Click += BtnHapusBarang_Click;
            this.Controls.Add(btnHapusBarang);

            // Label Judul
            Label lblJudul = new Label();
            lblJudul.Text = "MANAJEMEN BARANG TOKO";
            lblJudul.Font = new Font("Arial", 16, FontStyle.Bold);
            lblJudul.ForeColor = Color.DarkBlue;
            lblJudul.Size = new Size(400, 40);
            lblJudul.Location = new Point(100, 30);
            lblJudul.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblJudul);
        }

        // Event handlers untuk menu buttons
        private void BtnTambahBarang_Click(object sender, EventArgs e)
        {
            // Buka form tambah barang
            FormTambahBarang formTambah = new FormTambahBarang();
            formTambah.ShowDialog(); // ShowDialog biar modal
        }

        private void BtnLihatBarang_Click(object sender, EventArgs e)
        {
            FormLihatBarang formLihat = new FormLihatBarang();
            formLihat.ShowDialog();
        }

        private void btnUbahDeskripsi_Click(object sender, EventArgs e)
        {
            FormUbahDeskripsi formUbah = new FormUbahDeskripsi();
            formUbah.ShowDialog();
        }

        private void BtnHapusBarang_Click(object sender, EventArgs e)
        {
            FormHapusBarang formHapus = new FormHapusBarang();
            formHapus.ShowDialog();
        }

        // Event handlers yang udah ada - jangan dihapus
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Nanti dipake buat form lihat barang
        }

        private void txtNama_TextChanged(object sender, EventArgs e)
        {
            // Nanti dipake buat validation
        }

        private void ManajemenToko_Load(object sender, EventArgs e)
        {
            // Load form event - bisa dipake buat inisialisasi data
        }
    }
}