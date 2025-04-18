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
        int selectedRoomID = -1;
        int isArchived = 0;
        public RoomsPage()
        {
            InitializeComponent();
        }

        private void loadRooms()
        {
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                try
                {
                    conn.Open();


                    string query = "SELECT " +
                        "r.room_id," +
                        "r.room_number," +
                        "r.room_type," +
                        "rt.roomtype," +
                        "r.current_occupancy," +
                        "r.max_capacity," +
                        "r.room_status," +
                        "r.room_price," +
                        "r.is_archived " +
                        "FROM tblRooms r " +
                        "INNER JOIN tblRoomType rt ON r.room_type = rt.roomtype_id WHERE r.is_archived = 0";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dgvRoom.DataSource = dataTable;
                        dgvRoom.Columns["is_archived"].Visible = false;
                        dgvRoom.Columns["room_id"].Visible = false;
                        dgvRoom.Columns["room_type"].Visible = false;
                        dgvRoom.Columns["roomtype"].HeaderText = "Room Type";
                        dgvRoom.Columns["room_number"].HeaderText = "Room Number";
                        dgvRoom.Columns["current_occupancy"].HeaderText = "Current Occupancy";
                        dgvRoom.Columns["room_status"].HeaderText = "Room Status";
                        dgvRoom.Columns["max_capacity"].HeaderText = "Max Capacity";
                        dgvRoom.Columns["room_price"].HeaderText = "Room Price";
                        dgvRoom.Columns["room_price"].DefaultCellStyle.Format = "C2"; // Format as currency
                        dgvRoom.Columns["room_price"].DefaultCellStyle.FormatProvider = System.Globalization.CultureInfo.GetCultureInfo("en-PH");
                        dgvRoom.ClearSelection();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

            }
        }

        private void loadRoomType()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void RoomsPage_Load(object sender, EventArgs e)
        {
            loadRooms();
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

        private void addRoom()
        {
            try
            {
                string roomType = cbRoomType.SelectedValue.ToString();
                string roomNumber = txtRoomNumber.Text;
                int maxCapacity = int.Parse(txtMaxCapacity.Text);
                decimal roomPrice;

                // Remove ₱, commas, and spaces
                string raw = txtPrice.Text.Replace("₱", "").Replace(",", "").Trim();

                if (string.IsNullOrEmpty(raw))
                {
                    MessageBox.Show("Please enter a valid price.");
                    return;
                }

                // Parse the cleaned string to decimal
                if (!decimal.TryParse(raw, out roomPrice))
                {
                    MessageBox.Show("Invalid price format.");
                    return;
                }

                // Insert into database
                using (SqlConnection connection = new SqlConnection(dbConnection))
                {
                    connection.Open();
                    string query = "INSERT INTO tblRooms (room_type, room_number, max_capacity, room_price) " +
                                   "VALUES (@room_type, @roomnumber, @maxcapacity, @roomprice)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@room_type", roomType);
                    command.Parameters.AddWithValue("@roomnumber", roomNumber);
                    command.Parameters.AddWithValue("@maxcapacity", maxCapacity);
                    command.Parameters.AddWithValue("@roomprice", roomPrice); // ✅ Corrected
                    command.ExecuteNonQuery();
                    MessageBox.Show("Room added successfully!");

                }
                loadRooms();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRoomNumber.Text))
                {
                    MessageBox.Show("Please enter a room number.");
                    return;
                }
                else if (string.IsNullOrEmpty(txtMaxCapacity.Text))
                {
                    MessageBox.Show("Please enter a maximum capacity.");
                    return;
                }
                else if (txtPrice.Text == "₱0.00")
                {
                    MessageBox.Show("Please enter a valid price.");
                    return;
                }
                else
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to add this room?", "Add Room", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        addRoom();
                        txtRoomNumber.Clear();
                        txtMaxCapacity.Clear();
                        txtPrice.Text = "₱0.00";
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void dgvRoom_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvRoom.Columns[e.ColumnIndex].Name == "room_status" && e.Value != null)
            {
                string status = e.Value.ToString();

                if (status == "Available")
                {
                    e.CellStyle.ForeColor = Color.Green;
                    e.CellStyle.Font = new Font(dgvRoom.Font, FontStyle.Regular);
                }
                else if (status == "Occupied")
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font(dgvRoom.Font, FontStyle.Regular);
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Black;
                    e.CellStyle.Font = new Font(dgvRoom.Font, FontStyle.Regular);
                }
            }
        }

        private void dgvRoom_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvRoom.Rows[e.RowIndex];
                selectedRoomID = Convert.ToInt32(row.Cells["room_id"].Value);
                isArchived = Convert.ToInt32(row.Cells["is_archived"].Value);
                cbRoomType.SelectedValue = row.Cells["room_type"].Value.ToString();
                btnArchive.Text = isArchived == 0 ? "Archive" : "Unarchive";
                txtRoomNumber.Text = row.Cells["room_number"].Value.ToString();
                txtMaxCapacity.Text = row.Cells["max_capacity"].Value.ToString();
                txtPrice.Text = row.Cells["room_price"].Value.ToString();

                btnArchive.Text = isArchived == 0 ? "Archive" : "Unarchive";
                btnAdd.Enabled = false;
            }
        }

        private void editRoom()
        {
            try
            {
                string roomType = cbRoomType.SelectedValue.ToString();
                string roomNumber = txtRoomNumber.Text;
                int maxCapacity = int.Parse(txtMaxCapacity.Text);
                decimal roomPrice;

                // Remove ₱, commas, and spaces
                string raw = txtPrice.Text.Replace("₱", "").Replace(",", "").Trim();

                if (string.IsNullOrEmpty(raw))
                {
                    MessageBox.Show("Please enter a valid price.");
                    return;
                }

                // Parse the cleaned string to decimal
                if (!decimal.TryParse(raw, out roomPrice))
                {
                    MessageBox.Show("Invalid price format.");
                    return;
                }

                // Update into database
                using (SqlConnection conn = new SqlConnection(dbConnection))
                {
                    conn.Open();

                    string checkRoom = "SELECT COUNT(*) FROM tblRooms WHERE room_number = @roomnumber AND room_id != @roomid";

                    using (SqlCommand cmd = new SqlCommand(checkRoom, conn))
                    {
                        cmd.Parameters.AddWithValue("@roomnumber", txtRoomNumber.Text);
                        cmd.Parameters.AddWithValue("@roomid", selectedRoomID);
                        int count = (int)cmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Room number already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string query = "UPDATE tblRooms SET room_type = @room_type, room_number = @roomnumber, max_capacity = @maxcapacity, room_price = @roomprice WHERE room_id = @room_id";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@room_id", selectedRoomID);
                    command.Parameters.AddWithValue("@room_type", roomType);
                    command.Parameters.AddWithValue("@roomnumber", roomNumber);
                    command.Parameters.AddWithValue("@maxcapacity", maxCapacity);
                    command.Parameters.AddWithValue("@roomprice", roomPrice);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Room updated successfully!");

                }
                loadRooms();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRoomNumber.Text))
                {
                    MessageBox.Show("Please enter a room number.");
                    return;
                }
                else if (string.IsNullOrEmpty(txtMaxCapacity.Text))
                {
                    MessageBox.Show("Please enter a maximum capacity.");
                    return;
                }
                else if (txtPrice.Text == "₱0.00")
                {
                    MessageBox.Show("Please enter a valid price.");
                    return;
                }
                else
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to edit this room?", "Edit Room", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        editRoom();
                        txtRoomNumber.Clear();
                        txtMaxCapacity.Clear();
                        txtPrice.Text = "₱0.00";
                        selectedRoomID = -1;
                        btnAdd.Enabled = true;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtRoomNumber.Clear();
            txtMaxCapacity.Clear();
            txtPrice.Text = "₱0.00";
            selectedRoomID = -1;
            btnAdd.Enabled = true;
        }

        private void archiveRoom()
        {
            try
            {
                int status = isArchived == 0 ? 1 : 0;
                string roomstatus = isArchived == 0 ? "Unavailable" : "Available";
                using (SqlConnection conn = new SqlConnection(dbConnection))
                {
                    conn.Open();
                    string query = "UPDATE tblRooms SET is_archived = @is_archived, room_status = @roomstatus WHERE room_id = @room_id";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@room_id", selectedRoomID);
                    command.Parameters.AddWithValue("@is_archived", isArchived == 0 ? 1 : 0);
                    command.Parameters.AddWithValue("@roomstatus", roomstatus);
                    command.ExecuteNonQuery();
                    string message = status == 1 ? "Room archived successfully!" : "Room unarchived successfully!";
                    MessageBox.Show($"{message}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                loadRooms();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            btnArchive.Text = "Archive";
        }
        private void btnArchive_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you sure you want to " + (isArchived == 0 ? "archive" : "unarchive") + " this room?", "Archive Room", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    archiveRoom();
                    txtRoomNumber.Clear();
                    txtMaxCapacity.Clear();
                    txtPrice.Text = "₱0.00";
                    selectedRoomID = -1;
                    btnAdd.Enabled = true;
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (cbRoomFilter.SelectedItem == null)
            {
                MessageBox.Show("Please select a filter option.", "No filter selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string filter = cbRoomFilter.SelectedItem.ToString();

            if (filter == "Available")
            {
                using (SqlConnection conn = new SqlConnection(dbConnection))
                {
                    try
                    {
                        conn.Open();
                        string query = "SELECT " +
                        "r.room_id," +
                        "r.room_number," +
                        "r.room_type," +
                        "rt.roomtype," +
                        "r.current_occupancy," +
                        "r.max_capacity," +
                        "r.room_status," +
                        "r.room_price," +
                        "r.is_archived " +
                        "FROM tblRooms r " +
                        "INNER JOIN tblRoomType rt ON r.room_type = rt.roomtype_id WHERE r.is_archived = 0 AND r.room_status = 'Available'";
                        using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dgvRoom.DataSource = dataTable;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else if (filter == "Occupied")
            {
                using (SqlConnection conn = new SqlConnection(dbConnection))
                {
                    try
                    {
                        conn.Open();
                        string query = "SELECT " +
                       "r.room_id," +
                       "r.room_number," +
                       "r.room_type," +
                       "rt.roomtype," +
                       "r.current_occupancy," +
                       "r.max_capacity," +
                       "r.room_status," +
                       "r.room_price," +
                       "r.is_archived " +
                       "FROM tblRooms r " +
                       "INNER JOIN tblRoomType rt ON r.room_type = rt.roomtype_id WHERE r.is_archived = 0 AND r.room_status = 'Occupieds'";
                        using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dgvRoom.DataSource = dataTable;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(dbConnection))
                {
                    try
                    {
                        conn.Open();
                        string query = "SELECT " +
                       "r.room_id," +
                       "r.room_number," +
                       "r.room_type," +
                       "rt.roomtype," +
                       "r.current_occupancy," +
                       "r.max_capacity," +
                       "r.room_status," +
                       "r.room_price," +
                       "r.is_archived " +
                       "FROM tblRooms r " +
                       "INNER JOIN tblRoomType rt ON r.room_type = rt.roomtype_id WHERE r.is_archived = 1";
                        using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dgvRoom.DataSource = dataTable;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }
    }
}
