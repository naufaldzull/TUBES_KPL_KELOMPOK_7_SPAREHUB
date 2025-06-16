using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Customerservis.Controller;
using Customerservis.Model;
using static Customerservis.Model.CustomerService;

namespace Customerservis.Tests
{
    public class CustomerServiceControllerTests
    {
        [Fact]
        public void GetAllPesan_AwalnyaKosong_ReturnsEmptyList()
        {
            // Arrange
            var controller = new CustomerServiceController();

            // Act
            var result = controller.GetAllPesan();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var pesanList = Assert.IsType<List<Pesan>>(okResult.Value);
            Assert.Empty(pesanList);
        }

        [Fact]
        public void KirimPesan_InputValid_ReturnsOk()
        {
            // Arrange
            var controller = new CustomerServiceController();
            var pesan = new Pesan
            {
                NamaPengguna = "Rizki",
                IsiPesan = "Halo, ini test pesan."
            };

            // Act
            var result = controller.KirimPesan(pesan);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Pesan berhasil dikirim.", okResult.Value);
        }

        [Theory]
        [InlineData(null, "Pesan valid")]
        [InlineData("User", null)]
        [InlineData("", "Pesan valid")]
        [InlineData("User", "")]
        public void KirimPesan_InputTidakValid_ReturnsBadRequest(string nama, string isi)
        {
            // Arrange
            var controller = new CustomerServiceController();
            var pesan = new Pesan
            {
                NamaPengguna = nama,
                IsiPesan = isi
            };

            // Act
            var result = controller.KirimPesan(pesan);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Nama dan pesan tidak boleh kosong.", badRequest.Value);
        }

        [Fact]
        public void KirimPesan_LaluGetAllPesan_MengandungPesanTersebut()
        {
            // Arrange
            var controller = new CustomerServiceController();
            var pesanBaru = new Pesan
            {
                NamaPengguna = "Rafa",
                IsiPesan = "Test integrasi GET setelah POST"
            };

            // Act
            controller.KirimPesan(pesanBaru);
            var result = controller.GetAllPesan();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var pesanList = Assert.IsType<List<Pesan>>(okResult.Value);
            Assert.Contains(pesanList, p => p.NamaPengguna == "Rafa" && p.IsiPesan == "Test integrasi GET setelah POST");
        }
    }
}
