using ManajemenToko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ManajemenToko.Services
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
    }

    public class BarangService
    {
        // Singleton instance
        private static readonly BarangService _instance = new BarangService();
        public static BarangService Instance => _instance;

        // Constructor privat agar tidak bisa instance dari luar
        private BarangService() { }

        // Data storage
        private readonly List<Barang> _barangList = new List<Barang>();
        private int _nextId = 1;

        // API connection
        private static readonly HttpClient _httpClient = new HttpClient();
        private const string API_BASE_URL = "https://localhost:7067/api/toko";

        // CREATE - Tambah barang
        public bool TambahBarang(Barang barang)
        {
            barang.Id = _nextId++;
            _barangList.Add(barang);
            return true;
        }

        // READ - Ambil semua barang
        public List<Barang> GetAllBarang()
        {
            return new List<Barang>(_barangList);
        }

        // READ - Ambil barang by ID
        public Barang GetBarangById(int id)
        {
            return _barangList.FirstOrDefault(b => b.Id == id);
        }

        // UPDATE - Update barang
        public bool UpdateBarang(int id, Barang updatedBarang)
        {
            var barang = GetBarangById(id);
            if (barang == null) return false;

            barang.Nama = updatedBarang.Nama;
            barang.Deskripsi = updatedBarang.Deskripsi;
            barang.Harga = updatedBarang.Harga;
            barang.Stok = updatedBarang.Stok;
            barang.Model = updatedBarang.Model;
            barang.Merek = updatedBarang.Merek;
            barang.Jenis = updatedBarang.Jenis;
            barang.UpdatedAt = DateTime.Now;

            return true;
        }

        // UPDATE - Update deskripsi saja
        public bool UpdateDeskripsi(int id, string deskripsi)
        {
            var barang = GetBarangById(id);
            if (barang == null) return false;

            barang.Deskripsi = deskripsi;
            barang.UpdatedAt = DateTime.Now;

            return true;
        }

        // DELETE - Hapus barang
        public bool HapusBarang(int id)
        {
            var barang = GetBarangById(id);
            if (barang == null) return false;

            return _barangList.Remove(barang);
        }

        // SEARCH - Cari barang
        public List<Barang> CariBarang(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAllBarang();

            keyword = keyword.ToLower();
            return _barangList.Where(b =>
                b.Nama.ToLower().Contains(keyword) ||
                b.Deskripsi.ToLower().Contains(keyword) ||
                (b.Model?.ToLower().Contains(keyword) == true) ||
                (b.Merek?.ToLower().Contains(keyword) == true) ||
                b.Jenis.ToLower().Contains(keyword)
            ).ToList();
        }

        // API - Load data dari API
        public async Task<List<Barang>> LoadFromApiAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(API_BASE_URL);
                var json = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<Barang>>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var data = apiResponse?.Data ?? new List<Barang>();
                _barangList.Clear();
                _barangList.AddRange(data);

                if (_barangList.Any())
                    _nextId = _barangList.Max(b => b.Id) + 1;

                return _barangList;
            }
            catch
            {
                return new List<Barang>();
            }
        }

        // API - Sync data ke API
        public async Task SyncToApiAsync()
        {
            try
            {
                var json = JsonSerializer.Serialize(_barangList);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await _httpClient.PostAsync($"{API_BASE_URL}/sync", content);
            }
            catch
            {
                // Handle atau log errors di sini jika diinginkan
            }
        }
    }
}
