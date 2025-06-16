namespace SpareHub
{
    partial class UlasanDanRatingProdukForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            dataGridView1 = new DataGridView();
            fieldRating = new TextBox();
            label2 = new Label();
            label3 = new Label();
            textBox2 = new TextBox();
            btnKirim = new Button();
            btnClear = new Button();
            labelJudul = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(6, 56);
            label1.Name = "label1";
            label1.Size = new Size(248, 28);
            label1.TabIndex = 0;
            label1.Text = "Pilih Produk untuk Dirating";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 87);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 300;
            dataGridView1.Size = new Size(1045, 113);
            dataGridView1.TabIndex = 1;
            // 
            // fieldRating
            // 
            fieldRating.Location = new Point(155, 219);
            fieldRating.Name = "fieldRating";
            fieldRating.Size = new Size(27, 27);
            fieldRating.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 222);
            label2.Name = "label2";
            label2.Size = new Size(137, 20);
            label2.TabIndex = 3;
            label2.Text = "Berikan rating (1-5)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 264);
            label3.Name = "label3";
            label3.Size = new Size(115, 20);
            label3.TabIndex = 4;
            label3.Text = "Deskripsi ulasan";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(155, 264);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(902, 154);
            textBox2.TabIndex = 5;
            // 
            // btnKirim
            // 
            btnKirim.BackColor = Color.LightGreen;
            btnKirim.Location = new Point(453, 462);
            btnKirim.Name = "btnKirim";
            btnKirim.Size = new Size(94, 29);
            btnKirim.TabIndex = 6;
            btnKirim.Text = "Submit";
            btnKirim.UseVisualStyleBackColor = false;
            btnKirim.Click += BtnKirim_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(570, 462);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(94, 29);
            btnClear.TabIndex = 7;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += BtnBersihkan_Click;
            // 
            // labelJudul
            // 
            labelJudul.AutoSize = true;
            labelJudul.Font = new Font("Arial", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelJudul.ForeColor = Color.Black;
            labelJudul.Location = new Point(432, 9);
            labelJudul.Name = "labelJudul";
            labelJudul.Size = new Size(217, 33);
            labelJudul.TabIndex = 8;
            labelJudul.Text = "ULAS PRODUK";
            // 
            // UlasanDanRatingProduk
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1069, 565);
            Controls.Add(labelJudul);
            Controls.Add(btnClear);
            Controls.Add(btnKirim);
            Controls.Add(textBox2);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(fieldRating);
            Controls.Add(dataGridView1);
            Controls.Add(label1);
            Name = "UlasanDanRatingProduk";
            Text = "UlasanDanRatingProduk";
            Load += UlasanDanRatingProdukForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DataGridView dataGridView1;
        private TextBox fieldRating;
        private Label label2;
        private Label label3;
        private TextBox textBox2;
        private Button btnKirim;
        private Button btnClear;
        private Label labelJudul;
    }
}