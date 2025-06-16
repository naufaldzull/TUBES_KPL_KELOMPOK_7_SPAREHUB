namespace SpareHub
{
    partial class MenuRegister
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelLeft;
        private Label lblWelcome;
        private Label lblSubtext;
        private Label lblTitle;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private TextBox txtConfirmPassword;
        private Button btnSignUp;
        private Button btnLogin;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelLeft = new Panel();
            lblWelcome = new Label();
            lblSubtext = new Label();
            lblTitle = new Label();
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            txtConfirmPassword = new TextBox();
            btnSignUp = new Button();
            btnLogin = new Button();
            showPassword = new CheckBox();
            panelLeft.SuspendLayout();
            SuspendLayout();
            // 
            // panelLeft
            // 
            panelLeft.BackColor = Color.FromArgb(50, 90, 255);
            panelLeft.Controls.Add(lblWelcome);
            panelLeft.Controls.Add(lblSubtext);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 0);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(350, 500);
            panelLeft.TabIndex = 0;
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.White;
            lblWelcome.Location = new Point(30, 130);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(203, 108);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Hello,\nwelcome!";
            // 
            // lblSubtext
            // 
            lblSubtext.AutoSize = true;
            lblSubtext.Font = new Font("Segoe UI", 10F);
            lblSubtext.ForeColor = Color.White;
            lblSubtext.Location = new Point(30, 240);
            lblSubtext.Name = "lblSubtext";
            lblSubtext.Size = new Size(274, 46);
            lblSubtext.TabIndex = 1;
            lblSubtext.Text = "Welcome to SpareHub.\nBest place to find your spare parts.";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(50, 90, 255);
            lblTitle.Location = new Point(400, 50);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(170, 54);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Sign Up";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(400, 120);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(300, 27);
            txtEmail.TabIndex = 2;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(400, 170);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(300, 27);
            txtPassword.TabIndex = 3;
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Location = new Point(400, 220);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.Size = new Size(300, 27);
            txtConfirmPassword.TabIndex = 4;
            // 
            // btnSignUp
            // 
            btnSignUp.BackColor = Color.FromArgb(50, 90, 255);
            btnSignUp.FlatAppearance.BorderSize = 0;
            btnSignUp.FlatStyle = FlatStyle.Flat;
            btnSignUp.ForeColor = Color.White;
            btnSignUp.Location = new Point(400, 270);
            btnSignUp.Name = "btnSignUp";
            btnSignUp.Size = new Size(130, 35);
            btnSignUp.TabIndex = 5;
            btnSignUp.Text = "Sign Up";
            btnSignUp.UseVisualStyleBackColor = false;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.White;
            btnLogin.FlatAppearance.BorderColor = Color.FromArgb(50, 90, 255);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.ForeColor = Color.FromArgb(50, 90, 255);
            btnLogin.Location = new Point(570, 270);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(130, 35);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = false;
            // 
            // showPassword
            // 
            showPassword.AutoSize = true;
            showPassword.Location = new Point(400, 322);
            showPassword.Name = "showPassword";
            showPassword.Size = new Size(132, 24);
            showPassword.TabIndex = 7;
            showPassword.Text = "Show Password";
            showPassword.UseVisualStyleBackColor = true;
            // 
            // MenuRegister
            // 
            ClientSize = new Size(800, 500);
            Controls.Add(showPassword);
            Controls.Add(panelLeft);
            Controls.Add(lblTitle);
            Controls.Add(txtEmail);
            Controls.Add(txtPassword);
            Controls.Add(txtConfirmPassword);
            Controls.Add(btnSignUp);
            Controls.Add(btnLogin);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "MenuRegister";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SpareHub - Register";
            panelLeft.ResumeLayout(false);
            panelLeft.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        private CheckBox showPassword;
    }
}