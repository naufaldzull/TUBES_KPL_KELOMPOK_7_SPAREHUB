using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace SpareHub
{
    /// <summary>
    /// Form login untuk pengguna SpareHub.
    /// </summary>
    public partial class MenuLogin : Form
    {
        private readonly MenuRegister _registerForm = new();

        public MenuLogin()
        {
            InitializeComponent();

            SetPlaceholder(textBoxEmail, "Email address");
            SetPlaceholder(textBoxPassword, "Password");

            showPassword.CheckedChanged += ShowPassword_CheckedChanged;
            buttonLogin.Click += ButtonLogin_Click;
            buttonSignup.Click += ButtonSignup_Click;
        }

        /// <summary>
        /// Menangani toggle visibilitas password saat checkbox diubah.
        /// </summary>
        private void ShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (textBoxPassword.Text != "Password" && textBoxPassword.ForeColor != Color.Gray)
            {
                textBoxPassword.PasswordChar = showPassword.Checked ? '\0' : '●';
            }
        }

        /// <summary>
        /// Menangani proses login saat tombol Login diklik.
        /// </summary>
        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            string email = textBoxEmail.Text.Trim();
            string password = textBoxPassword.Text;

            if (email == "Email address" || password == "Password")
            {
                ShowWarning("Silakan masukkan email dan password terlebih dahulu.");
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
                OpenMainMenu();
            }
            else
            {
                ShowError("Email atau password salah.");
            }
        }

        /// <summary>
        /// Menampilkan form register jika tombol Signup ditekan.
        /// </summary>
        private void ButtonSignup_Click(object sender, EventArgs e)
        {
            Hide();
            _registerForm.ShowDialog();
            Show();
        }

        /// <summary>
        /// Mengatur placeholder untuk textbox input.
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

                    if (placeholder.Contains("password", StringComparison.OrdinalIgnoreCase))
                        textBox.PasswordChar = showPassword.Checked ? '\0' : '●';
                }
            };

            textBox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;

                    if (placeholder.Contains("password", StringComparison.OrdinalIgnoreCase))
                        textBox.PasswordChar = '\0';
                }
            };
        }

        /// <summary>
        /// Menghasilkan hash SHA-256 dari string input.
        /// </summary>
        private string Hash(string raw)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(raw));
            var sb = new StringBuilder();

            foreach (var b in bytes)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }

        /// <summary>
        /// Memuat data user dari file JSON.
        /// </summary>
        private List<UserCredential> LoadUsers()
        {
            string path = "../../../users.json";

            try
            {
                if (!File.Exists(path))
                    return new List<UserCredential>();

                string json = File.ReadAllText(path);
                return JsonSerializer.Deserialize<List<UserCredential>>(json) ?? new List<UserCredential>();
            }
            catch (Exception ex)
            {
                ShowError("Gagal membaca data user: " + ex.Message);
                return new List<UserCredential>();
            }
        }

        /// <summary>
        /// Menampilkan form MenuUtama setelah login berhasil.
        /// </summary>
        private void OpenMainMenu()
        {
            var menuUtama = new MenuUtama();
            Hide();
            menuUtama.ShowDialog();
            Dispose();
        }

        /// <summary>
        /// Menampilkan pesan peringatan kepada pengguna.
        /// </summary>
        private void ShowWarning(string message)
        {
            MessageBox.Show(message, "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Menampilkan pesan error kepada pengguna.
        /// </summary>
        private void ShowError(string message)
        {
            MessageBox.Show(message, "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}