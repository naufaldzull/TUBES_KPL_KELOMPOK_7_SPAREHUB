namespace ManajemenToko.API.Model 
{
    /// <summary>
    /// Representasi entity Barang untuk API layer.
    /// </summary>
    public class Barang // PascalCase 
    {
        public int Id { get; set; } // Properti umum 
        public string Nama { get; set; } = string.Empty; // Hindari null 
        public string Deskripsi { get; set; } = string.Empty;
        public decimal Harga { get; set; }
        public int Stok { get; set; }
        public string? Model { get; set; } // nullable 
        public string? Merek { get; set; }
        public string Jenis { get; set; } = string.Empty;
    }

    /// <summary>
    /// Generic response wrapper untuk REST API.
    /// </summary>
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        public static ApiResponse<T> SuccessResponse(T data, string message = "Success") =>
            new()
            {
                Success = true,
                Message = message,
                Data = data
            };

        public static ApiResponse<T> ErrorResponse(string message, List<string>? errors = null) =>
            new()
            {
                Success = false,
                Message = message,
                Errors = errors
            };
    }
}
