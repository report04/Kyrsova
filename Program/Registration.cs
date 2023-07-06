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
    public partial class Registration : Form
    {
        public static string serv = "DESKTOP-G6E941V\\SQLEXPRESS";
        public static string conn = "Data Source=" + serv + "; Initial Catalog=Household_Goods;Integrated Security=True";
        public SqlConnection myConnection;

        public Registration()
        {
            InitializeComponent();
            myConnection = new SqlConnection(conn);
            myConnection.Open();
            StartPosition = FormStartPosition.CenterScreen;
        }

        Point lastPoint;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void Zareg_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO client(cl_name, cl_surn, cl_email, cl_phn, cl_log, cl_pw, cl_role) " +
                    "VALUES (@cl_name, @cl_surn, @cl_email, @cl_phn, @cl_log, @cl_pw, @cl_role)";
            SqlCommand comm = new SqlCommand(query, myConnection);
            comm.Parameters.AddWithValue("@cl_name", username);
            comm.Parameters.AddWithValue("@cl_surn", Surname);
            comm.Parameters.AddWithValue("@cl_email", Email);
            comm.Parameters.AddWithValue("@cl_phn", PhoneNumber);
            comm.Parameters.AddWithValue("@cl_log", Login);
            comm.Parameters.AddWithValue("@cl_pw", Password);
            comm.Parameters.AddWithValue("@cl_role", "0");
            comm.ExecuteNonQuery();
            MessageBox.Show("Вітаємо! Ви успішно зареєструвалися");
            Authorization f = new Authorization();
            Registration ff = new Registration();
            ff.Hide();
            f.Show();
            this.Hide();

        }

        private void Back_Click(object sender, EventArgs e)
        {
            Authorization f = new Authorization();
            Registration ff = new Registration();
            ff.Hide();
            f.Show();
            this.Hide();
        }

    }
}
