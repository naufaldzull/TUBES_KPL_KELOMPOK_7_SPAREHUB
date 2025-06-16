using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SpareHub
{
    public partial class Wishlist : Form
    {
        // List untuk menyimpan item wishlist
        private List<string> _wishlistItems = new();

        public Wishlist()
        {
            InitializeComponent(); // Inisialisasi komponen GUI
            UpdateStatus();        // Tampilkan status awal
        }

        /// <summary>
        /// Event saat form pertama kali dimuat.
        /// </summary>
        private void Wishlist_Load(object sender, EventArgs e)
        {
            textBox1.Focus(); // Fokuskan kursor ke inputan
        }

        /// <summary>
        /// Event saat isi teks berubah, digunakan untuk mengaktifkan atau menonaktifkan tombol Tambah.
        /// </summary>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = !string.IsNullOrWhiteSpace(textBox1.Text);
        }

        /// <summary>
        /// Event saat user menekan Enter pada textbox — akan menambahkan item.
        /// </summary>
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(sender, e); // Jalankan tombol Tambah
                e.Handled = true;
            }
        }

        /// <summary>
        /// Event saat item di list dipilih — aktifkan tombol Hapus.
        /// </summary>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = listBox1.SelectedIndex >= 0;
        }

        /// <summary>
        /// Event saat user double-click item di list — akan menghapus item.
        /// </summary>
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
                button2_Click(sender, e);
        }

        /// <summary>
        /// Tombol Tambah — menambahkan item baru ke wishlist.
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            string newItem = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(newItem))
            {
                MessageBox.Show("Silakan masukkan item terlebih dahulu!", "Input Kosong",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_wishlistItems.Contains(newItem))
            {
                MessageBox.Show("Item sudah ada dalam wishlist!", "Duplikat Item",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _wishlistItems.Add(newItem);
            textBox1.Clear();      // Bersihkan input setelah ditambahkan
            textBox1.Focus();      // Kembalikan fokus
            UpdateWishlistDisplay();
            UpdateStatus();
        }

        /// <summary>
        /// Tombol Hapus — menghapus item yang dipilih dari wishlist.
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Silakan pilih item yang ingin dihapus!", "Tidak Ada Pilihan",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedItem = listBox1.SelectedItem.ToString();
            var result = MessageBox.Show($"Apakah Anda yakin ingin menghapus '{selectedItem}'?",
                                         "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _wishlistItems.RemoveAt(listBox1.SelectedIndex);
                UpdateWishlistDisplay();
                UpdateStatus();
            }
        }

        #region Helper Methods

        /// <summary>
        /// Memperbarui tampilan daftar item di listbox.
        /// </summary>
        private void UpdateWishlistDisplay()
        {
            listBox1.Items.Clear();
            foreach (var item in _wishlistItems)
            {
                listBox1.Items.Add(item);
            }
        }

        /// <summary>
        /// Memperbarui status label dan tombol hapus.
        /// </summary>
        private void UpdateStatus()
        {
            if (_wishlistItems.Count == 0)
            {
                label1.Text = "Status: Kosong";
                button2.Enabled = false;
            }
            else
            {
                label1.Text = $"Status: {_wishlistItems.Count} item dalam wishlist";
                button2.Enabled = listBox1.SelectedIndex >= 0;
            }
        }
        #endregion
    }
}
