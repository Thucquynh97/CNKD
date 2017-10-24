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
       public static string a { get; set; } //biến tĩnh
        public static int trakq = -1; //kiểm tra người dùng đã nhập user name chưa
        public Formluudiem()
        {
            InitializeComponent();
        }

        private void btchuanhoa(string name)
        {
            char[] arrchar = { ' ', '\n', '\t' };
            String[] arr = name.Split(arrchar, StringSplitOptions.RemoveEmptyEntries);
            name = "";
            for (int i = 0; i < arr.Length; i++)
            {
                String a = arr[i].ToLower();
                name += a.Substring(0, 1).ToUpper() + a.Substring(1) + " ";
                name.TrimEnd();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            a = txtname.Text.ToString();
            Formmain frm = new Formmain();
            btchuanhoa(a);
            frm.Show();
            trakq = 0;
            this.Hide();
        }

        private void FormDangnhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            Formmain frm = new Formmain();
            frm.Show();
            this.Hide();
        }
    }
}
