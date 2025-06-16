using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ManajemenToko.Models;
using ManajemenToko.Services;
using ManajemenToko.Controller;
using UlasanDanRatingProduk;

namespace SpareHub
{
    public partial class UlasanDanRatingProdukForm : Form
    {
        private readonly ReviewService _reviewService = ReviewService.Instance;
        private readonly BarangController _barangController = new();

        /// <summary>
        /// Inisialisasi form ulasan dan rating produk.
        /// </summary>
        public UlasanDanRatingProdukForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event saat form dimuat. Langsung memuat data barang ke DataGridView.
        /// </summary>
        private void UlasanDanRatingProdukForm_Load(object sender, EventArgs e)
        {
            LoadBarangToGrid();
        }

        /// <summary>
        /// Memuat data barang dari controller dan menampilkannya di DataGridView.
        /// Hanya kolom Id dan Nama yang ditampilkan.
        /// </summary>
        private void LoadBarangToGrid()
        {
            var result = _barangController.GetAllBarang();
            if (!result.Success)
            {
                _barangController.ShowErrorMessage(result.Message);
                return;
            }

            var dataToShow = result.Data.Select(b => new
            {
                b.Id,
                b.Nama
            }).ToList();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dataToShow;

            SetupGridKolomBarang();
        }

        /// <summary>
        /// Mengatur tampilan kolom DataGridView hanya untuk Id dan Nama produk.
        /// </summary>
        private void SetupGridKolomBarang()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "ID",
                DataPropertyName = "Id",
                Name = "Id",
                Width = 80
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Nama Produk",
                DataPropertyName = "Nama",
                Name = "Nama",
                Width = 200
            });

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
        }

        /// <summary>
        /// Event tombol Kirim diklik. Melakukan validasi dan menyimpan review ke service.
        /// </summary>
        private void BtnKirim_Click(object sender, EventArgs e)
        {
            try
            {
                if (!TryGetSelectedProduct(out string namaProduk))
                {
                    MessageBox.Show("Pilih satu produk terlebih dahulu.");
                    return;
                }

                string reviewer = textBoxNama.Text.Trim();
                string ratingInput = fieldRating.Text.Trim();
                string ulasan = textBox2.Text.Trim();

                if (string.IsNullOrWhiteSpace(reviewer))
                {
                    MessageBox.Show("Nama pengulas tidak boleh kosong.");
                    return;
                }

                if (!int.TryParse(ratingInput, out int rating) || rating < 1 || rating > 5)
                {
                    MessageBox.Show("Rating harus angka antara 1 sampai 5.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(ulasan))
                {
                    MessageBox.Show("Deskripsi ulasan tidak boleh kosong.");
                    return;
                }

                var review = new Review(reviewer, ulasan, rating);
                _reviewService.AddReview(namaProduk, review);
                review.Submit();

                MessageBox.Show($"Review dari {reviewer} untuk {namaProduk} berhasil dikirim!");

                fieldRating.Clear();
                textBox2.Clear();
                textBoxNama.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}");
            }
        }

        /// <summary>
        /// Event tombol Bersihkan diklik. Mengosongkan input rating, ulasan, dan nama.
        /// </summary>
        private void BtnBersihkan_Click(object sender, EventArgs e)
        {
            fieldRating.Clear();
            textBox2.Clear();
            textBoxNama.Clear();
        }

        /// <summary>
        /// Menampilkan review yang sudah disimpan untuk produk yang dipilih.
        /// </summary>
        private void ShowExistingReviews()
        {
            if (TryGetSelectedProduct(out string namaProduk))
            {
                _reviewService.ShowReviews(namaProduk);
            }
        }

        /// <summary>
        /// Mengambil nama produk dari baris terpilih di DataGridView.
        /// </summary>
        /// <param name="namaProduk">Nama produk yang dipilih</param>
        /// <returns>True jika satu baris valid dipilih</returns>
        private bool TryGetSelectedProduct(out string namaProduk)
        {
            namaProduk = string.Empty;

            if (dataGridView1.SelectedRows.Count != 1)
                return false;

            var row = dataGridView1.SelectedRows[0];

            if (row.Cells["Nama"]?.Value is not string name || string.IsNullOrWhiteSpace(name))
                return false;

            namaProduk = name;
            return true;
        }
    }
}