using ManajemenToko.Controller;
using ManajemenToko.Models;
using Xunit;
using System.Linq;

namespace ManajemenToko.Tests
{
    public class BarangControllerTests
    {
        private readonly BarangController _controller;

        public BarangControllerTests()
        {
            _controller = new BarangController();
        }

        [Fact]
        public void TambahBarang_ValidInput_ShouldReturnSuccess()
        {
            var result = _controller.TambahBarang("Ban Motor", "Ban tubeless ring 14", "150000", "10", "Ring 14", "FDR", "Kaki-kaki");

            Assert.True(result.Success);
            Assert.Equal("Barang berhasil ditambahkan!", result.Message);
        }

        [Fact]
        public void TambahBarang_EmptyNama_ShouldFail()
        {
            var result = _controller.TambahBarang("", "Desc", "10000", "5", "", "", "Mesin");

            Assert.False(result.Success);
            Assert.Equal("Nama tidak boleh kosong", result.Message);
        }

        [Fact]
        public void TambahBarang_InvalidHarga_ShouldFail()
        {
            var result = _controller.TambahBarang("Knalpot", "Desc", "abc", "5", "", "", "Mesin");

            Assert.False(result.Success);
            Assert.Equal("Harga harus angka dan lebih besar dari 0", result.Message);
        }

        [Fact]
        public void UpdateBarang_ShouldSucceed()
        {
            var addResult = _controller.TambahBarang("Oli", "Deskripsi", "45000", "3", "", "", "Mesin");
            var barang = _controller.GetAllBarang().Data.Last();

            var updateResult = _controller.UpdateBarang(barang.Id, "Oli Update", "Baru", "55000", "4", "", "", "Mesin");

            Assert.True(updateResult.Success);
        }

        [Fact]
        public void GetBarangById_Nonexistent_ShouldReturnFalse()
        {
            var result = _controller.GetBarangById(999);

            Assert.False(result.Success);
            Assert.Null(result.Data);
        }

        [Fact]
        public void DeleteBarang_ShouldSucceed()
        {
            var add = _controller.TambahBarang("Lampu", "LED", "30000", "2", "", "", "Kelistrikan");
            var barang = _controller.GetAllBarang().Data.Last();

            var delete = _controller.HapusBarang(barang.Id);

            Assert.True(delete.Success);
        }
    }
}
