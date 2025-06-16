namespace Customerservis.Model
{
    // Class utama sebagai container untuk struktur data terkait layanan pelanggan
    public class CustomerService
    {
        /// <summary>
        /// Representasi satu pesan dari pengguna.
        /// Berisi nama pengirim dan isi pesan.
        /// </summary>
        public class Pesan
        {
            /// <summary>
            /// Nama pengguna yang mengirim pesan.
            /// </summary>
            public string NamaPengguna { get; set; }

            /// <summary>
            /// Isi dari pesan yang dikirim oleh pengguna.
            /// </summary>
            public string IsiPesan { get; set; }
        }
    }
}
