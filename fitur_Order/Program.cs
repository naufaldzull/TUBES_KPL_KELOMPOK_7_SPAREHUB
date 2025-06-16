using System;

namespace fitur_Order
{
    class Program
    {
        static void Main(string[] args)
        {
            Order orderSystem = new Order();

            while (true)
            {
                Console.WriteLine("\n=== MENU ===");
                Console.WriteLine("1. Tambah produk ke keranjang");
                Console.WriteLine("2. Proses pesanan");
                Console.WriteLine("3. Keluar");
                Console.Write("Pilih opsi (1-3): ");
                string pilihan = Console.ReadLine();

                if (pilihan == "1")
                {
                    Console.Write("Masukkan nama produk: ");
                    string namaProduk = Console.ReadLine();

                    Console.Write("Masukkan jumlah: ");
                    int jumlah = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Masukkan harga (Rp): ");
                    decimal harga = Convert.ToDecimal(Console.ReadLine());

                    bool berhasil = orderSystem.AddToCart(namaProduk, jumlah, harga, out string pesanError);
                    if (berhasil)
                    {
                        Console.WriteLine("Produk berhasil ditambahkan ke keranjang.");
                    }
                    else
                    {
                        Console.WriteLine("Gagal menambahkan produk: " + pesanError);
                    }
                }
                else if (pilihan == "2")
                {
                    Console.WriteLine("\nPilih metode pembayaran:");
                    var metode = orderSystem.GetPaymentMethods();
                    for (int i = 0; i < metode.Count; i++)
                    {
                        Console.WriteLine($"{i}. {metode[i]}");
                    }

                    Console.Write("Masukkan nomor metode pembayaran: ");
                    int indexMetode = Convert.ToInt32(Console.ReadLine());

                    bool sukses = orderSystem.ProcessOrder(indexMetode, out string pesan, out decimal total);
                    Console.WriteLine(pesan);
                }
                else if (pilihan == "3")
                {
                    Console.WriteLine("Terima kasih! Program selesai.");
                    break;
                }
                else
                {
                    Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
                }
            }
        }
    }
}
