using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chiecnonkidieu;
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
            Functionplaygame Func = new Functionplaygame();
            string tk = txtusername.Text.Trim();
            string mk = txtpassword.Text.Trim();
            if(Func.DangNhap(tk,mk))
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

        private void button1_Click(object sender, EventArgs e)
        {
           
            this.Close();
        }

        private void Formdangnhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            Formmain frm = new Formmain();
            frm.Show();
        }

    }
}
