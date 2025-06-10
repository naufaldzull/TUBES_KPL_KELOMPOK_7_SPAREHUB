using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpareHub
{
    public partial class Wishlist : Form
    {
        private List<string> wishlistItems;

        public Wishlist()
        {
            InitializeComponent();
            wishlistItems = new List<string>();
            UpdateStatus();
        }

        private void Wishlist_Load(object sender, EventArgs e)
        {
            // Set initial focus to textbox
            textBox1.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Optional: Enable/disable add button based on text input
            button1.Enabled = !string.IsNullOrWhiteSpace(textBox1.Text);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Optional: Custom painting for the panel if needed
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Optional: Handle label click if needed
        }

        // Tombol "Tambah" - Add item to wishlist
        private void button1_Click(object sender, EventArgs e)
        {
            string newItem = textBox1.Text.Trim();

            if (!string.IsNullOrEmpty(newItem))
            {
                // Check if item already exists
                if (!wishlistItems.Contains(newItem))
                {
                    wishlistItems.Add(newItem);
                    UpdateWishlistDisplay();
                    textBox1.Clear();
                    textBox1.Focus();
                    UpdateStatus();
                }
                else
                {
                    MessageBox.Show("Item sudah ada dalam wishlist!", "Duplikat Item",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Silakan masukkan item terlebih dahulu!", "Input Kosong",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Tombol "Hapus" - Remove selected item from wishlist
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                DialogResult result = MessageBox.Show($"Apakah Anda yakin ingin menghapus '{listBox1.SelectedItem}'?",
                                                    "Konfirmasi Hapus",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int selectedIndex = listBox1.SelectedIndex;
                    wishlistItems.RemoveAt(selectedIndex);
                    UpdateWishlistDisplay();
                    UpdateStatus();
                }
            }
            else
            {
                MessageBox.Show("Silakan pilih item yang ingin dihapus!", "Tidak Ada Pilihan",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Update the display of wishlist items
        private void UpdateWishlistDisplay()
        {
            listBox1.Items.Clear();
            foreach (string item in wishlistItems)
            {
                listBox1.Items.Add(item);
            }
        }

        // Update status label
        private void UpdateStatus()
        {
            if (wishlistItems.Count == 0)
            {
                label1.Text = "Status: Kosong";
                button2.Enabled = false;
            }
            else
            {
                label1.Text = $"Status: {wishlistItems.Count} item dalam wishlist";
                button2.Enabled = true;
            }
        }

        // Handle Enter key press in textbox to add item
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(sender, e);
                e.Handled = true;
            }
        }

        // Handle double-click on listbox to remove item
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                button2_Click(sender, e);
            }
        }

        // Handle selection change in listbox
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = listBox1.SelectedIndex >= 0 && wishlistItems.Count > 0;
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}