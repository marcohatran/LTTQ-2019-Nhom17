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
            SqlConnection con = new SqlConnection(StringConnectionSql.StrConnect);
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
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
