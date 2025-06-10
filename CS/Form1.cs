using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;

namespace CS
{
    public partial class Form1 : Form
    {
        private readonly HttpClient httpClient = new HttpClient();

        public Form1()
        {
            InitializeComponent();

            // Set base URL ke API kamu
            httpClient.BaseAddress = new Uri("https://localhost:7043/");
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();

                // Ambil data pesan dari API (GET)
                List<Pesan> pesanList = await httpClient.GetFromJsonAsync<List<Pesan>>("api/CustomerService");

                // Tampilkan di ListBox
                foreach (var pesan in pesanList)
                {
                    listBox1.Items.Add($"{pesan.NamaPengguna}: {pesan.IsiPesan}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengambil data dari API:\n" + ex.Message);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kosong, jika kamu ingin tampilkan detail saat dipilih, bisa tambahkan logika di sini
        }
    }

    public class Pesan
    {
        public string NamaPengguna { get; set; }
        public string IsiPesan { get; set; }
    }
}
