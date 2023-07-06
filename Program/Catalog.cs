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
    public partial class Catalog : Form
    {
        public int god_id, user_id;
        public static string user;
        public static string serv = "DESKTOP-G6E941V\\SQLEXPRESS";
        public static string conn = "Data Source=" + serv + ";Initial Catalog=Household_Goods;Integrated Security=True";
        public SqlConnection myConnection;
        public SqlDataAdapter adapter;
        public DataTable table;

        public Catalog()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            myConnection = new SqlConnection(conn);
            myConnection.Open();
            adapter = new SqlDataAdapter("SELECT * FROM goods", myConnection);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            string queryid = "SELECT cl_id FROM clients WHERE cl_log='" + user + "'";
            SqlCommand commandid = new SqlCommand(queryid, myConnection);
            int id_user = Convert.ToInt32(commandid.ExecuteScalar().ToString());
            user_id = id_user;
            myConnection.Close();

        }

        private void Catalog_Load(object sender, EventArgs e)
        {
            myConnection = new SqlConnection(conn);
            myConnection.Open();
            adapter = new SqlDataAdapter("SELECT * FROM goods", myConnection);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            myConnection.Close();

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

        private void Buybutton_Click(object sender, EventArgs e)
        {
            myConnection.Open();
            string query = "INSERT INTO orders(or_id, cl_id, god_id, quantity, or_date, or_price) VALUES ( @or_id, @cl_id, @god_id, @quantity, @or_date, or_price)";
            SqlCommand comm = new SqlCommand(query, myConnection);
            comm.Parameters.AddWithValue("@or_id", textBox5.Text);
            comm.Parameters.AddWithValue("@cl_id", user_id);
            comm.Parameters.AddWithValue("@god_id", god_id);
            comm.Parameters.AddWithValue("@or_date", dateTimePicker1.Value);
            comm.Parameters.AddWithValue("@quantity", textBox3.Text);
            comm.Parameters.AddWithValue("@price_sa", textBox4.Text);
            comm.ExecuteNonQuery();
            string query1 = "INSERT INTO statistic(st_date, cl_id, god_id, god_n, cathegory, price, quantity, summary) VALUES (@st_date, @cl_id, (SELECT goods.cathegory FROM goods WHERE goods.god_id = @god_id), (SELECT goods.price FROM goods WHERE  goods.god_id = @god_id), @quantity, @summary)";
            SqlCommand comm1 = new SqlCommand(query1, myConnection);
            comm1.Parameters.AddWithValue("@date_st", dateTimePicker1.Value);
            comm1.Parameters.AddWithValue("@cl_id", user_id);
            comm1.Parameters.AddWithValue("@god_id", textBox5.Text);
            comm1.Parameters.AddWithValue("@god_n", textBox2.Text);
            comm1.Parameters.AddWithValue("@quantity", textBox3.Text);
            comm1.Parameters.AddWithValue("@summary_st", textBox4.Text);
            comm1.ExecuteNonQuery();
            myConnection.Close();
            MessageBox.Show("Ви успішно придбали товар");

        }

        private DataTable GetDataTableFromTable(string sqlCommand)
        {
            myConnection.Open();
            SqlCommand cmd = new SqlCommand(sqlCommand, myConnection);
            SqlDataReader rd = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(rd);
            myConnection.Close();
            return table;

        }

        private void searchbutton_Click(object sender, EventArgs e)
        {
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(GetDataTableFromTable("EXEC Sort @Name =  " + textBox1.Text));
            dataGridView1.DataSource = dataSet.Tables[0];

        }

        private void Back_Click(object sender, EventArgs e)
        {
            MenuClient f = new MenuClient();
            f.Show();
            this.Hide();
        }
    }
}
