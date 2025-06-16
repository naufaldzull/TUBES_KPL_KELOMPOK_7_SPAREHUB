using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Searching;
using SearchingModel;

namespace SearchingData
{
    public static class Utility
    {
        public static List<Sparepart> AmbilDataDummy()
        {
            return new List<Sparepart>
            {
                new Sparepart { Nama = "Oli", Kategori = "Mesin", Merek = "Shell", KompatibelDengan = "Vario", Harga = 60000 },
                new Sparepart { Nama = "Busi", Kategori = "Kelistrikan", Merek = "Honda", KompatibelDengan = "Beat", Harga = 25000 },
                new Sparepart { Nama = "Aki", Kategori = "Kelistrikan", Merek = "Yuasa", KompatibelDengan = "Vario", Harga = 150000 },
                new Sparepart { Nama = "Filter Udara", Kategori = "Mesin", Merek = "Aspira", KompatibelDengan = "Scoopy", Harga = 40000 },
                new Sparepart { Nama = "Kampas Rem", Kategori = "Rem", Merek = "Federal", KompatibelDengan = "Beat", Harga = 50000 },
                new Sparepart { Nama = "Rantai", Kategori = "Transmisi", Merek = "SSS", KompatibelDengan = "Supra X", Harga = 120000 },
                new Sparepart { Nama = "Lampu Depan", Kategori = "Kelistrikan", Merek = "Osram", KompatibelDengan = "NMAX", Harga = 85000 },
                new Sparepart { Nama = "Ban Belakang", Kategori = "Ban", Merek = "TTC", KompatibelDengan = "Vario", Harga = 200000 },
                new Sparepart { Nama = "Knalpot", Kategori = "Mesin", Merek = "R9", KompatibelDengan = "CB150R", Harga = 750000 },
                new Sparepart { Nama = "Speedometer", Kategori = "Kelistrikan", Merek = "Koso", KompatibelDengan = "Aerox", Harga = 550000 },
                new Sparepart { Nama = "Velg Racing", Kategori = "Roda", Merek = "TDR", KompatibelDengan = "Mio", Harga = 650000 },
                new Sparepart { Nama = "Rem Cakram", Kategori = "Rem", Merek = "Brembo", KompatibelDengan = "NMAX", Harga = 980000 },
                new Sparepart { Nama = "Shockbreaker", Kategori = "Suspensi", Merek = "SSS", KompatibelDengan = "PCX", Harga = 1150000 },
                new Sparepart { Nama = "Kabel Gas", Kategori = "Transmisi", Merek = "TK", KompatibelDengan = "Revo", Harga = 18000 },
                new Sparepart { Nama = "Kampas Kopling", Kategori = "Transmisi", Merek = "FCC", KompatibelDengan = "Vixion", Harga = 145000 },
                new Sparepart { Nama = "ECU Racing", Kategori = "Kelistrikan", Merek = "BRT", KompatibelDengan = "Aerox", Harga = 950000 },
                new Sparepart { Nama = "Ban Depan", Kategori = "Ban", Merek = "Zeneos", KompatibelDengan = "Mio", Harga = 185000 },
                new Sparepart { Nama = "Spion", Kategori = "Body", Merek = "KTC", KompatibelDengan = "CB150R", Harga = 90000 },
                new Sparepart { Nama = "Cover Body", Kategori = "Body", Merek = "Original", KompatibelDengan = "Beat", Harga = 320000 },
                new Sparepart { Nama = "Radiator", Kategori = "Mesin", Merek = "AHM", KompatibelDengan = "CB150R", Harga = 420000 },
                new Sparepart { Nama = "Garnish Lampu", Kategori = "Body", Merek = "Scarlet", KompatibelDengan = "XMAX", Harga = 125000 },
                new Sparepart { Nama = "Handle Rem", Kategori = "Rem", Merek = "KTC", KompatibelDengan = "PCX", Harga = 135000 },
                new Sparepart { Nama = "Windshield", Kategori = "Body", Merek = "Yamaha", KompatibelDengan = "Aerox", Harga = 180000 },
                new Sparepart { Nama = "Mabel Kopling", Kategori = "Transmisi", Merek = "Aspira", KompatibelDengan = "CB150R", Harga = 30000 },
                new Sparepart { Nama = "CDI Racing", Kategori = "Kelistrikan", Merek = "BRT", KompatibelDengan = "Vario", Harga = 450000 },
                new Sparepart { Nama = "Koil Ignition", Kategori = "Kelistrikan", Merek = "Honda", KompatibelDengan = "CB150R", Harga = 275000 },
                new Sparepart { Nama = "Roller CVT", Kategori = "Transmisi", Merek = "Yamaha", KompatibelDengan = "NMAX", Harga = 85000 },
                new Sparepart { Nama = "Per Kopling", Kategori = "Transmisi", Merek = "FCC", KompatibelDengan = "Vixion", Harga = 65000 },
                new Sparepart { Nama = "Lampu Stop", Kategori = "Kelistrikan", Merek = "Aspira", KompatibelDengan = "Mio", Harga = 45000 },
                new Sparepart { Nama = "Filter Oli", Kategori = "Mesin", Merek = "Honda", KompatibelDengan = "CB150R", Harga = 35000 }
            };
        }
    }
}