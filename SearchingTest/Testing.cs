using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Searching;
using SearchingModel;

namespace TestSearching
{
    /// <summary>
    /// Unit tests untuk SearchEngine class yang menguji fungsionalitas pencarian sparepart motor.
    /// </summary>
    [TestClass]
    public class SearchEngineTests
    {
        /// <summary>
        /// Data test yang digunakan untuk semua test cases.
        /// </summary>
        private List<Sparepart> _testData;

        /// <summary>
        /// Instance SearchEngine yang digunakan untuk testing.
        /// </summary>
        private SearchEngine<Sparepart> _searchEngine;

        /// <summary>
        /// Setup method yang dijalankan sebelum setiap test case.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            _testData = new List<Sparepart>
            {
                new Sparepart
                {
                    Nama = "Oli",
                    Kategori = "Mesin",
                    Merek = "Shell",
                    KompatibelDengan = "Vario",
                    Harga = 60000
                },
                new Sparepart
                {
                    Nama = "Busi",
                    Kategori = "Kelistrikan",
                    Merek = "Honda",
                    KompatibelDengan = "Beat",
                    Harga = 25000
                },
                new Sparepart
                {
                    Nama = "Filter Udara",
                    Kategori = "Mesin",
                    Merek = "Yamaha",
                    KompatibelDengan = "Mio",
                    Harga = 30000
                }
            };

            _searchEngine = new SearchEngine<Sparepart>(_testData);
        }

        /// <summary>
        /// Test pencarian berdasarkan kategori dengan input yang valid.
        /// </summary>
        [TestMethod]
        public void SearchByCategory_Should_ReturnCorrectItems_When_ValidCategoryProvided()
        {
            // Arrange
            const string category = "Mesin";

            // Act
            var result = _searchEngine.CariBerdasarkanKategori(category);

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(2, result.Count);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(result.Exists(item => item.Nama == "Oli"));
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(result.Exists(item => item.Nama == "Filter Udara"));
        }

        /// <summary>
        /// Test pencarian berdasarkan merek dengan input yang valid.
        /// </summary>
        [TestMethod]
        public void SearchByBrand_Should_ReturnCorrectItem_When_ValidBrandProvided()
        {
            // Arrange
            const string brand = "Honda";

            // Act
            var result = _searchEngine.CariBerdasarkanMerek(brand);

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(1, result.Count);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Busi", result[0].Nama);
        }

        /// <summary>
        /// Test pencarian berdasarkan kompatibilitas motor dengan input yang valid.
        /// </summary>
        [TestMethod]
        public void SearchByCompatibility_Should_ReturnCorrectItem_When_ValidMotorProvided()
        {
            // Arrange
            const string motorType = "Mio";

            // Act
            var result = _searchEngine.CariBerdasarkanKompatibilitas(motorType);

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(1, result.Count);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Filter Udara", result[0].Nama);
        }

        /// <summary>
        /// Test validasi input untuk kategori kosong, harus throw ArgumentException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SearchByCategory_Should_ThrowArgumentException_When_EmptyCategoryProvided()
        {
            // Arrange & Act & Assert
            _searchEngine.CariBerdasarkanKategori(string.Empty);
        }

        /// <summary>
        /// Test validasi input untuk kategori whitespace, harus throw ArgumentException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SearchByCategory_Should_ThrowArgumentException_When_WhitespaceCategoryProvided()
        {
            // Arrange & Act & Assert
            _searchEngine.CariBerdasarkanKategori("   ");
        }

        /// <summary>
        /// Test validasi constructor dengan data null, harus throw ArgumentNullException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Should_ThrowArgumentNullException_When_NullDataProvided()
        {
            // Arrange & Act & Assert
            new SearchEngine<Sparepart>(null);
        }

        /// <summary>
        /// Test validasi constructor dengan data kosong, harus throw ArgumentException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_Should_ThrowArgumentException_When_EmptyDataProvided()
        {
            // Arrange & Act & Assert
            new SearchEngine<Sparepart>(new List<Sparepart>());
        }

        /// <summary>
        /// Test pencarian kategori yang tidak ada, harus return list kosong.
        /// </summary>
        [TestMethod]
        public void SearchByCategory_Should_ReturnEmptyList_When_NonExistentCategoryProvided()
        {
            // Arrange
            const string nonExistentCategory = "NonExistent";

            // Act
            var result = _searchEngine.CariBerdasarkanKategori(nonExistentCategory);

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(0, result.Count);
        }

        /// <summary>
        /// Test pencarian merek yang tidak ada, harus return list kosong.
        /// </summary>
        [TestMethod]
        public void SearchByBrand_Should_ReturnEmptyList_When_NonExistentBrandProvided()
        {
            // Arrange
            const string nonExistentBrand = "NonExistent";

            // Act
            var result = _searchEngine.CariBerdasarkanMerek(nonExistentBrand);

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(0, result.Count);
        }

        /// <summary>
        /// Test method GetAllData untuk memverifikasi pengembalian semua data.
        /// </summary>
        [TestMethod]
        public void GetAllData_Should_ReturnAllItems_When_Called()
        {
            // Act
            var result = _searchEngine.GetAllData();

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_testData.Count, result.Count);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_testData[0].Nama, result[0].Nama);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_testData[1].Nama, result[1].Nama);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(_testData[2].Nama, result[2].Nama);
        }
    }
}