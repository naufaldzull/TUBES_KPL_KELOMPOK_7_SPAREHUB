using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlasanDanRatingProduk
{
    public class Product
    {
        public string Id { get; }
        public string Name { get; }

        public Product(string id, string name)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("ID produk tidak boleh kosong");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nama produk tidak boleh kosong");

            Debug.Assert(!string.IsNullOrWhiteSpace(id), "ID produk tidak boleh kosong");
            Debug.Assert(!string.IsNullOrWhiteSpace(name), "Nama produk tidak boleh kosong");

            Id = id;
            Name = name;
        }
    }
}
