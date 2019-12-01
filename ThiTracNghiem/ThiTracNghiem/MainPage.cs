using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace THITRACNGHIEM
{
    public partial class MainPage : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public MainPage()
        {
            InitializeComponent();
        }
        public void skins()
        {
           DevExpress.LookAndFeel.DefaultLookAndFeel sk = new DevExpress.LookAndFeel.DefaultLookAndFeel();
           sk.LookAndFeel.SkinName = "Springtime";
        }
        private void MainPage_Load(object sender, EventArgs e)
        {
            skins();
        }

        private void MainPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult rs;
            rs = XtraMessageBox.Show("Bạn có muốn thoát?","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (rs == DialogResult.No)
            {
                e.Cancel=true;
            }
        }
    }
}
