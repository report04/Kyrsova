using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Household_Goods
{
    public partial class Statistic : Form
    {
        public static string user;
        public static string serv = "DESKTOP-G6E941V\\SQLEXPRESS";
        public static string conn = "Data Source=" + serv + ";Initial Catalog=Household_Goods;Integrated Security=True";
        public SqlConnection myConnection;
        public SqlDataAdapter adapter;
        public DataTable table;
        public Statistic()
        {
            
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            myConnection = new SqlConnection(conn);
            myConnection.Open();
            adapter = new SqlDataAdapter("SELECT * FROM statistic", myConnection);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;

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
            MenuAdmin f = new MenuAdmin();
            f.Show();
            this.Hide();
        }

    }
}
