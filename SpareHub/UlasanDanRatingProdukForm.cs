using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ManajemenToko.Services;
using ManajemenToko.Models;
using UlasanDanRatingProduk;

namespace SpareHub
{
    public partial class UlasanDanRatingProdukForm : Form
    {
        /// <summary>
        /// Daftar produk yang tersedia.
        /// </summary>
        private List<Barang> _produkList = new();

        /// <summary>
        /// Service untuk mengelola review dan rating produk.
        /// </summary>
        private readonly ReviewService _reviewService = new();

        /// <summary>
        /// Inisialisasi form.
        /// </summary>
        public UlasanDanRatingProdukForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event handler saat form dimuat.
        /// </summary>
        private async void UlasanDanRatingProdukForm_Load(object sender, EventArgs e)
        {
            await LoadProdukAsync();
            SetupGrid();
            TampilkanProduk();
        }


        /// <summary>
        /// Memuat data produk dari service, atau menambahkan data dummy jika belum ada.
        /// </summary>
        private async Task LoadProdukAsync()
        {
            _produkList = await BarangService.Instance.LoadFromApiAsync();

            if (_produkList.Count == 0)
            {
                //TambahDataDummy();
                _produkList = BarangService.Instance.GetAllBarang();
            }
        }

        /// <summary>
        /// Menambahkan data dummy produk ke service.
        /// </summary>
        private void TambahDataDummy()
        {
            var dummyData = new List<Barang>
            {
                new() { Nama = "Kampas Rem Depan", Deskripsi = "Kampas rem depan original untuk motor matic Honda Vario", Harga = 150000, Stok = 10, Model = "Genuine Part", Merek = "Honda", Jenis = "Kaki-kaki" },
                new() { Nama = "Oli Mesin SAE 10W-40", Deskripsi = "Oli mesin 4T premium untuk motor bebek dan matic semua merk", Harga = 45000, Stok = 25, Model = "4T Premium", Merek = "Shell", Jenis = "Mesin" },
                new() { Nama = "Busi Iridium NGK", Deskripsi = "Busi iridium premium tahan lama untuk performa optimal", Harga = 85000, Stok = 15, Model = "G-Power", Merek = "NGK", Jenis = "Kelistrikan" },
                new() { Nama = "V-Belt CVT Honda", Deskripsi = "V-Belt transmisi CVT original untuk Honda Scoopy dan Vario", Harga = 120000, Stok = 8, Model = "CVT Belt", Merek = "Honda", Jenis = "Transmisi" },
                new() { Nama = "Kaca Spion Lipat", Deskripsi = "Spion lipat universal kanan kiri untuk semua jenis motor", Harga = 75000, Stok = 12, Model = "Universal", Merek = "KTC", Jenis = "Sparepart Lainnya" },
                new() { Nama = "Filter Udara K&N", Deskripsi = "Filter udara racing washable untuk performa maksimal", Harga = 200000, Stok = 6, Model = "Washable", Merek = "K&N", Jenis = "Mesin" },
                new() { Nama = "Lampu LED Philips", Deskripsi = "Lampu LED headlight putih terang hemat listrik", Harga = 95000, Stok = 18, Model = "Ultinon", Merek = "Philips", Jenis = "Kelistrikan" },
                new() { Nama = "Shock Belakang YSS", Deskripsi = "Shock absorber belakang adjustable untuk kenyamanan berkendara", Harga = 450000, Stok = 4, Model = "G-Series", Merek = "YSS", Jenis = "Kaki-kaki" }
            };

            foreach (var barang in dummyData)
            {
                BarangService.Instance.TambahBarang(barang);
            }
        }

        /// <summary>
        /// Mengatur tampilan kolom-kolom pada DataGridView.
        /// </summary>
        private void SetupGrid()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "ID",
                DataPropertyName = "Id",
                Width = 100
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Nama Produk",
                DataPropertyName = "Nama",
                Width = 300
            });

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
        }

        /// <summary>
        /// Menampilkan daftar produk ke DataGridView.
        /// </summary>
        private void TampilkanProduk()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _produkList;
        }

        /// <summary>
        /// Event handler tombol Kirim Review. Validasi input dan submit ulasan.
        /// </summary>
        private void BtnKirim_Click(object sender, EventArgs e)
        {
            try
            {
                if (!TryGetSelectedProductId(out int productId, out string namaProduk))
                {
                    MessageBox.Show("Pilih satu produk terlebih dahulu.");
                    return;
                }

                string ratingInput = fieldRating.Text.Trim();
                string ulasan = textBox2.Text.Trim();

                if (!int.TryParse(ratingInput, out int rating))
                {
                    MessageBox.Show("Rating harus berupa angka.");
                    return;
                }

                if (rating is < 1 or > 5)
                {
                    MessageBox.Show("Rating harus antara 1 dan 5.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(ulasan))
                {
                    MessageBox.Show("Deskripsi ulasan tidak boleh kosong.");
                    return;
                }

                const string reviewer = "User";
                var review = new Review(reviewer, ulasan, rating);

                _reviewService.AddReview(productId.ToString(), review);
                review.Submit();

                MessageBox.Show($"Review untuk {namaProduk} berhasil dikirim!");

                fieldRating.Clear();
                textBox2.Clear();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Input tidak valid: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}");
            }
        }

        /// <summary>
        /// Event handler tombol Bersihkan. Mengosongkan input rating dan ulasan.
        /// </summary>
        private void BtnBersihkan_Click(object sender, EventArgs e)
        {
            fieldRating.Clear();
            textBox2.Clear();
        }

        /// <summary>
        /// Menampilkan review yang telah disimpan untuk produk yang dipilih.
        /// </summary>
        private void ShowExistingReviews()
        {
            if (TryGetSelectedProductId(out int productId, out _))
            {
                _reviewService.ShowReviews(productId.ToString());
            }
        }

        /// <summary>
        /// Mengambil ID dan nama produk yang dipilih dari DataGridView.
        /// </summary>
        /// <param name="productId">Output ID produk.</param>
        /// <param name="namaProduk">Output nama produk.</param>
        /// <returns>True jika produk valid dipilih, otherwise false.</returns>
        private bool TryGetSelectedProductId(out int productId, out string namaProduk)
        {
            productId = 0;
            namaProduk = string.Empty;

            if (dataGridView1.SelectedRows.Count != 1)
                return false;

            if (dataGridView1.SelectedRows[0].Cells[0].Value is not int id)
                return false;

            productId = id;
            namaProduk = dataGridView1.SelectedRows[0].Cells[1].Value?.ToString() ?? "Produk";
            return true;
        }
    }
}