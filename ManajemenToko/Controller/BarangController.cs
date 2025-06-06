using ManajemenToko.Models;
using ManajemenToko.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManajemenToko.Controller
{
    class BarangController
    {
        private readonly BarangService _barangService;

        public BarangController()
        {
            _barangService = new BarangService();
        }

        // Handler untuk form tambah barang
        public (bool Success, string Message) TambahBarang(string nama, string deskripsi, string harga, string stok, string model, string merek, string jenis)
        {
            try
            {
                // Validasi input string
                if (string.IsNullOrWhiteSpace(nama))
                    return (false, "Nama barang tidak boleh kosong");

                if (string.IsNullOrWhiteSpace(deskripsi))
                    return (false, "Deskripsi tidak boleh kosong");

                if (string.IsNullOrWhiteSpace(jenis))
                    return (false, "Jenis tidak boleh kosong");

                // Parse harga
                if (!decimal.TryParse(harga, out decimal parsedHarga) || parsedHarga <= 0)
                    return (false, "Harga harus berupa angka yang valid dan lebih besar dari 0");

                // Parse stok
                if (!int.TryParse(stok, out int parsedStok) || parsedStok < 0)
                    return (false, "Stok harus berupa angka yang valid dan tidak boleh negatif");

                // Buat object barang
                var barang = new Barang(nama.Trim(), deskripsi.Trim(), parsedHarga, parsedStok,
                                       model?.Trim() ?? "", merek?.Trim() ?? "", jenis.Trim());

                // Panggil service
                bool result = _barangService.TambahBarang(barang);

                if (result)
                    return (true, "Barang berhasil ditambahkan!");
                else
                    return (false, "Gagal menambahkan barang");
            }
            catch (ArgumentException ex)
            {
                return (false, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return (false, ex.Message);
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        // Handler untuk form lihat barang
        public (bool Success, List<Barang> Data, string Message) GetAllBarang()
        {
            try
            {
                var barangList = _barangService.GetAllBarang();
                return (true, barangList, $"Berhasil memuat {barangList.Count} barang");
            }
            catch (Exception ex)
            {
                return (false, new List<Barang>(), $"Error memuat data: {ex.Message}");
            }
        }

        // Handler untuk form ubah barang
        public (bool Success, Barang Data, string Message) GetBarangById(int id)
        {
            try
            {
                var barang = _barangService.GetBarangById(id);
                if (barang != null)
                    return (true, barang, "Barang ditemukan");
                else
                    return (false, null, "Barang tidak ditemukan");
            }
            catch (Exception ex)
            {
                return (false, null, $"Error: {ex.Message}");
            }
        }

        // Handler untuk update barang
        public (bool Success, string Message) UpdateBarang(int id, string nama, string deskripsi, string harga, string stok, string model, string merek, string jenis)
        {
            try
            {
                // Validasi input string
                if (string.IsNullOrWhiteSpace(nama))
                    return (false, "Nama barang tidak boleh kosong");

                if (string.IsNullOrWhiteSpace(deskripsi))
                    return (false, "Deskripsi tidak boleh kosong");

                if (string.IsNullOrWhiteSpace(jenis))
                    return (false, "Jenis tidak boleh kosong");

                // Parse harga
                if (!decimal.TryParse(harga, out decimal parsedHarga) || parsedHarga <= 0)
                    return (false, "Harga harus berupa angka yang valid dan lebih besar dari 0");

                // Parse stok
                if (!int.TryParse(stok, out int parsedStok) || parsedStok < 0)
                    return (false, "Stok harus berupa angka yang valid dan tidak boleh negatif");

                // Buat object barang untuk update
                var updatedBarang = new Barang(nama.Trim(), deskripsi.Trim(), parsedHarga, parsedStok,
                                              model?.Trim() ?? "", merek?.Trim() ?? "", jenis.Trim());

                // Panggil service
                bool result = _barangService.UpdateBarang(id, updatedBarang);

                if (result)
                    return (true, "Barang berhasil diupdate!");
                else
                    return (false, "Gagal mengupdate barang");
            }
            catch (ArgumentException ex)
            {
                return (false, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return (false, ex.Message);
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        // Handler untuk update deskripsi saja
        public (bool Success, string Message) UpdateDeskripsi(int id, string deskripsi)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(deskripsi))
                    return (false, "Deskripsi tidak boleh kosong");

                bool result = _barangService.UpdateDeskripsi(id, deskripsi.Trim());

                if (result)
                    return (true, "Deskripsi barang berhasil diupdate!");
                else
                    return (false, "Gagal mengupdate deskripsi");
            }
            catch (ArgumentException ex)
            {
                return (false, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return (false, ex.Message);
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        // Handler untuk hapus barang
        public (bool Success, string Message) HapusBarang(int id)
        {
            try
            {
                bool result = _barangService.HapusBarang(id);

                if (result)
                    return (true, "Barang berhasil dihapus!");
                else
                    return (false, "Gagal menghapus barang");
            }
            catch (ArgumentException ex)
            {
                return (false, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return (false, ex.Message);
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        // Handler untuk cari barang
        public (bool Success, List<Barang> Data, string Message) CariBarang(string keyword)
        {
            try
            {
                var hasil = _barangService.CariBarang(keyword);
                return (true, hasil, $"Ditemukan {hasil.Count} barang");
            }
            catch (Exception ex)
            {
                return (false, new List<Barang>(), $"Error pencarian: {ex.Message}");
            }
        }

        // Handler untuk statistik
        public (bool Success, int TotalBarang, decimal TotalNilai, int TotalStok, string Message) GetStatistik()
        {
            try
            {
                var stats = _barangService.GetStatistik();
                return (true, stats.TotalBarang, stats.TotalNilai, stats.TotalStok, "Statistik berhasil dimuat");
            }
            catch (Exception ex)
            {
                return (false, 0, 0, 0, $"Error memuat statistik: {ex.Message}");
            }
        }

        // Helper method untuk validation
        public (bool IsValid, string ErrorMessage) ValidateBarangInput(string nama, string deskripsi, string harga, string stok, string jenis)
        {
            // Nama validation
            if (string.IsNullOrWhiteSpace(nama))
                return (false, "Nama barang tidak boleh kosong");

            if (nama.Trim().Length > 100)
                return (false, "Nama barang maksimal 100 karakter");

            // Deskripsi validation
            if (string.IsNullOrWhiteSpace(deskripsi))
                return (false, "Deskripsi tidak boleh kosong");

            if (deskripsi.Trim().Length > 500)
                return (false, "Deskripsi maksimal 500 karakter");

            // Jenis validation
            if (string.IsNullOrWhiteSpace(jenis))
                return (false, "Jenis tidak boleh kosong");

            if (!Barang.GetAvailableJenis().Contains(jenis))
                return (false, "Jenis yang dipilih tidak valid");

            // Harga validation
            if (!decimal.TryParse(harga, out decimal parsedHarga))
                return (false, "Harga harus berupa angka yang valid");

            if (parsedHarga <= 0)
                return (false, "Harga harus lebih besar dari 0");

            if (parsedHarga > 999999999)
                return (false, "Harga terlalu besar");

            // Stok validation
            if (!int.TryParse(stok, out int parsedStok))
                return (false, "Stok harus berupa angka yang valid");

            if (parsedStok < 0)
                return (false, "Stok tidak boleh negatif");

            if (parsedStok > 999999)
                return (false, "Stok terlalu besar");

            return (true, "Validation passed");
        }

        // Helper method untuk get available jenis
        public string[] GetAvailableJenis()
        {
            return Barang.GetAvailableJenis();
        }

        // Handler untuk filter by jenis
        public (bool Success, List<Barang> Data, string Message) GetBarangByJenis(string jenis)
        {
            try
            {
                var hasil = _barangService.GetBarangByJenis(jenis);
                return (true, hasil, $"Ditemukan {hasil.Count} barang untuk jenis {jenis}");
            }
            catch (Exception ex)
            {
                return (false, new List<Barang>(), $"Error filter by jenis: {ex.Message}");
            }
        }

        // Handler untuk filter by merek
        public (bool Success, List<Barang> Data, string Message) GetBarangByMerek(string merek)
        {
            try
            {
                var hasil = _barangService.GetBarangByMerek(merek);
                return (true, hasil, $"Ditemukan {hasil.Count} barang untuk merek {merek}");
            }
            catch (Exception ex)
            {
                return (false, new List<Barang>(), $"Error filter by merek: {ex.Message}");
            }
        }

        // Helper method untuk show confirmation dialog
        public bool ShowConfirmationDialog(string message, string title = "Konfirmasi")
        {
            DialogResult result = MessageBox.Show(message, title,
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);
            return result == DialogResult.Yes;
        }

        // Helper method untuk show success message
        public void ShowSuccessMessage(string message, string title = "Berhasil")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Helper method untuk show error message
        public void ShowErrorMessage(string message, string title = "Error")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
