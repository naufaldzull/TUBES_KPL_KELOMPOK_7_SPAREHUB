using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics;

namespace fitur_Order // Nama namespace sebaiknya PascalCase jika sesuai .NET convention
{
    /// <summary>
    /// Represents an item in the shopping cart
    /// </summary>
    public class CartItem // ini sudah PascalCase
    {
        public string ProductId { get; set; } = string.Empty; // ini sudah PascalCase, gunakan default string.Empty untuk hindari null
        public string ProductName { get; set; } = string.Empty; // ini sudah PascalCase
        public int Quantity { get; set; } // ini sudah PascalCase
        public decimal Price { get; set; } // ini sudah PascalCase

        /// <summary>
        /// Calculate subtotal for this cart item
        /// </summary>
        public decimal GetSubtotal() => Quantity * Price; // ini sudah PascalCase, method singkat & clean
    }

    /// <summary>
    /// Handles order processing dan cart management
    /// </summary>
    public class Order // ini sudah PascalCase
    {
        private readonly string _cartFilePath; // ini sudah camelCase + prefix underscore sesuai convention
        private const string CONFIG_FILE_PATH = "../../../../fitur_Order/config.json"; // ini sudah UPPER_CASE untuk const
        private const string STATUS_FILE_PATH = "../../../../fitur_Order/status_mapping.json"; // ini sudah UPPER_CASE untuk const

        private Dictionary<string, string> _configuration; // ini sudah camelCase + _
        private Dictionary<string, string> _statusMapping; // ini sudah camelCase + _

        private static readonly List<string> ValidPaymentMethods = new List<string> // ini sudah PascalCase
        {
            "COD", "Transfer", "E-Wallet", "Kartu Kredit"
        };

        public event Action<string> OnStatusUpdate; // ini sudah PascalCase
        public event Action<List<CartItem>> OnCartUpdated; // ini sudah PascalCase

        /// <summary>
        /// Initialize OrderManager dengan cart file path
        /// </summary>
        public Order(string cartFilePath = "../../../../fitur_Order/keranjang.json") // parameter pakai camelCase
        {
            _cartFilePath = cartFilePath ?? throw new ArgumentNullException(nameof(cartFilePath)); // Null check dengan throw
            _configuration = new Dictionary<string, string>(); // inisialisasi aman
            _statusMapping = new Dictionary<string, string>();

            InitializeConfiguration(); // method dipanggil secara eksplisit
            InitializeStatusMapping(); // method internal
        }

        private void InitializeConfiguration() // ini sudah PascalCase
        {
            try
            {
                if (File.Exists(CONFIG_FILE_PATH))
                {
                    string jsonContent = File.ReadAllText(CONFIG_FILE_PATH); // ini sudah camelCase
                    _configuration = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent)
                                   ?? GetDefaultConfiguration(); // fallback ke default
                }
                else
                {
                    _configuration = GetDefaultConfiguration(); // default config
                    SaveConfiguration(); // buat file default
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading configuration: {ex.Message}"); // log exception
                _configuration = GetDefaultConfiguration(); // fallback
            }
        }

        private static Dictionary<string, string> GetDefaultConfiguration() // ini sudah PascalCase
        {
            return new Dictionary<string, string>
            {
                { "language", "id" },
                { "payment_method", "COD" },
                { "shipping", "J&T" }
            };
        }

        private void SaveConfiguration() // ini sudah PascalCase
        {
            try
            {
                string json = JsonSerializer.Serialize(_configuration, new JsonSerializerOptions { WriteIndented = true }); // serialize dengan format rapi
                File.WriteAllText(CONFIG_FILE_PATH, json); // tulis ke file
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving configuration: {ex.Message}"); // handle exception
            }
        }

        private void InitializeStatusMapping() // ini sudah PascalCase
        {
            try
            {
                if (File.Exists(STATUS_FILE_PATH))
                {
                    string jsonContent = File.ReadAllText(STATUS_FILE_PATH);
                    _statusMapping = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent)
                                   ?? GetDefaultStatusMapping(); // fallback
                }
                else
                {
                    _statusMapping = GetDefaultStatusMapping();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading status mapping: {ex.Message}"); // log error
                _statusMapping = GetDefaultStatusMapping();
            }
        }

        private static Dictionary<string, string> GetDefaultStatusMapping() // ini sudah PascalCase
        {
            return new Dictionary<string, string>
            {
                { "1", "Diproses" },
                { "2", "Dikirim" },
                { "3", "Selesai" }
            };
        }

        public List<CartItem> LoadCart() // ini sudah PascalCase
        {
            try
            {
                if (!File.Exists(_cartFilePath))
                {
                    return new List<CartItem>(); // return kosong jika file tidak ada
                }

                string jsonContent = File.ReadAllText(_cartFilePath);

                if (string.IsNullOrWhiteSpace(jsonContent))
                {
                    return new List<CartItem>();
                }

                var cartItems = JsonSerializer.Deserialize<List<CartItem>>(jsonContent) ?? new List<CartItem>(); // deserialisasi aman
                return cartItems;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading cart: {ex.Message}");
                return new List<CartItem>(); // fallback list kosong
            }
        }

        public bool AddToCart(string productName, int quantity, decimal price, out string errorMessage) // parameter pakai camelCase
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(productName)) // validasi nama
            {
                errorMessage = "Nama produk tidak boleh kosong.";
                return false;
            }

            if (quantity <= 0) // validasi jumlah
            {
                errorMessage = "Jumlah harus lebih dari 0.";
                return false;
            }

            if (price <= 0) // validasi harga
            {
                errorMessage = "Harga harus lebih dari 0.";
                return false;
            }

            try
            {
                List<CartItem> cart = LoadCart(); // load cart dari file

                var newItem = new CartItem // objek item baru
                {
                    ProductId = Guid.NewGuid().ToString(),
                    ProductName = productName.Trim(),
                    Quantity = quantity,
                    Price = price
                };

                cart.Add(newItem);
                SaveCart(cart); // simpan keranjang baru

                OnCartUpdated?.Invoke(cart); // event callback
                OnStatusUpdate?.Invoke($"Produk '{productName}' berhasil ditambahkan ke keranjang.");

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Terjadi kesalahan: {ex.Message}";
                Debug.WriteLine($"Error adding to cart: {ex}");
                return false;
            }
        }

        private void SaveCart(List<CartItem> cart) // ini sudah PascalCase
        {
            string json = JsonSerializer.Serialize(cart, new JsonSerializerOptions { WriteIndented = true }); // simpan file keranjang
            File.WriteAllText(_cartFilePath, json);
        }

        public bool ProcessOrder(int paymentMethodIndex, out string message, out decimal totalAmount)
        {
            message = string.Empty;
            totalAmount = 0;

            try
            {
                List<CartItem> cart = LoadCart();

                if (cart.Count == 0)
                {
                    message = "Keranjang kosong. Tidak ada pesanan yang dapat diproses.";
                    return false;
                }

                if (paymentMethodIndex < 0 || paymentMethodIndex >= ValidPaymentMethods.Count) // validasi index pembayaran
                {
                    message = "Metode pembayaran tidak valid.";
                    return false;
                }

                foreach (var item in cart)
                {
                    if (item.Quantity <= 0 || item.Price <= 0)
                    {
                        message = $"Data produk '{item.ProductName}' tidak valid.";
                        return false;
                    }
                    totalAmount += item.GetSubtotal(); // hitung total
                }

                string selectedPayment = ValidPaymentMethods[paymentMethodIndex];
                _configuration["payment_method"] = selectedPayment;
                SaveConfiguration(); // simpan metode baru

                string orderId = $"SPH{DateTime.Now:yyyyMMddHHmmss}"; // Order id buatan (sekaligus memberi tau informasi waktu saat mengorder)
                string shippingMethod = _configuration.ContainsKey("shipping") ? _configuration["shipping"] : "J&T";
                string orderStatus = "Sedang Diproses";

                var orderSummary = new
                {
                    OrderId = orderId,
                    Items = cart,
                    PaymentMethod = selectedPayment,
                    ShippingMethod = shippingMethod,
                    Total = totalAmount,
                    Status = orderStatus,
                    OrderDate = DateTime.Now
                };

                SaveOrderHistory(orderSummary); // simpan riwayat pesanan
                SaveCart(new List<CartItem>()); // kosongkan cart

                message = $"PESANAN BERHASIL DIPROSES!\n\n" +
                         $"Order ID: {orderId}\n" +
                         $"Total: Rp{totalAmount:N0}\n" +
                         $"Pembayaran: {selectedPayment}\n" +
                         $"Pengiriman: {shippingMethod}\n" +
                         $"Tanggal: {DateTime.Now:dd/MM/yyyy HH:mm}\n" +
                         $"Status: {orderStatus}";

                OnCartUpdated?.Invoke(new List<CartItem>());
                OnStatusUpdate?.Invoke($"Order {orderId} berhasil diproses - Total: Rp{totalAmount:N0}");

                return true;
            }
            catch (Exception ex)
            {
                message = $"Terjadi kesalahan saat memproses pesanan: {ex.Message}";
                Debug.WriteLine($"Error processing order: {ex}");
                return false;
            }
        }

        private void SaveOrderHistory(object orderSummary) // ini sudah PascalCase
        {
            try
            {
                string historyFile = "../../../../fitur_Order/order_history.json";
                List<object> history = new List<object>();

                if (File.Exists(historyFile))
                {
                    string existingJson = File.ReadAllText(historyFile);
                    if (!string.IsNullOrWhiteSpace(existingJson))
                    {
                        history = JsonSerializer.Deserialize<List<object>>(existingJson) ?? new List<object>();
                    }
                }

                history.Add(orderSummary);

                string json = JsonSerializer.Serialize(history, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(historyFile, json);

                Debug.WriteLine($"Order history saved: {history.Count} orders total");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving order history: {ex.Message}");
            }
        }

        public List<string> GetPaymentMethods() => new List<string>(ValidPaymentMethods); // akses list metode bayar

        public Dictionary<string, string> GetConfiguration() => new Dictionary<string, string>(_configuration); // expose config

        public void ClearCart() // kosongkan keranjang
        {
            SaveCart(new List<CartItem>());
            OnCartUpdated?.Invoke(new List<CartItem>());
            OnStatusUpdate?.Invoke("Keranjang dikosongkan.");
        }

        public List<string> GetOrderStatusOptions() // opsi status pesanan
        {
            return new List<string>
            {
                "Sedang Diproses",
                "Dikonfirmasi",
                "Sedang Dikirim",
                "Selesai",
                "Dibatalkan"
            };
        }

        public List<object> GetOrderHistory() // riwayat pesanan untuk fitur tambahan
        {
            try
            {
                string historyFile = "../../../../fitur_Order/order_history.json";
                if (File.Exists(historyFile))
                {
                    string json = File.ReadAllText(historyFile);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        return JsonSerializer.Deserialize<List<object>>(json) ?? new List<object>();
                    }
                }
                return new List<object>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading order history: {ex.Message}");
                return new List<object>();
            }
        }
    }
}
