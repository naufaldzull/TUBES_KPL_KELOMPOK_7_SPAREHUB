using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlasanDanRatingProduk
{
    public class Review
    {
        public string Reviewer { get; }
        public string Comment { get; }
        public int Rating { get; }
        public ReviewState State { get; private set; }

        public Review(string reviewer, string comment, int rating)
        {
            if (string.IsNullOrWhiteSpace(reviewer))
                throw new ArgumentException("Reviewer wajib diisi");
            if (string.IsNullOrWhiteSpace(comment))
                throw new ArgumentException("Komentar tidak boleh kosong");
            if (rating < 1 || rating > 5)
                throw new ArgumentException("Rating harus antara 1–5");

            Debug.Assert(!string.IsNullOrWhiteSpace(reviewer), "Reviewer wajib diisi");
            Debug.Assert(!string.IsNullOrWhiteSpace(comment), "Komentar tidak boleh kosong");
            Debug.Assert(rating >= 1 && rating <= 5, "Rating harus antara 1–5");

            Reviewer = reviewer;
            Comment = comment;
            Rating = rating;
            State = ReviewState.Draft;
        }

        public void Submit()
        {
            if (State == ReviewState.Submitted)
                throw new InvalidOperationException("Review sudah pernah dikirim");

            Debug.Assert(State != ReviewState.Submitted, "Review seharusnya belum dikirim");

            State = ReviewState.Submitted;
            Console.WriteLine("Review berhasil dikirim.");
        }

        public void Display()
        {
            if (State == ReviewState.Submitted)
                Console.WriteLine($"Bintang {Rating} oleh {Reviewer}: {Comment}");
            else
                Console.WriteLine("Review belum dikirim.");
        }
    }
}
