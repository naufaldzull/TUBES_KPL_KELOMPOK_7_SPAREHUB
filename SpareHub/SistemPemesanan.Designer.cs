using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using fitur_Order;

namespace SpareHub
{
    /// <summary>
    /// Form pemesanan sederhana untuk SpareHub
    /// SIMPLE: Gak ribet, mudah dipahami pemula
    /// </summary>
    public partial class SistemPemesanan : Form
    {
        /// <summary>
        /// Required designer variable - PERBAIKAN: Tambah ini untuk fix error
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used - PERBAIKAN: Override Dispose yang proper
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }

                // Cleanup order events
                if (order != null)
                {
                    order.OnStatusUpdate -= UpdateStatus;
                    order.OnCartUpdated -= OnCartUpdated;
                }
            }
            base.Dispose(disposing);
        }
        private Order order;

        // Controls - nama simple dan jelas
        private DataGridView keranjangDGV;
        private TextBox nama_produkTxtBox;
        private TextBox jumlahTxtBox;
        private TextBox hargaTxtBox;
        private ComboBox paymentComboBox;
        private Button tambah_ke_keranjangBTN;
        private Button checkoutBTN;
        private Button clearBTN;
        private Label totalLabel;
        private Label statusLabel;


        public SistemPemesanan()
        {
            InitializeComponent();
            order = new Order();
            SetupForm();
            LoadCartToGrid();
        }


        /// <summary>
        /// Setup form - SIMPLE: Semua dalam satu method
        /// </summary>
        private void InitializeComponent()
        {
            // Tambahkan inisialisasi sebelum setting properti
            nama_produkTxtBox = new TextBox();
            jumlahTxtBox = new TextBox();
            hargaTxtBox = new TextBox();
            tambah_ke_keranjangBTN = new Button();
            clearBTN = new Button();
            checkoutBTN = new Button();
            keranjangDGV = new DataGridView();
            totalLabel = new Label();
            statusLabel = new Label();
            paymentComboBox = new ComboBox();


            // Form settings
            this.Text = "SpareHub - Sistem Pemesanan";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            // Title
            Label titleLabel = new Label();
            titleLabel.Text = "SISTEM PEMESANAN";
            titleLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            titleLabel.Location = new Point(250, 10);
            titleLabel.Size = new Size(400, 30);
            titleLabel.ForeColor = Color.DarkBlue;
            this.Controls.Add(titleLabel);

            // Input Section
            CreateInputSection();

            // Cart Section  
            CreateCartSection();

            // Payment Section
            CreatePaymentSection();

            // Status Section
            CreateStatusSection();
        }

        /// <summary>
        /// Buat bagian input produk - SIMPLE: Gak pake groupbox ribet
        /// </summary>
        private void CreateInputSection()
        {
            // Labels
            Label namaLabel = new Label();
            namaLabel.Text = "Nama Produk:";
            namaLabel.Location = new Point(20, 60);
            namaLabel.Size = new Size(100, 20);
            this.Controls.Add(namaLabel);

            Label jumlahLabel = new Label();
            jumlahLabel.Text = "Jumlah:";
            jumlahLabel.Location = new Point(300, 60);
            jumlahLabel.Size = new Size(60, 20);
            this.Controls.Add(jumlahLabel);

            Label hargaLabel = new Label();
            hargaLabel.Text = "Harga:";
            hargaLabel.Location = new Point(450, 60);
            hargaLabel.Size = new Size(60, 20);
            this.Controls.Add(hargaLabel);

            // TextBoxes - PERBAIKAN: Gak perlu new lagi karena udah di-initialize

            nama_produkTxtBox.Location = new Point(20, 85);
            nama_produkTxtBox.Size = new Size(250, 25);
            nama_produkTxtBox.TextChanged += nama_produkTxtBox_TextChanged;
            this.Controls.Add(nama_produkTxtBox);

            jumlahTxtBox.Location = new Point(300, 85);
            jumlahTxtBox.Size = new Size(80, 25);
            jumlahTxtBox.TextChanged += jumlahTxtBox_TextChanged;
            this.Controls.Add(jumlahTxtBox);

            hargaTxtBox.Location = new Point(450, 85);
            hargaTxtBox.Size = new Size(120, 25);
            hargaTxtBox.TextChanged += hargaTxtBox_TextChanged;
            this.Controls.Add(hargaTxtBox);

            // Add Button - PERBAIKAN: Gak perlu new lagi
            tambah_ke_keranjangBTN.Text = "Tambah ke Keranjang";
            tambah_ke_keranjangBTN.Location = new Point(600, 83);
            tambah_ke_keranjangBTN.Size = new Size(150, 30);
            tambah_ke_keranjangBTN.BackColor = Color.Green;
            tambah_ke_keranjangBTN.ForeColor = Color.White;
            tambah_ke_keranjangBTN.Click += tambah_ke_keranjangBTN_Click;
            this.Controls.Add(tambah_ke_keranjangBTN);
        }

        /// <summary>
        /// Buat bagian keranjang - SIMPLE: Langsung bikin DataGridView
        /// </summary>
        private void CreateCartSection()
        {
            Label cartLabel = new Label();
            cartLabel.Text = "Keranjang Belanja:";
            cartLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            cartLabel.Location = new Point(20, 130);
            cartLabel.Size = new Size(200, 25);
            this.Controls.Add(cartLabel);

            // PERBAIKAN: Gak perlu new lagi
            keranjangDGV.Location = new Point(20, 160);
            keranjangDGV.Size = new Size(840, 200);
            keranjangDGV.ReadOnly = true;
            keranjangDGV.AllowUserToAddRows = false;
            keranjangDGV.BackgroundColor = Color.LightGray;
            keranjangDGV.CellContentClick += keranjangDGV_CellContentClick;
            this.Controls.Add(keranjangDGV);

            // Total dan Clear button - PERBAIKAN: Gak perlu new lagi
            totalLabel.Text = "Total: Rp 0";
            totalLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            totalLabel.Location = new Point(650, 370);
            totalLabel.Size = new Size(200, 25);
            totalLabel.ForeColor = Color.DarkGreen;
            this.Controls.Add(totalLabel);

            clearBTN.Text = "Kosongkan Keranjang";
            clearBTN.Location = new Point(20, 370);
            clearBTN.Size = new Size(150, 30);
            clearBTN.BackColor = Color.Orange;
            clearBTN.ForeColor = Color.White;
            clearBTN.Click += clearBTN_Click;
            this.Controls.Add(clearBTN);
        }

        /// <summary>
        /// Buat bagian pembayaran - SIMPLE: Combo + Button aja
        /// </summary>
        private void CreatePaymentSection()
        {
            Label paymentLabel = new Label();
            paymentLabel.Text = "Metode Pembayaran:";
            paymentLabel.Location = new Point(20, 420);
            paymentLabel.Size = new Size(130, 20);
            this.Controls.Add(paymentLabel);

            // PERBAIKAN: Gak perlu new lagi
            paymentComboBox.Location = new Point(160, 418);
            paymentComboBox.Size = new Size(150, 25);
            paymentComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Controls.Add(paymentComboBox);

            checkoutBTN.Text = "Proses Pesanan";
            checkoutBTN.Location = new Point(350, 415);
            checkoutBTN.Size = new Size(120, 30);
            checkoutBTN.BackColor = Color.Blue;
            checkoutBTN.ForeColor = Color.White;
            checkoutBTN.Click += checkoutBTN_Click;
            this.Controls.Add(checkoutBTN);
        }

        /// <summary>
        /// Buat status bar - SIMPLE: Label aja
        /// </summary>
        private void CreateStatusSection()
        {
            // PERBAIKAN: Gak perlu new lagi
            statusLabel.Text = "Siap menerima pesanan...";
            statusLabel.Location = new Point(20, 520);
            statusLabel.Size = new Size(840, 25);
            statusLabel.BackColor = Color.LightYellow;
            statusLabel.BorderStyle = BorderStyle.FixedSingle;
            statusLabel.Padding = new Padding(5, 0, 0, 0);
            this.Controls.Add(statusLabel);
        }

        /// <summary>
        /// Setup awal form - SIMPLE: Load data aja
        /// </summary>
        private void SetupForm()
        {
            // Load payment methods
            var payments = order.GetPaymentMethods();
            paymentComboBox.DataSource = payments;

            // Subscribe ke events
            order.OnStatusUpdate += UpdateStatus;
            order.OnCartUpdated += OnCartUpdated;
        }

        /// <summary>
        /// Load cart ke grid - KEEP: Method original lu
        /// </summary>
        private void LoadCartToGrid()
        {
            var cart = order.LoadCart();
            keranjangDGV.DataSource = cart;
            UpdateTotal();
        }

        /// <summary>
        /// Update total harga - SIMPLE: Hitung total aja
        /// </summary>
        private void UpdateTotal()
        {
            var cart = order.LoadCart();
            decimal total = 0;
            foreach (var item in cart)
            {
                total += item.Quantity * item.Price;
            }
            totalLabel.Text = $"Total: Rp {total:N0}";
        }

        /// <summary>
        /// Update status label - SIMPLE: Ganti text aja
        /// </summary>
        private void UpdateStatus(string message)
        {
            statusLabel.Text = message;
        }

        /// <summary>
        /// Event ketika cart diupdate - SIMPLE: Refresh grid
        /// </summary>
        private void OnCartUpdated(List<CartItem> cart)
        {
            LoadCartToGrid();
        }

        

        private void nama_produkTxtBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void jumlahTxtBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void hargaTxtBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void keranjangDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// Tambah produk ke keranjang - IMPROVED: Pake method baru tapi tetep simple
        /// </summary>
        private void tambah_ke_keranjangBTN_Click(object sender, EventArgs e)
        {
            try
            {
                string nama = nama_produkTxtBox.Text.Trim();

                if (string.IsNullOrEmpty(nama))
                {
                    MessageBox.Show("Nama produk harus diisi!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int jumlah = int.Parse(jumlahTxtBox.Text);
                decimal harga = decimal.Parse(hargaTxtBox.Text);

                if (order.AddToCart(nama, jumlah, harga, out string error))
                {
                    MessageBox.Show("Produk berhasil ditambahkan!");

                    // Clear input
                    nama_produkTxtBox.Clear();
                    jumlahTxtBox.Clear();
                    hargaTxtBox.Clear();
                    nama_produkTxtBox.Focus();

                    LoadCartToGrid();
                }
                else
                {
                    MessageBox.Show("Error: " + error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Proses checkout - IMPROVED: Pake method baru
        /// </summary>
        private void checkoutBTN_Click(object sender, EventArgs e)
        {
            var cart = order.LoadCart();
            if (cart.Count == 0)
            {
                MessageBox.Show("Keranjang kosong!");
                return;
            }

            // Konfirmasi
            var result = MessageBox.Show("Proses pesanan sekarang?", "Konfirmasi",
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int paymentIndex = paymentComboBox.SelectedIndex;

                if (order.ProcessOrder(paymentIndex, out string message, out decimal total))
                {
                    MessageBox.Show(message, "Pesanan Berhasil!");
                    LoadCartToGrid();
                }
                else
                {
                    MessageBox.Show(message, "Error!");
                }
            }
        }

        /// <summary>
        /// Clear keranjang - SIMPLE: Method baru
        /// </summary>
        private void clearBTN_Click(object sender, EventArgs e)
        {
            if (order.LoadCart().Count == 0)
            {
                MessageBox.Show("Keranjang sudah kosong!");
                return;
            }

            var result = MessageBox.Show("Kosongkan keranjang?", "Konfirmasi",
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                order.ClearCart();
                LoadCartToGrid();
                MessageBox.Show("Keranjang dikosongkan!");
            }
        }

        /// <summary>
        /// Cleanup ketika form ditutup - SUDAH ADA DI ATAS, HAPUS INI
        /// </summary>
        // HAPUS: Method dispose yang duplicate

        // PERBAIKAN: Tambah method untuk external access (opsional)
        /// <summary>
        /// Get current cart data untuk integration dengan fitur lain
        /// </summary>
        public List<CartItem> GetCurrentCart()
        {
            return order.LoadCart();
        }

        /// <summary>
        /// Add product from external source (untuk integration)
        /// </summary>
        public bool AddProductFromExternal(string productName, decimal price)
        {
            nama_produkTxtBox.Text = productName;
            hargaTxtBox.Text = price.ToString();
            jumlahTxtBox.Focus();
            return true;
        }
    }
}