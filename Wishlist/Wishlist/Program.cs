using System;
using System.Collections.Generic;

namespace WishlistApp
{
    // Enum untuk merepresentasikan status dari wishlist
    public enum StatusWishlist
    {
        Kosong,
        AdaItem
    }

    // Class utama yang menangani logika wishlist
    public class WishlistClass
    {
        // List untuk menyimpan item wishlist
        private List<string> _items = new List<string>();

        // Properti untuk menyimpan status wishlist (Kosong / AdaItem)
        public StatusWishlist Status { get; private set; } = StatusWishlist.Kosong;

        /// <summary>
        /// Menambahkan item ke dalam daftar wishlist.
        /// Akan melempar exception jika item kosong.
        /// </summary>
        /// <param name="_item">Nama item yang ingin ditambahkan</param>
        public void TambahItem(string item)
        {
            if (string.IsNullOrWhiteSpace(item))
            {
                throw new ArgumentException("Item tidak boleh kosong.");
            }

            _items.Add(item);
            UpdateStatus(); // Perbarui status setelah menambah item
            Console.WriteLine($"'{item}' ditambahkan.");
        }

        /// <summary>
        /// Menghapus item dari wishlist jika ditemukan.
        /// </summary>
        /// <param name="item">Nama item yang ingin dihapus</param>
        public void HapusItem(string item)
        {
            if (_items.Remove(item))
            {
                UpdateStatus(); // Perbarui status setelah menghapus item
                Console.WriteLine($"'{item}' dihapus.");
            }
            else
            {
                Console.WriteLine($"'{item}' tidak ditemukan.");
            }
        }

        /// <summary>
        /// Menampilkan seluruh item dalam wishlist.
        /// Jika kosong, akan ditampilkan pesan 'Kosong.'
        /// </summary>
        public void TampilkanWishlist()
        {
            Console.WriteLine("Wishlist Kamu:");
            if (_items.Count == 0)
            {
                Console.WriteLine("Kosong.");
            }
            else
            {
                foreach (var item in _items)
                {
                    Console.WriteLine($"- {item}");
                }
            }
        }

        /// <summary>
        /// Memperbarui status wishlist sesuai jumlah item saat ini.
        /// </summary>
        private void UpdateStatus()
        {
            Status = _items.Count == 0 ? StatusWishlist.Kosong : StatusWishlist.AdaItem;
        }
    }


    // Program utama (console app)
    class Program
    {
        static void Main(string[] args)
        {
            // Inisialisasi objek wishlist
            var wishlist = new WishlistClass();

            Console.WriteLine("=== Wishlist Console App ===");

            // Loop utama menu
            while (true)
            {
                // Tampilkan menu
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Tambah item");
                Console.WriteLine("2. Hapus item");
                Console.WriteLine("3. Tampilkan wishlist");
                Console.WriteLine("4. Keluar");
                Console.Write("Pilih opsi (1-4): ");
                string pilihan = Console.ReadLine();

                // Logika menu
                switch (pilihan)
                {
                    case "1":
                        Console.Write("Masukkan item: ");
                        string item = Console.ReadLine();
                        try
                        {
                            wishlist.TambahItem(item); // Tambah item ke wishlist
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        break;

                    case "2":
                        Console.Write("Item yang ingin dihapus: ");
                        string itemHapus = Console.ReadLine();
                        wishlist.HapusItem(itemHapus); // Hapus item dari wishlist
                        break;

                    case "3":
                        wishlist.TampilkanWishlist(); // Tampilkan isi wishlist
                        break;

                    case "4":
                        Console.WriteLine("Terima kasih, program selesai.");
                        return;

                    default:
                        Console.WriteLine("Opsi tidak valid."); // Opsi tidak dikenal
                        break;
                }
            }
        }
    }
}
