using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SpareHub
{
    public partial class MenuLogin : Form
    {
        public MenuLogin()
        {
            this.Text = "SpareHub - Login";
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            InitUI();
        }

        private void InitUI()
        {
            // Panel kanan (form login)
            Panel panelRight = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            this.Controls.Add(panelRight);

            // Panel kiri (branding)
            Panel panelLeft = new Panel()
            {
                BackColor = Color.FromArgb(50, 90, 255),
                Size = new Size(350, this.Height),
                Dock = DockStyle.Left
            };
            this.Controls.Add(panelLeft);

            // Logo
            PictureBox logoImg = new PictureBox()
            {
                Size = new Size(60, 60),
                Location = new Point(30, 30),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            if (File.Exists("Resources/logo.png"))
                logoImg.Image = Image.FromFile("Resources/logo.png");
            panelLeft.Controls.Add(logoImg);

            Label title = new Label()
            {
                Text = "Hello,\nwelcome!",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                Location = new Point(30, 130),
                AutoSize = true
            };
            panelLeft.Controls.Add(title);

            Label subText = new Label()
            {
                Text = "Welcome to SpareHub.\nBest place to find your spare parts.",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                Location = new Point(30, 240),
                AutoSize = true
            };
            panelLeft.Controls.Add(subText);

            // Form controls
            int formStartX = 50;

            TextBox txtEmail = new TextBox()
            {
                Width = 300,
                Location = new Point(formStartX, 120)
            };
            panelRight.Controls.Add(txtEmail);
            SetPlaceholder(txtEmail, "Email address");

            TextBox txtPassword = new TextBox()
            {
                Width = 300,
                Location = new Point(formStartX, 170)
            };
            panelRight.Controls.Add(txtPassword);
            SetPlaceholder(txtPassword, "Password");

            CheckBox rememberMe = new CheckBox()
            {
                Text = "Remember me",
                Location = new Point(formStartX, 220),
                AutoSize = true
            };
            panelRight.Controls.Add(rememberMe);

            LinkLabel forgotLink = new LinkLabel()
            {
                Text = "Forgot password?",
                AutoSize = true,
                Location = new Point(formStartX + 200, 220)
            };
            panelRight.Controls.Add(forgotLink);

            Button loginButton = new Button()
            {
                Text = "Login",
                Size = new Size(130, 35),
                BackColor = Color.FromArgb(50, 90, 255),
                ForeColor = Color.White,
                Location = new Point(formStartX, 270),
                FlatStyle = FlatStyle.Flat
            };
            loginButton.FlatAppearance.BorderSize = 0;
            panelRight.Controls.Add(loginButton);

            loginButton.Click += (s, e) =>
            {
                string username = txtEmail.Text.Trim();
                string password = txtPassword.Text;

                if (username != "Email address" && password != "Password")
                {
                    if (username.Equals("kelompok7", StringComparison.OrdinalIgnoreCase) && password == "1234567")
                    {
                        MessageBox.Show("Login berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // TODO: Bisa tambahkan buka form baru di sini
                    }
                    else
                    {
                        MessageBox.Show("Username atau password salah.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Silakan masukkan username dan password terlebih dahulu.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            Button signupButton = new Button()
            {
                Text = "Sign up",
                Size = new Size(130, 35),
                Location = new Point(formStartX + 150, 270),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(50, 90, 255),
                FlatStyle = FlatStyle.Flat
            };
            signupButton.FlatAppearance.BorderColor = Color.FromArgb(50, 90, 255);
            signupButton.FlatAppearance.BorderSize = 1;
            panelRight.Controls.Add(signupButton);

            Label followLabel = new Label()
            {
                Text = "Follow us:",
                Location = new Point(formStartX + 100, 330),
                AutoSize = true
            };
            panelRight.Controls.Add(followLabel);

            // Icons
            string[] iconFiles = { "facebook.png", "twitter.png", "instagram.png" };
            for (int i = 0; i < iconFiles.Length; i++)
            {
                PictureBox iconBox = new PictureBox()
                {
                    Size = new Size(32, 32),
                    Location = new Point(formStartX + 100 + (i * 40), 360),
                    SizeMode = PictureBoxSizeMode.Zoom
                };

                string path = Path.Combine("Resources", iconFiles[i]);
                if (File.Exists(path))
                    iconBox.Image = Image.FromFile(path);

                panelRight.Controls.Add(iconBox);
            }
        }

        private void SetPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;

            textBox.GotFocus += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                    if (placeholder.ToLower().Contains("password"))
                        textBox.UseSystemPasswordChar = true;
                }
            };

            textBox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
                    if (placeholder.ToLower().Contains("password"))
                        textBox.UseSystemPasswordChar = false;
                }
            };
        }
    }
}
