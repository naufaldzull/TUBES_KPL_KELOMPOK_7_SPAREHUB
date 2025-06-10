using System;
using System.Collections.Generic;

namespace UlasanDanRatingProduk
{
    internal class Program
    {
        static ReviewService reviewService = new ReviewService();
        static Dictionary<string, Product> produkList = new();

        static void Main(string[] args)
        {
            SeedData();

            int pilihan;
            do
            {
                Console.Clear();
                Console.WriteLine("Menu Ulasan & Rating Produk");
                Console.WriteLine("1. Lihat produk");
                Console.WriteLine("2. Tambah review produk");
                Console.WriteLine("3. Lihat review produk");
                Console.WriteLine("4. Kirim review draft");
                Console.WriteLine("0. Keluar");
                Console.Write("Pilih menu: ");

                if (!int.TryParse(Console.ReadLine(), out pilihan))
                {
                    Console.WriteLine("Input tidak valid. Tekan enter untuk lanjut...");
                    Console.ReadLine();
                    continue;
                }

                switch (pilihan)
                {
                    case 1:
                        TampilkanProduk();
                        break;
                    case 2:
                        TambahReview();
                        break;
                    case 3:
                        LihatReview();
                        break;
                    case 4:
                        KirimReviewDraft();
                        break;
                    case 0:
                        Console.WriteLine("Terima kasih!");
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak tersedia.");
                        break;
                }

                Console.WriteLine("\nTekan enter untuk kembali ke menu...");
                Console.ReadLine();

            } while (pilihan != 0);
        }

        static void SeedData()
        {
            produkList["P001"] = new Product("P001", "Laptop Gaming XYZ");
            produkList["P002"] = new Product("P002", "Smartphone Pro 12");
            produkList["P003"] = new Product("P003", "Kipas Angin Turbo");
        }

        static void TampilkanProduk()
        {
            Console.WriteLine("\nDaftar Produk:");
            foreach (var p in produkList.Values)
            {
                Console.WriteLine($"- {p.Id}: {p.Name}");
            }
        }

        static void TambahReview()
        {
            TampilkanProduk();
            Console.Write("\nMasukkan ID produk: ");
            string id = Console.ReadLine()?.Trim().ToUpper();

            if (!produkList.ContainsKey(id))
            {
                Console.WriteLine("Produk tidak ditemukan.");
                return;
            }

            try
            {
                Console.Write("Nama Anda: ");
                string reviewer = Console.ReadLine()?.Trim();

                Console.Write("Komentar: ");
                string komentar = Console.ReadLine()?.Trim();

                Console.Write("Rating (1-5): ");
                int rating = Convert.ToInt32(Console.ReadLine());

                Review review;
                try
                {
                    review = new Review(reviewer, komentar, rating);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Gagal membuat review: {ex.Message}");
                    return;
                }

                Console.WriteLine("Apakah review ingin disubmit?");
                Console.WriteLine("1. Submit");
                Console.WriteLine("2. Simpan sebagai draft");
                Console.Write("Pilih: ");

                if (!int.TryParse(Console.ReadLine(), out int pilihanTambahReview))
                {
                    Console.WriteLine("Input tidak valid. Review tidak disimpan.");
                    return;
                }

                switch (pilihanTambahReview)
                {
                    case 1:
                        try
                        {
                            review.Submit();
                            reviewService.AddReview(id, review);
                            Console.WriteLine("Review berhasil ditambahkan.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Gagal submit review: " + ex.Message);
                        }
                        break;

                    case 2:
                        reviewService.AddReview(id, review);
                        Console.WriteLine("Review disimpan sebagai draft.");
                        break;

                    default:
                        Console.WriteLine("Pilihan tidak dikenal. Review tidak disimpan.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Gagal submit review: " + ex.Message);
            }
        }

        static void LihatReview()
        {
            TampilkanProduk();
            Console.Write("\nMasukkan ID produk: ");
            string id = Console.ReadLine()?.Trim().ToUpper();

            if (!produkList.ContainsKey(id))
            {
                Console.WriteLine("Produk tidak ditemukan.");
                return;
            }

            Console.WriteLine($"\nReview untuk produk: {produkList[id].Name}");
            reviewService.ShowReviews(id);
        }

        static void KirimReviewDraft()
        {
            TampilkanProduk();
            Console.Write("\nMasukkan ID produk: ");
            string id = Console.ReadLine()?.Trim().ToUpper();

            if (!produkList.ContainsKey(id))
            {
                Console.WriteLine("Produk tidak ditemukan.");
                return;
            }

            Console.WriteLine($"\nDaftar draft review untuk: {produkList[id].Name}");
            reviewService.ShowDrafts(id);

            Console.Write("\nPilih nomor draft yang ingin dikirim (0 untuk batal): ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 0)
            {
                Console.WriteLine("Input tidak valid.");
                return;
            }

            if (index == 0)
            {
                Console.WriteLine("Pengiriman draft dibatalkan.");
                return;
            }

            reviewService.SubmitDraft(id, index);
        }
    }
}