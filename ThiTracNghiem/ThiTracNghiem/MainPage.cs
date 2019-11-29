using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThiTracNghiem
{
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void tạoMớiToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            NewAccount newacc = new NewAccount();
            newacc.MdiParent = this;
            newacc.Show();
        }

        private void chỉnhSửaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditAccount editacc = new EditAccount();
            editacc.MdiParent = this;
            editacc.Show();
        }

        private void tạoMãMônThiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddMaMH addmamh = new AddMaMH();
            addmamh.MdiParent = this;
            addmamh.Show();
        }

        private void tạoCâuHỏiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cauhoi ch = new Cauhoi();
            ch.MdiParent = this;
            ch.Show();
        }
    }
}
