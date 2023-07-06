using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Household_Goods
{
    public partial class MenuClient : Form
    {
        public MenuClient()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        
            Point lastPoint;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void Back_Click(object sender, EventArgs e)
        {
            Authorization f = new Authorization();
            f.Show();
            this.Hide();
        }

        private void Catalog_Click(object sender, EventArgs e)
        {
            Catalog f = new Catalog();
            f.Show();
            this.Hide();
        }

        private void Myorders_Click(object sender, EventArgs e)
        {
            MyOrder f = new MyOrder();
            f.Show();
        }

        private void Owncabinet_Click(object sender, EventArgs e)
        {
            Cabinet f = new Cabinet();
            f.Show();
            this.Hide();
        }
    }
}
