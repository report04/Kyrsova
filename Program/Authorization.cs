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
using System.Diagnostics;

namespace Household_Goods
{
    public partial class Authorization : Form
    {
        public static int UserId;
        public SqlConnection con;
        public SqlConnection myConnection;
        public static string serv = "DESKTOP-G6E941V\\SQLEXPRESS";
        public static string conn = "Data Source=" + serv + ";Initial Catalog=Household_Goods;Integrated Security=True";
        
        private void LogInto_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(conn);
                con.Open();

                SqlCommand auth = new SqlCommand("Select * from clients where cl_log = @login and cl_pw = @pass", con);
                auth.Parameters.AddWithValue("@login", textBox1.Text.Trim());
                auth.Parameters.AddWithValue("@pass", textBox2.Text.Trim());
                string query = "SELECT cl_role FROM clients WHERE cl_log='" + textBox1.Text + "'";
                SqlCommand command = new SqlCommand(query, con);
                string adm = command.ExecuteScalar().ToString();
             
                if (con.State == ConnectionState.Open)
                {
                    if (adm == "True")
                    {
                        MenuAdmin f = new MenuAdmin();
                        f.Show();
                        this.Hide();

                    }
                    else
                    {
                        Cabinet.user = textBox1.Text;
                        MyOrder.user = textBox1.Text;
                        Catalog.user = textBox1.Text;
                        
                        
                        MenuClient f = new MenuClient();
                        f.Show();
                        this.Hide();
                    }
                }
                else
                    throw new Exception("Користувача не знайдено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                textBox1.Text = "";
                textBox2.Text = "";

            }
            con.Close();

        }

        private void Donthaveacc_Click(object sender, EventArgs e)
        {
            Registration f = new Registration();
            f.Show();
            this.Hide();

        }
        
        public Authorization()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            myConnection = new SqlConnection(conn);
            con = new SqlConnection(conn);
        }

        private void Exitbutton_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void Authorization_Load(object sender, EventArgs e)
        {

            textBox1.MaxLength = 50;
            textBox2.MaxLength = 50;
        }
        // перетягування меню
        Point lastPoint;
        private void Authorization_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void Authorization_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
    }
}


    


