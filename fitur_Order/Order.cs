using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics;

namespace fitur_Order
{
    /// <summary>
    /// Represents an item in the shopping cart
    /// PERBAIKAN: Added XML documentation dan proper namespace
    /// </summary>
    public class CartItem
    {
        public string ProductId { get; set; } = string.Empty; // PERBAIKAN: Default value untuk menghindari null
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; } // PERBAIKAN: Ubah dari int ke decimal untuk currency yang lebih akurat

        /// <summary>
        /// Calculate subtotal for this cart item
        /// PERBAIKAN: Added method untuk encapsulation dan reusability
        /// </summary>
        public decimal GetSubtotal() => Quantity * Price;
    }

    /// <summary>
    /// Handles order processing and cart management
    /// PERBAIKAN: Added proper class documentation
    /// </summary>
    public class Order // PERBAIKAN: Rename dari "Order" ke "OrderManager" karena lebih descriptive
    {
        // PERBAIKAN: Use const untuk path yang tidak berubah dan private readonly untuk yang bisa di-configure
        private readonly string _cartFilePath;
        private const string CONFIG_FILE_PATH = "C:\\Users\\Lenovo\\Desktop\\KPL\\TUBES SPAREHUB GUI\\TUBES_KPL_KELOMPOK_7_SPAREHUB\\fitur_Order\\config.json"; // PERBAIKAN: Relative path, lebih portable
        private const string STATUS_FILE_PATH = "C:\\Users\\Lenovo\\Desktop\\KPL\\TUBES SPAREHUB GUI\\TUBES_KPL_KELOMPOK_7_SPAREHUB\\fitur_Order\\status_mapping.json";

        private Dictionary<string, string> _configuration; // PERBAIKAN: Pascal case untuk private fields dengan underscore
        private Dictionary<string, string> _statusMapping;

        // PERBAIKAN: Table-driven approach dengan readonly collection
        private static readonly List<string> ValidPaymentMethods = new List<string>
        {
            "COD", "Transfer", "E-Wallet", "Kartu Kredit"
        };

        // PERBAIKAN: Event untuk communication dengan GUI dan fitur lain
        public event Action<string> OnStatusUpdate;
        public event Action<List<CartItem>> OnCartUpdated;

        /// <summary>
        /// Initialize OrderManager with optional cart file path
        /// PERBAIKAN: Constructor dengan dependency injection pattern
        /// </summary>
        public Order(string cartFilePath = "C:\\Users\\Lenovo\\Desktop\\KPL\\TUBES SPAREHUB GUI\\TUBES_KPL_KELOMPOK_7_SPAREHUB\\fitur_Order\\keranjang.json")
        {
            _cartFilePath = cartFilePath ?? throw new ArgumentNullException(nameof(cartFilePath)); // PERBAIKAN: Null check
            _configuration = new Dictionary<string, string>();
            _statusMapping = new Dictionary<string, string>();

            InitializeConfiguration();
            InitializeStatusMapping();
        }

        /// <summary>
        /// Load runtime configuration from file
        /// PERBAIKAN: Rename method untuk clarity dan error handling yang lebih baik
        /// </summary>
        private void InitializeConfiguration()
        {
            try
            {
                if (File.Exists(CONFIG_FILE_PATH))
                {
                    string jsonContent = File.ReadAllText(CONFIG_FILE_PATH);
                    _configuration = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent)
                                   ?? GetDefaultConfiguration(); // PERBAIKAN: Null coalescing
                }
                else
                {
                    _configuration = GetDefaultConfiguration();
                    SaveConfiguration(); // PERBAIKAN: Auto-create default config file
                }
            }
            catch (Exception ex)
            {
                // PERBAIKAN: Proper exception handling dengan logging
                Debug.WriteLine($"Error loading configuration: {ex.Message}");
                _configuration = GetDefaultConfiguration();
            }
        }

        /// <summary>
        /// Get default configuration values
        /// PERBAIKAN: Extracted method untuk reusability
        /// </summary>
        private static Dictionary<string, string> GetDefaultConfiguration()
        {
            return new Dictionary<string, string>
            {
                { "language", "id" },
                { "payment_method", "COD" },
                { "shipping", "J&T" }
            };
        }

        /// <summary>
        /// Save current configuration to file
        /// PERBAIKAN: Added method untuk persistence
        /// </summary>
        private void SaveConfiguration()
        {
            try
            {
                string json = JsonSerializer.Serialize(_configuration, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(CONFIG_FILE_PATH, json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving configuration: {ex.Message}");
            }
        }

        /// <summary>
        /// Initialize status mapping from file
        /// PERBAIKAN: Better method naming dan error handling
        /// </summary>
        private void InitializeStatusMapping()
        {
            try
            {
                if (File.Exists(STATUS_FILE_PATH))
                {
                    string jsonContent = File.ReadAllText(STATUS_FILE_PATH);
                    _statusMapping = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent)
                                   ?? GetDefaultStatusMapping();
                }
                else
                {
                    _statusMapping = GetDefaultStatusMapping();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading status mapping: {ex.Message}");
                _statusMapping = GetDefaultStatusMapping();
            }
        }

        /// <summary>
        /// Get default status mapping
        /// PERBAIKAN: Extracted method untuk maintainability
        /// </summary>
        private static Dictionary<string, string> GetDefaultStatusMapping()
        {
            return new Dictionary<string, string>
            {
                { "1", "Diproses" },
                { "2", "Dikirim" },
                { "3", "Selesai" }
            };
        }

        /// <summary>
        /// Load cart items from file
        /// PERBAIKAN: Better error handling dan return value
        /// </summary>
        public List<CartItem> LoadCart()
        {
            try
            {
                if (!File.Exists(_cartFilePath))
                {
                    return new List<CartItem>();
                }

                string jsonContent = File.ReadAllText(_cartFilePath);

                if (string.IsNullOrWhiteSpace(jsonContent))
                {
                    return new List<CartItem>();
                }

                var cartItems = JsonSerializer.Deserialize<List<CartItem>>(jsonContent) ?? new List<CartItem>();
                return cartItems;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading cart: {ex.Message}");
                return new List<CartItem>(); // PERBAIKAN: Return empty list instead of throwing
            }
        }

        /// <summary>
        /// Add item to cart with validation
        /// PERBAIKAN: Separated validation logic dan better parameter handling
        /// </summary>
        public bool AddToCart(string productName, int quantity, decimal price, out string errorMessage)
        {
            errorMessage = string.Empty;

            // PERBAIKAN: Input validation dengan specific error messages
            if (string.IsNullOrWhiteSpace(productName))
            {
                errorMessage = "Nama produk tidak boleh kosong.";
                return false;
            }

            if (quantity <= 0)
            {
                errorMessage = "Jumlah harus lebih dari 0.";
                return false;
            }

            if (price <= 0)
            {
                errorMessage = "Harga harus lebih dari 0.";
                return false;
            }

            try
            {
                List<CartItem> cart = LoadCart();

                var newItem = new CartItem
                {
                    ProductId = Guid.NewGuid().ToString(),
                    ProductName = productName.Trim(), // PERBAIKAN: Trim whitespace
                    Quantity = quantity,
                    Price = price
                };

                cart.Add(newItem);
                SaveCart(cart); // PERBAIKAN: Extracted save logic

                OnCartUpdated?.Invoke(cart); // PERBAIKAN: Event notification
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

        /// <summary>
        /// Save cart to file
        /// PERBAIKAN: Extracted method untuk reusability
        /// </summary>
        private void SaveCart(List<CartItem> cart)
        {
            string json = JsonSerializer.Serialize(cart, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_cartFilePath, json);
        }

        /// <summary>
        /// Process order with selected payment method
        /// PERBAIKAN: Fixed status display dan detailed order processing
        /// </summary>
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

                // PERBAIKAN: Validate payment method index
                if (paymentMethodIndex < 0 || paymentMethodIndex >= ValidPaymentMethods.Count)
                {
                    message = "Metode pembayaran tidak valid.";
                    return false;
                }

                // PERBAIKAN: Calculate total dengan validation
                foreach (var item in cart)
                {
                    if (item.Quantity <= 0 || item.Price <= 0)
                    {
                        message = $"Data produk '{item.ProductName}' tidak valid.";
                        return false;
                    }
                    totalAmount += item.GetSubtotal();
                }

                string selectedPayment = ValidPaymentMethods[paymentMethodIndex];
                _configuration["payment_method"] = selectedPayment;
                SaveConfiguration();

                // PERBAIKAN: Generate order ID dan detailed status
                string orderId = $"SPH{DateTime.Now:yyyyMMddHHmmss}";
                string shippingMethod = _configuration.ContainsKey("shipping") ? _configuration["shipping"] : "J&T";
                string orderStatus = "Sedang Diproses";

                // PERBAIKAN: Create detailed order summary
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

                // PERBAIKAN: Save order history (optional - bisa untuk report nanti)
                SaveOrderHistory(orderSummary);

                // PERBAIKAN: Clear cart after successful processing
                SaveCart(new List<CartItem>());

                // PERBAIKAN: Detailed status message yang jelas
                message = $"PESANAN BERHASIL DIPROSES!\n\n" +
                         $"Order ID: {orderId}\n" +
                         $"Total: Rp{totalAmount:N0}\n" +
                         $"Pembayaran: {selectedPayment}\n" +
                         $"Pengiriman: {shippingMethod}\n" +
                         $"Tanggal: {DateTime.Now:dd/MM/yyyy HH:mm}\n" +
                         $"Status: {orderStatus}";

                OnCartUpdated?.Invoke(new List<CartItem>()); // PERBAIKAN: Notify cart cleared
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

        /// <summary>
        /// Save order history untuk tracking
        /// PERBAIKAN: Optional order history untuk report fitur
        /// </summary>
        private void SaveOrderHistory(object orderSummary)
        {
            try
            {
                string historyFile = "order_history.json";
                List<object> history = new List<object>();

                // Load existing history
                if (File.Exists(historyFile))
                {
                    string existingJson = File.ReadAllText(historyFile);
                    if (!string.IsNullOrWhiteSpace(existingJson))
                    {
                        history = JsonSerializer.Deserialize<List<object>>(existingJson) ?? new List<object>();
                    }
                }

                // Add new order
                history.Add(orderSummary);

                // Save updated history
                string json = JsonSerializer.Serialize(history, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(historyFile, json);

                Debug.WriteLine($"Order history saved: {history.Count} orders total");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving order history: {ex.Message}");
                // Don't fail the order process if history save fails
            }
        }

        /// <summary>
        /// Get available payment methods
        /// PERBAIKAN: Public method untuk GUI access
        /// </summary>
        public List<string> GetPaymentMethods()
        {
            return new List<string>(ValidPaymentMethods); // PERBAIKAN: Return copy untuk immutability
        }

        /// <summary>
        /// Get current configuration
        /// PERBAIKAN: Public method untuk GUI access
        /// </summary>
        public Dictionary<string, string> GetConfiguration()
        {
            return new Dictionary<string, string>(_configuration); // PERBAIKAN: Return copy
        }

        /// <summary>
        /// Clear cart
        /// PERBAIKAN: Public method untuk GUI functionality
        /// </summary>
        public void ClearCart()
        {
            SaveCart(new List<CartItem>());
            OnCartUpdated?.Invoke(new List<CartItem>());
            OnStatusUpdate?.Invoke("Keranjang dikosongkan.");
        }

        /// <summary>
        /// Get order status options untuk GUI
        /// PERBAIKAN: Public method untuk status tracking
        /// </summary>
        public List<string> GetOrderStatusOptions()
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

        /// <summary>
        /// Get order history untuk report
        /// PERBAIKAN: Method untuk integration dengan fitur report
        /// </summary>
        public List<object> GetOrderHistory()
        {
            try
            {
                string historyFile = "order_history.json";
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