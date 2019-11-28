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

namespace ThiTracNghiem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=MAYTINH\SQLEXPRESS;Initial Catalog=ThiTracNghiem;User ID=sa;Password=loc");
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from Login where Username='" + textBox1.Text + " ' and Password = '" + textBox2.Text +" ' ",con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
            
            this.Hide();
            MainPage ss = new MainPage();
            ss.Show();
            }
            else
            {
                MessageBox.Show("Tài khoản không tồn tại");
            }
        }
    }
}
