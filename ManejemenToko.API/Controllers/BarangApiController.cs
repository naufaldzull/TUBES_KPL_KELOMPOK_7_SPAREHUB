using ManajemenToko.API.Model;
using ManajemenToko.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ManajemenToko.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokoController : ControllerBase
    {
        private readonly IBarangApiService _barangService;

        public TokoController(IBarangApiService barangService)
        {
            _barangService = barangService;
        }

        /// <summary>
        /// Ambil semua barang dari database.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<Barang>>>> GetAllBarang()
        {
            try
            {
                var barangList = await _barangService.GetAllBarangAsync();
                return Ok(ApiResponse<List<Barang>>.SuccessResponse(barangList, $"Berhasil mengambil {barangList.Count} data barang"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<List<Barang>>.ErrorResponse($"Terjadi kesalahan saat mengambil data: {ex.Message}"));
            }
        }

        /// <summary>
        /// Ambil barang berdasarkan ID.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<Barang>>> GetBarangById(int id)
        {
            try
            {
                var barang = await _barangService.GetBarangByIdAsync(id);

                if (barang == null)
                    return NotFound(ApiResponse<Barang>.ErrorResponse($"Barang dengan ID {id} tidak ditemukan"));

                return Ok(ApiResponse<Barang>.SuccessResponse(barang, "Barang ditemukan"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<Barang>.ErrorResponse($"Terjadi kesalahan: {ex.Message}"));
            }
        }

        /// <summary>
        /// Cari barang berdasarkan keyword.
        /// </summary>
        [HttpGet("search")]
        public async Task<ActionResult<ApiResponse<List<Barang>>>> SearchBarang([FromQuery] string keyword = "")
        {
            try
            {
                var result = await _barangService.SearchBarangAsync(keyword);
                var message = string.IsNullOrWhiteSpace(keyword)
                    ? "Berhasil mengambil semua barang"
                    : $"Ditemukan {result.Count} hasil untuk '{keyword}'";

                return Ok(ApiResponse<List<Barang>>.SuccessResponse(result, message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<List<Barang>>.ErrorResponse($"Error saat pencarian: {ex.Message}"));
            }
        }

        /// <summary>
        /// Filter barang berdasarkan jenis.
        /// </summary>
        [HttpGet("jenis/{jenis}")]
        public async Task<ActionResult<ApiResponse<List<Barang>>>> GetBarangByJenis(string jenis)
        {
            try
            {
                var result = await _barangService.GetBarangByJenisAsync(jenis);
                return Ok(ApiResponse<List<Barang>>.SuccessResponse(result, $"Ditemukan {result.Count} barang untuk jenis '{jenis}'"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<List<Barang>>.ErrorResponse($"Error saat filter: {ex.Message}"));
            }
        }

        /// <summary>
        /// Sync data barang dari frontend (bulk overwrite).
        /// </summary>
        [HttpPost("sync")]
        public ActionResult<ApiResponse<bool>> SyncDataFromFrontend([FromBody] List<Barang> frontendData)
        {
            try
            {
                _barangService.SyncDataFromFrontend(frontendData);
                return Ok(ApiResponse<bool>.SuccessResponse(true, $"Sync berhasil. Total: {frontendData.Count} barang"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.ErrorResponse($"Gagal sync data: {ex.Message}"));
            }
        }

        /// <summary>
        /// Hapus barang berdasarkan ID.
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteBarang(int id)
        {
            try
            {
                var deleted = await _barangService.DeleteBarangAsync(id);

                if (!deleted)
                    return NotFound(ApiResponse<bool>.ErrorResponse($"Barang dengan ID {id} tidak ditemukan"));

                return Ok(ApiResponse<bool>.SuccessResponse(true, $"Barang dengan ID {id} berhasil dihapus"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.ErrorResponse($"Gagal menghapus barang: {ex.Message}"));
            }
        }
    }
}
