using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Searching;
using SearchingData;
using SearchingModel;

namespace SpareHub
{
    /// <summary>
    /// Form UI untuk pencarian sparepart motor menggunakan SearchEngine.
    /// </summary>
    public partial class SearchingView : Form
    {
        private ComboBox? _cmbTipeSearch;
        private TextBox? _txtKeyword;
        private Button? _btnCari;
        private DataGridView? _dgvHasil;
        private Label? _lblJumlah;

        public SearchingView()
        {
            SetupForm();
            LoadData();
        }

        private void SetupForm()
        {
            Text = "Pencarian Spare Part";
            Size = new Size(800, 600);
            StartPosition = FormStartPosition.CenterScreen;

            var lblJudul = new Label
            {
                Text = "Pencarian Sparepart Motor",
                Location = new Point(20, 10),
                Size = new Size(300, 20),
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            var lblTipe = new Label
            {
                Text = "Tipe Pencarian:",
                Location = new Point(20, 50),
                Size = new Size(100, 20)
            };

            _cmbTipeSearch = new ComboBox
            {
                Location = new Point(130, 48),
                Size = new Size(150, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            _cmbTipeSearch.Items.AddRange(new[] { "Kategori", "Merek", "Model" });
            _cmbTipeSearch.SelectedIndex = 0;

            var lblKeyword = new Label
            {
                Text = "Keyword:",
                Location = new Point(300, 50),
                Size = new Size(60, 20)
            };

            _txtKeyword = new TextBox
            {
                Location = new Point(370, 48),
                Size = new Size(200, 25)
            };

            _btnCari = new Button
            {
                Text = "Cari",
                Location = new Point(580, 47),
                Size = new Size(80, 30)
            };
            _btnCari.Click += BtnCari_Click;

            _dgvHasil = new DataGridView
            {
                Location = new Point(20, 100),
                Size = new Size(750, 400),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            _lblJumlah = new Label
            {
                Text = "Total: 0 item",
                Location = new Point(20, 510),
                Size = new Size(200, 20)
            };

            Controls.Add(lblJudul);
            Controls.Add(lblTipe);
            Controls.Add(_cmbTipeSearch);
            Controls.Add(lblKeyword);
            Controls.Add(_txtKeyword);
            Controls.Add(_btnCari);
            Controls.Add(_dgvHasil);
            Controls.Add(_lblJumlah);
        }

        private void LoadData()
        {
            // Inisialisasi data dummy ke dalam SearchEngine singleton
            SearchEngine<Sparepart>.SearchEngineSingleton<Sparepart>.Initialize(() => Utility.AmbilDataDummy());

            var searchEngine = SearchEngine<Sparepart>.SearchEngineSingleton<Sparepart>.Instance;
            var allData = searchEngine?.GetAllData() ?? new List<Sparepart>();

            if (_dgvHasil != null)
            {
                _dgvHasil.DataSource = allData;
            }

            if (_lblJumlah != null)
            {
                _lblJumlah.Text = $"Total: {allData.Count} item";
            }
        }

        private void BtnCari_Click(object? sender, EventArgs e)
        {
            if (_txtKeyword == null || string.IsNullOrWhiteSpace(_txtKeyword.Text))
            {
                MessageBox.Show("Keyword tidak boleh kosong!");
                return;
            }

            var hasil = new List<Sparepart>();
            var keyword = _txtKeyword.Text.Trim();
            var selectedType = _cmbTipeSearch?.SelectedItem?.ToString() ?? "";

            try
            {
                var searchEngine = SearchEngine<Sparepart>.SearchEngineSingleton<Sparepart>.Instance;

                if (searchEngine != null)
                {
                    hasil = selectedType switch
                    {
                        "Kategori" => searchEngine.CariBerdasarkanKategori(keyword),
                        "Merek" => searchEngine.CariBerdasarkanMerek(keyword),
                        "Model" => searchEngine.CariBerdasarkanKompatibilitas(keyword),
                        _ => new List<Sparepart>()
                    };
                }

                if (_dgvHasil != null)
                {
                    _dgvHasil.DataSource = hasil;
                }

                if (_lblJumlah != null)
                {
                    _lblJumlah.Text = $"Total: {hasil.Count} item";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }
    }
}
