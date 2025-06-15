using System.Drawing;
using System.Windows.Forms;

namespace SpareHub
{
    partial class MenuUtama
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Text = "SpareHub - Menu Utama";
            this.ClientSize = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // ========== SIDEBAR KIRI ==========
            Panel sidebar = new Panel();
            sidebar.BackColor = Color.RoyalBlue;
            sidebar.Dock = DockStyle.Left;
            sidebar.Width = 300;
            this.Controls.Add(sidebar);

            Label lblLogo = new Label();
            lblLogo.Text = "YOUR LOGO";
            lblLogo.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(30, 40);
            sidebar.Controls.Add(lblLogo);

            Label lblWelcome = new Label();
            lblWelcome.Text = "Hello,\nwelcome!";
            lblWelcome.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblWelcome.ForeColor = Color.White;
            lblWelcome.Location = new Point(30, 100);
            lblWelcome.AutoSize = true;
            sidebar.Controls.Add(lblWelcome);

            Label lblTagline = new Label();
            lblTagline.Text = "Kelola toko, belanja, ulas, dan simpan favoritmu.";
            lblTagline.Font = new Font("Segoe UI", 9);
            lblTagline.ForeColor = Color.WhiteSmoke;
            lblTagline.Location = new Point(30, 210);
            lblTagline.Size = new Size(240, 60);
            sidebar.Controls.Add(lblTagline);

            // ========== MENU TOMBOL ==========
            string[] menuLabels = {
                "🔍 Pencarian Produk",
                "🏪 Kelola Toko",
                "🛒 Pemesanan",
                "🌟 Ulasan & Rating",
                "❤️ Wishlist",
                "📞 Keluhan"
            };

            int startX = 340;
            int startY = 80;
            int spacing = 60;

            for (int i = 0; i < menuLabels.Length; i++)
            {
                Button btn = new Button();
                btn.Text = menuLabels[i];
                btn.Location = new Point(startX, startY + i * spacing);
                btn.Size = new Size(220, 40);
                btn.Font = new Font("Segoe UI", 10);
                btn.BackColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderColor = Color.RoyalBlue;
                btn.FlatAppearance.BorderSize = 1;
                btn.Cursor = Cursors.Hand;
                btn.Click += (s, e) => MessageBox.Show($"Fitur \"{btn.Text}\" diklik!");
                this.Controls.Add(btn);
            }
        }
    }
}