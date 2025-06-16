using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace UlasanDanRatingProduk
{
    /// <summary>
    /// Service untuk menyimpan dan mengelola review produk secara lokal.
    /// </summary>
    public class ReviewService
    {
        private static readonly ReviewService _instance = new();
        public static ReviewService Instance => _instance;

        private readonly Dictionary<string, List<Review>> _reviewMap = new();

        private ReviewService() { }

        /// <summary>
        /// Menambahkan review ke produk tertentu.
        /// </summary>
        public void AddReview(string productId, Review review)
        {
            if (string.IsNullOrWhiteSpace(productId))
                throw new ArgumentException("Product ID tidak boleh kosong");
            if (review == null)
                throw new ArgumentNullException(nameof(review));

            Debug.Assert(!string.IsNullOrWhiteSpace(productId), "Product ID tidak boleh kosong");
            Debug.Assert(review != null, "Review tidak boleh null");

            if (!_reviewMap.ContainsKey(productId))
                _reviewMap[productId] = new List<Review>();

            _reviewMap[productId].Add(review);
        }

        /// <summary>
        /// Menampilkan review yang sudah dikirim ke console (debug use).
        /// </summary>
        public void ShowReviews(string productId)
        {
            if (string.IsNullOrWhiteSpace(productId))
            {
                Console.WriteLine("ID produk tidak valid.");
                return;
            }

            Debug.Assert(!string.IsNullOrWhiteSpace(productId));

            if (!_reviewMap.ContainsKey(productId) || _reviewMap[productId].Count == 0)
            {
                Console.WriteLine("Belum ada ulasan untuk produk ini.");
                return;
            }

            foreach (var review in _reviewMap[productId])
            {
                review.Display();
            }
        }

        /// <summary>
        /// Menampilkan draft review (belum dikirim) untuk produk tertentu.
        /// </summary>
        public void ShowDrafts(string productId)
        {
            if (string.IsNullOrWhiteSpace(productId))
                throw new ArgumentException("ID produk tidak boleh kosong");

            Debug.Assert(!string.IsNullOrWhiteSpace(productId));

            if (!_reviewMap.ContainsKey(productId))
            {
                Console.WriteLine("Belum ada review untuk produk ini.");
                return;
            }

            var drafts = _reviewMap[productId]
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

        /// <summary>
        /// Submit draft review berdasarkan indeks.
        /// </summary>
        public void SubmitDraft(string productId, int index)
        {
            if (string.IsNullOrWhiteSpace(productId))
                throw new ArgumentException("ID produk tidak boleh kosong");
            if (index < 1)
                throw new ArgumentOutOfRangeException(nameof(index), "Index harus lebih dari 0");

            Debug.Assert(!string.IsNullOrWhiteSpace(productId));
            Debug.Assert(index >= 1);

            if (!_reviewMap.ContainsKey(productId))
                throw new InvalidOperationException("Tidak ditemukan review untuk produk ini.");

            var drafts = _reviewMap[productId]
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

        /// <summary>
        /// Mendapatkan semua review yang sudah disubmit untuk produk tertentu.
        /// </summary>
        public List<Review> GetSubmittedReviews(string productId)
        {
            if (string.IsNullOrWhiteSpace(productId))
                return new();

            if (!_reviewMap.ContainsKey(productId))
                return new();

            return _reviewMap[productId]
                .Where(r => r.State == ReviewState.Submitted)
                .ToList();
        }
    }
}