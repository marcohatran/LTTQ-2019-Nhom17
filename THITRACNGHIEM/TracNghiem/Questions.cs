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


namespace TracNghiem
{
    public partial class Questions : DevExpress.XtraEditors.XtraForm
    {
        private string conStr = @"Data Source=MAYTINH\SQLEXPRESS;Initial Catalog=TRACNGHIEM;User ID=sa;Password=loc";
        private SqlConnection mySqlConnection;
        private SqlCommand mySqlCommand;
        private bool IsNew = false;

        private SqlDataAdapter mySqlDataAdapter;
        private SqlCommandBuilder mySqlCommandBuilder;
        private DataTable dtQuestion;
      
        public Questions()
        {
            InitializeComponent();
        }

        private void labelControl7_Click(object sender, EventArgs e)
        {

        }

        private void Questions_Load(object sender, EventArgs e)
        {
            //Ket noi toi CSDL
            mySqlConnection = new SqlConnection(conStr);
            mySqlConnection.Open();
            Display();
            SetControls(false);
        }
        private void Display()
        {
            //Truy van thi du lieu
            string sSql = "SELECT * FROM CAUHOI";
            mySqlCommand = new SqlCommand(sSql, mySqlConnection);
            SqlDataReader drSuppliers = mySqlCommand.ExecuteReader();

            //Hien thi len luoi
            DataTable dtSuppliers = new DataTable();
            dtSuppliers.Load(drSuppliers);
            dataGridView1.DataSource = dtSuppliers;
        }
        private void SetControls(bool edit)
        {
            textBox1.Enabled = edit;
            textBox2.Enabled = edit;
            textBox3.Enabled = !edit;// để mò thêm xíu nữa, tí hỏi nha .chua tim kiem dc á, ukm, mới chỉ nhập .đbưỏợc ô tim kiếm thôi
            textBox4.Enabled = edit;
            textBox5.Enabled = edit;
            textBox6.Enabled = edit;
            textBox7.Enabled = edit;
            comboBox1.Enabled = edit;

            simpleButton1.Enabled = !edit;
            simpleButton2.Enabled = !edit;
            simpleButton3.Enabled = !edit;
            simpleButton4.Enabled = edit;
            simpleButton5.Enabled = !edit; 
            simpleButton6.Enabled = edit;
        }
        private void Find()
        {
            String sSql = "SELECT * FROM CAUHOI WHERE MaCH LIKE '%" + textBox3.Text + "%'";
            mySqlDataAdapter = new SqlDataAdapter(sSql, mySqlConnection);
            mySqlCommandBuilder = new SqlCommandBuilder(mySqlDataAdapter);
            dtQuestion = new DataTable();
            mySqlDataAdapter.Fill(dtQuestion);
            //Hien thi len luoi
            dataGridView1.DataSource = dtQuestion;
        }
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            
            /*//Truy van thi du lieu
            string sSql = "SELECT * FROM CAUHOI WHERE MaCH LIKE '%" + textBox1.Text + "%'";
            mySqlCommand = new SqlCommand(sSql, mySqlConnection);
            SqlDataReader drUsers = mySqlCommand.ExecuteReader();

            //Hien thi len luoi
            DataTable dtUsers = new DataTable();
            dtUsers.Load(drUsers);
            dataGridView1.DataSource = dtUsers;
            SetControls(false);*/
            Find();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Ghi lại
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Trim() == "")
            {
                MessageBox.Show("Đề nghị nhập nội dung câu hỏi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox2.Focus();
                return;
            }
            if (IsNew == true)
            {
                //Them moi
                string sSql = "INSERT INTO CAUHOI (MaCH,Cauhoi,A,B,C,D,Dapan) VALUES (@MaCH,@Cauhoi,@A,@B,@C,@D,@Dapan)";
               // string sSql = "INSERT INTO CAUHOI (MaCH,Cauhoi,A,B,C,D,Dapan) VALUES (N'" + textBox1.Text + "',N'" + textBox2.Text + "',,N'" + textBox4.Text + "',,N'" + textBox5.Text + "',,N'" + textBox6.Text + "',,N'" + textBox7.Text + "',,N'" + comboBox1.Text + "')";
                mySqlCommand = new SqlCommand(sSql, mySqlConnection);
               //Dinh nghia cac tham so
                mySqlCommand.Parameters.Add("@MaCH", SqlDbType.NVarChar, 10);
                mySqlCommand.Parameters.Add("@Cauhoi", SqlDbType.NVarChar, 250);
                mySqlCommand.Parameters.Add("@A", SqlDbType.NVarChar, 50);
                mySqlCommand.Parameters.Add("@B", SqlDbType.NVarChar, 50);
                mySqlCommand.Parameters.Add("@C", SqlDbType.NVarChar, 50);
                mySqlCommand.Parameters.Add("@D", SqlDbType.NVarChar, 50);
                mySqlCommand.Parameters.Add("@Dapan", SqlDbType.NVarChar, 50);
                //Gan gia tri cho cac tham so
                mySqlCommand.Parameters["@MaCH"].Value = textBox1.Text;
                mySqlCommand.Parameters["@Cauhoi"].Value = textBox2.Text;
                mySqlCommand.Parameters["@A"].Value = textBox4.Text;
                mySqlCommand.Parameters["@B"].Value = textBox5.Text;
                mySqlCommand.Parameters["@C"].Value = textBox6.Text;
                mySqlCommand.Parameters["@D"].Value = textBox7.Text;
                mySqlCommand.Parameters["@Dapan"].Value = comboBox1.Text; 

                mySqlCommand.ExecuteNonQuery();
            }
            else
            {
                //Sua du lieu

                int row = dataGridView1.CurrentRow.Index;
                string MaCH = dataGridView1.Rows[row].Cells[0].Value.ToString();
                
                string sSql = "UPDATE CAUHOI SET Cauhoi = @Cauhoi,A=@A,B=@B,C=@C,D=@D,Dapan=@Dapan WHERE MaCH = @MaCH"; // Ngáo à,. Update tài khoản làm gì, cau phía dưới á.
               // string sSql = "UPDATE CAUHOI SET Cauhoi = N'" + textBox2.Text + "',A = N'" + textBox4.Text + "',B = N'" + textBox5.Text + "',C= N'" + textBox6.Text + "',D = N'" + textBox7.Text + "',Dapan = N'" + comboBox1.Text + "' WHERE MaCH = " + Ma;
                mySqlCommand = new SqlCommand(sSql, mySqlConnection);
                //Dinh nghia cac tham so
                mySqlCommand.Parameters.Add("@MaCH", SqlDbType.NVarChar, 10);
                mySqlCommand.Parameters.Add("@Cauhoi", SqlDbType.NVarChar, 250);
                mySqlCommand.Parameters.Add("@A", SqlDbType.NVarChar, 50);
                mySqlCommand.Parameters.Add("@B", SqlDbType.NVarChar, 50);
                mySqlCommand.Parameters.Add("@C", SqlDbType.NVarChar, 50);
                mySqlCommand.Parameters.Add("@D", SqlDbType.NVarChar, 50);
                mySqlCommand.Parameters.Add("@Dapan", SqlDbType.NVarChar, 50);
                //Gan gia tri cho cac tham so
                mySqlCommand.Parameters["@MaCH"].Value = textBox1.Text;
                mySqlCommand.Parameters["@Cauhoi"].Value = textBox2.Text;
                mySqlCommand.Parameters["@A"].Value = textBox4.Text;
                mySqlCommand.Parameters["@B"].Value = textBox5.Text;
                mySqlCommand.Parameters["@C"].Value = textBox6.Text;
                mySqlCommand.Parameters["@D"].Value = textBox7.Text;
                mySqlCommand.Parameters["@Dapan"].Value = comboBox1.Text;
                mySqlCommand.ExecuteNonQuery();
            }
            Display();
            SetControls(false);

          
        }

        //xóa
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            DialogResult dr = new System.Windows.Forms.DialogResult();
            dr = MessageBox.Show("Chắc chắn xóa câu hỏi đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == System.Windows.Forms.DialogResult.No) return;

            //Sua du lieu
            int row = dataGridView1.CurrentRow.Index;
            string ID = dataGridView1.Rows[row].Cells[0].Value.ToString();

            string sSql = "DELETE FROM CAUHOI  WHERE MaCH = " + ID;
            mySqlCommand = new SqlCommand(sSql, mySqlConnection);
            mySqlCommand.ExecuteNonQuery();
            Display();
        }

        //sửa
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            SetControls(true);
            textBox1.Focus();
            IsNew = false;
        }

        //thêm
  

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            SetControls(true);
            IsNew = true;
            textBox1.Focus();
            textBox1.Clear();
            textBox2.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
        }

        
    }
}