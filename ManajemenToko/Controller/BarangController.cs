using ManajemenToko.Models;
using ManajemenToko.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManajemenToko.Controller
{
    /// <summary>
    /// Controller untuk pengelolaan Barang via service layer.
    /// Mengikuti .NET naming convention dan clean structure.
    /// </summary>
    public class BarangController // PascalCase 
    {
        private readonly BarangService _barangService; // camelCase private field 

        public BarangController()
        {
            _barangService = BarangService.Instance; // Singleton 
        }

        public (bool Success, string Message) TambahBarang(
            string nama,
            string deskripsi,
            string harga,
            string stok,
            string model,
            string merek,
            string jenis)
        {
            if (string.IsNullOrWhiteSpace(nama))
                return (false, "Nama tidak boleh kosong");

            if (string.IsNullOrWhiteSpace(deskripsi))
                return (false, "Deskripsi tidak boleh kosong");

            if (string.IsNullOrWhiteSpace(jenis))
                return (false, "Jenis tidak boleh kosong");

            if (!decimal.TryParse(harga, out decimal parsedHarga) || parsedHarga <= 0)
                return (false, "Harga harus angka dan lebih besar dari 0");

            if (!int.TryParse(stok, out int parsedStok) || parsedStok < 0)
                return (false, "Stok harus angka dan tidak boleh negatif");

            var barang = new Barang(nama, deskripsi, parsedHarga, parsedStok, model ?? "", merek ?? "", jenis);
            var result = _barangService.TambahBarang(barang);

            return result
                ? (true, "Barang berhasil ditambahkan!")
                : (false, "Gagal menambahkan barang");
        }

        public (bool Success, List<Barang> Data, string Message) GetAllBarang()
        {
            var data = _barangService.GetAllBarang();
            return (true, data, $"Berhasil memuat {data.Count} barang");
        }

        public (bool Success, Barang Data, string Message) GetBarangById(int id)
        {
            var barang = _barangService.GetBarangById(id);
            return barang != null
                ? (true, barang, "Barang ditemukan")
                : (false, null, "Barang tidak ditemukan");
        }

        public (bool Success, string Message) UpdateBarang(
            int id,
            string nama,
            string deskripsi,
            string harga,
            string stok,
            string model,
            string merek,
            string jenis)
        {
            if (string.IsNullOrWhiteSpace(nama))
                return (false, "Nama tidak boleh kosong");

            if (!decimal.TryParse(harga, out decimal parsedHarga) || parsedHarga <= 0)
                return (false, "Harga tidak valid");

            if (!int.TryParse(stok, out int parsedStok) || parsedStok < 0)
                return (false, "Stok tidak valid");

            var updatedBarang = new Barang(nama, deskripsi, parsedHarga, parsedStok, model ?? "", merek ?? "", jenis);
            var result = _barangService.UpdateBarang(id, updatedBarang);

            return result
                ? (true, "Barang berhasil diupdate!")
                : (false, "Barang tidak ditemukan");
        }

        public (bool Success, string Message) UpdateDeskripsi(int id, string deskripsi)
        {
            if (string.IsNullOrWhiteSpace(deskripsi))
                return (false, "Deskripsi tidak boleh kosong");

            var result = _barangService.UpdateDeskripsi(id, deskripsi);

            return result
                ? (true, "Deskripsi berhasil diupdate!")
                : (false, "Barang tidak ditemukan");
        }

        public (bool Success, string Message) HapusBarang(int id)
        {
            var result = _barangService.HapusBarang(id);

            return result
                ? (true, "Barang berhasil dihapus!")
                : (false, "Barang tidak ditemukan");
        }

        public (bool Success, List<Barang> Data, string Message) CariBarang(string keyword)
        {
            var hasil = _barangService.CariBarang(keyword);
            return (true, hasil, $"Ditemukan {hasil.Count} barang");
        }

        public string[] GetAvailableJenis() => Barang.GetAvailableJenis(); // PascalCase method 

        public void ShowSuccessMessage(string message) =>
            MessageBox.Show(message, "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information); // PascalCase method 

        public void ShowErrorMessage(string message) =>
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // PascalCase method 

        public bool ShowConfirmationDialog(string message, string title = "Konfirmasi") =>
            MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes; // PascalCase method 

        // Tambahan untuk integrasi delete ke API (opsional)
        public async Task<bool> SyncDeleteBarangToApiAsync(int id) // PascalCase method 
        {
            // Tambahkan logika sync delete ke API backend jika diperlukan
            return await Task.FromResult(true);
        }
    }
}
