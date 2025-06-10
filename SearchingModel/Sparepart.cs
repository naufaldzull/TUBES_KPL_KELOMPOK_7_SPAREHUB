using System;
using Searching;

namespace SearchingModel
{
    /// <summary>
    /// Representasi entitas data sparepart yang mengimplementasikan ISparepart.
    /// </summary>
    public class Sparepart : ISparepart
    {
        /// <summary>
        /// Nama sparepart.
        /// </summary>
        public string Nama { get; set; } = string.Empty;

        /// <summary>
        /// Kategori sparepart (misal: Mesin, Elektrikal, dll).
        /// </summary>
        public string Kategori { get; set; } = string.Empty;

        /// <summary>
        /// Merek dari sparepart.
        /// </summary>
        public string Merek { get; set; } = string.Empty;

        /// <summary>
        /// Nama motor atau kendaraan yang kompatibel dengan sparepart ini.
        /// </summary>
        public string KompatibelDengan { get; set; } = string.Empty;

        /// <summary>
        /// Harga dari sparepart.
        /// </summary>
        public decimal Harga { get; set; }
    }
}
