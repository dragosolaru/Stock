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
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }

        private void Products_Load(object sender, EventArgs e)
        {
            comboBoxProductStatus.SelectedIndex = 0;
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-MJRNSH14\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
            //insert logic
            con.Open();
            Boolean status = false;
            if (comboBoxProductStatus.SelectedIndex == 0)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            var sqlQuery = "";
            if (IfProductExist(con, textBoxProductCode.Text))
            {
                sqlQuery = @"UPDATE [Products] SET [ProductName] = '" + textBoxProductName.Text + "',[ProductStatus] ='" + status + "' WHERE [ProductCode] = '" + textBoxProductCode.Text + "'";
            }
            else
            {
                sqlQuery = @"INSERT INTO [Stock].[dbo].[Products] ([ProductCode],[ProductName],[ProductStatus]) VALUES
                             ('" + textBoxProductCode.Text + "','" + textBoxProductName.Text + "','" + status + "')";
            }
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            cmd.ExecuteNonQuery();

            con.Close();
            //Reading data
            LoadData();
        }
        private bool IfProductExist(SqlConnection con , string productCode)
        {
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * FROM [Stock].[dbo].[Products] WHERE [ProductCode] = '" + productCode+ "'" , con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false; 
        }
        public void LoadData()
        {
            SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-MJRNSH14\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * FROM [Stock].[dbo].[Products]", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["ProductName"].ToString();
                if ((bool)item["ProductStatus"])
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Active";
                }
                else
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Deactive";
                }

            }
        } 

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-MJRNSH14\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
            var sqlQuery = "";
            if (IfProductExist(con, textBoxProductCode.Text))
            {
                con.Open();
                sqlQuery = @"DELETE FROM [Products] WHERE [ProductCode] = '" + textBoxProductCode.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
                textBoxProductCode.Clear();
                textBoxProductName.Clear();
                
                con.Close();
            }
            else
            {
                MessageBox.Show("Inregistarea nu exista....");
            }
          
            //Reading data
            LoadData();

        }


        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBoxProductCode.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBoxProductName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString() == "Active")
            {

            }
            else
            {
                comboBoxProductStatus.SelectedIndex = 1;
            }
        }
    }
}
