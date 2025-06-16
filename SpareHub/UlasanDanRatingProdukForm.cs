using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
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
            LoadOrderData();
            SetupGrid();
        }

        /// <summary>
        /// Memuat data order dari file JSON lokal.
        /// </summary>
        private void LoadOrderData()
        {
            try
            {
                string filePath = "../../../../fitur_Order/order_history.json";

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("File orders.json tidak ditemukan.");
                    return;
                }

                string json = File.ReadAllText(filePath);
                var orders = JsonSerializer.Deserialize<List<Order>>(json);

                if (orders == null || orders.Count == 0)
                {
                    MessageBox.Show("Data pesanan kosong.");
                    return;
                }

                var itemsToShow = orders
                    .SelectMany(order => order.Items.Select(item => new
                    {
                        order.OrderId,
                        item.ProductName,
                        item.Quantity,
                        item.Price
                    }))
                    .ToList();

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = itemsToShow;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memuat data order: {ex.Message}");
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
                HeaderText = "Order ID",
                DataPropertyName = "OrderId",
                Name = "OrderId",
                Width = 150
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Nama Produk",
                DataPropertyName = "ProductName",
                Name = "ProductName",
                Width = 200
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Quantity",
                DataPropertyName = "Quantity",
                Name = "Quantity",
                Width = 100
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Harga",
                DataPropertyName = "Price",
                Name = "Price",
                Width = 100
            });

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
        }

        /// <summary>
        /// Event handler tombol Kirim Review. Validasi input dan submit ulasan.
        /// </summary>
        private void BtnKirim_Click(object sender, EventArgs e)
        {
            try
            {
                if (!TryGetSelectedProductId(out _, out string namaProduk))
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

                _reviewService.AddReview(namaProduk, review);
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
            if (TryGetSelectedProductId(out _, out string namaProduk))
            {
                _reviewService.ShowReviews(namaProduk);
            }
        }

        /// <summary>
        /// Mengambil nama produk yang dipilih dari DataGridView.
        /// </summary>
        /// <param name="productId">ID produk (dummy karena tidak lagi digunakan)</param>
        /// <param name="namaProduk">Nama produk terpilih</param>
        /// <returns>True jika baris valid dipilih</returns>
        private bool TryGetSelectedProductId(out int productId, out string namaProduk)
        {
            productId = 0;
            namaProduk = string.Empty;

            if (dataGridView1.SelectedRows.Count != 1)
                return false;

            var row = dataGridView1.SelectedRows[0];

            if (row.Cells["ProductName"].Value is not string name || string.IsNullOrWhiteSpace(name))
                return false;

            namaProduk = name;
            return true;
        }

        /// <summary>
        /// Representasi item pesanan dari JSON.
        /// </summary>
        public class OrderItem
        {
            public string ProductId { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public int Price { get; set; }
        }

        /// <summary>
        /// Representasi pesanan dari JSON.
        /// </summary>
        public class Order
        {
            public string OrderId { get; set; }
            public List<OrderItem> Items { get; set; }
            public string PaymentMethod { get; set; }
            public string ShippingMethod { get; set; }
            public int Total { get; set; }
            public string Status { get; set; }
            public DateTime OrderDate { get; set; }
        }
    }
}