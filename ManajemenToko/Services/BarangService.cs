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
    /// <summary>
    /// Generic API response wrapper.
    /// </summary>
    public class ApiResponse<T> // PascalCase 
    {
        public bool Success { get; set; } // PascalCase 
        public string Message { get; set; } // PascalCase 
        public T? Data { get; set; } // Nullable + PascalCase 
    }

    /// <summary>
    /// Service for managing Barang operations.
    /// Singleton pattern + in-memory + API sync.
    /// </summary>
    public class BarangService // PascalCase 
    {
        // Singleton instance
        private static readonly BarangService _instance = new(); // camelCase for private field 
        public static BarangService Instance => _instance; // PascalCase for public static property 

        // Constructor privat agar tidak bisa instance dari luar
        private BarangService() { }

        // Data storage
        private readonly List<Barang> _barangList = new(); // camelCase 
        private int _nextId = 1;

        // API connection
        private static readonly HttpClient _httpClient = new(); // camelCase 
        private const string ApiBaseUrl = "https://localhost:7067/api/toko"; // PascalCase for constants 

        // CREATE - Tambah barang
        public bool TambahBarang(Barang barang) // PascalCase method, camelCase parameter 
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

        // UPDATE - Update barang lengkap
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

        // SEARCH - Cari barang by keyword
        public List<Barang> CariBarang(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAllBarang();

            var lowerKeyword = keyword.ToLower(); // camelCase 

            return _barangList.Where(b =>
                b.Nama.ToLower().Contains(lowerKeyword) ||
                b.Deskripsi.ToLower().Contains(lowerKeyword) ||
                (b.Model?.ToLower().Contains(lowerKeyword) == true) ||
                (b.Merek?.ToLower().Contains(lowerKeyword) == true) ||
                b.Jenis.ToLower().Contains(lowerKeyword)
            ).ToList();
        }

        // API - Load data dari API
        public async Task<List<Barang>> LoadFromApiAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(ApiBaseUrl);
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
                await _httpClient.PostAsync($"{ApiBaseUrl}/sync", content);
            }
            catch
            {
                // Optional: log or handle error
            }
        }
    }
}
