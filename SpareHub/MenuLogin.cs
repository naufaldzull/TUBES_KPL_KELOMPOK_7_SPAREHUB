using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using Microsoft.Win32;


namespace SpareHub
{
    public partial class MenuLogin : Form
    {

        MenuRegister register = new MenuRegister();
        public MenuLogin()
        {
            InitializeComponent();

            SetPlaceholder(textBoxEmail, "Email address");
            SetPlaceholder(textBoxPassword, "Password");

            showPassword.CheckedChanged += (s, e) =>
            {
                if (textBoxPassword.Text != "Password" && textBoxPassword.ForeColor != Color.Gray)
                {
                    textBoxPassword.PasswordChar = showPassword.Checked ? '\0' : '●';
                }
            };

            buttonLogin.Click += (s, e) =>
            {
                string email = textBoxEmail.Text.Trim();
                string password = textBoxPassword.Text;

                if (email == "Email address" || password == "Password")
                {
                    MessageBox.Show("Silakan masukkan email dan password terlebih dahulu.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var users = LoadUsers();
                string passwordHash = Hash(password);

                var matchedUser = users.Find(u =>
                    u.email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                    u.passwordHash == passwordHash);

                if (matchedUser != null)
                {
                    MessageBox.Show("Login berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MenuUtama menuUtama = new MenuUtama();
                    this.Hide();
                    menuUtama.ShowDialog();
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("Email atau password salah.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            buttonSignup.Click += (s, e) =>
            {
                this.Hide();
                register.ShowDialog();
                this.Show();
            };
        }

        private void SetPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;
            textBox.PasswordChar = '\0';

            textBox.GotFocus += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;

                    if (placeholder.ToLower().Contains("password"))
                        textBox.PasswordChar = showPassword.Checked ? '\0' : '●';
                }
            };

            textBox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;

                    if (placeholder.ToLower().Contains("password"))
                        textBox.PasswordChar = '\0';
                }
            };
        }

        private string Hash(string raw)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(raw));
                StringBuilder sb = new StringBuilder();
                foreach (var b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        private List<UserCredential> LoadUsers()
        {
            string path = "../../../users.json";
            try
            {
                if (!File.Exists(path)) return new List<UserCredential>();
                string json = File.ReadAllText(path);
                return JsonSerializer.Deserialize<List<UserCredential>>(json) ?? new List<UserCredential>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membaca data user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<UserCredential>();
            }
        }
    }
}