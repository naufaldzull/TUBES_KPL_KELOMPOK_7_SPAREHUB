namespace ManajemenToko
{
    partial class ManajemenToko
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridViewBarang1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridViewBarang1).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewBarang1
            // 
            dataGridViewBarang1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewBarang1.Location = new Point(1, 265);
            dataGridViewBarang1.Name = "dataGridViewBarang1";
            dataGridViewBarang1.RowHeadersWidth = 51;
            dataGridViewBarang1.Size = new Size(803, 457);
            dataGridViewBarang1.TabIndex = 0;
            // 
            // ManajemenToko
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridViewBarang1);
            Name = "ManajemenToko";
            Text = "ManajemenToko";
            Load += ManajemenToko_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewBarang1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridViewBarang1;
    }
}
