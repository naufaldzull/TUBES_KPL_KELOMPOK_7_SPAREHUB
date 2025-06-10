using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlasanDanRatingProduk
{
    public class ReviewService
    {
        private readonly Dictionary<string, List<Review>> reviewMap = new();

        public void AddReview(string productId, Review review)
        {
            if (string.IsNullOrWhiteSpace(productId))
                throw new ArgumentException("Product ID tidak boleh kosong");
            if (review == null)
                throw new ArgumentNullException(nameof(review));

            Debug.Assert(!string.IsNullOrWhiteSpace(productId), "Product ID tidak boleh kosong");
            Debug.Assert(review != null, "Review tidak boleh null");

            if (!reviewMap.ContainsKey(productId))
                reviewMap[productId] = new List<Review>();

            reviewMap[productId].Add(review);
        }

        public void ShowReviews(string productId)
        {
            if (string.IsNullOrWhiteSpace(productId))
            {
                Console.WriteLine("ID produk tidak valid.");
                return;
            }

            Debug.Assert(!string.IsNullOrWhiteSpace(productId), "Product ID tidak boleh kosong");

            if (!reviewMap.ContainsKey(productId) || reviewMap[productId].Count == 0)
            {
                Console.WriteLine("Belum ada ulasan untuk produk ini.");
                return;
            }

            foreach (var review in reviewMap[productId])
            {
                review.Display();
            }
        }

        public void ShowDrafts(string productId)
        {
            if (string.IsNullOrWhiteSpace(productId))
                throw new ArgumentException("ID produk tidak boleh kosong");

            Debug.Assert(!string.IsNullOrWhiteSpace(productId), "ID produk tidak boleh kosong");

            if (!reviewMap.ContainsKey(productId))
            {
                Console.WriteLine("Belum ada review untuk produk ini.");
                return;
            }

            var drafts = reviewMap[productId]
                .Where(r => r.State == ReviewState.Draft)
                .ToList();

            if (drafts.Count == 0)
            {
                Console.WriteLine("Tidak ada review draft untuk produk ini.");
                return;
            }

            for (int i = 0; i < drafts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Draft oleh {drafts[i].Reviewer}: {drafts[i].Comment} ({drafts[i].Rating} bintang)");
            }
        }

        public void SubmitDraft(string productId, int index)
        {
            if (string.IsNullOrWhiteSpace(productId))
                throw new ArgumentException("ID produk tidak boleh kosong");

            if (index < 1)
                throw new ArgumentOutOfRangeException(nameof(index), "Index harus lebih dari 0");

            Debug.Assert(!string.IsNullOrWhiteSpace(productId), "ID produk tidak boleh kosong");
            Debug.Assert(index >= 1, "Index harus lebih dari atau sama dengan 1");

            if (!reviewMap.ContainsKey(productId))
                throw new InvalidOperationException("Tidak ditemukan review untuk produk ini.");

            var drafts = reviewMap[productId]
                .Where(r => r.State == ReviewState.Draft)
                .ToList();

            if (index > drafts.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index melebihi jumlah draft yang tersedia.");

            var draftToSubmit = drafts[index - 1];

            try
            {
                draftToSubmit.Submit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Gagal mengirim draft: " + ex.Message);
            }
        }
    }
}
