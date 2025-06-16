using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Searching
{
    /// <summary>
    /// Representasi kontrak dasar untuk sebuah sparepart.
    /// </summary>
    public interface ISparepart
    {
        string Nama { get; }
        string Kategori { get; }
        string Merek { get; }
        string KompatibelDengan { get; }
        decimal Harga { get; }
    }

    /// <summary>
    /// Mesin pencari generik untuk data sparepart.
    /// </summary>
    public class SearchEngine<T> where T : ISparepart
    {
        private readonly List<T> _daftarSparepart;
        private readonly Dictionary<string, List<T>> _tabelKategori;
        private readonly Dictionary<string, List<T>> _tabelMerek;

        /// <summary>
        /// Implementasi singleton generik untuk SearchEngine.
        /// </summary>
        public static class SearchEngineSingleton<TSparepart> where TSparepart : class, ISparepart
        {
            private static SearchEngine<TSparepart>? _instance;
            private static readonly object _lock = new();
            private static Func<List<TSparepart>>? _dataProvider;

            /// <summary>
            /// Instance singleton SearchEngine.
            /// </summary>
            public static SearchEngine<TSparepart>? Instance
            {
                get
                {
                    if (_instance == null && _dataProvider != null)
                    {
                        lock (_lock)
                        {
                            if (_instance == null && _dataProvider != null)
                            {
                                var data = _dataProvider();
                                _instance = new SearchEngine<TSparepart>(data);
                            }
                        }
                    }
                    return _instance;
                }
            }

            /// <summary>
            /// Inisialisasi singleton dengan data provider.
            /// </summary>
            public static void Initialize(Func<List<TSparepart>> dataProvider)
            {
                _dataProvider = dataProvider;
            }

            /// <summary>
            /// Reset instance singleton.
            /// </summary>
            public static void ResetInstance()
            {
                lock (_lock)
                {
                    _instance = null;
                }
            }
        }

        /// <summary>
        /// Jumlah total data sparepart.
        /// </summary>
        public int JumlahData => _daftarSparepart.Count;

        /// <summary>
        /// Daftar kategori unik.
        /// </summary>
        public IReadOnlyCollection<string> DaftarKategori => _tabelKategori.Keys;

        /// <summary>
        /// Daftar merek unik.
        /// </summary>
        public IReadOnlyCollection<string> DaftarMerek => _tabelMerek.Keys;

        /// <summary>
        /// Konstruktor utama untuk SearchEngine.
        /// </summary>
        public SearchEngine(List<T> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data), "Data sparepart tidak boleh null");

            if (data.Count == 0)
                throw new ArgumentException("Data sparepart tidak boleh kosong", nameof(data));

            _daftarSparepart = new List<T>(data);
            _tabelKategori = new Dictionary<string, List<T>>(StringComparer.OrdinalIgnoreCase);
            _tabelMerek = new Dictionary<string, List<T>>(StringComparer.OrdinalIgnoreCase);

            BangunTabel();
        }

        /// <summary>
        /// Ambil semua data sparepart.
        /// </summary>
        public List<T> GetAllData()
        {
            return new List<T>(_daftarSparepart);
        }

        private void BangunTabel()
        {
            foreach (var item in _daftarSparepart)
            {
                if (item == null || string.IsNullOrWhiteSpace(item.Kategori) || string.IsNullOrWhiteSpace(item.Merek))
                    continue;

                BuatIndeksKategori(item);
                BuatIndeksMerek(item);
            }
        }

        private void BuatIndeksKategori(T item)
        {
            if (!_tabelKategori.ContainsKey(item.Kategori))
                _tabelKategori[item.Kategori] = new List<T>();
            _tabelKategori[item.Kategori].Add(item);
        }

        private void BuatIndeksMerek(T item)
        {
            if (!_tabelMerek.ContainsKey(item.Merek))
                _tabelMerek[item.Merek] = new List<T>();
            _tabelMerek[item.Merek].Add(item);
        }

        /// <summary>
        /// Cari sparepart berdasarkan kategori.
        /// </summary>
        public List<T> CariBerdasarkanKategori(string kategori)
        {
            var sanitizedKategori = ValidateSearchInput(kategori, "Kategori");
            return _tabelKategori.TryGetValue(sanitizedKategori, out var list) ? new List<T>(list) : new List<T>();
        }

        /// <summary>
        /// Cari sparepart berdasarkan merek.
        /// </summary>
        public List<T> CariBerdasarkanMerek(string merek)
        {
            var sanitizedMerek = ValidateSearchInput(merek, "Merek");
            return _tabelMerek.TryGetValue(sanitizedMerek, out var list) ? new List<T>(list) : new List<T>();
        }

        /// <summary>
        /// Cari sparepart berdasarkan kompatibilitas dengan nama motor.
        /// </summary>
        public List<T> CariBerdasarkanKompatibilitas(string motor)
        {
            var sanitizedMotor = ValidateSearchInput(motor, "Nama motor");

            var hasil = new List<T>();
            foreach (var item in _daftarSparepart)
            {
                if (item?.KompatibelDengan != null &&
                    item.KompatibelDengan.IndexOf(sanitizedMotor, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    hasil.Add(item);
                }
            }

            return hasil;
        }

        /// <summary>
        /// Cari sparepart berdasarkan rentang harga.
        /// </summary>
        public List<T> CariBerdasarkanRangeHarga(decimal hargaMin, decimal hargaMax)
        {
            if (hargaMin < 0 || hargaMax < 0)
                throw new ArgumentException("Harga tidak boleh negatif");

            if (hargaMin > hargaMax)
                throw new ArgumentException("Harga minimum tidak boleh lebih besar dari harga maksimum");

            var hasil = new List<T>();
            foreach (var item in _daftarSparepart)
            {
                if (item.Harga >= hargaMin && item.Harga <= hargaMax)
                    hasil.Add(item);
            }

            return hasil;
        }

        /// <summary>
        /// Ambil statistik jumlah sparepart per kategori.
        /// </summary>
        public Dictionary<string, int> GetStatistikKategori()
        {
            var stats = new Dictionary<string, int>();
            foreach (var kategori in _tabelKategori)
            {
                stats[kategori.Key] = kategori.Value.Count;
            }
            return stats;
        }

        /// <summary>
        /// Kembalikan informasi ringkas mengenai mesin pencari.
        /// </summary>
        public string GetInfoSearchEngine()
        {
            return $"Total Sparepart: {JumlahData}, " +
                   $"Jumlah Kategori: {_tabelKategori.Count}, " +
                   $"Jumlah Merek: {_tabelMerek.Count}";
        }

        /// <summary>
        /// Menambahkan validasi input untuk pencarian.
        /// </summary>
        private static string ValidateSearchInput(string input, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException($"{parameterName} tidak boleh kosong", nameof(input));

            var sanitized = input.Trim();
            sanitized = Regex.Replace(sanitized, @"[<>""'%;()&+]", "");

            if (sanitized.Length > 100)
                sanitized = sanitized[..100];

            if (string.IsNullOrWhiteSpace(sanitized))
                throw new ArgumentException($"{parameterName} tidak valid setelah sanitasi", nameof(input));

            return sanitized;
        }
    }
}
