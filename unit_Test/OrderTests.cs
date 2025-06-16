using Xunit;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System;
using fitur_Order; // pastikan namespace sesuai dengan lokasi class Order dan CartItem

namespace unit_Test
{
    public class OrderTests : IDisposable
    {
        private const string TestCartFile = "test_keranjang.json";
        private readonly Order _order;

        public OrderTests()
        {
            // Pastikan cart test file bersih sebelum setiap test
            if (File.Exists(TestCartFile))
                File.Delete(TestCartFile);

            _order = new Order(TestCartFile);
        }

        [Fact]
        public void LoadCart_ShouldReturnEmptyList_IfFileDoesNotExist()
        {
            // Arrange
            if (File.Exists(TestCartFile))
                File.Delete(TestCartFile);

            // Act
            var cart = _order.LoadCart();

            // Assert
            Assert.NotNull(cart);
            Assert.Empty(cart);
        }

        [Fact]
        public void AddToCart_ShouldAddValidItem()
        {
            // Arrange
            string error;

            // Act
            bool result = _order.AddToCart("Ban Motor", 3, 125000, out error);
            var cart = _order.LoadCart();

            // Assert
            Assert.True(result);
            Assert.True(string.IsNullOrEmpty(error));
            Assert.Single(cart);
            Assert.Equal("Ban Motor", cart[0].ProductName);
            Assert.Equal(3, cart[0].Quantity);
            Assert.Equal(125000, cart[0].Price);
        }

        [Theory]
        [InlineData(null, 1, 100000, "Nama produk tidak boleh kosong.")]
        [InlineData("", 1, 100000, "Nama produk tidak boleh kosong.")]
        [InlineData("Oli", 0, 100000, "Jumlah harus lebih dari 0.")]
        [InlineData("Oli", 1, -5000, "Harga harus lebih dari 0.")]
        public void AddToCart_ShouldFailForInvalidInput(string name, int qty, decimal price, string expectedError)
        {
            // Act
            bool result = _order.AddToCart(name, qty, price, out string error);

            // Assert
            Assert.False(result);
            Assert.Equal(expectedError, error);
        }

        public void Dispose()
        {
            // Cleanup file setelah test selesai
            if (File.Exists(TestCartFile))
                File.Delete(TestCartFile);
        }
    }
}
