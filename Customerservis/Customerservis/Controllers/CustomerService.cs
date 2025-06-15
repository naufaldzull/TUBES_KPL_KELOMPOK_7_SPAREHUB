using Microsoft.AspNetCore.Mvc;
using Customerservis.Model;
using System.Collections.Generic;
using static Customerservis.Model.CustomerService;

namespace Customerservis.Controller
{
    // Menandai class ini sebagai API controller agar routing otomatis diaktifkan
    [ApiController]

    // Routing endpoint-nya: /api/CustomerService
    [Route("api/[controller]")]
    public class CustomerServiceController : ControllerBase
    {
        // List statis untuk menyimpan pesan-pesan yang masuk
        private static readonly List<Pesan> _PesanMasuk = new List<Pesan>();

        /// <summary>
        /// Mengambil seluruh pesan yang telah dikirim.
        /// Endpoint: GET /api/CustomerService
        /// </summary>
        /// <returns>Daftar semua pesan</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Pesan>> GetAllPesan()
        {
            // Mengembalikan semua pesan dalam bentuk response 200 OK
            return Ok(_PesanMasuk);
        }

        /// <summary>
        /// Mengirim pesan baru dari pengguna.
        /// Endpoint: POST /api/CustomerService
        /// </summary>
        /// <param name="pesan">Objek pesan yang dikirim oleh pengguna</param>
        /// <returns>Response status sukses atau gagal</returns>
        [HttpPost]
        public ActionResult KirimPesan([FromBody] Pesan pesan)
        {
            // Validasi input: nama pengguna dan isi pesan tidak boleh kosong
            if (string.IsNullOrWhiteSpace(pesan.NamaPengguna) || string.IsNullOrWhiteSpace(pesan.IsiPesan))
            {
                return BadRequest("Nama dan pesan tidak boleh kosong.");
            }

            // Menambahkan pesan ke daftar
            _PesanMasuk.Add(pesan);

            // Mengembalikan response sukses
            return Ok("Pesan berhasil dikirim.");
        }
    }
}
