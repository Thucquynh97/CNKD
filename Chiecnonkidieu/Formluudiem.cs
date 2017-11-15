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
    public partial class Formluudiem : Form
    {
        private int diem;
        private Chuanhoachuoi chuanhoa;
        private Connectsql cn = null;
        public Formluudiem()
        {
            InitializeComponent();
        }
        public Formluudiem(int diem)
        {
            this.diem = diem;
            InitializeComponent();
        }
        private void Formluudiem_Load(object sender, EventArgs e)
        {
            cn = new Connectsql();
            chuanhoa = new Chuanhoachuoi();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtname.Text.ToString();

            if (name != "")
            {
                cn.Connect();
                name = chuanhoa.btchuanhoa(name);
                cn.ImportPoint(name, diem);
                cn.Disconnect();
                this.Close();
            }
            else
                MessageBox.Show("Bạn Chưa Nhập Tên!");
        }

        private void Formluudiem_FormClosing(object sender, FormClosingEventArgs e)
        {
            Formmain frm = new Formmain();
            frm.Show();
        }
    }
}
