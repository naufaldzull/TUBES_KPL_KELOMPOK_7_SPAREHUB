using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Searching;
using SearchingData;
using SearchingModel;
using ManajemenToko.API.Model;
using ManajemenToko.API.Services;

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
        private Button? _btnRefresh;
        private DataGridView? _dgvHasil;
        private Label? _lblJumlah;
        private IBarangApiService _barangService;

        public SearchingView()
        {
            _barangService = new BarangApiService();
            SetupForm();
            LoadDataAsync();
        }

        private void SetupForm()
        {
            Text = "Pencarian Spare Part";
            Size = new Size(850, 600);
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
                Location = new Point(290, 50),
                Size = new Size(70, 20)
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

            _btnRefresh = new Button
            {
                Text = "Refresh",
                Location = new Point(670, 47),
                Size = new Size(80, 30),
                BackColor = Color.LightGray
            };
            _btnRefresh.Click += BtnRefresh_Click;

            _dgvHasil = new DataGridView
            {
                Location = new Point(20, 100),
                Size = new Size(800, 400),
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
            Controls.Add(_btnRefresh);
            Controls.Add(_dgvHasil);
            Controls.Add(_lblJumlah);
        }

        private async Task LoadDataAsync()
        {
            try
            {
                if (_lblJumlah != null)
                    _lblJumlah.Text = "Loading...";

                var allData = await _barangService.GetAllBarangAsync();

                if (_dgvHasil != null)
                {
                    _dgvHasil.DataSource = allData;
                }

                if (_lblJumlah != null)
                {
                    _lblJumlah.Text = $"Total: {allData.Count} item";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }

        private async void BtnCari_Click(object? sender, EventArgs e)
        {
            if (_txtKeyword == null || string.IsNullOrWhiteSpace(_txtKeyword.Text))
            {
                MessageBox.Show("Keyword tidak boleh kosong!");
                return;
            }

            try
            {
                if (_btnCari != null)
                {
                    _btnCari.Enabled = false;
                    _btnCari.Text = "Searching...";
                }

                var keyword = _txtKeyword.Text.Trim().ToLower();
                var selectedType = _cmbTipeSearch?.SelectedItem?.ToString() ?? "";

                List<Barang> hasil;

                if (selectedType == "Model")
                {
                    var allData = await _barangService.GetAllBarangAsync();
                    hasil = allData.Where(b =>
                        !string.IsNullOrWhiteSpace(b.Model) &&
                        b.Model.ToLower().Contains(keyword))
                        .ToList();
                }
                else if (selectedType == "Merek")
                {
                    var allData = await _barangService.GetAllBarangAsync();
                    hasil = allData.Where(b =>
                        !string.IsNullOrWhiteSpace(b.Merek) &&
                        b.Merek.ToLower().Contains(keyword))
                        .ToList();
                }
                else if (selectedType == "Kategori")
                {
                    hasil = await _barangService.GetBarangByJenisAsync(_txtKeyword.Text.Trim());
                }
                else
                {
                    hasil = await _barangService.SearchBarangAsync(_txtKeyword.Text.Trim());
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
            finally
            {
                if (_btnCari != null)
                {
                    _btnCari.Enabled = true;
                    _btnCari.Text = "Cari";
                }
            }
        }

        private async void BtnRefresh_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_btnRefresh != null)
                {
                    _btnRefresh.Enabled = false;
                    _btnRefresh.Text = "Refreshing...";
                }

                if (_txtKeyword != null)
                    _txtKeyword.Text = "";

                if (_cmbTipeSearch != null)
                    _cmbTipeSearch.SelectedIndex = 0;

                await LoadDataAsync();


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saat refresh: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (_btnRefresh != null)
                {
                    _btnRefresh.Enabled = true;
                    _btnRefresh.Text = "Refresh";
                }
            }
        }
    }
}