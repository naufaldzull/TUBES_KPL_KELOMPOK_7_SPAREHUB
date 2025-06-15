namespace SpareHub
{
    partial class Wishlist
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.textBox1 = new TextBox();
            this.button1 = new Button();
            this.button2 = new Button();
            this.label1 = new Label();
            this.listBox1 = new ListBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(632, 27);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyPress += new KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // button1
            // 
            this.button1.Location = new Point(675, 12);
            this.button1.Name = "button1";
            this.button1.Size = new Size(94, 29);
            this.button1.TabIndex = 1;
            this.button1.Text = "Tambah";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new Point(675, 60);
            this.button2.Name = "button2";
            this.button2.Size = new Size(94, 29);
            this.button2.TabIndex = 2;
            this.button2.Text = "Hapus";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Enabled = false; // nonaktif awalnya
            this.button2.Click += new EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 411);
            this.label1.Name = "label1";
            this.label1.Size = new Size(49, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Status";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new Point(12, 45);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(632, 344);
            this.listBox1.TabIndex = 5;
            this.listBox1.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);
            this.listBox1.DoubleClick += new EventHandler(this.listBox1_DoubleClick);
            // 
            // Wishlist
            // 
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 450);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Name = "Wishlist";
            this.Text = "Wishlist";
            this.Load += new EventHandler(this.Wishlist_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button button1;
        private Button button2;
        private Label label1;
        private ListBox listBox1;
    }
}
