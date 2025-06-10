using System;
using System.Collections.Generic;
using Wishlist;

namespace Wishlist
{
    public enum StatusWishlist
    {
        Kosong, AdaItem
    }

    public class WishlistClass
    {
        private List<string> items = new List<string>();
        public StatusWishlist Status { get; private set; } = StatusWishlist.Kosong;

        public void TambahItem(string item)
        {
            if (string.IsNullOrWhiteSpace(item))
            {
                throw new ArgumentException("Item tidak boleh kosong ygy");
            }
            items.Add(item);
            UpdateStatus();
            Console.WriteLine($"'{item}' ditambahkan.");
        }

        public void HapusItem(string item)
        {
            if (items.Remove(item))
            {
                UpdateStatus();
                Console.WriteLine($"'{item}' dihapus.");
            }
            else
            {
                Console.WriteLine($"'{item}' tidak ditemukan.");
            }
        }

        public void TampilkanWishlist()
        {
            Console.WriteLine("Wishlist Kamu: ");
            if (items.Count == 0)
            {
                Console.WriteLine("Kosong.");
            }
            else
            {
                foreach (var item in items)
                {
                    Console.WriteLine($"- {item}");
                }
            }
        }

        private void UpdateStatus()
        {
            Status = items.Count == 0 ? StatusWishlist.Kosong : StatusWishlist.AdaItem;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        WishlistClass wishlist = new WishlistClass();

        Console.WriteLine("=== Wishlist Console App ===");
        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Tambah item");
            Console.WriteLine("2. Hapus item");
            Console.WriteLine("3. Tampilkan wishlist");
            Console.WriteLine("4. Keluar");
            Console.Write("Pilih opsi (1-4): ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Write("Masukkan item: ");
                    string item = Console.ReadLine();
                    try
                    {
                        wishlist.TambahItem(item);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                    break;
                case "2":
                    Console.Write("Item yang ingin dihapus: ");
                    string hapus = Console.ReadLine();
                    wishlist.HapusItem(hapus);
                    break;
                case "3":
                    wishlist.TampilkanWishlist();
                    break;
                case "4":
                    Console.WriteLine("Terima kasih, program selesai.");
                    return;
                default:
                    Console.WriteLine("Opsi tidak valid.");
                    break;
            }
        }
    }
}