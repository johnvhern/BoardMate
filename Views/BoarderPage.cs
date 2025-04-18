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
    public partial class BoarderPage : Form
    {
        private string dbConnection = "Data Source=LLOMI\\SQLEXPRESS;Initial Catalog=BoardMate;Integrated Security=True;";
        public BoarderPage()
        {
            InitializeComponent();
        }

        private void BoarderPage_Load(object sender, EventArgs e)
        {
            txtPhoneNumber.Text = "+639";
            txtEmergencyNumber.Text = "+639";
            txtPhoneNumber.SelectionStart = txtPhoneNumber.Text.Length;
            txtEmergencyNumber.SelectionStart = txtEmergencyNumber.Text.Length;
            txtPhoneNumber.MaxLength = 13;
            txtEmergencyNumber.MaxLength = 13;
            loadRoomNumbers();
        }

        private void loadRoomNumbers()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(dbConnection))
                {
                    conn.Open();
                    string query = "SELECT room_id, room_number, room_price FROM tblRooms WHERE is_archived = 0";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RoomItem room = new RoomItem
                            {
                                RoomId = (int)reader["room_id"],
                                RoomNumber = reader["room_number"].ToString(),
                                RoomPrice = (decimal)reader["room_price"]
                            };

                            cbRoomNumber.Items.Add(room);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control characters (like Backspace)
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            // Allow digits
            if (char.IsDigit(e.KeyChar))
            {
                return;
            }

            // Allow only one '+' at the beginning
            if (e.KeyChar == '+' && txtPhoneNumber.SelectionStart == 0 && !txtPhoneNumber.Text.Contains("+"))
            {
                return;
            }


            // Block anything else
            e.Handled = true;
        }

        private void txtPhoneNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete) && txtPhoneNumber.SelectionStart <= 4)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtPhoneNumber_TextChanged(object sender, EventArgs e)
        {
            if (!txtPhoneNumber.Text.StartsWith("+639"))
            {
                int cursorPos = txtPhoneNumber.SelectionStart;
                txtPhoneNumber.Text = "+639";
                txtPhoneNumber.SelectionStart = txtPhoneNumber.Text.Length;
            }
        }

        private void txtEmergencyNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control characters (like Backspace)
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            // Allow digits
            if (char.IsDigit(e.KeyChar))
            {
                return;
            }

            // Allow only one '+' at the beginning
            if (e.KeyChar == '+' && txtEmergencyNumber.SelectionStart == 0 && !txtEmergencyNumber.Text.Contains("+"))
            {
                return;
            }


            // Block anything else
            e.Handled = true;
        }

        private void txtEmergencyNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete) && txtEmergencyNumber.SelectionStart <= 4)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtEmergencyNumber_TextChanged(object sender, EventArgs e)
        {
            if (!txtEmergencyNumber.Text.StartsWith("+639"))
            {
                int cursorPos = txtEmergencyNumber.SelectionStart;
                txtEmergencyNumber.Text = "+639";
                txtEmergencyNumber.SelectionStart = txtEmergencyNumber.Text.Length;
            }
        }

        private void cbRoomNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRoomNumber.SelectedItem is RoomItem selectedRoom)
            {
                decimal price = selectedRoom.RoomPrice;
                txtMonthlyRent.Text = price.ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
                txtDeposit.Text = price.ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("en-PH"));
            }
        }
    }
}
