using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Searching;
using SearchingModel;

namespace TestSearching
{
    [TestClass]
    public class Testing
    {
        private List<Sparepart> dummyData;

        [TestInitialize]
        public void Setup()
        {
            dummyData = new List<Sparepart>
            {
                new Sparepart { Nama = "Oli", Kategori = "Mesin", Merek = "Shell", KompatibelDengan = "Vario", Harga = 60000 },
                new Sparepart { Nama = "Busi", Kategori = "Kelistrikan", Merek = "Honda", KompatibelDengan = "Beat", Harga = 25000 },
                new Sparepart { Nama = "Filter Udara", Kategori = "Mesin", Merek = "Yamaha", KompatibelDengan = "Mio", Harga = 30000 }
            };
        }

        [TestMethod]
        public void CariBerdasarkanKategori()
        {
            var engine = new SearchEngine<Sparepart>(dummyData);
            var hasil = engine.CariBerdasarkanKategori("Mesin");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(2, hasil.Count);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(hasil.Exists(s => s.Nama == "Oli"));
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(hasil.Exists(s => s.Nama == "Filter Udara"));
        }

        [TestMethod]
        public void CariBerdasarkanMerek()
        {
            var engine = new SearchEngine<Sparepart>(dummyData);
            var hasil = engine.CariBerdasarkanMerek("Honda");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(1, hasil.Count);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Busi", hasil[0].Nama);
        }

        [TestMethod]
        public void CariBerdasarkanKompatibilitas()
        {
            var engine = new SearchEngine<Sparepart>(dummyData);
            var hasil = engine.CariBerdasarkanKompatibilitas("Mio");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(1, hasil.Count);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Filter Udara", hasil[0].Nama);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CariBerdasarkanKategori_ShouldThrow_WhenKategoriKosong()
        {
            var engine = new SearchEngine<Sparepart>(dummyData);
            var hasil = engine.CariBerdasarkanKategori("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldThrow_WhenDataNull()
        {
            var engine = new SearchEngine<Sparepart>(null);
        }
    }
}