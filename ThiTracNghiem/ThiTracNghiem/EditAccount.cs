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
    public partial class EditAccount : Form
    {
        private string conStr = "Data Source=MAYTINH\\SQLEXPRESS;Initial Catalog=ThiTracNghiem;User ID=sa;Password=loc";
        private SqlConnection mySqlConnection;
        private SqlCommand mySqlCommand; 

        public EditAccount()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void EditAccount_Load(object sender, EventArgs e)
        {          
            //Ket noi toi CSDL
            mySqlConnection = new SqlConnection(conStr);
            mySqlConnection.Open();
            Display();
            //button1.PerformClick();
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

        //Tìm kiếm
        private void button1_Click(object sender, EventArgs e)
        {
            string sSql = "SELECT * FROM  [NewAccount] WHERE MaSV LIKE '%" + textBox1.Text + "%'";
            mySqlCommand = new SqlCommand(sSql, mySqlConnection);
            SqlDataReader drUsers = mySqlCommand.ExecuteReader();

            //Hien thi len luoi
            DataTable dtUsers = new DataTable();
            dtUsers.Load(drUsers);
            dataGridView1.DataSource = dtUsers;
        }

        //sửa
        private void button2_Click(object sender, EventArgs e)
        {
            int row = dataGridView1.CurrentRow.Index;
            string m_MaSV = dataGridView1.Rows[row].Cells[0].Value.ToString();
            //2. Xac nhan co sửa khong
            DialogResult drResult = new System.Windows.Forms.DialogResult();
            drResult = MessageBox.Show("Chắc chắn sửa thông tin người đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //
            if (drResult == System.Windows.Forms.DialogResult.No) return;

            //Dinh nghia cau lenh SQL
            string sSql = "UPDATE NewAccount SET MaSV = N'" + textBox1.Text + "', Username = N'" + textBox2.Text + "', Password = N'" + textBox3.Text + "' WHERE MaSV =" + m_MaSV;
            mySqlCommand = new SqlCommand(sSql, mySqlConnection);
            mySqlCommand.ExecuteNonQuery();
            //Hien thi lai du lieu
            //button1.PerformClick();
           Display();
        }

        //Xóa
        private void button3_Click(object sender, EventArgs e)
        {
            //1. Xoa du lieu
            //Lay USerID
            int row = dataGridView1.CurrentRow.Index;
            string m_MaSV = dataGridView1.Rows[row].Cells[0].Value.ToString();
            //2. Xac nhan co xoa khong
            DialogResult drResult = new System.Windows.Forms.DialogResult();
            drResult = MessageBox.Show("Chắc chắn xóa người đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //
            if (drResult == System.Windows.Forms.DialogResult.No) return;

            //Dinh nghia cau lenh SQL
            string sSql = "DELETE FROM NewAccount WHERE MaSV =" + m_MaSV;
            mySqlCommand = new SqlCommand(sSql, mySqlConnection);
            mySqlCommand.ExecuteNonQuery();
            //Hien thi lai du lieu
            //button1.PerformClick();
            Display();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            textBox1.Text = dataGridView1.Rows[row].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[row].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[row].Cells[2].Value.ToString();
        }
    }
}
