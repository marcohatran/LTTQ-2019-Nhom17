using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;

namespace QTSoftware
{
    public partial class Quanlyuser : DevExpress.XtraEditors.XtraForm
    {
        private string conStr = @"Data Source=MAYTINH\SQLEXPRESS;Initial Catalog=THITRACNGHIEM;User ID=sa;Password=loc";
        private SqlConnection mySqlConnection;
        private SqlCommand mySqlCommand;
        private bool IsNew = false;

        private SqlDataAdapter mySqlDataAdapter;
        private SqlCommandBuilder mySqlCommandBuilder;
        private DataTable dtQuestion;
        public Quanlyuser()
        {
            InitializeComponent();
        }
        //load luôn dữ liệu vào data girdview
        private void SetControls(bool edit)
        {
            textBox1.Enabled = edit;
            textBox2.Enabled = edit;
            textBox3.Enabled = edit;
            textBox4.Enabled = !edit;
            dateTimePicker1.Enabled = edit;
            dateTimePicker2.Enabled = edit;


            button1.Enabled = !edit;
            button2.Enabled = !edit;
            button3.Enabled = !edit;
            button4.Enabled = edit;
            button5.Enabled = !edit;
            button6.Enabled = !edit;
        }
        private void Display()
        {
            //Truy van thi du lieu
            string sSql = "SELECT * FROM THISINHX";
            mySqlCommand = new SqlCommand(sSql, mySqlConnection);
            SqlDataReader drSuppliers = mySqlCommand.ExecuteReader();

            //Hien thi len luoi
            DataTable dtSuppliers = new DataTable();
            dtSuppliers.Load(drSuppliers);
            dataGridView1.DataSource = dtSuppliers;
        }
        private void Quanlyuser_Load(object sender, EventArgs e)
        {
            //Ket noi toi CSDL
            mySqlConnection = new SqlConnection(conStr);
            mySqlConnection.Open();
            Display();
            SetControls(false);   
        }
        private void Find()
        {
            String sSql = "SELECT * FROM THISINHX WHERE MATHISINH LIKE '%" + textBox4.Text + "%'";
            mySqlDataAdapter = new SqlDataAdapter(sSql, mySqlConnection);
            mySqlCommandBuilder = new SqlCommandBuilder(mySqlDataAdapter);
            dtQuestion = new DataTable();
            mySqlDataAdapter.Fill(dtQuestion);
            //Hien thi len luoi
            dataGridView1.DataSource = dtQuestion;
        }
        //tìm kiếm
        private void button5_Click(object sender, EventArgs e)
        {
            /*//Truy van thi du lieu
            string sSql = "SELECT * FROM CAUHOI WHERE MaCH LIKE '%" + textBox1.Text + "%'";
            mySqlCommand = new SqlCommand(sSql, mySqlConnection);
            SqlDataReader drUsers = mySqlCommand.ExecuteReader();

            //Hien thi len luoi
            DataTable dtUsers = new DataTable();
            dtUsers.Load(drUsers);
            dataGridView1.DataSource = dtUsers;;*/
            Find();
        }
        //thêm
        private void button1_Click(object sender, EventArgs e)
        {

            SetControls(true);
            IsNew = true;
            textBox1.Focus();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            //dateTimePicker1.Clear();
            //dateTimePicker2.Clear();
        }
        //sửa
        private void button2_Click(object sender, EventArgs e)
        {
            SetControls(true);
            textBox1.Focus();
            IsNew = false;
        }
        //xóa
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dr = new System.Windows.Forms.DialogResult();
            dr = MessageBox.Show("Chắc chắn xóa thí sinh đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == System.Windows.Forms.DialogResult.No) return;

            //Sua du lieu
            int row = dataGridView1.CurrentRow.Index;
            string ID = dataGridView1.Rows[row].Cells[0].Value.ToString();

            string sSql = "DELETE FROM THISINHX  WHERE MATHISINH = N'" + ID + "'";//.mã để kiểu nvachar nên thêm chữ N''
            mySqlCommand = new SqlCommand(sSql, mySqlConnection);
            mySqlCommand.ExecuteNonQuery();
            Display();
        }
        //ghi lại
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Đề nghị nhập mã thí sinh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Focus();
                return;
            }
            if (IsNew == true)
            {
                //Them moi
                string sSql = "INSERT INTO THISINHX (MATHISINH,NGAYGIANHAP,NGAYSINH,DIACHI,GMAIL) VALUES (@MATHISINH,@NGAYGIANHAP,@NGAYSINH,@DIACHI,@GMAIL)";
                // string sSql = "INSERT INTO CAUHOI (MaCH,Cauhoi,A,B,C,D,Dapan) VALUES (N'" + textBox1.Text + "',N'" + textBox2.Text + "',,N'" + textBox4.Text + "',,N'" + textBox5.Text + "',,N'" + textBox6.Text + "',,N'" + textBox7.Text + "',,N'" + comboBox1.Text + "')";
                mySqlCommand = new SqlCommand(sSql, mySqlConnection);
                //Dinh nghia cac tham so
                mySqlCommand.Parameters.Add("@MATHISINH", SqlDbType.NVarChar, 10);
                mySqlCommand.Parameters.Add("@NGAYGIANHAP", SqlDbType.DateTime, 250);
                mySqlCommand.Parameters.Add("@NGAYSINH", SqlDbType.DateTime, 50);
                mySqlCommand.Parameters.Add("@DIACHI", SqlDbType.NVarChar, 50);
                mySqlCommand.Parameters.Add("@GMAIL", SqlDbType.NVarChar, 50);

                //Gan gia tri cho cac tham so
                mySqlCommand.Parameters["@MATHISINH"].Value = textBox1.Text;
                mySqlCommand.Parameters["@NGAYGIANHAP"].Value = dateTimePicker2.Text;
                mySqlCommand.Parameters["@NGAYSINH"].Value = dateTimePicker1.Text;
                mySqlCommand.Parameters["@DIACHI"].Value = textBox3.Text;
                mySqlCommand.Parameters["@GMAIL"].Value = textBox2.Text;

                mySqlCommand.ExecuteNonQuery();
            }
            else
            {
                //Sua du lieu

                int row = dataGridView1.CurrentRow.Index;
                string MATHISINH = dataGridView1.Rows[row].Cells[0].Value.ToString();

                string sSql = "UPDATE THISINHX SET NGAYGIANHAP=@NGAYGIANHAP,NGAYSINH=@NGAYSINH,DIACHI=@DIACHI,GMAIL=@GMAIL WHERE MATHISINH = @MATHISINH";
                // string sSql = "UPDATE CAUHOI SET Cauhoi = N'" + textBox2.Text + "',A = N'" + textBox4.Text + "',B = N'" + textBox5.Text + "',C= N'" + textBox6.Text + "',D = N'" + textBox7.Text + "',Dapan = N'" + comboBox1.Text + "' WHERE MaCH = " + Ma;
                mySqlCommand = new SqlCommand(sSql, mySqlConnection);
                //Dinh nghia cac tham so
                mySqlCommand.Parameters.Add("@MATHISINH", SqlDbType.NVarChar, 10);
                mySqlCommand.Parameters.Add("@NGAYGIANHAP", SqlDbType.DateTime, 250);
                mySqlCommand.Parameters.Add("@NGAYSINH", SqlDbType.DateTime, 50);
                mySqlCommand.Parameters.Add("@DIACHI", SqlDbType.NVarChar, 50);
                mySqlCommand.Parameters.Add("@GMAIL", SqlDbType.NVarChar, 50);

                //Gan gia tri cho cac tham so
                mySqlCommand.Parameters["@MATHISINH"].Value = textBox1.Text;
                mySqlCommand.Parameters["@NGAYGIANHAP"].Value = dateTimePicker2.Text;
                mySqlCommand.Parameters["@NGAYSINH"].Value = dateTimePicker1.Text;
                mySqlCommand.Parameters["@DIACHI"].Value = textBox3.Text;
                mySqlCommand.Parameters["@GMAIL"].Value = textBox2.Text;

                mySqlCommand.ExecuteNonQuery();
            }
            Display();
            SetControls(false);
        }
        //thoát
        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            dateTimePicker2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }
              
    }
}