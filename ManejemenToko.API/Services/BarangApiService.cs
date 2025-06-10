using ManejemenToko.API.Model;
using System.Collections.Concurrent;

namespace ManejemenToko.API.Services
{
    public interface IBarangApiService
    {
        Task<List<Barang>> GetAllBarangAsync();
        Task<Barang?> GetBarangByIdAsync(int id);
        Task<List<Barang>> SearchBarangAsync(string keyword);
        Task<List<Barang>> GetBarangByJenisAsync(string jenis);
        Task<bool> DeleteBarangAsync(int id);
        // Method untuk sync data dari frontend
        void SyncDataFromFrontend(List<Barang> frontendData);
    }

    public class BarangApiService : IBarangApiService
    {
        // Simulate database dengan thread-safe collection
        private static readonly ConcurrentDictionary<int, Barang> _databaseBarang = new();
        private static int _nextId = 1;

        static BarangApiService()
        {
            // Initialize "database" dengan sample data
            LoadDatabaseData();
        }

        private static void LoadDatabaseData()
        {
            if (_databaseBarang.IsEmpty)
            {
                var sampleData = new List<Barang>
                {
                    new Barang
                    {
                        Id = _nextId++,
                        Nama = "Kampas Rem Depan",
                        Deskripsi = "Kampas rem depan original untuk motor matic Honda Vario",
                        Harga = 150000,
                        Stok = 10,
                        Model = "Genuine Part",
                        Merek = "Honda",
                        Jenis = "Kaki-kaki"
                    },
                    new Barang
                    {
                        Id = _nextId++,
                        Nama = "Oli Mesin SAE 10W-40",
                        Deskripsi = "Oli mesin 4T premium untuk motor bebek dan matic semua merk",
                        Harga = 45000,
                        Stok = 25,
                        Model = "4T Premium",
                        Merek = "Shell",
                        Jenis = "Mesin"
                    },
                    new Barang
                    {
                        Id = _nextId++,
                        Nama = "Busi Iridium NGK",
                        Deskripsi = "Busi iridium premium tahan lama untuk performa optimal",
                        Harga = 85000,
                        Stok = 15,
                        Model = "G-Power",
                        Merek = "NGK",
                        Jenis = "Kelistrikan"
                    },
                    new Barang
                    {
                        Id = _nextId++,
                        Nama = "V-Belt CVT Honda",
                        Deskripsi = "V-Belt transmisi CVT original untuk Honda Scoopy dan Vario",
                        Harga = 120000,
                        Stok = 8,
                        Model = "CVT Belt",
                        Merek = "Honda",
                        Jenis = "Transmisi"
                    },
                    new Barang
                    {
                        Id = _nextId++,
                        Nama = "Kaca Spion Lipat",
                        Deskripsi = "Spion lipat universal kanan kiri untuk semua jenis motor",
                        Harga = 75000,
                        Stok = 12,
                        Model = "Universal",
                        Merek = "KTC",
                        Jenis = "Sparepart Lainnya"
                    },
                    new Barang
                    {
                        Id = _nextId++,
                        Nama = "Filter Udara K&N",
                        Deskripsi = "Filter udara racing washable untuk performa maksimal",
                        Harga = 200000,
                        Stok = 6,
                        Model = "Washable",
                        Merek = "K&N",
                        Jenis = "Mesin"
                    },
                    new Barang
                    {
                        Id = _nextId++,
                        Nama = "Lampu LED Philips",
                        Deskripsi = "Lampu LED headlight putih terang hemat listrik",
                        Harga = 95000,
                        Stok = 18,
                        Model = "Ultinon",
                        Merek = "Philips",
                        Jenis = "Kelistrikan"
                    },
                    new Barang
                    {
                        Id = _nextId++,
                        Nama = "Shock Belakang YSS",
                        Deskripsi = "Shock absorber belakang adjustable untuk kenyamanan berkendara",
                        Harga = 450000,
                        Stok = 4,
                        Model = "G-Series",
                        Merek = "YSS",
                        Jenis = "Kaki-kaki"
                    }
                };

                foreach (var barang in sampleData)
                {
                    _databaseBarang.TryAdd(barang.Id, barang);
                }
            }
        }

        // Simulate database access dengan delay
        public async Task<List<Barang>> GetAllBarangAsync()
        {
            // Simulate database query delay
            await Task.Delay(100);

            return _databaseBarang.Values.OrderBy(b => b.Id).ToList();
        }

        public async Task<Barang?> GetBarangByIdAsync(int id)
        {
            // Simulate database query delay
            await Task.Delay(50);

            _databaseBarang.TryGetValue(id, out var barang);
            return barang;
        }

        public async Task<List<Barang>> SearchBarangAsync(string keyword)
        {
            // Simulate database search delay
            await Task.Delay(80);

            if (string.IsNullOrWhiteSpace(keyword))
                return await GetAllBarangAsync();

            keyword = keyword.ToLower();
            return _databaseBarang.Values
                .Where(b => b.Nama.ToLower().Contains(keyword) ||
                           b.Deskripsi.ToLower().Contains(keyword) ||
                           (!string.IsNullOrWhiteSpace(b.Model) && b.Model.ToLower().Contains(keyword)) ||
                           (!string.IsNullOrWhiteSpace(b.Merek) && b.Merek.ToLower().Contains(keyword)) ||
                           b.Jenis.ToLower().Contains(keyword))
                .OrderBy(b => b.Id)
                .ToList();
        }

        public async Task<List<Barang>> GetBarangByJenisAsync(string jenis)
        {
            // Simulate database filter delay
            await Task.Delay(60);

            if (string.IsNullOrWhiteSpace(jenis))
                return await GetAllBarangAsync();

            return _databaseBarang.Values
                .Where(b => b.Jenis.Equals(jenis, StringComparison.OrdinalIgnoreCase))
                .OrderBy(b => b.Id)
                .ToList();
        }
        public async Task<bool> DeleteBarangAsync(int id)
        {
            await Task.Delay(50); // simulate delay
            return _databaseBarang.TryRemove(id, out _);
        }

        // Method untuk sync data dari frontend (ketika ada tambah/edit/hapus di frontend)
        public void SyncDataFromFrontend(List<Barang> frontendData)
        {
            // Clear existing data
            _databaseBarang.Clear();

            // Reset next ID
            _nextId = 1;

            // Add all data from frontend
            foreach (var barang in frontendData.OrderBy(b => b.Id))
            {
                // Update ID jika perlu
                if (barang.Id == 0)
                    barang.Id = _nextId++;
                else if (barang.Id >= _nextId)
                    _nextId = barang.Id + 1;

                _databaseBarang.TryAdd(barang.Id, barang);
            }
        }
    }
}
