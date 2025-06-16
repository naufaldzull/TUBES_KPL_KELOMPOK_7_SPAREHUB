using System.Drawing;
using System.Windows.Forms;

namespace SpareHub
{
    partial class MenuUtama
    {
        private System.ComponentModel.IContainer components = null;

        private Panel sidebar;
        private Label lblWelcome;
        private Label lblTagline;

        private Button btnPencarianProduk;
        private Button btnKelolaToko;
        private Button btnPemesanan;
        private Button btnUlasanRating;
        private Button btnWishlist;
        private Button btnKeluhan;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            sidebar = new Panel();
            lblWelcome = new Label();
            lblTagline = new Label();
            btnPencarianProduk = new Button();
            btnKelolaToko = new Button();
            btnPemesanan = new Button();
            btnUlasanRating = new Button();
            btnWishlist = new Button();
            btnKeluhan = new Button();
            sidebar.SuspendLayout();
            SuspendLayout();
            // 
            // sidebar
            // 
            sidebar.BackColor = Color.RoyalBlue;
            sidebar.Controls.Add(lblWelcome);
            sidebar.Controls.Add(lblTagline);
            sidebar.Dock = DockStyle.Left;
            sidebar.Location = new Point(0, 0);
            sidebar.Name = "sidebar";
            sidebar.Size = new Size(300, 600);
            sidebar.TabIndex = 0;
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.White;
            lblWelcome.Location = new Point(30, 100);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(203, 108);
            lblWelcome.TabIndex = 1;
            lblWelcome.Text = "Hello,\nwelcome!";
            // 
            // lblTagline
            // 
            lblTagline.Font = new Font("Segoe UI", 9F);
            lblTagline.ForeColor = Color.WhiteSmoke;
            lblTagline.Location = new Point(30, 210);
            lblTagline.Name = "lblTagline";
            lblTagline.Size = new Size(240, 60);
            lblTagline.TabIndex = 2;
            lblTagline.Text = "Kelola toko, belanja, ulas, dan simpan favoritmu.";
            // 
            // btnPencarianProduk
            // 
            btnPencarianProduk.BackColor = Color.White;
            btnPencarianProduk.Cursor = Cursors.Hand;
            btnPencarianProduk.FlatAppearance.BorderColor = Color.RoyalBlue;
            btnPencarianProduk.FlatStyle = FlatStyle.Flat;
            btnPencarianProduk.Font = new Font("Segoe UI", 10F);
            btnPencarianProduk.Location = new Point(484, 90);
            btnPencarianProduk.Name = "btnPencarianProduk";
            btnPencarianProduk.Size = new Size(220, 40);
            btnPencarianProduk.TabIndex = 1;
            btnPencarianProduk.Text = "🔍 Pencarian Produk";
            btnPencarianProduk.UseVisualStyleBackColor = false;
            btnPencarianProduk.Click += btnPencarianProduk_Click;
            // 
            // btnKelolaToko
            // 
            btnKelolaToko.BackColor = Color.White;
            btnKelolaToko.Cursor = Cursors.Hand;
            btnKelolaToko.FlatAppearance.BorderColor = Color.RoyalBlue;
            btnKelolaToko.FlatStyle = FlatStyle.Flat;
            btnKelolaToko.Font = new Font("Segoe UI", 10F);
            btnKelolaToko.Location = new Point(484, 150);
            btnKelolaToko.Name = "btnKelolaToko";
            btnKelolaToko.Size = new Size(220, 40);
            btnKelolaToko.TabIndex = 2;
            btnKelolaToko.Text = "🏪 Kelola Toko";
            btnKelolaToko.UseVisualStyleBackColor = false;
            btnKelolaToko.Click += btnKelolaToko_Click;
            // 
            // btnPemesanan
            // 
            btnPemesanan.BackColor = Color.White;
            btnPemesanan.Cursor = Cursors.Hand;
            btnPemesanan.FlatAppearance.BorderColor = Color.RoyalBlue;
            btnPemesanan.FlatStyle = FlatStyle.Flat;
            btnPemesanan.Font = new Font("Segoe UI", 10F);
            btnPemesanan.Location = new Point(484, 210);
            btnPemesanan.Name = "btnPemesanan";
            btnPemesanan.Size = new Size(220, 40);
            btnPemesanan.TabIndex = 3;
            btnPemesanan.Text = "\U0001f6d2 Pemesanan";
            btnPemesanan.UseVisualStyleBackColor = false;
            btnPemesanan.Click += btnPemesanan_Click;
            // 
            // btnUlasanRating
            // 
            btnUlasanRating.BackColor = Color.White;
            btnUlasanRating.Cursor = Cursors.Hand;
            btnUlasanRating.FlatAppearance.BorderColor = Color.RoyalBlue;
            btnUlasanRating.FlatStyle = FlatStyle.Flat;
            btnUlasanRating.Font = new Font("Segoe UI", 10F);
            btnUlasanRating.Location = new Point(484, 270);
            btnUlasanRating.Name = "btnUlasanRating";
            btnUlasanRating.Size = new Size(220, 40);
            btnUlasanRating.TabIndex = 4;
            btnUlasanRating.Text = "🌟 Ulasan & Rating";
            btnUlasanRating.UseVisualStyleBackColor = false;
            btnUlasanRating.Click += btnUlasanRating_Click;
            // 
            // btnWishlist
            // 
            btnWishlist.BackColor = Color.White;
            btnWishlist.Cursor = Cursors.Hand;
            btnWishlist.FlatAppearance.BorderColor = Color.RoyalBlue;
            btnWishlist.FlatStyle = FlatStyle.Flat;
            btnWishlist.Font = new Font("Segoe UI", 10F);
            btnWishlist.Location = new Point(484, 330);
            btnWishlist.Name = "btnWishlist";
            btnWishlist.Size = new Size(220, 40);
            btnWishlist.TabIndex = 5;
            btnWishlist.Text = "❤️ Wishlist";
            btnWishlist.UseVisualStyleBackColor = false;
            btnWishlist.Click += btnWishlist_Click;
            // 
            // btnKeluhan
            // 
            btnKeluhan.BackColor = Color.White;
            btnKeluhan.Cursor = Cursors.Hand;
            btnKeluhan.FlatAppearance.BorderColor = Color.RoyalBlue;
            btnKeluhan.FlatStyle = FlatStyle.Flat;
            btnKeluhan.Font = new Font("Segoe UI", 10F);
            btnKeluhan.Location = new Point(484, 390);
            btnKeluhan.Name = "btnKeluhan";
            btnKeluhan.Size = new Size(220, 40);
            btnKeluhan.TabIndex = 6;
            btnKeluhan.Text = "📞 Keluhan";
            btnKeluhan.UseVisualStyleBackColor = false;
            btnKeluhan.Click += btnKeluhan_Click;
            // 
            // MenuUtama
            // 
            ClientSize = new Size(900, 600);
            Controls.Add(sidebar);
            Controls.Add(btnPencarianProduk);
            Controls.Add(btnKelolaToko);
            Controls.Add(btnPemesanan);
            Controls.Add(btnUlasanRating);
            Controls.Add(btnWishlist);
            Controls.Add(btnKeluhan);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "MenuUtama";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SpareHub - Menu Utama";
            sidebar.ResumeLayout(false);
            sidebar.PerformLayout();
            ResumeLayout(false);
        }
    }
}