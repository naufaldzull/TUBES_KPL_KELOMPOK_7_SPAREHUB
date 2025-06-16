using System;
using System.Diagnostics;

namespace UlasanDanRatingProduk
{
    /// <summary>
    /// Representasi satu ulasan produk yang berisi komentar, rating, dan status pengiriman.
    /// </summary>
    public class Review
    {
        /// <summary>
        /// Nama pengulas.
        /// </summary>
        public string Reviewer { get; }

        /// <summary>
        /// Komentar ulasan.
        /// </summary>
        public string Comment { get; }

        /// <summary>
        /// Nilai rating antara 1–5.
        /// </summary>
        public int Rating { get; }

        /// <summary>
        /// Status dari review (Draft atau Submitted).
        /// </summary>
        public ReviewState State { get; private set; }

        /// <summary>
        /// Membuat objek Review baru dengan data awal sebagai draft.
        /// </summary>
        /// <param name="reviewer">Nama pengulas</param>
        /// <param name="comment">Komentar</param>
        /// <param name="rating">Nilai rating 1–5</param>
        /// <exception cref="ArgumentException"></exception>
        public Review(string reviewer, string comment, int rating)
        {
            if (string.IsNullOrWhiteSpace(reviewer))
            {
                throw new ArgumentException("Reviewer wajib diisi.", nameof(reviewer));
            }

            if (string.IsNullOrWhiteSpace(comment))
            {
                throw new ArgumentException("Komentar tidak boleh kosong.", nameof(comment));
            }

            if (rating < 1 || rating > 5)
            {
                throw new ArgumentException("Rating harus bernilai antara 1 sampai 5.", nameof(rating));
            }

            Debug.Assert(!string.IsNullOrWhiteSpace(reviewer), "Reviewer wajib diisi.");
            Debug.Assert(!string.IsNullOrWhiteSpace(comment), "Komentar tidak boleh kosong.");
            Debug.Assert(rating >= 1 && rating <= 5, "Rating harus antara 1–5.");

            Reviewer = reviewer;
            Comment = comment;
            Rating = rating;
            State = ReviewState.Draft;
        }

        /// <summary>
        /// Menandai ulasan sebagai telah dikirim (Submitted).
        /// </summary>
        /// <exception cref="InvalidOperationException">Jika sudah pernah dikirim</exception>
        public void Submit()
        {
            if (State == ReviewState.Submitted)
            {
                throw new InvalidOperationException("Review sudah dalam status Submitted.");
            }

            Debug.Assert(State != ReviewState.Submitted, "Review seharusnya belum dikirim.");

            State = ReviewState.Submitted;
            Console.WriteLine("Review berhasil dikirim.");
        }

        /// <summary>
        /// Menampilkan isi review ke console (debug use).
        /// </summary>
        public void Display()
        {
            if (State == ReviewState.Submitted)
            {
                Console.WriteLine($"Bintang {Rating} oleh {Reviewer}: {Comment}");
            }
            else
            {
                Console.WriteLine("Review belum dikirim.");
            }
        }
    }
}