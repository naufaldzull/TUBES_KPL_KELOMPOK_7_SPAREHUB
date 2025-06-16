using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Customerservis.Controller;
using Customerservis.Model;
using static Customerservis.Model.CustomerService;

namespace Customerservis.Tests
{
    public class CustomerServiceControllerTests
    {
        [Fact]
        public void GetAllPesan_ReturnsOkResult()
        {
            var controller = new CustomerServiceController();
            var result = controller.GetAllPesan();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var pesanList = Assert.IsAssignableFrom<List<Pesan>>(okResult.Value);
        }

        [Fact]
        public void KirimPesan_ValidPesan_ReturnsOkResult()
        {
            var controller = new CustomerServiceController();
            var pesan = new Pesan
            {
                NamaPengguna = "Test User",
                IsiPesan = "Ini adalah pesan test"
            };

            var result = controller.KirimPesan(pesan);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Pesan berhasil dikirim.", okResult.Value);
        }

        [Fact]
        public void KirimPesan_NamaEmpty_ReturnsBadRequest()
        {
            var controller = new CustomerServiceController();
            var pesan = new Pesan
            {
                NamaPengguna = "",
                IsiPesan = "Ini adalah pesan test"
            };

            var result = controller.KirimPesan(pesan);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Nama dan pesan tidak boleh kosong.", badRequestResult.Value);
        }

        [Fact]
        public void KirimPesan_PesanEmpty_ReturnsBadRequest()
        {
            var controller = new CustomerServiceController();
            var pesan = new Pesan
            {
                NamaPengguna = "Test User",
                IsiPesan = ""
            };

            var result = controller.KirimPesan(pesan);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Nama dan pesan tidak boleh kosong.", badRequestResult.Value);
        }

        [Fact]
        public void KirimPesan_AddsPesanToList()
        {
            var controller = new CustomerServiceController();
            var pesan = new Pesan
            {
                NamaPengguna = "Test User",
                IsiPesan = "Ini adalah pesan test"
            };

            var initialResult = controller.GetAllPesan();
            var initialOkResult = Assert.IsType<OkObjectResult>(initialResult.Result);
            var initialPesanList = Assert.IsAssignableFrom<List<Pesan>>(initialOkResult.Value);
            var initialCount = initialPesanList.Count;

            controller.KirimPesan(pesan);

            var finalResult = controller.GetAllPesan();
            var finalOkResult = Assert.IsType<OkObjectResult>(finalResult.Result);
            var finalPesanList = Assert.IsAssignableFrom<List<Pesan>>(finalOkResult.Value);

            Assert.Equal(initialCount + 1, finalPesanList.Count);

            var addedPesan = finalPesanList.LastOrDefault();
            Assert.NotNull(addedPesan);
            Assert.Equal("Test User", addedPesan.NamaPengguna);
            Assert.Equal("Ini adalah pesan test", addedPesan.IsiPesan);
        }
    }
}