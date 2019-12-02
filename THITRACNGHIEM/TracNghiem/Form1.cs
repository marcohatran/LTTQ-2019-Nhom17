using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;

namespace TracNghiem
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public static bool ResultOk = false;
        public Form1()
        {
            InitializeComponent();
        }

        
        public void skins()
        {
            DevExpress.LookAndFeel.DefaultLookAndFeel sk = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            sk.LookAndFeel.SkinName = "Springtime";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            skins();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            /*SqlConnection con = new SqlConnection(StringConnectionSql.StrConnect);
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From TAIKHOAN where Username='"+textEdit1.Text+"' and Password='"+textEdit2.Text+"'",con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if(dt.Rows[0][0].ToString()=="1")
            {
                this.Hide();
                MainPage mp = new MainPage();
                mp.Show();
            }
            else
            {
                MessageBox.Show("Đăng nhập không thành công");
            }*/
            CheckLogin();
        }
        public void CheckLogin()
        {
   
            {
                
                SqlConnection con = new SqlConnection(StringConnectionSql.StrConnect);
                con.Open();
                // Lấy mật Khẩu Nếu Tên Tài Khoản đang Trùng với EditText tài khoản
                SqlCommand comand = new SqlCommand("Select Password from TAIKHOAN WHERE Username ='" + textEdit1.Text.Trim() + "'", con);

                SqlDataReader r = comand.ExecuteReader();
                r.Read();

                if (textEdit1.Text == "" || textEdit2.Text == "")
                {
                    MessageBox.Show("Vui Lòng Xem lại Tài Khoản Mật Khẩu !");

                }
                else if (textEdit2.Text.Trim() == r.GetValue(0).ToString())
                {
                    //  Load f = new Load();
                    //  f.Show();

                    MessageBox.Show("Đăng Nhập Thành Công !");
                    this.Hide();
                    r.Close();
                    con.Close();
                    ResultOk = true;
                    //   f.Close();
                    MainPage m = new MainPage(this, textEdit1.Text.Trim());
                    m.Show();

                }
                else
                {
                    MessageBox.Show("Đăng Nhập Thất Bại ");
                }
                r.Close();
                con.Close();



            }
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
