using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoardMate.Models
{
    public partial class SideNavigation : UserControl
    {
        private Views.MainPage mainPage;
        private Button activeButtonn;
        private bool isCollapsed;
        public SideNavigation(Views.MainPage form)
        {
            InitializeComponent();
            this.mainPage = form;
            timer1.Start();
            ColorActiveButton(btnDashboard);
        }

        private void ColorActiveButton(Button button)
        {
            if (activeButtonn != null)
            {
                activeButtonn.BackColor = SystemColors.ControlLight;
                activeButtonn.ForeColor = Color.Black;
            }

            activeButtonn = button;
            activeButtonn.BackColor = SystemColors.ActiveBorder;
            activeButtonn.ForeColor = Color.Black;
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            mainPage.OpenForm(new Views.DashboardPage());
            ColorActiveButton((Button)sender);
        }

        private void btnRooms_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void btnBoarder_Click(object sender, EventArgs e)
        {
            mainPage.OpenForm(new Views.BoarderPage());
            ColorActiveButton((Button)sender);
        }

        private void btnContract_Click(object sender, EventArgs e)
        {
            mainPage.OpenForm(new Views.ContractPage());
            ColorActiveButton((Button)sender);
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            mainPage.OpenForm(new Views.InvoicePage());
            ColorActiveButton((Button)sender);
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            mainPage.OpenForm(new Views.PaymentPage());
            ColorActiveButton((Button)sender);
        }

        private void btnSMS_Click(object sender, EventArgs e)
        {
            ColorActiveButton((Button)sender);
            Views.SMSPage sMSPage = new Views.SMSPage();
            sMSPage.ShowDialog();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            mainPage.OpenForm(new Views.SettingsPage());
            ColorActiveButton((Button)sender);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                mainPage.Hide();
                Views.LoginPage loginForm = new Views.LoginPage();
                loginForm.ShowDialog();
            }
            else
            {
                return;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isCollapsed)
            {
                panelBtnRoom.Height += 10;
                btnRooms.Text = "  Rooms        ⮝";
                if (panelBtnRoom.Size == panelBtnRoom.MaximumSize)
                {
                    timer1.Stop();
                    isCollapsed = false;
                }
            }
            else
            {
                panelBtnRoom.Height -= 10;
                btnRooms.Text = "  Rooms        ⮟";
                if (panelBtnRoom.Size == panelBtnRoom.MinimumSize)
                {
                    timer1.Stop();
                    isCollapsed = true;
                }
            }
        }

        private void btnManageRooms_Click(object sender, EventArgs e)
        {
            mainPage.OpenForm(new Views.RoomsPage());
            ColorActiveButton((Button)sender);
        }

        private void btnRoomType_Click(object sender, EventArgs e)
        {
            Views.RoomTypePage roomTypePage = new Views.RoomTypePage();
            roomTypePage.ShowDialog();
            ColorActiveButton((Button)sender);
        }
    }
}
