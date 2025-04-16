using BoardMate.Models;
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
    public partial class MainPage : Form
    {
        private bool isDragging = false;
        public MainPage()
        {
            InitializeComponent();
            SideNavigation sideNavigation = new SideNavigation(this);
            sideNavigation.Dock = DockStyle.Left;
            this.Controls.Add(sideNavigation);
            this.Load += MainPage_Load;
            this.WindowState = FormWindowState.Maximized;
            typeof(Panel).InvokeMember("DoubleBuffered",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
            null, mainPanel, new object[] { true });
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED - enables smoother UI rendering
                return cp;
            }
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            OpenForm(new DashboardPage());
        }

        public void OpenForm(Form form)
        {
            mainPanel.Controls.Clear();

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.None;
            form.Size = mainPanel.ClientSize;

            mainPanel.Controls.Add(form);
            form.Show();
        }
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int WM_NCLBUTTONDBLCLK = 0x00A3;
            const int SC_MOVE = 0xF010; // Move command
            const int WM_EXITSIZEMOVE = 0x0232; // Event when dragging stops

            if (m.Msg == WM_NCLBUTTONDBLCLK)
            {
                return; // Ignore the double-click event on the title bar
            }

            // Detect when dragging starts
            if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt32() & 0xFFF0) == SC_MOVE)
            {
                isDragging = true;
            }

            // Detect when dragging stops
            if (m.Msg == WM_EXITSIZEMOVE && isDragging)
            {
                isDragging = false;
                this.WindowState = FormWindowState.Maximized; // Re-maximize
            }

            base.WndProc(ref m);
        }
    }
}
