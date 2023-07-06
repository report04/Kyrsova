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
    public partial class Cabinet : Form
    {
        public static string serv = "DESKTOP-G6E941V\\SQLEXPRESS";
        public static string conn = "Data Source=" + serv + ";Initial Catalog=Household_Goods;Integrated Security=True";
        public SqlConnection myConnection;
        public static string user, id_user;
        public int counter = 0;

        public Cabinet()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            myConnection = new SqlConnection(conn);
            myConnection.Open();
            textBox5.Text = user;

            string query = "SELECT cl_name FROM clients WHERE cl_log='" + user + "'";
            SqlCommand command = new SqlCommand(query, myConnection);
            textBox1.Text = command.ExecuteScalar().ToString();

            string query2 = "SELECT cl_surn FROM clients WHERE cl_log='" + user + "'";
            SqlCommand command2 = new SqlCommand(query2, myConnection);
            textBox2.Text = command2.ExecuteScalar().ToString();

            string query3 = "SELECT cl_phn FROM clients WHERE cl_log='" + user + "'";
            SqlCommand command3 = new SqlCommand(query3, myConnection);
            textBox3.Text = command3.ExecuteScalar().ToString();

            string query4 = "SELECT cl_email FROM clients WHERE cl_log='" + user + "'";
            SqlCommand command4 = new SqlCommand(query4, myConnection);
            textBox4.Text = command4.ExecuteScalar().ToString();

            string query5 = "SELECT cl_pw FROM clients WHERE cl_log='" + user + "'";
            SqlCommand command5 = new SqlCommand(query5, myConnection);
            textBox6.Text = command5.ExecuteScalar().ToString();

            string query6 = "SELECT cl_id FROM clients WHERE cl_log='" + user + "'";
            SqlCommand command6 = new SqlCommand(query6, myConnection);
            id_user = command6.ExecuteScalar().ToString();
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
