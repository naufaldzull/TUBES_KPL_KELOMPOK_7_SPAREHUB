using ManejemenToko.API.Model;
using ManejemenToko.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ManejemenToko.API.Controllers
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
        /// GET /api/toko - Ambil semua barang dari database
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<Barang>>>> GetAllBarang()
        {
            try
            {
                var barang = await _barangService.GetAllBarangAsync();
                return Ok(ApiResponse<List<Barang>>.SuccessResponse(barang, $"Berhasil mengambil {barang.Count} data barang dari database"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<List<Barang>>.ErrorResponse($"Error mengakses database: {ex.Message}"));
            }
        }

        /// <summary>
        /// GET /api/toko/{id} - Ambil barang by ID dari database
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Barang>>> GetBarangById(int id)
        {
            try
            {
                var barang = await _barangService.GetBarangByIdAsync(id);

                if (barang == null)
                    return NotFound(ApiResponse<Barang>.ErrorResponse($"Barang dengan ID {id} tidak ditemukan di database"));

                return Ok(ApiResponse<Barang>.SuccessResponse(barang, "Data berhasil diambil dari database"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<Barang>.ErrorResponse($"Error mengakses database: {ex.Message}"));
            }
        }

        /// <summary>
        /// GET /api/toko/search?keyword={keyword} - Cari barang di database
        /// </summary>
        [HttpGet("search")]
        public async Task<ActionResult<ApiResponse<List<Barang>>>> SearchBarang([FromQuery] string keyword = "")
        {
            try
            {
                var barang = await _barangService.SearchBarangAsync(keyword);
                string message = string.IsNullOrWhiteSpace(keyword)
                    ? "Semua data berhasil diambil dari database"
                    : $"Pencarian '{keyword}' menemukan {barang.Count} hasil dari database";

                return Ok(ApiResponse<List<Barang>>.SuccessResponse(barang, message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<List<Barang>>.ErrorResponse($"Error mencari data di database: {ex.Message}"));
            }
        }

        /// <summary>
        /// GET /api/toko/jenis/{jenis} - Filter barang by jenis dari database
        /// </summary>
        [HttpGet("jenis/{jenis}")]
        public async Task<ActionResult<ApiResponse<List<Barang>>>> GetBarangByJenis(string jenis)
        {
            try
            {
                var barang = await _barangService.GetBarangByJenisAsync(jenis);
                return Ok(ApiResponse<List<Barang>>.SuccessResponse(barang, $"Filter jenis '{jenis}' menemukan {barang.Count} barang dari database"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<List<Barang>>.ErrorResponse($"Error filter data di database: {ex.Message}"));
            }
        }

        /// <summary>
        /// POST /api/toko/sync - Sync data dari frontend ke database
        /// </summary>
        [HttpPost("sync")]
        public ActionResult<ApiResponse<bool>> SyncDataFromFrontend([FromBody] List<Barang> frontendData)
        {
            try
            {
                _barangService.SyncDataFromFrontend(frontendData);
                return Ok(ApiResponse<bool>.SuccessResponse(true, $"Berhasil sync {frontendData.Count} data dari frontend ke database"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.ErrorResponse($"Error sync data: {ex.Message}"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteBarang(int id)
        {
            try
            {
                var success = await _barangService.DeleteBarangAsync(id);

                if (!success)
                    return NotFound(ApiResponse<bool>.ErrorResponse($"Barang dengan ID {id} tidak ditemukan"));

                return Ok(ApiResponse<bool>.SuccessResponse(true, $"Barang dengan ID {id} berhasil dihapus"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.ErrorResponse($"Error menghapus data: {ex.Message}"));
            }
        }
    }
}
