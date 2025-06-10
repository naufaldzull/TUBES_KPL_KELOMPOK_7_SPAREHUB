using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;

namespace CustomerServiceClient
{
    public partial class MainForm : Form
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl = "https://localhost:5001/api/CustomerService"; // Ubah ke URL Swagger kamu

        private TextBox namaTextBox;
        private TextBox pesanTextBox;
        private Button kirimButton;
        private Button lihatButton;
        private ListBox pesanListBox;
        private Label statusLabel;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Customer Service via API";
            this.Width = 500;
            this.Height = 400;

            Label namaLabel = new Label { Text = "Nama:", Left = 20, Top = 20 };
            namaTextBox = new TextBox { Left = 100, Top = 20, Width = 300 };

            Label pesanLabel = new Label { Text = "Pesan:", Left = 20, Top = 60 };
            pesanTextBox = new TextBox { Left = 100, Top = 60, Width = 300, Height = 60, Multiline = true };

            kirimButton = new Button { Text = "Kirim Pesan", Left = 100, Top = 140, Width = 120 };
            lihatButton = new Button { Text = "Lihat Semua Pesan", Left = 230, Top = 140, Width = 170 };

            pesanListBox = new ListBox { Left = 20, Top = 180, Width = 430, Height = 150 };
            statusLabel = new Label { Left = 20, Top = 340, Width = 400 };

            kirimButton.Click += KirimButton_Click;
            lihatButton.Click += LihatButton_Click;

            this.Controls.Add(namaLabel);
            this.Controls.Add(namaTextBox);
            this.Controls.Add(pesanLabel);
            this.Controls.Add(pesanTextBox);
            this.Controls.Add(kirimButton);
            this.Controls.Add(lihatButton);
            this.Controls.Add(pesanListBox);
            this.Controls.Add(statusLabel);
        }

        private async void KirimButton_Click(object sender, EventArgs e)
        {
            var pesan = new
            {
                NamaPengguna = namaTextBox.Text,
                IsiPesan = pesanTextBox.Text
            };

            try
            {
                var response = await httpClient.PostAsJsonAsync(apiUrl, pesan);
                string result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    statusLabel.Text = "✅ " + result;
                    namaTextBox.Clear();
                    pesanTextBox.Clear();
                }
                else
                {
                    statusLabel.Text = "❌ Error: " + result;
                }
            }
            catch (Exception ex)
            {
                statusLabel.Text = "❌ Exception: " + ex.Message;
            }
        }

        private async void LihatButton_Click(object sender, EventArgs e)
        {
            try
            {
                var pesanList = await httpClient.GetFromJsonAsync<List<Pesan>>(apiUrl);
                pesanListBox.Items.Clear();

                foreach (var p in pesanList)
                {
                    pesanListBox.Items.Add($"{p.NamaPengguna}: {p.IsiPesan}");
                }

                statusLabel.Text = $"✅ {pesanList.Count} pesan ditampilkan.";
            }
            catch (Exception ex)
            {
                statusLabel.Text = "❌ Gagal mengambil pesan: " + ex.Message;
            }
        }
    }

    public class Pesan
    {
        public string NamaPengguna { get; set; }
        public string IsiPesan { get; set; }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
