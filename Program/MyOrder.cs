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

namespace Household_Goods
{
    public partial class MyOrder : Form
    {
        public static string user;
        public static string serv = "DESKTOP-G6E941V\\SQLEXPRESS";
        public static string conn = "Data Source=" + serv + ";Initial Catalog=Household_Goods;Integrated Security=True";
        public SqlConnection myConnection;
        public SqlDataAdapter adapter;
        public DataTable table;

        public MyOrder()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            myConnection = new SqlConnection(conn);
            myConnection.Open();
            string queryid = "SELECT cl_id FROM clients WHERE cl_log='" + user + "'";
            SqlCommand commandid = new SqlCommand(queryid, myConnection);
            int id_user = Convert.ToInt32(commandid.ExecuteScalar().ToString());
            adapter = new SqlDataAdapter("SELECT * FROM orders WHERE cl_id='" + id_user + "'", myConnection);
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
            MenuClient f = new MenuClient();
            f.Show();
            this.Hide();
        }

    }
}
