using ManajemenToko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManajemenToko.Service
{
    class BarangService
    {
        // In-memory storage (nanti ganti dengan database)
        private static List<Barang> _barangList = new List<Barang>();
        private static int _nextId = 1;

        static BarangService()
        {
            // Initialize dengan sample data
            InitializeSampleData();
        }

        private static void InitializeSampleData()
        {
            if (_barangList.Count == 0)
            {
                _barangList.AddRange(new List<Barang>
                {
                    new Barang("Kampas Rem Depan", "Kampas rem depan original", 150000, 10, "Vario", "Honda", "Kaki-kaki") { Id = _nextId++ },
                    new Barang("Oli Mesin", "Oli mesin 4T premium", 45000, 25, "", "Shell", "Mesin") { Id = _nextId++ },
                    new Barang("Busi Iridium", "Busi iridium tahan lama", 85000, 15, "Beat", "Honda", "Kelistrikan") { Id = _nextId++ },
                    new Barang("V-Belt", "V-Belt transmisi automatic", 120000, 8, "Scoopy", "Honda", "Transmisi") { Id = _nextId++ },
                    new Barang("Kaca Spion", "Spion lipat kanan kiri", 75000, 12, "", "KTC", "Sparepart Lainnya") { Id = _nextId++ }
                });
            }
        }

        // CREATE - Tambah barang baru
        public bool TambahBarang(Barang barang)
        {
            try
            {
                // Validasi input (Design by Contract - Precondition)
                if (barang == null)
                    throw new ArgumentNullException(nameof(barang), "Barang tidak boleh null");

                if (!barang.IsValid())
                    throw new ArgumentException("Data barang tidak valid");

                // Validasi jenis harus dari list yang tersedia
                if (!Barang.GetAvailableJenis().Contains(barang.Jenis))
                    throw new ArgumentException($"Jenis '{barang.Jenis}' tidak valid");

                // Cek duplikasi nama dengan model dan merek yang sama
                bool isDuplicate = _barangList.Any(b =>
                    b.Nama.Equals(barang.Nama, StringComparison.OrdinalIgnoreCase) &&
                    (string.IsNullOrWhiteSpace(barang.Model) || b.Model.Equals(barang.Model, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrWhiteSpace(barang.Merek) || b.Merek.Equals(barang.Merek, StringComparison.OrdinalIgnoreCase)));

                if (isDuplicate)
                    throw new InvalidOperationException("Barang dengan nama, model, dan merek tersebut sudah ada");

                // Set ID dan timestamp
                barang.Id = _nextId++;
                barang.CreatedAt = DateTime.Now;

                // Add to list
                _barangList.Add(barang);

                return true; // Postcondition: barang berhasil ditambahkan
            }
            catch (Exception)
            {
                throw; // Re-throw untuk handling di level atas
            }
        }

        // READ - Ambil semua barang
        public List<Barang> GetAllBarang()
        {
            try
            {
                // Return copy untuk mencegah modification dari luar
                return new List<Barang>(_barangList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // READ - Ambil barang by ID
        public Barang GetBarangById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("ID harus lebih besar dari 0");

                var barang = _barangList.FirstOrDefault(b => b.Id == id);
                return barang; // Null jika tidak ditemukan
            }
            catch (Exception)
            {
                throw;
            }
        }

        // UPDATE - Update barang
        public bool UpdateBarang(int id, Barang updatedBarang)
        {
            try
            {
                // Validasi input
                if (id <= 0)
                    throw new ArgumentException("ID harus lebih besar dari 0");

                if (updatedBarang == null)
                    throw new ArgumentNullException(nameof(updatedBarang));

                if (!updatedBarang.IsValid())
                    throw new ArgumentException("Data barang tidak valid");

                // Cari barang yang akan diupdate
                var existingBarang = _barangList.FirstOrDefault(b => b.Id == id);
                if (existingBarang == null)
                    throw new InvalidOperationException($"Barang dengan ID {id} tidak ditemukan");

                // Cek duplikasi nama (kecuali untuk barang itu sendiri)
                if (_barangList.Any(b => b.Id != id && b.Nama.Equals(updatedBarang.Nama, StringComparison.OrdinalIgnoreCase)))
                    throw new InvalidOperationException("Barang dengan nama tersebut sudah ada");

                // Update data
                existingBarang.Nama = updatedBarang.Nama;
                existingBarang.Deskripsi = updatedBarang.Deskripsi;
                existingBarang.Harga = updatedBarang.Harga;
                existingBarang.Stok = updatedBarang.Stok;
                existingBarang.Model = updatedBarang.Model;
                existingBarang.Merek = updatedBarang.Merek;
                existingBarang.Jenis = updatedBarang.Jenis;
                existingBarang.UpdatedAt = DateTime.Now;

                return true; // Postcondition: barang berhasil diupdate
            }
            catch (Exception)
            {
                throw;
            }
        }

        // UPDATE - Update deskripsi saja
        public bool UpdateDeskripsi(int id, string deskripsi)
        {
            try
            {
                // Validasi input
                if (id <= 0)
                    throw new ArgumentException("ID harus lebih besar dari 0");

                if (string.IsNullOrWhiteSpace(deskripsi))
                    throw new ArgumentException("Deskripsi tidak boleh kosong");

                // Cari barang
                var barang = _barangList.FirstOrDefault(b => b.Id == id);
                if (barang == null)
                    throw new InvalidOperationException($"Barang dengan ID {id} tidak ditemukan");

                // Update deskripsi
                barang.Deskripsi = deskripsi.Trim();
                barang.UpdatedAt = DateTime.Now;

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE - Hapus barang
        public bool HapusBarang(int id)
        {
            try
            {
                // Validasi input
                if (id <= 0)
                    throw new ArgumentException("ID harus lebih besar dari 0");

                // Cari dan hapus barang
                var barang = _barangList.FirstOrDefault(b => b.Id == id);
                if (barang == null)
                    throw new InvalidOperationException($"Barang dengan ID {id} tidak ditemukan");

                bool removed = _barangList.Remove(barang);
                return removed; // Postcondition: barang berhasil dihapus
            }
            catch (Exception)
            {
                throw;
            }
        }

        // UTILITY - Cari barang by nama, model, merek, atau jenis
        public List<Barang> CariBarang(string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                    return GetAllBarang();

                keyword = keyword.ToLower();
                return _barangList
                    .Where(b => b.Nama.ToLower().Contains(keyword) ||
                               b.Deskripsi.ToLower().Contains(keyword) ||
                               (!string.IsNullOrWhiteSpace(b.Model) && b.Model.ToLower().Contains(keyword)) ||
                               (!string.IsNullOrWhiteSpace(b.Merek) && b.Merek.ToLower().Contains(keyword)) ||
                               b.Jenis.ToLower().Contains(keyword))
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // UTILITY - Filter barang by jenis
        public List<Barang> GetBarangByJenis(string jenis)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(jenis))
                    return GetAllBarang();

                return _barangList
                    .Where(b => b.Jenis.Equals(jenis, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // UTILITY - Filter barang by merek
        public List<Barang> GetBarangByMerek(string merek)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(merek))
                    return GetAllBarang();

                return _barangList
                    .Where(b => !string.IsNullOrWhiteSpace(b.Merek) &&
                               b.Merek.Equals(merek, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // UTILITY - Get statistik
        public (int TotalBarang, decimal TotalNilai, int TotalStok) GetStatistik()
        {
            try
            {
                var totalBarang = _barangList.Count;
                var totalNilai = _barangList.Sum(b => b.Harga * b.Stok);
                var totalStok = _barangList.Sum(b => b.Stok);

                return (totalBarang, totalNilai, totalStok);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // UTILITY - Validasi stok
        public bool IsStokCukup(int id, int jumlah)
        {
            try
            {
                var barang = GetBarangById(id);
                return barang != null && barang.Stok >= jumlah;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // UTILITY - Reset data (untuk testing)
        public void ResetData()
        {
            _barangList.Clear();
            _nextId = 1;
            InitializeSampleData();
        }
    }
}
