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
    public partial class AddMaMH : Form
    {
        private string conStr = "Data Source=MAYTINH\\SQLEXPRESS;Initial Catalog=ThiTracNghiem;User ID=sa;Password=loc";
        private SqlConnection mySqlConnection;
        private SqlCommand mySqlCommand;

        public AddMaMH()
        {
            InitializeComponent();
        }

        private void AddMaMH_Load(object sender, EventArgs e)
        {
            mySqlConnection = new SqlConnection(conStr);
            mySqlConnection.Open();
            Display();
        }

        private void Display()
        {
            //Truy van thi du lieu
            string sSql = "SELECT * FROM [Monhoc]";
            mySqlCommand = new SqlCommand(sSql, mySqlConnection);
            SqlDataReader drUsers = mySqlCommand.ExecuteReader();

            //Hien thi len luoi
            DataTable dtUsers = new DataTable();
            dtUsers.Load(drUsers);
            dataGridView1.DataSource = dtUsers;
        }

        //thêm
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox1.Focus();
        }

        //lưu
        private void button2_Click(object sender, EventArgs e)
        {
            string sSql = "INSERT INTO Monhoc (MaMH, TenMH) VALUES (N'" + textBox1.Text + "', N'" + textBox2.Text + "')";
            mySqlCommand = new SqlCommand(sSql, mySqlConnection);
            mySqlCommand.ExecuteNonQuery();
            Display();
        }

        //tìm kiếm
        private void button3_Click(object sender, EventArgs e)
        {
            //Truy van thi du lieu
            string sSql = "SELECT * FROM  [Monhoc] WHERE MaMH LIKE '%" + textBox1.Text + "%'";
            mySqlCommand = new SqlCommand(sSql, mySqlConnection);
            SqlDataReader drUsers = mySqlCommand.ExecuteReader();

            //Hien thi len luoi
            DataTable dtUsers = new DataTable();
            dtUsers.Load(drUsers);
            dataGridView1.DataSource = dtUsers;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
