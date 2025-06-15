using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace SpareHub
{
    public partial class CustomerServiceForm : Form
    {
        // HttpClient digunakan untuk komunikasi dengan API
        private readonly HttpClient _httpClient;

        public CustomerServiceForm()
        {
            InitializeComponent();

            // Inisialisasi HttpClient dan konfigurasi validasi sertifikat (sementara, untuk pengujian lokal)
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:7043/")
            };
        }

        /// <summary>
        /// Mengambil pesan dari API dan menampilkannya di ListBox saat tombol ditekan.
        /// </summary>
        private async void ButtonLoadMessages_Click(object sender, EventArgs e)
        {
            try
            {
                _listBoxMessages.Items.Clear();

                // Ambil data pesan dari endpoint API
                var pesanList = await _httpClient.GetFromJsonAsync<List<Message>>("api/CustomerService");

                // Tampilkan isi pesan ke dalam ListBox
                foreach (var message in pesanList)
                {
                    _listBoxMessages.Items.Add($"{message.UserName}: {message.Content}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengambil data dari API:\n" + ex.Message);
            }
        }

        /// <summary>
        /// Event saat item pada ListBox dipilih (kosong untuk saat ini).
        /// </summary>
        private void ListBoxMessages_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kosong, bisa diisi fitur tambahan jika dibutuhkan
        }
    }

    /// <summary>
    /// Representasi model data pesan yang dikirim oleh pengguna.
    /// </summary>
    public class Message
    {
        [JsonPropertyName("namaPengguna")]
        public string UserName { get; set; }

        [JsonPropertyName("isiPesan")]
        public string Content { get; set; }
    }
}
