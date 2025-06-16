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
    /// Form sistem pemesanan untuk aplikasi SpareHub
    /// Mengelola input produk, keranjang belanja, dan proses checkout
    /// </summary>
    public partial class SistemPemesanan : Form
    {
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Dispose managed dan unmanaged resources
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false</param>
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

        /// <summary>
        /// Instance Order class untuk mengelola logika bisnis pemesanan
        /// </summary>
        private Order order;

        // Private fields: camelCase + English + full words (no abbreviations)
        private DataGridView cartDataGridView;           // camelCase + English + no abbreviation
        private TextBox productNameTextBox;              // camelCase + English + no underscore
        private TextBox quantityTextBox;                 // camelCase + English
        private TextBox priceTextBox;                    // camelCase + English
        private ComboBox paymentComboBox;                // camelCase
        private Button addToCartButton;                  // camelCase + English + full word
        private Button checkoutButton;                   // camelCase + full word
        private Button clearCartButton;                  // camelCase + descriptive name
        private Button backToMainMenuButton;             // camelCase + descriptive name
        private Label totalLabel;                        // camelCase
        private Label statusLabel;                       // camelCase
        private Label titleLabel;                        // camelCase

        /// <summary>
        /// Constructor - Inisialisasi form dan komponen sistem pemesanan
        /// </summary>
        public SistemPemesanan()
        {
            InitializeComponent();
            order = new Order();

            CreateInputSection();
            CreateCartSection();
            CreatePaymentSection();
            CreateNavigationSection();
            CreateStatusSection();

            SetupForm();
            LoadCartToGrid();
        }

        /// <summary>
        /// Inisialisasi komponen UI form
        /// </summary>
        private void InitializeComponent()
        {
            // Component initialization: camelCase + English
            productNameTextBox = new TextBox();          // camelCase + English
            quantityTextBox = new TextBox();             // camelCase + English
            priceTextBox = new TextBox();                // camelCase + English
            addToCartButton = new Button();              // camelCase + English + full word
            clearCartButton = new Button();              // camelCase + descriptive name
            checkoutButton = new Button();               // camelCase + full word
            backToMainMenuButton = new Button();         // camelCase + descriptive name
            cartDataGridView = new DataGridView();       // camelCase + English + no abbreviation
            totalLabel = new Label();
            statusLabel = new Label();
            paymentComboBox = new ComboBox();
            titleLabel = new Label();

            ((ISupportInitialize)cartDataGridView).BeginInit();
            SuspendLayout();
            // 
            // productNameTextBox
            // 
            productNameTextBox.Location = new Point(0, 0);
            productNameTextBox.Name = "productNameTextBox";    // Consistent naming
            productNameTextBox.Size = new Size(100, 27);
            productNameTextBox.TabIndex = 0;
            // 
            // quantityTextBox
            // 
            quantityTextBox.Location = new Point(0, 0);
            quantityTextBox.Name = "quantityTextBox";          // English + camelCase
            quantityTextBox.Size = new Size(100, 27);
            quantityTextBox.TabIndex = 0;
            // 
            // priceTextBox
            // 
            priceTextBox.Location = new Point(0, 0);
            priceTextBox.Name = "priceTextBox";                // English + camelCase
            priceTextBox.Size = new Size(100, 27);
            priceTextBox.TabIndex = 0;
            // 
            // addToCartButton
            // 
            addToCartButton.Location = new Point(0, 0);
            addToCartButton.Name = "addToCartButton";          // English + full word + camelCase
            addToCartButton.Size = new Size(75, 23);
            addToCartButton.TabIndex = 0;
            // 
            // clearCartButton
            // 
            clearCartButton.Location = new Point(0, 0);
            clearCartButton.Name = "clearCartButton";          // Descriptive name + camelCase
            clearCartButton.Size = new Size(75, 23);
            clearCartButton.TabIndex = 0;
            // 
            // checkoutButton
            // 
            checkoutButton.Location = new Point(0, 0);
            checkoutButton.Name = "checkoutButton";            // Full word + camelCase
            checkoutButton.Size = new Size(75, 23);
            checkoutButton.TabIndex = 0;
            // 
            // backToMainMenuButton
            // 
            backToMainMenuButton.Location = new Point(0, 0);
            backToMainMenuButton.Name = "backToMainMenuButton";    // Descriptive name + camelCase
            backToMainMenuButton.Size = new Size(75, 23);
            backToMainMenuButton.TabIndex = 0;
            // 
            // cartDataGridView
            // 
            cartDataGridView.ColumnHeadersHeight = 29;
            cartDataGridView.Location = new Point(0, 0);
            cartDataGridView.Name = "cartDataGridView";        // English + no abbreviation + camelCase
            cartDataGridView.RowHeadersWidth = 51;
            cartDataGridView.Size = new Size(240, 150);
            cartDataGridView.TabIndex = 0;
            // 
            // totalLabel
            // 
            totalLabel.Location = new Point(0, 0);
            totalLabel.Name = "totalLabel";
            totalLabel.Size = new Size(100, 23);
            totalLabel.TabIndex = 0;
            // 
            // statusLabel
            // 
            statusLabel.Location = new Point(0, 0);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(100, 23);
            statusLabel.TabIndex = 0;
            // 
            // paymentComboBox
            // 
            paymentComboBox.Location = new Point(0, 0);
            paymentComboBox.Name = "paymentComboBox";
            paymentComboBox.Size = new Size(121, 28);
            paymentComboBox.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.Font = new Font("Arial", 14F, FontStyle.Bold);
            titleLabel.ForeColor = Color.DarkBlue;
            titleLabel.Location = new Point(250, 10);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(400, 30);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "SISTEM PEMESANAN";
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            titleLabel.Click += titleLabel_Click;
            // 
            // SistemPemesanan
            // 
            BackColor = Color.White;
            ClientSize = new Size(900, 600);
            Controls.Add(titleLabel);
            Name = "SistemPemesanan";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SpareHub - Sistem Pemesanan";
            Load += SistemPemesanan_Load_1;
            ((ISupportInitialize)cartDataGridView).EndInit();
            ResumeLayout(false);
        }

        /// <summary>
        /// Membuat section input untuk nama produk, jumlah, dan harga
        /// </summary>
        private void CreateInputSection()
        {
            // Local variables: camelCase + English
            Label productNameLabel = new Label();             // camelCase + English
            productNameLabel.Text = "Nama Produk:";
            productNameLabel.Location = new Point(20, 60);
            productNameLabel.Size = new Size(100, 20);
            this.Controls.Add(productNameLabel);

            Label quantityLabel = new Label();                // camelCase + English
            quantityLabel.Text = "Jumlah:";
            quantityLabel.Location = new Point(300, 60);
            quantityLabel.Size = new Size(60, 20);
            this.Controls.Add(quantityLabel);

            Label priceLabel = new Label();                   // camelCase + English
            priceLabel.Text = "Harga:";
            priceLabel.Location = new Point(450, 60);
            priceLabel.Size = new Size(60, 20);
            this.Controls.Add(priceLabel);

            // TextBoxes configuration
            productNameTextBox.Location = new Point(20, 85);
            productNameTextBox.Size = new Size(250, 25);
            productNameTextBox.TextChanged += productNameTextBox_TextChanged;
            this.Controls.Add(productNameTextBox);

            quantityTextBox.Location = new Point(300, 85);
            quantityTextBox.Size = new Size(80, 25);
            quantityTextBox.TextChanged += quantityTextBox_TextChanged;
            this.Controls.Add(quantityTextBox);

            priceTextBox.Location = new Point(450, 85);
            priceTextBox.Size = new Size(120, 25);
            priceTextBox.TextChanged += priceTextBox_TextChanged;
            this.Controls.Add(priceTextBox);

            // Add Button configuration
            addToCartButton.Text = "Tambah ke Keranjang";
            addToCartButton.Location = new Point(600, 83);
            addToCartButton.Size = new Size(150, 30);
            addToCartButton.BackColor = Color.Blue;
            addToCartButton.ForeColor = Color.White;
            addToCartButton.Click += addToCartButton_Click;
            this.Controls.Add(addToCartButton);
        }

        /// <summary>
        /// Membuat section keranjang belanja dengan DataGridView dan tombol clear
        /// </summary>
        private void CreateCartSection()
        {
            Label cartLabel = new Label();                    // camelCase + English
            cartLabel.Text = "Keranjang Belanja:";
            cartLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            cartLabel.Location = new Point(20, 130);
            cartLabel.Size = new Size(200, 25);
            this.Controls.Add(cartLabel);

            // DataGridView setup
            cartDataGridView.Location = new Point(20, 160);
            cartDataGridView.Size = new Size(840, 200);
            cartDataGridView.ReadOnly = true;
            cartDataGridView.AllowUserToAddRows = false;
            cartDataGridView.AllowUserToDeleteRows = false;
            cartDataGridView.BackgroundColor = Color.LightGray;
            cartDataGridView.BorderStyle = BorderStyle.Fixed3D;
            cartDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            cartDataGridView.AutoGenerateColumns = true;
            cartDataGridView.CellContentClick += cartDataGridView_CellContentClick;
            this.Controls.Add(cartDataGridView);

            // Total label
            totalLabel.Text = "Total: Rp 0";
            totalLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            totalLabel.Location = new Point(650, 370);
            totalLabel.Size = new Size(200, 25);
            totalLabel.ForeColor = Color.DarkGreen;
            totalLabel.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(totalLabel);

            // Clear button configuration
            clearCartButton.Text = "Kosongkan Keranjang";
            clearCartButton.Location = new Point(20, 370);
            clearCartButton.Size = new Size(150, 30);
            clearCartButton.BackColor = Color.Blue;
            clearCartButton.ForeColor = Color.White;
            clearCartButton.FlatStyle = FlatStyle.Flat;
            clearCartButton.Click += clearCartButton_Click;
            this.Controls.Add(clearCartButton);
        }

        /// <summary>
        /// Membuat section pembayaran dengan ComboBox metode dan tombol checkout
        /// </summary>
        private void CreatePaymentSection()
        {
            Label paymentLabel = new Label();                 // camelCase
            paymentLabel.Text = "Metode Pembayaran:";
            paymentLabel.Location = new Point(20, 420);
            paymentLabel.Size = new Size(130, 20);
            this.Controls.Add(paymentLabel);

            // Payment ComboBox
            paymentComboBox.Location = new Point(160, 418);
            paymentComboBox.Size = new Size(150, 25);
            paymentComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Controls.Add(paymentComboBox);

            // Checkout button configuration
            checkoutButton.Text = "Proses Pesanan";
            checkoutButton.Location = new Point(350, 415);
            checkoutButton.Size = new Size(120, 30);
            checkoutButton.BackColor = Color.Blue;
            checkoutButton.ForeColor = Color.White;
            checkoutButton.FlatStyle = FlatStyle.Flat;
            checkoutButton.Click += checkoutButton_Click;
            this.Controls.Add(checkoutButton);
        }

        /// <summary>
        /// Membuat section navigasi dengan tombol kembali ke main menu
        /// </summary>
        private void CreateNavigationSection()
        {
            // Back to main menu button - positioned at top right corner
            backToMainMenuButton.Text = "Kembali ke Menu";
            backToMainMenuButton.Location = new Point(730, 15);
            backToMainMenuButton.Size = new Size(140, 35);
            backToMainMenuButton.BackColor = Color.Gray;
            backToMainMenuButton.ForeColor = Color.White;
            backToMainMenuButton.FlatStyle = FlatStyle.Flat;
            backToMainMenuButton.Font = new Font("Arial", 9, FontStyle.Bold);
            backToMainMenuButton.Click += backToMainMenuButton_Click;
            this.Controls.Add(backToMainMenuButton);
        }

        /// <summary>
        /// Membuat section status untuk menampilkan informasi sistem
        /// </summary>
        private void CreateStatusSection()
        {
            // Status label
            statusLabel.Text = "Siap menerima pesanan...";
            statusLabel.Location = new Point(20, 470);
            statusLabel.Size = new Size(840, 25);
            statusLabel.BackColor = Color.LightYellow;
            statusLabel.BorderStyle = BorderStyle.FixedSingle;
            statusLabel.Padding = new Padding(5, 0, 0, 0);
            statusLabel.TextAlign = ContentAlignment.MiddleLeft;
            this.Controls.Add(statusLabel);
        }

        /// <summary>
        /// Setup form dengan data dan event handlers
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
        /// Load data keranjang ke DataGridView dan update total
        /// </summary>
        private void LoadCartToGrid()
        {
            var cart = order.LoadCart();
            cartDataGridView.DataSource = cart;
            UpdateTotal();
        }

        /// <summary>
        /// Update label total berdasarkan isi keranjang
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
        /// Update status label dengan pesan baru
        /// </summary>
        /// <param name="message">Pesan status yang akan ditampilkan</param>
        private void UpdateStatus(string message)
        {
            statusLabel.Text = message;
        }

        /// <summary>
        /// Event handler ketika keranjang diupdate dari Order class
        /// </summary>
        /// <param name="cart">List item dalam keranjang yang telah diupdate</param>
        private void OnCartUpdated(List<CartItem> cart)
        {
            LoadCartToGrid();
        }

        // Event handlers: methodName_EventType pattern
        /// <summary>
        /// Event handler untuk perubahan text di nama produk
        /// </summary>
        private void productNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Event handler untuk perubahan text di jumlah
        /// </summary>
        private void quantityTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Event handler untuk perubahan text di harga
        /// </summary>
        private void priceTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Event handler untuk click di cell DataGridView keranjang
        /// </summary>
        private void cartDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// Event handler untuk tombol tambah ke keranjang
        /// Validasi input dan menambahkan produk ke keranjang
        /// </summary>
        private void addToCartButton_Click(object sender, EventArgs e)
        {
            try
            {
                string nama = productNameTextBox.Text.Trim();

                if (string.IsNullOrEmpty(nama))
                {
                    MessageBox.Show("Nama produk harus diisi!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int jumlah = int.Parse(quantityTextBox.Text);
                decimal harga = decimal.Parse(priceTextBox.Text);

                if (order.AddToCart(nama, jumlah, harga, out string error))
                {
                    MessageBox.Show("Produk berhasil ditambahkan!");

                    // Clear input fields
                    productNameTextBox.Clear();
                    quantityTextBox.Clear();
                    priceTextBox.Clear();
                    productNameTextBox.Focus();

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
        /// Event handler untuk tombol checkout
        /// Memproses pesanan dengan metode pembayaran yang dipilih
        /// </summary>
        private void checkoutButton_Click(object sender, EventArgs e)
        {
            var cart = order.LoadCart();
            if (cart.Count == 0)
            {
                MessageBox.Show("Keranjang kosong!");
                return;
            }

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
        /// Event handler untuk tombol kosongkan keranjang
        /// Menghapus semua item dari keranjang setelah konfirmasi
        /// </summary>
        private void clearCartButton_Click(object sender, EventArgs e)
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
        /// Event handler untuk tombol kembali ke main menu
        /// Menutup form sistem pemesanan dan kembali ke menu utama
        /// </summary>
        private void backToMainMenuButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Kembali ke menu utama?", "Konfirmasi",
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Clear keranjang jika ada isinya sebelum keluar
                if (order.LoadCart().Count > 0)
                {
                    var clearCart = MessageBox.Show("Kosongkan keranjang sebelum keluar?", "Konfirmasi",
                                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (clearCart == DialogResult.Yes)
                    {
                        order.ClearCart();
                    }
                }

                this.Close();
                
            }
        }

        // Public methods: PascalCase
        /// <summary>
        /// Mendapatkan data keranjang saat ini
        /// </summary>
        /// <returns>List item dalam keranjang</returns>
        public List<CartItem> GetCurrentCart()
        {
            return order.LoadCart();
        }

        /// <summary>
        /// Menambahkan produk dari external source ke form input
        /// </summary>
        /// <param name="productName">Nama produk yang akan ditambahkan</param>
        /// <param name="price">Harga produk</param>
        /// <returns>True jika berhasil, false jika gagal</returns>
        public bool AddProductFromExternal(string productName, decimal price)
        {
            productNameTextBox.Text = productName;
            priceTextBox.Text = price.ToString();
            quantityTextBox.Focus();
            return true;
        }
    }
}