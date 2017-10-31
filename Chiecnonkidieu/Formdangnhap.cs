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
    public partial class Formdangnhap : Form
    {
        public Formdangnhap()
        {
            InitializeComponent();
        }

        private void Formdangnhap_Load(object sender, EventArgs e)
        {
            

        }

        private void btdangnhap_Click(object sender, EventArgs e)
        {
            string tk = txtusername.Text.Trim();
            string mk = txtpassword.Text.Trim();
            if (tk == "phu" && mk == "12345")
            {
                Formcauhoi frm = new Formcauhoi();
                frm.Show();
                this.Hide();
            }
            else
            {
                if(tk == "" || mk == "")
                    MessageBox.Show("Bạn chưa nhâp tên đăng nhập hoặc mật khẩu");
                else
                    MessageBox.Show("Bạn Nhập sai tên đăng nhập hoặc mật khẩu");
            }
        }
    }
}
