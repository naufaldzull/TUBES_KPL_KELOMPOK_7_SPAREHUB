using System;
using System.IO;
using UlasanDanRatingProduk;
using Xunit;

namespace UnitTestUlasanDanRatingProduk
{
    public class ReviewServiceTests
    {
        /// <summary>
        /// Menyimpan review dan menampilkannya setelah disubmit.
        /// </summary>
        [Fact]
        public void AddReview_ReviewTersimpanDanDitampilkanSetelahSubmit()
        {
            var service = ReviewService.Instance;
            var productId = Guid.NewGuid().ToString(); // Hindari ID bentrok antar test
            var review = new Review("Alice", "Bagus sekali", 5);

            service.AddReview(productId, review);

            using var sw = new StringWriter();
            Console.SetOut(sw);

            review.Submit();
            service.ShowReviews(productId);

            var output = sw.ToString().Trim();
            Assert.Contains("Bintang 5 oleh Alice: Bagus sekali", output);
        }

        /// <summary>
        /// Menampilkan pesan jika tidak ada review.
        /// </summary>
        [Fact]
        public void ShowReviews_TidakAdaReview_MenampilkanPesanKosong()
        {
            var service = ReviewService.Instance;
            var productId = Guid.NewGuid().ToString();

            using var sw = new StringWriter();
            Console.SetOut(sw);

            service.ShowReviews(productId);

            var output = sw.ToString().Trim();
            Assert.Equal("Belum ada ulasan untuk produk ini.", output);
        }

        /// <summary>
        /// Konstruktor Review melempar exception jika input invalid.
        /// </summary>
        [Fact]
        public void Review_KonstruktorInputInvalid_ThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Review("", "Komentar", 3));
            Assert.Throws<ArgumentException>(() => new Review("Bob", "", 3));
            Assert.Throws<ArgumentException>(() => new Review("Bob", "Komentar", 0));
            Assert.Throws<ArgumentException>(() => new Review("Bob", "Komentar", 6));
        }

        /// <summary>
        /// Submit() mengubah state dan menampilkan pesan ke console.
        /// </summary>
        [Fact]
        public void Review_Submit_UbahStateDanPrintPesan()
        {
            var review = new Review("Charlie", "Lumayan", 4);

            using var sw = new StringWriter();
            Console.SetOut(sw);

            review.Submit();
            var output = sw.ToString().Trim();

            Assert.Equal("Review berhasil dikirim.", output);
            Assert.Throws<InvalidOperationException>(() => review.Submit());
        }

        /// <summary>
        /// ShowDrafts hanya menampilkan review dalam draft.
        /// </summary>
        [Fact]
        public void ShowDrafts_MenampilkanHanyaReviewDraft()
        {
            var service = ReviewService.Instance;
            var productId = Guid.NewGuid().ToString();

            var draftReview = new Review("Sari", "Masih ragu", 3);
            var submittedReview = new Review("Rian", "Bagus banget", 5);

            service.AddReview(productId, draftReview);
            service.AddReview(productId, submittedReview);
            submittedReview.Submit();

            using var sw = new StringWriter();
            Console.SetOut(sw);

            service.ShowDrafts(productId);
            var output = sw.ToString().Trim();

            Assert.Contains("Draft oleh Sari: Masih ragu (3 bintang)", output);
            Assert.DoesNotContain("Rian", output);
        }

        /// <summary>
        /// SubmitDraft mengubah state review dari draft menjadi submitted.
        /// </summary>
        [Fact]
        public void SubmitDraft_ReviewBerhasilDikirim()
        {
            var service = ReviewService.Instance;
            var productId = Guid.NewGuid().ToString();
            var draft = new Review("Tina", "Boleh dicoba", 4);

            service.AddReview(productId, draft);

            using var sw = new StringWriter();
            Console.SetOut(sw);

            service.SubmitDraft(productId, 1);
            service.ShowReviews(productId);

            var output = sw.ToString().Trim();
            Assert.Contains("Bintang 4 oleh Tina: Boleh dicoba", output);
        }
    }
}