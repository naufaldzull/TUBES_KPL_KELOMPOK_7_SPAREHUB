using Microsoft.AspNetCore.Mvc;
using Customerservis.Model;
using System.Collections.Generic;
using static Customerservis.Model.CustomerService;

namespace Customerservis.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerServiceController : ControllerBase
    {
        private static readonly List<Pesan> PesanMasuk = new List<Pesan>();

        [HttpGet]
        public ActionResult<IEnumerable<Pesan>> GetAllPesan()
        {
            return Ok(PesanMasuk);
        }

        [HttpPost]
        public ActionResult KirimPesan([FromBody] Pesan pesan)
        {
            if (string.IsNullOrWhiteSpace(pesan.NamaPengguna) || string.IsNullOrWhiteSpace(pesan.IsiPesan))
            {
                return BadRequest("Nama dan pesan tidak boleh kosong.");
            }

            PesanMasuk.Add(pesan);
            return Ok("Pesan berhasil dikirim.");
        }
    }
}