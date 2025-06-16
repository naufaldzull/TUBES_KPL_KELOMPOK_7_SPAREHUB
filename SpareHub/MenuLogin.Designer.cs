namespace SpareHub
{
    partial class MenuLogin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelLeft = new Panel();
            labelTitle = new Label();
            labelSubtext = new Label();
            textBoxEmail = new TextBox();
            textBoxPassword = new TextBox();
            buttonLogin = new Button();
            buttonSignup = new Button();
            showPassword = new CheckBox();
            lblTitle = new Label();
            panelLeft.SuspendLayout();
            SuspendLayout();
            // 
            // panelLeft
            // 
            panelLeft.BackColor = Color.FromArgb(50, 90, 255);
            panelLeft.Controls.Add(labelTitle);
            panelLeft.Controls.Add(labelSubtext);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 0);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(350, 500);
            panelLeft.TabIndex = 0;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            labelTitle.ForeColor = Color.White;
            labelTitle.Location = new Point(30, 130);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(203, 108);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Hello,\nwelcome!";
            // 
            // labelSubtext
            // 
            labelSubtext.AutoSize = true;
            labelSubtext.Font = new Font("Segoe UI", 10F);
            labelSubtext.ForeColor = Color.White;
            labelSubtext.Location = new Point(30, 240);
            labelSubtext.Name = "labelSubtext";
            labelSubtext.Size = new Size(274, 46);
            labelSubtext.TabIndex = 1;
            labelSubtext.Text = "Welcome to SpareHub.\nBest place to find your spare parts.";
            // 
            // textBoxEmail
            // 
            textBoxEmail.Location = new Point(400, 120);
            textBoxEmail.Name = "textBoxEmail";
            textBoxEmail.Size = new Size(300, 27);
            textBoxEmail.TabIndex = 1;
            // 
            // textBoxPassword
            // 
            textBoxPassword.Location = new Point(400, 170);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.Size = new Size(300, 27);
            textBoxPassword.TabIndex = 2;
            // 
            // buttonLogin
            // 
            buttonLogin.BackColor = Color.FromArgb(50, 90, 255);
            buttonLogin.FlatAppearance.BorderSize = 0;
            buttonLogin.FlatStyle = FlatStyle.Flat;
            buttonLogin.ForeColor = Color.White;
            buttonLogin.Location = new Point(400, 220);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(130, 35);
            buttonLogin.TabIndex = 3;
            buttonLogin.Text = "Login";
            buttonLogin.UseVisualStyleBackColor = false;
            // 
            // buttonSignup
            // 
            buttonSignup.BackColor = Color.White;
            buttonSignup.FlatAppearance.BorderColor = Color.FromArgb(50, 90, 255);
            buttonSignup.FlatStyle = FlatStyle.Flat;
            buttonSignup.ForeColor = Color.FromArgb(50, 90, 255);
            buttonSignup.Location = new Point(570, 220);
            buttonSignup.Name = "buttonSignup";
            buttonSignup.Size = new Size(130, 35);
            buttonSignup.TabIndex = 4;
            buttonSignup.Text = "Sign up";
            buttonSignup.UseVisualStyleBackColor = false;
            // 
            // showPassword
            // 
            showPassword.AutoSize = true;
            showPassword.Location = new Point(400, 270);
            showPassword.Name = "showPassword";
            showPassword.Size = new Size(132, 24);
            showPassword.TabIndex = 5;
            showPassword.Text = "Show Password";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(50, 90, 255);
            lblTitle.Location = new Point(400, 50);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(127, 54);
            lblTitle.TabIndex = 6;
            lblTitle.Text = "Login";
            // 
            // MenuLogin
            // 
            ClientSize = new Size(800, 500);
            Controls.Add(lblTitle);
            Controls.Add(panelLeft);
            Controls.Add(textBoxEmail);
            Controls.Add(textBoxPassword);
            Controls.Add(buttonLogin);
            Controls.Add(buttonSignup);
            Controls.Add(showPassword);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "MenuLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SpareHub - Login";
            panelLeft.ResumeLayout(false);
            panelLeft.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelSubtext;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Button buttonSignup;
        private System.Windows.Forms.CheckBox showPassword;
        private Label lblTitle;
    }
}