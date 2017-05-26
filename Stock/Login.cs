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

namespace Stock
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            textUserName.Text = ""; //sau la fel cu Clear();
            textPassword.Clear();
            textUserName.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //TO-DO Check Login username and password
            SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-MJRNSH14\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT *
                        FROM [Stock].[dbo].[LogIn] where UserName = '" + textUserName.Text + "' and Password = '" + textPassword.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                this.Hide();
                StockMain main = new StockMain();
                main.Show();
            }
            else
            {
                MessageBox.Show("Invalid UserName and Password...!","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                button1_Click(sender, e);
            }
        }
    }
}
