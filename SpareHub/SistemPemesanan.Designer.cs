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
    public partial class SistemPemesanan : Form
    {
        private System.ComponentModel.IContainer components = null;

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
        private Label titleLabel;

        public SistemPemesanan()
        {
            InitializeComponent();
            order = new Order();

            // PERBAIKAN: Panggil semua method create sections!
            CreateInputSection();
            CreateCartSection();
            CreatePaymentSection();
            CreateStatusSection();

            SetupForm();
            LoadCartToGrid();
        }

        private void InitializeComponent()
        {
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
            titleLabel = new Label();
            ((ISupportInitialize)keranjangDGV).BeginInit();
            SuspendLayout();
            // 
            // nama_produkTxtBox
            // 
            nama_produkTxtBox.Location = new Point(0, 0);
            nama_produkTxtBox.Name = "nama_produkTxtBox";
            nama_produkTxtBox.Size = new Size(100, 27);
            nama_produkTxtBox.TabIndex = 0;
            // 
            // jumlahTxtBox
            // 
            jumlahTxtBox.Location = new Point(0, 0);
            jumlahTxtBox.Name = "jumlahTxtBox";
            jumlahTxtBox.Size = new Size(100, 27);
            jumlahTxtBox.TabIndex = 0;
            // 
            // hargaTxtBox
            // 
            hargaTxtBox.Location = new Point(0, 0);
            hargaTxtBox.Name = "hargaTxtBox";
            hargaTxtBox.Size = new Size(100, 27);
            hargaTxtBox.TabIndex = 0;
            // 
            // tambah_ke_keranjangBTN
            // 
            tambah_ke_keranjangBTN.Location = new Point(0, 0);
            tambah_ke_keranjangBTN.Name = "tambah_ke_keranjangBTN";
            tambah_ke_keranjangBTN.Size = new Size(75, 23);
            tambah_ke_keranjangBTN.TabIndex = 0;
            // 
            // clearBTN
            // 
            clearBTN.Location = new Point(0, 0);
            clearBTN.Name = "clearBTN";
            clearBTN.Size = new Size(75, 23);
            clearBTN.TabIndex = 0;
            // 
            // checkoutBTN
            // 
            checkoutBTN.Location = new Point(0, 0);
            checkoutBTN.Name = "checkoutBTN";
            checkoutBTN.Size = new Size(75, 23);
            checkoutBTN.TabIndex = 0;
            // 
            // keranjangDGV
            // 
            keranjangDGV.ColumnHeadersHeight = 29;
            keranjangDGV.Location = new Point(0, 0);
            keranjangDGV.Name = "keranjangDGV";
            keranjangDGV.RowHeadersWidth = 51;
            keranjangDGV.Size = new Size(240, 150);
            keranjangDGV.TabIndex = 0;
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
            ((ISupportInitialize)keranjangDGV).EndInit();
            ResumeLayout(false);
        }

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

            // TextBoxes 
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

            // Add Button
            tambah_ke_keranjangBTN.Text = "Tambah ke Keranjang";
            tambah_ke_keranjangBTN.Location = new Point(600, 83);
            tambah_ke_keranjangBTN.Size = new Size(150, 30);
            tambah_ke_keranjangBTN.BackColor = Color.Blue;
            tambah_ke_keranjangBTN.ForeColor = Color.White;
            tambah_ke_keranjangBTN.Click += tambah_ke_keranjangBTN_Click;
            this.Controls.Add(tambah_ke_keranjangBTN);
        }

        private void CreateCartSection()
        {
            Label cartLabel = new Label();
            cartLabel.Text = "Keranjang Belanja:";
            cartLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            cartLabel.Location = new Point(20, 130);
            cartLabel.Size = new Size(200, 25);
            this.Controls.Add(cartLabel);

            // Setup DataGridView
            keranjangDGV.Location = new Point(20, 160);
            keranjangDGV.Size = new Size(840, 200);
            keranjangDGV.ReadOnly = true;
            keranjangDGV.AllowUserToAddRows = false;
            keranjangDGV.AllowUserToDeleteRows = false;
            keranjangDGV.BackgroundColor = Color.LightGray;
            keranjangDGV.BorderStyle = BorderStyle.Fixed3D;
            keranjangDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            keranjangDGV.AutoGenerateColumns = true;
            keranjangDGV.CellContentClick += keranjangDGV_CellContentClick;
            this.Controls.Add(keranjangDGV);

            // Total label
            totalLabel.Text = "Total: Rp 0";
            totalLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            totalLabel.Location = new Point(650, 370);
            totalLabel.Size = new Size(200, 25);
            totalLabel.ForeColor = Color.DarkGreen;
            totalLabel.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(totalLabel);

            // Clear button
            clearBTN.Text = "Kosongkan Keranjang";
            clearBTN.Location = new Point(20, 370);
            clearBTN.Size = new Size(150, 30);
            clearBTN.BackColor = Color.Blue;
            clearBTN.ForeColor = Color.White;
            clearBTN.FlatStyle = FlatStyle.Flat;
            clearBTN.Click += clearBTN_Click;
            this.Controls.Add(clearBTN);
        }

        private void CreatePaymentSection()
        {
            Label paymentLabel = new Label();
            paymentLabel.Text = "Metode Pembayaran:";
            paymentLabel.Location = new Point(20, 420);
            paymentLabel.Size = new Size(130, 20);
            this.Controls.Add(paymentLabel);

            // Payment ComboBox
            paymentComboBox.Location = new Point(160, 418);
            paymentComboBox.Size = new Size(150, 25);
            paymentComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Controls.Add(paymentComboBox);

            // Checkout button
            checkoutBTN.Text = "Proses Pesanan";
            checkoutBTN.Location = new Point(350, 415);
            checkoutBTN.Size = new Size(120, 30);
            checkoutBTN.BackColor = Color.Blue;
            checkoutBTN.ForeColor = Color.White;
            checkoutBTN.FlatStyle = FlatStyle.Flat;
            checkoutBTN.Click += checkoutBTN_Click;
            this.Controls.Add(checkoutBTN);
        }

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

        private void SetupForm()
        {
            // Load payment methods
            var payments = order.GetPaymentMethods();
            paymentComboBox.DataSource = payments;

            // Subscribe ke events
            order.OnStatusUpdate += UpdateStatus;
            order.OnCartUpdated += OnCartUpdated;
        }

        private void LoadCartToGrid()
        {
            var cart = order.LoadCart();
            keranjangDGV.DataSource = cart;
            UpdateTotal();
        }

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

        private void UpdateStatus(string message)
        {
            statusLabel.Text = message;
        }

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

        private void checkoutBTN_Click(object sender, EventArgs e)
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

        public List<CartItem> GetCurrentCart()
        {
            return order.LoadCart();
        }

        public bool AddProductFromExternal(string productName, decimal price)
        {
            nama_produkTxtBox.Text = productName;
            hargaTxtBox.Text = price.ToString();
            jumlahTxtBox.Focus();
            return true;
        }
    }
}