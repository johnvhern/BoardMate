using MetroFramework.Controls;
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
    public partial class LoginPage : Form
    {
        private string dbConnection = "Data Source=LLOMI\\SQLEXPRESS;Initial Catalog=BoardMate;Integrated Security=True;";
        bool showPassword = false;

        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnShowPass_MouseDown(object sender, MouseEventArgs e)
        {
            showPassword = !showPassword;
            txtPassword.PasswordChar = showPassword ? '\0' : '●';
        }

        private void btnShowPass_MouseUp(object sender, MouseEventArgs e)
        {

            showPassword = !showPassword;
            txtPassword.PasswordChar = showPassword ? '\0' : '●';
        }

        public bool verifyLogin(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM tblAdmin WHERE username = @username AND user_pass = @password";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (verifyLogin(username, password))
                {
                    MessageBox.Show("Login successful!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Proceed to the next form or functionality
                    this.Hide();
                    MainPage mainPage = new MainPage();
                    mainPage.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
