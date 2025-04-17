using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoardMate.Views
{
    public partial class RoomTypePage : Form
    {
        private string dbConnection = "Data Source=LLOMI\\SQLEXPRESS;Initial Catalog=BoardMate;Integrated Security=True;";
        int selectedID = -1;
        public RoomTypePage()
        {
            InitializeComponent();
        }

        private void loadRoomType()
        {
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                try
                {
                    conn.Open();


                    string loadRoomType = "SELECT roomtype_id, roomtype FROM tblRoomType WHERE roomtype IS NOT NULL AND roomtype <> '' AND is_archived = 0";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(loadRoomType, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvRoomType.DataSource = dt;
                        dgvRoomType.Columns["roomtype_id"].Visible = false;
                        dgvRoomType.ClearSelection();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

            }
        }

        private void addRoomType(string roomtype)
        {
            if (!Regex.IsMatch(roomtype, @"^[a-zA-Z\s]+$"))
            {
                MessageBox.Show("Only letters are allowed!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                try
                {
                    conn.Open();
                    string checkRoomType = "SELECT COUNT(*) FROM tblRoomType WHERE roomtype = @roomtype";

                    using (SqlCommand cmd = new SqlCommand(checkRoomType, conn))
                    {
                        cmd.Parameters.AddWithValue("@roomtype", roomtype);
                        int count = (int)cmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Room type already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    string addRoomType = "INSERT INTO tblRoomType (roomtype) VALUES (@roomtype)";

                    using (SqlCommand cmd = new SqlCommand(addRoomType, conn))
                    {
                        cmd.Parameters.AddWithValue("@roomtype", roomtype);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Room type added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadRoomType();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

            }
        }

        private void editRoomType(string roomtype)
        {
            if (!Regex.IsMatch(roomtype, @"^[a-zA-Z\s]+$"))
            {
                MessageBox.Show("Only letters are allowed!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                try
                {
                    conn.Open();
                    string checkRoomType = "SELECT COUNT(*) FROM tblRoomType WHERE roomtype = @roomtype";

                    using (SqlCommand cmd = new SqlCommand(checkRoomType, conn))
                    {
                        cmd.Parameters.AddWithValue("@roomtype", roomtype);
                        int count = (int)cmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Room type already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    string addRoomType = "UPDATE tblRoomType SET roomtype = @roomtype WHERE roomtype_id = @roomtype_id";

                    using (SqlCommand cmd = new SqlCommand(addRoomType, conn))
                    {
                        cmd.Parameters.AddWithValue("@roomtype_id", selectedID);
                        cmd.Parameters.AddWithValue("@roomtype", roomtype);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Room type updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    selectedID = -1;
                    loadRoomType();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

            }
        }

        private void archiveRoomType()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(dbConnection))
                {
                    try
                    {
                        conn.Open();
                        string archiveRoomType = "UPDATE tblRoomType SET is_archived = 1 WHERE roomtype_id = @roomtype_id";
                        using (SqlCommand cmd = new SqlCommand(archiveRoomType, conn))
                        {
                            cmd.Parameters.AddWithValue("@roomtype_id", selectedID);
                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show("Room type archived successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        selectedID = -1;
                        loadRoomType();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string roomtype = txtRoomType.Text.Trim();

            if (string.IsNullOrEmpty(roomtype))
            {
                MessageBox.Show("Please enter a room type.", "Empty Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (!string.IsNullOrEmpty(roomtype))
            {
                addRoomType(roomtype);
                txtRoomType.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a valid room type.", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RoomTypePage_Load(object sender, EventArgs e)
        {
            loadRoomType();
        }

        private void dgvRoomType_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvRoomType.Rows[e.RowIndex];
                selectedID = Convert.ToInt32(row.Cells["roomtype_id"].Value);
                txtRoomType.Text = row.Cells["roomtype"].Value.ToString();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string roomtype = txtRoomType.Text.Trim();

            if (selectedID == -1)
            {
                MessageBox.Show("Please select a room type.", "No room type selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (selectedID != -1)
            {
                editRoomType(roomtype);
                txtRoomType.Clear();
                selectedID = -1;
            }
            else
            {
                MessageBox.Show("Please enter a valid room type.", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtRoomType.Clear();
            selectedID = -1;
        }

        private void btnArchive_Click(object sender, EventArgs e)
        {
            if (selectedID == -1)
            {
                MessageBox.Show("Please select a room type.", "No room type selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                archiveRoomType();
                txtRoomType.Clear();
                selectedID = -1;
            }
        }

        private void filterRoomType()
        {
            string status = cbFilter.SelectedItem.ToString();

            if (status.Equals("Active"))
            {
                using (SqlConnection conn = new SqlConnection(dbConnection))
                {
                    string filterQuery = "SELECT roomtype_id, roomtype FROM tblRoomType WHERE roomtype IS NOT NULL AND roomtype <> '' AND is_archived = 0";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(filterQuery, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvRoomType.DataSource = dt;
                        dgvRoomType.Columns["roomtype_id"].Visible = false;
                        dgvRoomType.ClearSelection();
                    }
                }
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(dbConnection))
                {
                    string filterQuery = "SELECT roomtype_id, roomtype FROM tblRoomType WHERE roomtype IS NOT NULL AND roomtype <> '' AND is_archived = 1";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(filterQuery, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvRoomType.DataSource = dt;
                        dgvRoomType.Columns["roomtype_id"].Visible = false;
                        dgvRoomType.ClearSelection();
                    }
                }
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            filterRoomType();
        }
    }
}
