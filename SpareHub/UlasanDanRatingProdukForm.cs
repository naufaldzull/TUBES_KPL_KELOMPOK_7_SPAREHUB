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
        private void UlasanDanRatingProdukForm_Load(object sender, EventArgs e)
        {
            LoadProduk();
            SetupGrid();
            TampilkanProduk();
        }

        /// <summary>
        /// Memuat data produk dari service, atau menambahkan data dummy jika belum ada.
        /// </summary>
        private void LoadProduk()
        {
            _produkList = BarangService.Instance.GetAllBarang();

            if (_produkList.Count == 0)
            {
                TambahDataDummy();
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
                new() { Nama = "Laptop Gaming XYZ", Deskripsi = "Laptop gaming high end", Harga = 15000000, Stok = 5, Jenis = "Elektronik" },
                new() { Nama = "Smartphone Pro 12", Deskripsi = "Smartphone flagship", Harga = 8000000, Stok = 10, Jenis = "Elektronik" },
                new() { Nama = "Kipas Angin Turbo", Deskripsi = "Kipas angin kencang", Harga = 500000, Stok = 20, Jenis = "Elektronik" },
                new() { Nama = "Monitor Ultrawide 34\"", Deskripsi = "Monitor gaming ultrawide", Harga = 6000000, Stok = 3, Jenis = "Elektronik" },
                new() { Nama = "Mouse Wireless RGB", Deskripsi = "Mouse gaming wireless", Harga = 300000, Stok = 15, Jenis = "Aksesoris" }
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