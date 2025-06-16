using System;
using System.Windows.Forms;

namespace SpareHub
{
    /// <summary>
    /// Form utama navigasi aplikasi SpareHub.
    /// </summary>
    public partial class MenuUtama : Form
    {
        public MenuUtama()
        {
            InitializeComponent();
            Hide(); // auto-hide saat diinisialisasi (perlu dijelaskan kenapa)
        }

        /// <summary>
        /// Membuka form target secara modal, sambil menyembunyikan form utama sementara.
        /// </summary>
        /// <param name="form">Instance form yang ingin dibuka</param>
        private void OpenFormDialog(Form form)
        {
            Hide();
            form.ShowDialog();
            Show();
        }

        private void BtnPencarianProduk_Click(object sender, EventArgs e)
        {
            var searchingView = new SearchingView();
            OpenFormDialog(searchingView);
        }

        private void BtnKelolaToko_Click(object sender, EventArgs e)
        {
            var formManajemenToko = new ManajemenToko.ManajemenToko();
            OpenFormDialog(formManajemenToko);
        }

        private void BtnPemesanan_Click(object sender, EventArgs e)
        {
            var sistemPemesananForm = new SistemPemesanan();
            OpenFormDialog(sistemPemesananForm);
        }

        private void BtnUlasanRating_Click(object sender, EventArgs e)
        {
            var ulasanForm = new UlasanDanRatingProdukForm();
            OpenFormDialog(ulasanForm);
        }

        private void BtnWishlist_Click(object sender, EventArgs e)
        {
            var wishlistForm = new Wishlist();
            OpenFormDialog(wishlistForm);
        }

        private void BtnKeluhan_Click(object sender, EventArgs e)
        {
            var customerServiceForm = new CustomerServiceForm();
            OpenFormDialog(customerServiceForm);
        }

        private void BtnLihatProduk_Click(object sender, EventArgs e)
        {
            var lihatBarangForm = new LihatBarangAll();
            OpenFormDialog(lihatBarangForm);
        }
    }
}