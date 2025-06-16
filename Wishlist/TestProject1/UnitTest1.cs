using Wishlist;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void TambahItem()
        {
            var wishlist = new WishlistClass();
            wishlist.TambahItem("Book");
            Assert.Equal(StatusWishlist.AdaItem, wishlist.Status);
        }

        [Fact]
        public void HapusItem()
        {
            var wishlist = new WishlistClass();
            wishlist.TambahItem("Book");
            wishlist.HapusItem("Book");
            Assert.Equal(StatusWishlist.Kosong, wishlist.Status);
        }

        [Fact]
        public void TampilkanWishlist()
        {
            var wishlist = new WishlistClass();
            wishlist.TambahItem("Book");

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                wishlist.TampilkanWishlist();
                var output = sw.ToString();

                Assert.Contains("Wishlist Kamu:", output);
                Assert.Contains("- Book", output);
            }
        }
    }
}