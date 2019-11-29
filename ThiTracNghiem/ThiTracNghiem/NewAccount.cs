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
    public partial class NewAccount : Form
    {
        private string conStr = "Data Source=MAYTINH\\SQLEXPRESS;Initial Catalog=ThiTracNghiem;User ID=sa;Password=loc";
        private SqlConnection mySqlConnection;
        private SqlCommand mySqlCommand;

        public NewAccount()
        {
            InitializeComponent();
        }

        //Thêm mới
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox1.Focus();
        }

        private void NewAccount_Load(object sender, EventArgs e)
        {
            //Ket noi toi CSDL
            mySqlConnection = new SqlConnection(conStr);
            mySqlConnection.Open();
            Display();
        }

        private void Display()
        {
            //Truy van thi du lieu
            string sSql = "SELECT * FROM [NewAccount]";
            mySqlCommand = new SqlCommand(sSql, mySqlConnection);
            SqlDataReader drUsers = mySqlCommand.ExecuteReader();

            //Hien thi len luoi
            DataTable dtUsers = new DataTable();
            dtUsers.Load(drUsers);
            dataGridView1.DataSource = dtUsers;
        }

        //Lưu lại
        private void button2_Click(object sender, EventArgs e)
        {
            string sSql = "INSERT INTO NewAccount (MaSV, Username, Password) VALUES (N'" + textBox1.Text + "', N'" + textBox2.Text + "', N'" + textBox3.Text + "')";
            mySqlCommand = new SqlCommand(sSql, mySqlConnection);
            mySqlCommand.ExecuteNonQuery();
            Display();
        }

        //Tìm kiếm
        private void button3_Click(object sender, EventArgs e)
        {
            //Truy van thi du lieu
            string sSql = "SELECT * FROM  [NewAccount] WHERE MaSV LIKE '%" + textBox1.Text + "%'";
            mySqlCommand = new SqlCommand(sSql, mySqlConnection);
            SqlDataReader drUsers = mySqlCommand.ExecuteReader();

            //Hien thi len luoi
            DataTable dtUsers = new DataTable();
            dtUsers.Load(drUsers);
            dataGridView1.DataSource = dtUsers;
        }

        //Thoát
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }    
    }
}
