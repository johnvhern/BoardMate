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
    public partial class CreateContractDialog : Form
    {
        private string dbConnection = "Data Source=LLOMI\\SQLEXPRESS;Initial Catalog=BoardMate;Integrated Security=True;";
        private byte[] _pic;
        private string _firstname;
        private string _midname;
        private string _lastname;
        private string _address;
        private string _phone;
        private string _emergency;
        private string _mother;
        private string _father;
        private string _room;
        private DateTime _date;
        private decimal _monthly;
        private decimal _deposit;
        private int _roomID;

        decimal totalDue;
        decimal amountReceived;
        decimal change;

        public CreateContractDialog(byte[] pic, string firstname, string midname, string lastname, string address, string phone, string emergency, string mother, string father, string room, int roomID, DateTime date, decimal monthly, decimal deposit)
        {
            _pic = pic;
            _firstname = firstname;
            _midname = midname;
            _lastname = lastname;
            _address = address;
            _phone = phone;
            _emergency = emergency;
            _mother = mother;
            _father = father;
            _room = room;
            _roomID = roomID;
            _date = date;
            _monthly = monthly;
            _deposit = deposit;
            InitializeComponent();
        }

        private void CreateContractDialog_Load(object sender, EventArgs e)
        {
            txtName.Text = $"{_firstname} {_midname} {_lastname}";
            txtAddress.Text = _address;
            txtPhoneNumber.Text = _phone;
            txtEmergencyNumber.Text = _emergency;
            txtMotherName.Text = _mother;
            txtFatherName.Text = _father;
            txtRoomNumber.Text = _room;
            txtStartDate.Text = _date.ToString("dd/MM/yyyy");
            txtMonthlyRent.Text = "₱" + Convert.ToDecimal(_monthly).ToString();
            txtDeposit.Text = "₱" + Convert.ToDecimal(_deposit).ToString();
            txtTotalDue.Text = "₱" + Convert.ToDecimal(_monthly + _deposit).ToString();
        }

        private void txtTotalAmountReceived_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control keys
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTotalAmountReceived_TextChanged(object sender, EventArgs e)
        {
            string raw = new string(txtTotalAmountReceived.Text.Where(char.IsDigit).ToArray());

            if (string.IsNullOrEmpty(raw))
            {
                txtTotalAmountReceived.Text = "₱0.00";
                txtTotalAmountReceived.SelectionStart = txtTotalAmountReceived.Text.Length;
                return;
            }

            // Parse as integer, treat last 2 digits as centavos
            if (long.TryParse(raw, out long value))
            {
                decimal amount = value / 100m;
                txtTotalAmountReceived.Text = "₱" + amount.ToString("N2");
                txtTotalAmountReceived.SelectionStart = txtTotalAmountReceived.Text.Length;
            }

            txtChange.Text = "₱" + (Convert.ToDecimal(txtTotalAmountReceived.Text.Replace("₱", "").Replace(",", "")) - Convert.ToDecimal(txtTotalDue.Text.Replace("₱", "").Replace(",", ""))).ToString("N2");
        }

        private void addBoarder()
        {
            totalDue = Convert.ToDecimal(txtTotalDue.Text.Replace("₱", "").Replace(",", "").Trim());
            amountReceived = Convert.ToDecimal(txtTotalAmountReceived.Text.Replace("₱", "").Replace(",", "").Trim());
            change = Convert.ToDecimal(txtChange.Text.Replace("₱", "").Replace(",", "").Trim());

            using (SqlConnection conn = new SqlConnection(dbConnection))
            {

                string insertBoarder = "INSERT INTO tblBoarders (room_id, first_name, middle_initial, last_name, boarder_address, contact_number, emergency_contact, mother_name, father_name, boarder_picture) " +
                    "VALUES (@roomid, @firstname, @middleinitial, @lastname, @boarderaddress, @contactnumber, @emergencynumber, @mothername, @fathername, @boarderpicture)";

                using (SqlCommand cmd = new SqlCommand(insertBoarder, conn))
                {
                    cmd.Parameters.AddWithValue("@roomid", _roomID);
                    cmd.Parameters.AddWithValue("@firstname", _firstname);
                    cmd.Parameters.AddWithValue("@middleinitial", _midname);
                    cmd.Parameters.AddWithValue("@lastname", _lastname);
                    cmd.Parameters.AddWithValue("@boarderaddress", _address);
                    cmd.Parameters.AddWithValue("@contactnumber", _phone);
                    cmd.Parameters.AddWithValue("@emergencynumber", _emergency);
                    cmd.Parameters.AddWithValue("@mothername", _mother);
                    cmd.Parameters.AddWithValue("@fathername", _father);
                    cmd.Parameters.Add("@boarderpicture", SqlDbType.VarBinary).Value = (object)_pic ?? DBNull.Value;

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Boarder added successfully.");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add boarder.");
                    }
                }

            }
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTotalAmountReceived.Text))
                {
                    MessageBox.Show("Please pay first before proceeding.");
                    return;
                }

                decimal parsedTotalDue = Convert.ToDecimal(txtTotalDue.Text.Replace("₱", "").Replace(",", "").Trim());
                decimal parsedAmountReceived = Convert.ToDecimal(txtTotalAmountReceived.Text.Replace("₱", "").Replace(",", "").Trim());

                if (parsedAmountReceived < parsedTotalDue)
                {
                    MessageBox.Show("Amount received is less than the total due.");
                }
                else
                {
                    totalDue = parsedTotalDue;
                    amountReceived = parsedAmountReceived;
                    change = Convert.ToDecimal(txtChange.Text.Replace("₱", "").Replace(",", "").Trim());

                    addBoarder();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


    }
}
