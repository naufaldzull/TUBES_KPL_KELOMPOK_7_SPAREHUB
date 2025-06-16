using System;

namespace UlasanDanRatingProduk
{
    /// <summary>
    /// Menentukan status dari sebuah ulasan.
    /// </summary>
    public enum ReviewState
    {
        /// <summary>
        /// Ulasan masih dalam bentuk draft, belum dikirim.
        /// </summary>
        Draft,

        /// <summary>
        /// Ulasan sudah dikirim (final).
        /// </summary>
        Submitted
    }
}