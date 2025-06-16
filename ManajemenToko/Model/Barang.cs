using System;
using System.ComponentModel.DataAnnotations;

namespace ManajemenToko.Models
{
    public class Barang // PascalCase untuk class name 
    {
        public int Id { get; set; } // PascalCase untuk property 

        [Required(ErrorMessage = "Nama barang tidak boleh kosong")]
        [StringLength(100, ErrorMessage = "Nama barang maksimal 100 karakter")]
        public string Nama { get; set; } // PascalCase 

        [Required(ErrorMessage = "Deskripsi tidak boleh kosong")]
        [StringLength(500, ErrorMessage = "Deskripsi maksimal 500 karakter")]
        public string Deskripsi { get; set; } // PascalCase 

        [Required(ErrorMessage = "Harga tidak boleh kosong")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Harga harus lebih besar dari 0")]
        public decimal Harga { get; set; } // PascalCase 

        [Required(ErrorMessage = "Stok tidak boleh kosong")]
        [Range(0, int.MaxValue, ErrorMessage = "Stok tidak boleh negatif")]
        public int Stok { get; set; } // PascalCase 

        [StringLength(50, ErrorMessage = "Model maksimal 50 karakter")]
        public string Model { get; set; } // PascalCase 

        [StringLength(50, ErrorMessage = "Merek maksimal 50 karakter")]
        public string Merek { get; set; } // PascalCase 

        [Required(ErrorMessage = "Jenis tidak boleh kosong")]
        public string Jenis { get; set; } // PascalCase 

        public DateTime CreatedAt { get; set; } = DateTime.Now; // PascalCase 
        public DateTime? UpdatedAt { get; set; } // PascalCase 

        // Constructor
        public Barang() // PascalCase for constructor 
        {
            CreatedAt = DateTime.Now;
        }

        public Barang( // PascalCase constructor name 
            string nama, // camelCase untuk parameter 
            string deskripsi,
            decimal harga,
            int stok,
            string model = "",
            string merek = "",
            string jenis = "")
        {
            Nama = nama;
            Deskripsi = deskripsi;
            Harga = harga;
            Stok = stok;
            Model = model;
            Merek = merek;
            Jenis = jenis;
            CreatedAt = DateTime.Now;
        }

        // Method untuk validasi
        public bool IsValid() // PascalCase untuk method 
        {
            return !string.IsNullOrWhiteSpace(Nama) &&
                   !string.IsNullOrWhiteSpace(Deskripsi) &&
                   !string.IsNullOrWhiteSpace(Jenis) &&
                   Harga > 0 &&
                   Stok >= 0;
        }

        // Static method untuk jenis yang tersedia
        public static string[] GetAvailableJenis() // PascalCase 
        {
            return new[]
            {
                "Kelistrikan",
                "Mesin",
                "Transmisi",
                "Kaki-kaki",
                "Sparepart Lainnya"
            };
        }

        // Override ToString untuk debugging
        public override string ToString() // PascalCase 
        {
            var kategori = string.Empty; // camelCase 

            if (!string.IsNullOrWhiteSpace(Model) || !string.IsNullOrWhiteSpace(Merek))
            {
                kategori = $" [{Merek} {Model} - {Jenis}]";
            }
            else if (!string.IsNullOrWhiteSpace(Jenis))
            {
                kategori = $" [{Jenis}]";
            }

            return $"[{Id}] {Nama}{kategori} - Rp{Harga:N0} (Stok: {Stok})";
        }
    }
}
