using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chiecnonkidieu
{
    public partial class Formmain : Form
    {
        //Image img;
        Bitmap btm;
        public Formmain()
        {
            InitializeComponent();
        }

        private void playgane_Click(object sender, EventArgs e)
        {
            FormPlaygame frm = new FormPlaygame();
            frm.Show();
            this.Hide();
        }

        private void xemdiem_Click(object sender, EventArgs e)
        {
            FormXemdiem frm = new FormXemdiem();
            frm.ShowDialog();
            
        }

        private void thoat_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }


        private void tuychinh_Click(object sender, EventArgs e)
        {
            Formdangnhap frm = new Formdangnhap();
            frm.ShowDialog();
            this.Hide();
        }

        private void huongdan_Click(object sender, EventArgs e)
        {
            Formhuongdan frm = new Formhuongdan();
            frm.ShowDialog();
        }


    }
}
