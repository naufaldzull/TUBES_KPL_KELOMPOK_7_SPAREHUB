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
    /// <summary>
    /// Form registrasi akun baru untuk pengguna.
    /// </summary>
    public partial class MenuRegister : Form
    {
        private readonly string _usersFilePath = "../../../users.json";

        public MenuRegister()
        {
            InitializeComponent();

            SetPlaceholder(txtEmail, "Email address");
            SetPlaceholder(txtPassword, "Password");
            SetPlaceholder(txtConfirmPassword, "Confirm Password");

            btnSignUp.Click += BtnSignUp_Click;
            btnLogin.Click += (s, e) => Close();

            showPassword.CheckedChanged += (s, e) =>
            {
                TogglePasswordVisibility(txtPassword, showPassword.Checked, "Password");
                TogglePasswordVisibility(txtConfirmPassword, showPassword.Checked, "Confirm Password");
            };
        }

        /// <summary>
        /// Mengatur visibilitas karakter password.
        /// </summary>
        private void TogglePasswordVisibility(TextBox textBox, bool visible, string placeholder)
        {
            if (textBox.Text == placeholder && textBox.ForeColor == Color.Gray)
            {
                textBox.PasswordChar = '\0';
                return;
            }

            textBox.PasswordChar = visible ? '\0' : '●';
        }

        /// <summary>
        /// Event klik tombol Sign Up. Validasi dan simpan akun baru.
        /// </summary>
        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (IsPlaceholder(email, "Email address") ||
                IsPlaceholder(password, "Password") ||
                IsPlaceholder(confirmPassword, "Confirm Password"))
            {
                ShowWarning("Harap isi semua field.");
                return;
            }

            if (!IsValidEmail(email))
            {
                ShowError("Format email tidak valid.");
                return;
            }

            if (password.Length < 6)
            {
                ShowError("Password minimal 6 karakter.");
                return;
            }

            if (password != confirmPassword)
            {
                ShowError("Konfirmasi password tidak cocok.");
                return;
            }

            var users = LoadUsers();

            if (users.Exists(u => u.email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                ShowError("Email sudah terdaftar.");
                return;
            }

            users.Add(new UserCredential
            {
                email = email,
                passwordHash = Hash(password)
            });

            SaveUsers(users);
            MessageBox.Show("Registrasi berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        /// <summary>
        /// Mengatur placeholder untuk TextBox.
        /// </summary>
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

                    if (placeholder.Contains("Password", StringComparison.OrdinalIgnoreCase) && !showPassword.Checked)
                        textBox.PasswordChar = '●';
                }
            };

            textBox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;

                    if (placeholder.Contains("Password", StringComparison.OrdinalIgnoreCase))
                        textBox.PasswordChar = '\0';
                }
            };
        }

        /// <summary>
        /// Cek apakah input masih placeholder.
        /// </summary>
        private bool IsPlaceholder(string text, string placeholder)
        {
            return string.IsNullOrWhiteSpace(text) || text == placeholder;
        }

        /// <summary>
        /// Validasi email menggunakan regex.
        /// </summary>
        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        /// <summary>
        /// Menampilkan pesan error.
        /// </summary>
        private void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Menampilkan pesan warning.
        /// </summary>
        private void ShowWarning(string message)
        {
            MessageBox.Show(message, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Menghasilkan hash SHA-256 dari string input.
        /// </summary>
        private string Hash(string raw)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(raw));
            StringBuilder sb = new();

            foreach (var b in bytes)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }

        /// <summary>
        /// Memuat user dari file JSON.
        /// </summary>
        private List<UserCredential> LoadUsers()
        {
            try
            {
                if (!File.Exists(_usersFilePath))
                    return new List<UserCredential>();

                string json = File.ReadAllText(_usersFilePath);
                return JsonSerializer.Deserialize<List<UserCredential>>(json) ?? new List<UserCredential>();
            }
            catch (Exception ex)
            {
                ShowError("Gagal memuat data user: " + ex.Message);
                return new List<UserCredential>();
            }
        }

        /// <summary>
        /// Menyimpan daftar user ke file JSON.
        /// </summary>
        private void SaveUsers(List<UserCredential> users)
        {
            try
            {
                string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_usersFilePath, json);
            }
            catch (Exception ex)
            {
                ShowError("Gagal menyimpan data user: " + ex.Message);
            }
        }
    }
}