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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Household_Goods
{
    public partial class ProductManagament : Form
    {
        public static string serv = "DESKTOP-G6E941V\\SQLEXPRESS";
        public static string conn = "Data Source=" + serv + ";Initial Catalog=Household_Goods;Integrated Security=True";
        public SqlConnection myConnection;
        public List<string> items = new List<string>();
        
        public ProductManagament()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            myConnection = new SqlConnection(conn);
        }

        public void DateRefresh()
        {
            myConnection.Open();
            SqlCommand cmd = new SqlCommand("select god_id from goods", myConnection);
            myConnection.Close();
        }


        private void Product_Load(object sender, EventArgs e)
        {
            DateRefresh();

        }
        public void Search(ListBox listBox, List<string> q)
        {

            bool flag = false;
            List<int> c = new List<int>();
            List<string> match = new List<string>();
            int n = 0;
            double p = 0;
            int count = 0;
            int index = 0;
            List<string> s = new List<string>();
            for (int i = 0; i < Productname.Text.Length; i++)
            {
                for (int j = 0; j < s.Count; j++)
                {
                    for (int k = 0; k < s[j].Length; k++)
                    {
                        if (s[j][k] == Productname.Text[i] || char.ToLower(s[j][k]) == Productname.Text[i] || char.ToUpper(s[j][k]) == Productname.Text[i])
                        {
                            c[j] = ++c[j];
                            count++;
                            flag = true;
                        }
                        else if (s[j][0] != Productname.Text[0])
                        {
                            break;
                        }
                        else if (Productname.Text.Length > 2 && s[j].Length > 2)
                        {
                            if (s[j][1] != Productname.Text[1])
                            {
                                break;
                            }
                        }
                    }
                    if (c[j] > n)
                    {
                        n = c[j];
                        index = j;
                    }
                }
            }
            if (flag)
            {
                listBox.Items.Clear();
                for (int i = 0; i < c.Count; i++)
                {
                    p += c[i];
                }
                p /= c.Count;
                for (int i = 0; i < s.Count; i++)
                {
                    if (c[i] > p)
                    {
                        listBox.Items.Add(s[i]);
                    }
                }
            }
            if (!flag)
            {
                listBox.Items.Clear();
                for (int i = 0; i < q.Count; i++)
                {
                    listBox.Items.Add(q[i]);
                }
            }
            flag = false;
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

        private void Productname_TextChanged(object sender, EventArgs e)
        {

        }

        private void Addbutton_Click(object sender, EventArgs e)
        {
            myConnection.Open();
            string query = "INSERT INTO goods(god_id, god_n, cathegory, price, quantity) VALUES (@god_id, @god_n, @cathegory, @price, @quantity)";
            SqlCommand commm = new SqlCommand(query, myConnection);
            commm.Parameters.AddWithValue("@god_id", God_id.Text);
            commm.Parameters.AddWithValue("@god_n", Productname.Text);
            commm.Parameters.AddWithValue("@cathegory", Category.Text);
            commm.Parameters.AddWithValue("@price", Price.Text);
            commm.Parameters.AddWithValue("@quantity", Quantity.Text);
            commm.ExecuteNonQuery();
            myConnection.Close();
            DateRefresh();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            myConnection.Open();
            string query = "DELETE FROM product WHERE god_id='" + God_id.Text + "'";
            SqlCommand commm = new SqlCommand(query, myConnection);
            commm.ExecuteNonQuery();
            myConnection.Close();
            DateRefresh();
            Productname.Text = "";
            Category.Text = "";
            Price.Text = "";
            Quantity.Text = "";
            God_id.Text = "";

        }
        private void Back_Click(object sender, EventArgs e)
        {
            MenuAdmin f = new MenuAdmin();
            f.Show();
            this.Hide();
        }

    }
}

