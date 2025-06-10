using System;
using UlasanDanRatingProduk;
using Xunit;

namespace UnitTestUlasanDanRatingProduk
{
    public class UnitTest
    {
        [Fact]
        public void AddReview_ReviewTersimpanDalamMap()
        {
            var service = new ReviewService();
            var review = new Review("Alice", "Bagus sekali", 5);
            var productId = "P001";

            service.AddReview(productId, review);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                review.Submit();
                service.ShowReviews(productId);
                var output = sw.ToString().Trim();

                Assert.Contains("Bintang 5 oleh Alice: Bagus sekali", output);
            }
        }

        [Fact]
        public void ShowReviews_MenunjukanPesanJikaTidakAdaReview()
        {
            var service = new ReviewService();
            var productId = "P002";

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                service.ShowReviews(productId);
                var output = sw.ToString().Trim();

                Assert.Equal("Belum ada ulasan untuk produk ini.", output);
            }
        }

        [Fact]
        public void Review_MelemparExceptionJikaDataInvalid()
        {
            Assert.Throws<ArgumentException>(() => new Review("", "Komentar", 3));
            Assert.Throws<ArgumentException>(() => new Review("Bob", "", 3));
            Assert.Throws<ArgumentException>(() => new Review("Bob", "Komentar", 0));
            Assert.Throws<ArgumentException>(() => new Review("Bob", "Komentar", 6));
        }

        [Fact]
        public void Review_SubmitMengubahStateDanPrintPesan()
        {
            var review = new Review("Charlie", "Lumayan", 4);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                review.Submit();
                var output = sw.ToString().Trim();

                Assert.Equal("Review berhasil dikirim.", output);
                Assert.Throws<InvalidOperationException>(() => review.Submit());
            }
        }

        [Fact]
        public void Product_MelemparExceptionJikaInputConstructorInvalid()
        {
            Assert.Throws<ArgumentException>(() => new Product("", "Produk A"));
            Assert.Throws<ArgumentException>(() => new Product("P003", ""));
        }

        [Fact]
        public void ShowDrafts_MenampilkanHanyaReviewDraft()
        {
            var service = new ReviewService();
            var productId = "P100";

            var draftReview = new Review("Sari", "Masih ragu", 3);
            var submittedReview = new Review("Rian", "Bagus banget", 5);

            service.AddReview(productId, draftReview);
            service.AddReview(productId, submittedReview);

            var originalOut = Console.Out;
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                submittedReview.Submit();

                service.ShowDrafts(productId);
                var output = sw.ToString().Trim();

                Assert.Contains("Draft oleh Sari: Masih ragu (3 bintang)", output);
                Assert.DoesNotContain("Rian", output);
            }
            Console.SetOut(originalOut);
        }

        [Fact]
        public void SubmitDraft_MengubahStateDanTampilDiShowReviews()
        {
            var service = new ReviewService();
            var productId = "P101";

            var draft = new Review("Tina", "Boleh dicoba", 4);
            service.AddReview(productId, draft);

            var originalOut = Console.Out;
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                service.SubmitDraft(productId, 1);

                service.ShowReviews(productId);
                var output = sw.ToString().Trim();

                Assert.Contains("Bintang 4 oleh Tina: Boleh dicoba", output);
            }
            Console.SetOut(originalOut);
        }

    }
}