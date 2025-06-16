using System;
using WishlistApp;
using Xunit;

namespace TestWishlist.Tests
{
    public class UnitTest
    {
        [Fact]
        public void TambahItem_ValidItem_ShouldBeAdded()
        {
            // Arrange
            var wishlist = new WishlistClass();

            // Act
            wishlist.TambahItem("Laptop");

            // Assert
            Assert.Equal(StatusWishlist.AdaItem, wishlist.Status);
        }

        [Fact]
        public void TambahItem_EmptyItem_ShouldThrowException()
        {
            // Arrange
            var wishlist = new WishlistClass();

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => wishlist.TambahItem("   "));
            Assert.Equal("Item tidak boleh kosong.", ex.Message);
        }

        [Fact]
        public void HapusItem_ItemExists_ShouldUpdateStatus()
        {
            // Arrange
            var wishlist = new WishlistClass();
            wishlist.TambahItem("Buku");

            // Act
            wishlist.HapusItem("Buku");

            // Assert
            Assert.Equal(StatusWishlist.Kosong, wishlist.Status);
        }

        [Fact]
        public void HapusItem_ItemDoesNotExist_ShouldNotThrow()
        {
            // Arrange
            var wishlist = new WishlistClass();
            wishlist.TambahItem("Buku");

            // Act
            wishlist.HapusItem("Pulpen");

            // Assert
            Assert.Equal(StatusWishlist.AdaItem, wishlist.Status); // Status tidak berubah
        }

        [Fact]
        public void Status_ShouldBeKosong_WhenNoItem()
        {
            // Arrange
            var wishlist = new WishlistClass();

            // Assert
            Assert.Equal(StatusWishlist.Kosong, wishlist.Status);
        }

        [Fact]
        public void Status_ShouldBeAdaItem_WhenItemAdded()
        {
            // Arrange
            var wishlist = new WishlistClass();

            // Act
            wishlist.TambahItem("Meja");

            // Assert
            Assert.Equal(StatusWishlist.AdaItem, wishlist.Status);
        }
    }
}
