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
    public partial class TimeDateBottomBar : UserControl
    {
        public TimeDateBottomBar()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            lblTimeDate.Text = now.ToString("f");
        }

        private void TimeDateBottomBar_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
