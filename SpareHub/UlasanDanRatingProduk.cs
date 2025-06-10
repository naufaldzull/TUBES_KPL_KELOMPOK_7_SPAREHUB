using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UlasanDanRatingProduk;

namespace SpareHub
{
    public partial class UlasanDanRatingProduk : Form
    {
        private Dictionary<string, Product> produkList = new();

        public UlasanDanRatingProduk()
        {
            InitializeComponent();
        }

        private void UlasanDanRatingProduk_Load(object sender, EventArgs e)
        {
            SeedData();
            SetupColumns();
            TampilkanData();
        }

        private void SeedData()
        {
            produkList.Clear();
            produkList["P001"] = new Product("P001", "Laptop Gaming XYZ");
            produkList["P002"] = new Product("P002", "Smartphone Pro 12");
            produkList["P003"] = new Product("P003", "Kipas Angin Turbo");
            produkList["P004"] = new Product("P004", "Monitor Ultrawide 34\"");
            produkList["P005"] = new Product("P005", "Mouse Wireless RGB");
        }

        private void SetupColumns()
        {
            dataGridView1.Columns.Clear();

            DataGridViewTextBoxColumn colId = new()
            {
                HeaderText = "ID",
                DataPropertyName = "Id",
                Width = 100
            };
            dataGridView1.Columns.Add(colId);

            DataGridViewTextBoxColumn colNama = new()
            {
                HeaderText = "Nama Produk",
                DataPropertyName = "Name",
                Width = 300
            };
            dataGridView1.Columns.Add(colNama);

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
        }

        private void TampilkanData()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = produkList.Values.ToList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional: highlight produk untuk ulasan
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnKirim_Click(object sender, EventArgs e)
        {
            // Tombol Kirim
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih produk terlebih dahulu.");
                return;
            }

            if (dataGridView1.SelectedRows.Count > 1)
            {
                MessageBox.Show("Pilih salah satu produk saja.");
                return;
            }

            string ratingStr = fieldRating.Text.Trim();
            string ulasan = textBox2.Text.Trim();

            if (!int.TryParse(ratingStr, out int rating) || rating < 1 || rating > 5)
            {
                MessageBox.Show("Masukkan rating antara 1 sampai 5.");
                return;
            }
            string namaProduk = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

            MessageBox.Show($"Rating untuk {namaProduk} berhasil dikirim !");

            fieldRating.Clear();
            textBox2.Clear();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            fieldRating.Clear();
            textBox2.Clear();
        }
    }
}