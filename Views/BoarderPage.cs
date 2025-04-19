using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoardMate.Views
{
    public partial class BoarderPage : Form
    {
        private string dbConnection = "Data Source=LLOMI\\SQLEXPRESS;Initial Catalog=BoardMate;Integrated Security=True;";
        int roomID;
        RoomItem roomItem;
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
            loadAllBoarders();
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
                            roomItem = new RoomItem
                            {
                                RoomId = (int)reader["room_id"],
                                RoomNumber = reader["room_number"].ToString(),
                                RoomPrice = (decimal)reader["room_price"]
                            };

                            cbRoomNumber.Items.Add(roomItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void loadAllBoarders()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(dbConnection))
                {
                    conn.Open();
                    string loadBoarders = "SELECT " +
                        "b.boarder_id," +
                        "b.room_id," +
                        "b.first_name," +
                        "b.middle_initial," +
                        "b.last_name," +
                        "b.boarder_address," +
                        "b.contact_number," +
                        "b.emergency_contact," +
                        "b.mother_name," +
                        "b.father_name," +
                        "b.is_archived, " +
                        "r.room_number FROM tblBoarders b " +
                        "INNER JOIN tblRooms r ON b.room_id = r.room_id WHERE r.is_archived = 0";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(loadBoarders, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        // Assuming you have a DataGridView named dataGridView1
                        dgvBoarders.DataSource = dt;
                        // Optionally, you can set the column headers
                        dgvBoarders.Columns["boarder_id"].Visible = false;
                        dgvBoarders.Columns["room_id"].Visible = false;
                        dgvBoarders.Columns["is_archived"].Visible = false;
                        dgvBoarders.Columns["room_number"].HeaderText = "Room Number";
                        dgvBoarders.Columns["first_name"].HeaderText = "First Name";
                        dgvBoarders.Columns["middle_initial"].HeaderText = "Middle Initial";
                        dgvBoarders.Columns["last_name"].HeaderText = "Last Name";
                        dgvBoarders.Columns["boarder_address"].HeaderText = "Address";
                        dgvBoarders.Columns["contact_number"].HeaderText = "Contact Number";
                        dgvBoarders.Columns["emergency_contact"].HeaderText = "Emergency Contact";
                        dgvBoarders.Columns["mother_name"].HeaderText = "Mother's Name";
                        dgvBoarders.Columns["father_name"].HeaderText = "Father's Name";
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

        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Select Picture";
                dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    picID.Image = Image.FromFile(dialog.FileName);
                }
            }
        }

        public static class Validator
        {
            public static bool IsNameValid(string name) =>
                !string.IsNullOrWhiteSpace(name) && Regex.IsMatch(name, @"^[A-Za-z\s\-]+$");

            public static bool IsMiddleInitialValid(string middleInitial) =>
                string.IsNullOrWhiteSpace(middleInitial) || Regex.IsMatch(middleInitial, @"^[A-Z]{1}$");

            public static bool IsPhoneNumberValid(string phone) =>
                Regex.IsMatch(phone, @"^\+639\d{9}$");

            public static bool IsComboBoxSelected(ComboBox comboBox) =>
                comboBox.SelectedIndex >= 0;
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            if (!Validator.IsNameValid(txtFirstName.Text))
            {
                MessageBox.Show("First name is required and must contain letters only.", "Invalid Entry" ,MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFirstName.Focus();
                return;
            }

            if (!Validator.IsMiddleInitialValid(txtMiddleInitial.Text))
            {
                MessageBox.Show("Middle initial must be a single uppercase letter.", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMiddleInitial.Focus();
                return;
            }

            if (!Validator.IsNameValid(txtLastName.Text))
            {
                MessageBox.Show("Last name is required and must contain letters only.", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLastName.Focus();
                return;
            }

            if (!Validator.IsPhoneNumberValid(txtPhoneNumber.Text))
            {
                MessageBox.Show("Phone number must be a valid 11-digit number starting with +639.", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPhoneNumber.Focus();
                return;
            }

            if (!Validator.IsPhoneNumberValid(txtEmergencyNumber.Text))
            {
                MessageBox.Show("Emergency number must be a valid 11-digit number starting with +639.", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmergencyNumber.Focus();
                return;
            }

            if (!Validator.IsNameValid(txtMotherName.Text))
            {
                MessageBox.Show("Mother's name is required and must contain letters only.", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMotherName.Focus();
                return;
            }

            if (!Validator.IsNameValid(txtFatherName.Text))
            {
                MessageBox.Show("Father's name is required and must contain letters only.", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFatherName.Focus();
                return;
            }

            if (!Validator.IsComboBoxSelected(cbRoomNumber))
            {
                MessageBox.Show("Please select a room number.", "No Room Number Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbRoomNumber.Focus();
                return;
            }


            // Continue your logic here
            string monthly = txtMonthlyRent.Text.Replace("₱", "").Replace(",", "").Trim();
            string deposit = txtDeposit.Text.Replace("₱", "").Replace(",", "").Trim();
            string fname = txtFirstName.Text;
            string mname = txtMiddleInitial.Text;
            string lname = txtLastName.Text;
            string address = txtAddress.Text;
            string phone = txtPhoneNumber.Text;
            string emergency = txtEmergencyNumber.Text;
            string mother = txtMotherName.Text;
            string father = txtFatherName.Text;
            string room = cbRoomNumber.Text;
            DateTime date = dtStartDate.Value;
            decimal finalMonthly = Convert.ToDecimal(monthly);
            decimal finalDeposit = Convert.ToDecimal(deposit);
            byte[] pic = null;

            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                conn.Open();
                string getRoomID = "SELECT room_id FROM tblRooms WHERE room_number = @room_number";

                SqlCommand cmd = new SqlCommand(getRoomID, conn);
                cmd.Parameters.AddWithValue("@room_number", room);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    roomID = Convert.ToInt32(reader["room_id"]);
                }
            }

            if (picID.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    picID.Image.Save(ms, picID.Image.RawFormat);
                    pic = ms.ToArray();
                }
            }
            else
            {
                pic = null;
            }

                CreateContractDialog contract = new CreateContractDialog(pic, fname, mname, lname, address, phone, emergency, mother, father, room, roomID, date, finalMonthly, finalDeposit);

            if (contract.ShowDialog() == DialogResult.OK)
            {
                loadAllBoarders();
            }
            else
            {
               
            }
        }
    }
}
