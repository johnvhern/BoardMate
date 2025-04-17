using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoardMate.Views
{
    public partial class RoomsPage : Form
    {
        private string dbConnection = "Data Source=LLOMI\\SQLEXPRESS;Initial Catalog=BoardMate;Integrated Security=True;";
        public RoomsPage()
        {
            InitializeComponent();
        }

        private void loadRoomType()
        {
            string query = "SELECT * FROM tblRoomType";
            using (SqlConnection connection = new SqlConnection(dbConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                cbRoomType.DataSource = dataTable;
                cbRoomType.DisplayMember = "roomtype";
                cbRoomType.ValueMember = "roomtype_id";
            }
        }

        private void RoomsPage_Load(object sender, EventArgs e)
        {
            loadRoomType();
            txtPrice.Text = "₱0.00";

        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control keys
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            // Remove ₱, commas, and periods from raw input
            string raw = new string(txtPrice.Text.Where(char.IsDigit).ToArray());

            if (string.IsNullOrEmpty(raw))
            {
                txtPrice.Text = "₱0.00";
                txtPrice.SelectionStart = txtPrice.Text.Length;
                return;
            }

            // Parse as integer, treat last 2 digits as centavos
            if (long.TryParse(raw, out long value))
            {
                decimal amount = value / 100m;
                txtPrice.Text = "₱" + amount.ToString("N2");
                txtPrice.SelectionStart = txtPrice.Text.Length;
            }

            //string clean = txtPrice.Text.Replace("₱", "").Replace(",", "").Trim();
            //if (long.TryParse(clean, out long value))
            //{
            //    Console.WriteLine("Parsed value: " + value); // e.g. 1000000
            //}
        }

        private void txtMaxCapacity_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys like Backspace
            if (char.IsControl(e.KeyChar)) return;

            // Allow digits
            if (char.IsDigit(e.KeyChar)) return;

            // Block anything else
            e.Handled = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
    }
}
