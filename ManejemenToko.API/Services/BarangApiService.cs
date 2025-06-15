using ManajemenToko.API.Model; 
using System.Collections.Concurrent;

namespace ManajemenToko.API.Services
{
    public interface IBarangApiService
    {
        Task<List<Barang>> GetAllBarangAsync();
        Task<Barang?> GetBarangByIdAsync(int id);
        Task<List<Barang>> SearchBarangAsync(string keyword);
        Task<List<Barang>> GetBarangByJenisAsync(string jenis);
        Task<bool> DeleteBarangAsync(int id);
        void SyncDataFromFrontend(List<Barang> frontendData);
    }

    public class BarangApiService : IBarangApiService
    {
        private static readonly ConcurrentDictionary<int, Barang> _barangStorage = new(); // Penamaan lebih eksplisit 
        private static int _nextId = 1;

        static BarangApiService()
        {
            LoadDatabaseData();
        }

        private static void LoadDatabaseData()
        {
            if (_barangStorage.IsEmpty)
            {
                var sampleData = new List<Barang>
                {
                    new() { Id = _nextId++, Nama = "Kampas Rem Depan", Deskripsi = "Kampas rem depan original untuk motor matic Honda Vario", Harga = 150_000, Stok = 10, Model = "Genuine Part", Merek = "Honda", Jenis = "Kaki-kaki" },
                    new() { Id = _nextId++, Nama = "Oli Mesin SAE 10W-40", Deskripsi = "Oli mesin 4T premium untuk motor bebek dan matic semua merk", Harga = 45_000, Stok = 25, Model = "4T Premium", Merek = "Shell", Jenis = "Mesin" },
                    new() { Id = _nextId++, Nama = "Busi Iridium NGK", Deskripsi = "Busi iridium premium tahan lama untuk performa optimal", Harga = 85_000, Stok = 15, Model = "G-Power", Merek = "NGK", Jenis = "Kelistrikan" },
                    new() { Id = _nextId++, Nama = "V-Belt CVT Honda", Deskripsi = "V-Belt transmisi CVT original untuk Honda Scoopy dan Vario", Harga = 120_000, Stok = 8, Model = "CVT Belt", Merek = "Honda", Jenis = "Transmisi" },
                    new() { Id = _nextId++, Nama = "Kaca Spion Lipat", Deskripsi = "Spion lipat universal kanan kiri untuk semua jenis motor", Harga = 75_000, Stok = 12, Model = "Universal", Merek = "KTC", Jenis = "Sparepart Lainnya" },
                    new() { Id = _nextId++, Nama = "Filter Udara K&N", Deskripsi = "Filter udara racing washable untuk performa maksimal", Harga = 200_000, Stok = 6, Model = "Washable", Merek = "K&N", Jenis = "Mesin" },
                    new() { Id = _nextId++, Nama = "Lampu LED Philips", Deskripsi = "Lampu LED headlight putih terang hemat listrik", Harga = 95_000, Stok = 18, Model = "Ultinon", Merek = "Philips", Jenis = "Kelistrikan" },
                    new() { Id = _nextId++, Nama = "Shock Belakang YSS", Deskripsi = "Shock absorber belakang adjustable untuk kenyamanan berkendara", Harga = 450_000, Stok = 4, Model = "G-Series", Merek = "YSS", Jenis = "Kaki-kaki" }
                };

                foreach (var barang in sampleData)
                    _barangStorage.TryAdd(barang.Id, barang);
            }
        }

        public async Task<List<Barang>> GetAllBarangAsync()
        {
            await Task.Delay(100); // Simulasi delay 
            return _barangStorage.Values.OrderBy(b => b.Id).ToList();
        }

        public async Task<Barang?> GetBarangByIdAsync(int id)
        {
            await Task.Delay(50);
            _barangStorage.TryGetValue(id, out var barang);
            return barang;
        }

        public async Task<List<Barang>> SearchBarangAsync(string keyword)
        {
            await Task.Delay(80);

            if (string.IsNullOrWhiteSpace(keyword))
                return await GetAllBarangAsync();

            var lowerKeyword = keyword.ToLower();

            return _barangStorage.Values
                .Where(b =>
                    b.Nama.ToLower().Contains(lowerKeyword) ||
                    b.Deskripsi.ToLower().Contains(lowerKeyword) ||
                    (!string.IsNullOrWhiteSpace(b.Model) && b.Model.ToLower().Contains(lowerKeyword)) ||
                    (!string.IsNullOrWhiteSpace(b.Merek) && b.Merek.ToLower().Contains(lowerKeyword)) ||
                    b.Jenis.ToLower().Contains(lowerKeyword)
                )
                .OrderBy(b => b.Id)
                .ToList();
        }

        public async Task<List<Barang>> GetBarangByJenisAsync(string jenis)
        {
            await Task.Delay(60);

            if (string.IsNullOrWhiteSpace(jenis))
                return await GetAllBarangAsync();

            return _barangStorage.Values
                .Where(b => b.Jenis.Equals(jenis, StringComparison.OrdinalIgnoreCase))
                .OrderBy(b => b.Id)
                .ToList();
        }

        public async Task<bool> DeleteBarangAsync(int id)
        {
            await Task.Delay(50);
            return _barangStorage.TryRemove(id, out _);
        }

        public void SyncDataFromFrontend(List<Barang> frontendData)
        {
            _barangStorage.Clear();
            _nextId = 1;

            foreach (var barang in frontendData.OrderBy(b => b.Id))
            {
                if (barang.Id == 0)
                    barang.Id = _nextId++;
                else if (barang.Id >= _nextId)
                    _nextId = barang.Id + 1;

                _barangStorage.TryAdd(barang.Id, barang);
            }
        }
    }
}
