using System;
using System.Windows.Forms;
using ManajemenToko;

namespace SpareHub
{
    public partial class MenuUtama : Form
    {
        public MenuUtama()
        {
            InitializeComponent();
            this.Hide();
        }

        private void btnPencarianProduk_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Fitur \"Pencarian Produk\" diklik!");
        }

        private void btnKelolaToko_Click(object sender, EventArgs e)
        {
            ManajemenToko.ManajemenToko formManajemenToko = new ManajemenToko.ManajemenToko();
            this.Hide();
            formManajemenToko.ShowDialog();
            this.Show();
        }

        private void btnPemesanan_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Fitur \"Pemesanan\" diklik!");
        }

        private void btnUlasanRating_Click(object sender, EventArgs e)
        {
            UlasanDanRatingProdukForm ulasanDanRatingProdukForm = new UlasanDanRatingProdukForm();
            this.Hide();
            ulasanDanRatingProdukForm.ShowDialog();
            this.Show();
        }

        private void btnWishlist_Click(object sender, EventArgs e)
        {
            Wishlist wishlistForm = new Wishlist();
            this.Hide();
            wishlistForm.ShowDialog();
            this.Show();
        }

        private void btnKeluhan_Click(object sender, EventArgs e)
        {
            CustomerServiceForm customerServiceForm = new CustomerServiceForm();
            this.Hide();
            customerServiceForm.ShowDialog();
            this.Show();
        }
    }
}