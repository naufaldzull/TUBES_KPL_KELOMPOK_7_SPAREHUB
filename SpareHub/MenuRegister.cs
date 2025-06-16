using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SpareHub
{
    public partial class MenuRegister : Form
    {
        private string usersFilePath;

        public MenuRegister()
        {
            InitializeComponent();
            usersFilePath = "../../../users.json";

            SetPlaceholder(txtEmail, "Email address");
            SetPlaceholder(txtPassword, "Password");
            SetPlaceholder(txtConfirmPassword, "Confirm Password");

            btnSignUp.Click += BtnSignUp_Click;
            btnLogin.Click += (s, e) => this.Close();

            showPassword.CheckedChanged += (s, e) =>
            {
                TogglePasswordVisibility(txtPassword, showPassword.Checked, "Password");
                TogglePasswordVisibility(txtConfirmPassword, showPassword.Checked, "Confirm Password");
            };
        }

        private void TogglePasswordVisibility(TextBox textBox, bool visible, string placeholder)
        {
            // Kalau lagi nampilkan placeholder, jangan ubah tampilannya
            if (textBox.Text == placeholder && textBox.ForeColor == Color.Gray)
            {
                textBox.PasswordChar = '\0';
                return;
            }

            textBox.PasswordChar = visible ? '\0' : '●';
        }

        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirm = txtConfirmPassword.Text.Trim();

            if (IsPlaceholder(email, "Email address") ||
                IsPlaceholder(password, "Password") ||
                IsPlaceholder(confirm, "Confirm Password"))
            {
                MessageBox.Show("Harap isi semua field.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Format email tidak valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Password minimal 6 karakter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (password != confirm)
            {
                MessageBox.Show("Konfirmasi password tidak cocok.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var users = LoadUsers();
            if (users.Exists(u => u.email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Email sudah terdaftar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            users.Add(new UserCredential
            {
                email = email,
                passwordHash = Hash(password)
            });

            SaveUsers(users);
            MessageBox.Show("Registrasi berhasil!");
            this.Close();
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

                    if (placeholder.ToLower().Contains("password") && !showPassword.Checked)
                        textBox.PasswordChar = '●';
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


        private bool IsPlaceholder(string text, string placeholder)
        {
            return string.IsNullOrWhiteSpace(text) || text == placeholder;
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
            try
            {
                if (!File.Exists(usersFilePath)) return new List<UserCredential>();
                string json = File.ReadAllText(usersFilePath);
                return JsonSerializer.Deserialize<List<UserCredential>>(json) ?? new List<UserCredential>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<UserCredential>();
            }
        }

        private void SaveUsers(List<UserCredential> users)
        {
            try
            {
                string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(usersFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan data user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}