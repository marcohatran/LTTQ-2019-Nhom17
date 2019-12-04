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
    public partial class Quiz : DevExpress.XtraEditors.XtraForm
    {
        // Start Code ######
        private float Diem = 0;
        //Giờ Phút Giây để load lên form
        private int gio, phut, giay;

        private String TenThiSinh;

        private int SocauDung = 0, SoCauSai = 0;

        private int Cauhientai = 0;

        private int Socauhoi = 0;

        private int SocauNgauNhien = 20;

        String Mon;
        public Quiz(String Hoten)
        {
            InitializeComponent();
            TenThiSinh = Hoten;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        void Load_DeThiThat()
        {
            try
            {
                SqlConnection cnn = new SqlConnection(StringConnectionSql.StrConnect);
                SqlDataAdapter da = new SqlDataAdapter("select CAUHOI.MACH,TENCH,A,B,C,D ,Dapan from CAUHOI,DAPAN where CAUHOI.MACH=DAPAN.MACH", cnn);
                DataTable BangQuestion = new DataTable();
                da.Fill(BangQuestion);
                //TaobangRandomCauhoi(BangQuestion);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            giay = 1;//nhap thoi gian thi 
            phut = 30;
            gio = 0;
        }
        private void Load_CauHoi_VaoControl()
        {
            /*groupControl1.Text = "Câu Hỏi Số " + (Cauhientai + 1).ToString();
            label1.Text = BangQuestion.Rows[Cauhientai][1].ToString();
            label2.Text = BangQuestion.Rows[Cauhientai][2].ToString();
            label4.Text = BangQuestion.Rows[Cauhientai][3].ToString();
            label3.Text = BangQuestion.Rows[Cauhientai][4].ToString();
            label5.Text = BangQuestion.Rows[Cauhientai][5].ToString();

            if (BangQuestion.Rows[Cauhientai][7].ToString().Contains("A"))
                radioButton1.Checked = true;
            if (BangQuestion.Rows[Cauhientai][7].ToString().Contains("B"))
                radioButton3.Checked = true;
            if (BangQuestion.Rows[Cauhientai][7].ToString().Contains("C"))
                radioButton2.Checked = true;
            if (BangQuestion.Rows[Cauhientai][7].ToString().Contains("D"))
                radioButton4.Checked = true;*/
        }
        private void LoadcauhoiLenFrom()
        {
            Load_DeThiThat();
            //RandomCauTraloi();
            Load_CauHoi_VaoControl();
 
        }
        private void Quiz_Load(object sender, EventArgs e)
        {
            LoadcauhoiLenFrom();
            label10.Text = TenThiSinh;
        }
      
    }
}