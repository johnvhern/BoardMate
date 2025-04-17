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
    public partial class DashboardPage : Form
    {
        private string dbConnection = "Data Source=LLOMI\\SQLEXPRESS;Initial Catalog=BoardMate;Integrated Security=True;";
        public DashboardPage()
        {
            InitializeComponent();
        }

        private void loadAllRooms()
        {
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                conn.Open();
                string loadRoomsQuery = "SELECT COUNT(*) FROM tblRooms";

                using (SqlCommand cmd = new SqlCommand(loadRoomsQuery, conn))
                {
                    int roomCount = (int)cmd.ExecuteScalar();
                    lblAllRooms.Text = roomCount.ToString();
                }
            }
        }

        private void loadAvailableRooms()
        {
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                conn.Open();
                string loadAvailableRooms = "SELECT COUNT(*) FROM tblRooms WHERE room_status = 'Available'";

                using (SqlCommand cmd = new SqlCommand(loadAvailableRooms, conn))
                {
                    int availableRoomCount = (int)cmd.ExecuteScalar();
                    lblAvailableRooms.Text = availableRoomCount.ToString();
                }
            }
        }

        private void DashboardPage_Load(object sender, EventArgs e)
        {
            loadAllRooms();
            loadAvailableRooms();
        }
    }
}
