namespace SpareHub
{
    partial class CustomerServiceForm
    {
        private System.Windows.Forms.ListBox _listBoxMessages;
        private System.Windows.Forms.Button _buttonLoadMessages;

        private void InitializeComponent()
        {
            this._listBoxMessages = new System.Windows.Forms.ListBox();
            this._buttonLoadMessages = new System.Windows.Forms.Button();

            // INI HARUS ADA
            this.SuspendLayout();

            // Properti listBoxMessages
            this._listBoxMessages.FormattingEnabled = true;
            this._listBoxMessages.Location = new System.Drawing.Point(200, 30);
            this._listBoxMessages.Name = "_listBoxMessages";
            this._listBoxMessages.Size = new System.Drawing.Size(580, 400);
            this._listBoxMessages.TabIndex = 0;
            this._listBoxMessages.SelectedIndexChanged += new System.EventHandler(this.ListBoxMessages_SelectedIndexChanged);

            // Properti buttonLoadMessages
            this._buttonLoadMessages.Location = new System.Drawing.Point(10, 30);
            this._buttonLoadMessages.Name = "_buttonLoadMessages";
            this._buttonLoadMessages.Size = new System.Drawing.Size(180, 30);
            this._buttonLoadMessages.TabIndex = 1;
            this._buttonLoadMessages.Text = "Tampilkan Semua Pesan";
            this._buttonLoadMessages.UseVisualStyleBackColor = true;
            this._buttonLoadMessages.Click += new System.EventHandler(this.ButtonLoadMessages_Click);

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._buttonLoadMessages);
            this.Controls.Add(this._listBoxMessages);
            this.Name = "CustomerServiceForm";
            this.Text = "Customer Service";

            // INI HARUS ADA
            this.ResumeLayout(false);
        }
    }
}
