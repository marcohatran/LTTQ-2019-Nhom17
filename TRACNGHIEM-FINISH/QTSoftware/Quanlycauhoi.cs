
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
    public partial class Quanlycauhoi : DevExpress.XtraEditors.XtraForm
    {
        public Quanlycauhoi()
        {
            InitializeComponent();
        }
        
        private void Quanlycauhoi_Load(object sender, EventArgs e)
        {
           

        }

        //Nút thêm Dữ liệu
        private void button1_Click(object sender, EventArgs e)
        {

            //mở Cổng kết Nối Với Database
                SqlConnection con = new SqlConnection(StringConnectionSql.StrConnect);
                con.Open();
                SqlConnection con2 = new SqlConnection(StringConnectionSql.StrConnect);
                con2.Open();
                SqlCommand cmd = new SqlCommand();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = con2;
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.CommandText = "INSERT_DAPAN";
                cmd2.Parameters.Add(new SqlParameter("@sMACH", textEdit1.Text.Trim()));
                cmd2.Parameters.Add(new SqlParameter("@sA", textEdit2.Text.Trim()));
                cmd2.Parameters.Add(new SqlParameter("@sB", textEdit4.Text.Trim()));
                cmd2.Parameters.Add(new SqlParameter("@sC", textEdit5.Text.Trim()));
                cmd2.Parameters.Add(new SqlParameter("@sD", textEdit6.Text.Trim()));
                cmd2.Parameters.Add(new SqlParameter("@sDAPAN", comboBox1.Text.Trim()));
                cmd2.ExecuteNonQuery();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                //Tên PROC
                cmd.CommandText = "INSERT_CH";
                cmd.Parameters.Add(new SqlParameter("@sMACH", textEdit1.Text.Trim()));
                cmd.Parameters.Add(new SqlParameter("@sTENCH", textEdit3.Text.Trim()));
                cmd.Parameters.Add(new SqlParameter("@sLOAICH", "null"));
              //  SqlDataReader r = cmd.ExecuteReader();
              //  r.Read();
                //Thực thi Stored Procedure
                
                cmd.ExecuteNonQuery();
                con2.Close();
                con.Close();
        
                MessageBox.Show("Thêm Thành Công");
            
        }


        //Load Dữ Liệu datable lên girdview
        private void button2_Click(object sender, EventArgs e)
        {            
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DataSource = GetData().Tables[0];
        }
       
        
        private  DataSet GetData()
        {
                DataSet data = new DataSet();            
                SqlConnection con = new SqlConnection(StringConnectionSql.StrConnect);
                con.Open();
                String Query = "SELECT CAUHOI.MACH,TENCH,A,B,C,D,DAPAN FROM CAUHOI,DAPAN WHERE CAUHOI.MACH=DAPAN.MACH";
                SqlCommand cmd = new SqlCommand(Query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(data);
                con.Close();
                return data;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                int index = e.RowIndex;
                DataGridViewRow SelectRow = dataGridView1.Rows[index];
                textEdit1.Text = SelectRow.Cells[0].Value.ToString();
                textEdit3.Text = SelectRow.Cells[1].Value.ToString();
                textEdit2.Text = SelectRow.Cells[2].Value.ToString();
                textEdit4.Text = SelectRow.Cells[3].Value.ToString();
                textEdit5.Text = SelectRow.Cells[4].Value.ToString();
                textEdit6.Text = SelectRow.Cells[5].Value.ToString();
                comboBox1.Text = SelectRow.Cells[6].Value.ToString();
            
        }
        //Xóa Dữ Liệu 
        private void button3_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text.Equals(""))
            {
                MessageBox.Show("Ma Câu hỏi  Không Hợp lệ");
            }
            else
            {               
                    SqlConnection con = new SqlConnection(StringConnectionSql.StrConnect);
                    con.Open();
                    SqlCommand cmd = new SqlCommand();

                    cmd.Connection = con;

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "DELETE_CAUHOI";

                    cmd.Parameters.Add(new SqlParameter("@sMACH", textEdit1.Text.Trim()));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Xóa Thành công Câu Hỏi Mã CH =" + textEdit1.Text.Trim());
                    con.Close();                          
            }
        }
        //Update 
        private void button4_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text.Equals(""))
            {
                MessageBox.Show("Ma Câu hỏi  Không Hợp lệ");
            }
            else
            {
                
                    SqlConnection con = new SqlConnection(StringConnectionSql.StrConnect);
                    con.Open();
                    SqlCommand cmd = new SqlCommand();

                    cmd.Connection = con;

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "UPDATE_CAUHOI_AND_DAPAN";
              
                    cmd.Parameters.Add(new SqlParameter("@sMACH", textEdit1.Text.Trim()));
                    cmd.Parameters.Add(new SqlParameter("@sTENCH", textEdit3.Text.Trim()));
                    cmd.Parameters.Add(new SqlParameter("@sLOAICH", "null"));
                    cmd.Parameters.Add(new SqlParameter("@sA", textEdit2.Text.Trim()));
                    cmd.Parameters.Add(new SqlParameter("@sB", textEdit4.Text.Trim()));
                    cmd.Parameters.Add(new SqlParameter("@sC", textEdit5.Text.Trim()));
                    cmd.Parameters.Add(new SqlParameter("@sD", textEdit6.Text.Trim()));
                    cmd.Parameters.Add(new SqlParameter("@sDAPAN", comboBox1.Text.Trim()));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cập Nhật Thành Công" );
                    con.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // tìm kiếm theo mã câu hỏi
        private DataSet TimkiemTheoma()
        {
                DataSet data = new DataSet();
                SqlConnection con = new SqlConnection(StringConnectionSql.StrConnect);
                con.Open();
                String Query = "SELECT CAUHOI.MACH,TENCH,A,B,C,D,DAPAN FROM CAUHOI,DAPAN WHERE CAUHOI.MACH=DAPAN.MACH AND CAUHOI.MACH='" + textEdit7.Text + "'";
                SqlCommand cmd = new SqlCommand(Query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(data);
                con.Close(); return data;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DataSource = TimkiemTheoma().Tables[0]; 
        }
       
        
    }
}