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

namespace TracNghiem
{
    public partial class DesktopExam : DevExpress.XtraEditors.XtraForm
    {
      
        public DesktopExam()
        {
            InitializeComponent();
        }
        private void DesktopExam_Load(object sender, EventArgs e)
        {           
            label6.Text = "30 Phút";
            label7.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.Equals("Toán"))
            {
                Quiz ft = new Quiz();
                ft.Show();
            }
            else
            {
                MessageBox.Show("Tiếng anh chưa Có Câu Hỏi ");
            }
        }

       
    }
}