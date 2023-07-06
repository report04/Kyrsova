using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Household_Goods
{
    public partial class MenuAdmin : Form
    {
        public MenuAdmin()
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

        private void Statistics_Click(object sender, EventArgs e)
        {
            Statistic f = new Statistic();
            f.Show();
            this.Hide();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            Authorization f = new Authorization();
            f.Show();
            this.Hide();
        }

        private void ProductManagament_Click(object sender, EventArgs e)
        {
            ProductManagament f = new ProductManagament();
            f.Show();
            this.Hide();
        }
    }
}
