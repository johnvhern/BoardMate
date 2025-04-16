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


                    string loadRoomType = "SELECT roomtype FROM tblRoomType WHERE roomtype IS NOT NULL AND roomtype <> '' AND is_archived = 0";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(loadRoomType, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvRoomType.DataSource = dt;
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

                    loadRoomType();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                   
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
    }
}
