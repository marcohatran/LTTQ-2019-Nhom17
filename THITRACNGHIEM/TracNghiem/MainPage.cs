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
    public partial class MainPage : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Form1 frmLogin;
        String IdName;

        public MainPage(Form1 Form, String Usename)
        {
            InitializeComponent();
            frmLogin = Form;
            IdName = Usename;
            Role_USer();
            //Kiểm tra quyền thôi :V
            //  MessageBox.Show(x);
        }

        String x;
        public bool Check_Role()
        {
            // Mở kết Nối 
            SqlConnection con = new SqlConnection(StringConnectionSql.StrConnect);
            con.Open();

            SqlCommand comand = new SqlCommand("Select Role from TAIKHOAN WHERE Username ='" + IdName + "'", con);
            SqlDataReader r = comand.ExecuteReader();
            r.Read();
            x = r.GetValue(0).ToString();
            if (r.GetValue(0).ToString()=="True")
                return true;
            else
                return false;
     
            r.Close();
            con.Close();
        }
        //Phân Quyền :v
        public void Role_USer()
        {
            if (Check_Role() == true)
            {
                barButtonItem1.Enabled = true;
                barButtonItem2.Enabled = true;
                barButtonItem3.Enabled = true;
                barButtonItem7.Enabled = true;
                barButtonItem8.Enabled = true;
            }
            else
            {
                barButtonItem1.Enabled = true;
                barButtonItem2.Enabled = true;
                barButtonItem3.Enabled = true;
                barButtonItem7.Enabled = false;
                barButtonItem8.Enabled = false;
                
            }
	  
        }
       /* public MainPage()
        {
            InitializeComponent();
        }*/
        public void skins()
        {
            DevExpress.LookAndFeel.DefaultLookAndFeel sk = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            sk.LookAndFeel.SkinName = "Springtime";
        }
        private void MainPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult rs;
            rs = XtraMessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            skins();
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Questions qs = new Questions();
            qs.Show();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DesktopExam dke = new DesktopExam(IdName);
            dke.Show();
        }

        private void ribbon_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            User us = new User();
            us.Show();

        }
    }
}