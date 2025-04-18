using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoardMate.Views
{
    public partial class CreateContractDialog : Form
    {
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

        public CreateContractDialog(string firstname, string midname, string lastname, string address, string phone, string emergency, string mother, string father, string room, DateTime date, decimal monthly, decimal deposit)
        {
            _firstname = firstname;
            _midname = midname;
            _lastname = lastname;
            _address = address;
            _phone = phone;
            _emergency = emergency;
            _mother = mother;
            _father = father;
            _room = room;
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
            txtTotalDue.Text = "₱" + (_monthly + _deposit).ToString("N2");
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

        private void btnConfirm_Click(object sender, EventArgs e)
        {

        }
    }
}
